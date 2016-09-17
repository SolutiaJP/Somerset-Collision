using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SeedApp.Common.Enums;
using SeedApp.DataContracts.Helpers;
using SeedApp.DataContracts.Interfaces;
using SeedApp.WebApi.Helpers;
using StructureMap;


namespace SeedApp.WebApi.Handlers
{
	/// <summary>
	/// HANDLES X-POPS-AUTH TOKEN IF IT'S PRESENT IN HTTP REQUEST
	/// </summary>
	public class XAuthorizationMessageHandler : DelegatingHandler
	{
		protected override Task<HttpResponseMessage> SendAsync
		(
			HttpRequestMessage request,
			CancellationToken cancellationToken
		)
		{
			IEnumerable<string> authorizationValue;
			var tokenExpiresHours = int.Parse(ConfigurationManager.AppSettings["Max_Token_Expires_Hours"]);
			var tokenReissueHours = int.Parse(ConfigurationManager.AppSettings["Token_Reissue_Hours"]);			
			var hasAutorization = request.Headers.TryGetValues("X-SEEDAPP-AUTH-TOKEN", out authorizationValue);
			var reIssueToken = false;

			IUserModel currentUser = null;

			if (hasAutorization)
			{
				var token = authorizationValue.First();

				/*-----------------------------------------------------------------------------------------
				 INSPECT TOKEN TO SEE IF USER EXISTS AND TOKEN HAS NOT EXPIRED
				 -----------------------------------------------------------------------------------------*/
				DateTime timeStampTokenCreated;
				DateTime timeStampTokenExpires;

				var userGlobalId = AuthTokenHelper.UnPackAuthToken(token, out timeStampTokenCreated, out timeStampTokenExpires);
				var timeSpan = DateTime.Now - timeStampTokenCreated;
				var totalHours = timeSpan.TotalHours;

				//IF TOKEN EXPIRES TIMESTAMP IS LESS THAN NOW, ABORT
				if (timeStampTokenExpires < DateTime.Now)
				{
					return CreateUnauthorizedResponse("Authorization has expired.  Please login again.");
				}

				//GET USER
				currentUser = SecurityHelper.RegisteredUserByGlobalId(userGlobalId);

				//IF TIMESTAMP IN TOKEN  IS BETWEEN [TOKEN_REISSUE_HOURS] AND [MAX_TOKEN_EXPIRES_HOURS] HOURS OLD, RENEW IT
				reIssueToken = (totalHours < tokenExpiresHours && totalHours > tokenReissueHours);

				//IF USER IS UNKNOWN, THEN ABORT
				if (currentUser == null || currentUser.PersonId == 0)
				{
					return CreateUnauthorizedResponse("User is unknown.");
				}

				ObjectFactory.Configure(x => x.For<IUserModel>().Singleton().Use(currentUser));

				//CONVERT CURRENT USER TO AN AUTHENTICATED USER
				Thread.CurrentPrincipal = currentUser.ToClaimsPrincipal();

				//SET HTTP CONTEXT CURRENT USER TO AUTHENTICATED USER
				if (HttpContext.Current != null)
				{
					HttpContext.Current.User = Thread.CurrentPrincipal;
				}
			}

			return base.SendAsync(request, cancellationToken)
			   .ContinueWith(task =>
			   {
				   var response = task.Result;

				   //IF USER IS ANONYMOUS, OR WE NEED TO RE-ISSUE A TOKEN, THEN INCLUDE USER INFORMATION IN HEADER
				   if (reIssueToken && currentUser != null)
				   {
					   var authenticatedUser = currentUser.ToAuthenticatedUser();
					   var emailAddress = (authenticatedUser.CurrentUser.EmailAddress != null) ? authenticatedUser.CurrentUser.EmailAddress.ToString(CultureInfo.InvariantCulture) : String.Empty;

					   response.Headers.Add("AuthenticationStatus", "Authorized");

					   response.Headers.Add("UserId", authenticatedUser.CurrentUser.UserId.ToString(CultureInfo.InvariantCulture));
					   response.Headers.Add("PersonId", authenticatedUser.CurrentUser.PersonId.ToString(CultureInfo.InvariantCulture));
					   response.Headers.Add("FirstName", authenticatedUser.CurrentUser.FirstName.ToString(CultureInfo.InvariantCulture));
					   response.Headers.Add("LastName", authenticatedUser.CurrentUser.LastName.ToString(CultureInfo.InvariantCulture));
					   response.Headers.Add("Name", authenticatedUser.CurrentUser.Name.ToString(CultureInfo.InvariantCulture));
					   response.Headers.Add("EmailAddress", emailAddress);
					   response.Headers.Add("IsAnonymous", (authenticatedUser.CurrentUser.AuthenticationTypeId == (int)AuthenticationType.Unknown) ? "true" : "false");
					   response.Headers.Add("IsTemporaryPassword", "false");
					   response.Headers.Add("IsLockedOut", "false");
					   response.Headers.Add("LastLoginDate", DateTime.Today.ToString(CultureInfo.InvariantCulture));
					   response.Headers.Add("AccountCreateDate", authenticatedUser.CurrentUser.AccountCreateDate.ToString(CultureInfo.InvariantCulture));
					   response.Headers.Add("Token", authenticatedUser.Token);

					   response.Headers.Add("Access-Control-Expose-Headers", "UserId");
					   response.Headers.Add("Access-Control-Expose-Headers", "PersonId");
					   response.Headers.Add("Access-Control-Expose-Headers", "FirstName");
					   response.Headers.Add("Access-Control-Expose-Headers", "LastName");
					   response.Headers.Add("Access-Control-Expose-Headers", "Name");
					   response.Headers.Add("Access-Control-Expose-Headers", "EmailAddress");
					   response.Headers.Add("Access-Control-Expose-Headers", "IsAnonymous");
					   response.Headers.Add("Access-Control-Expose-Headers", "IsTemporaryPassword");
					   response.Headers.Add("Access-Control-Expose-Headers", "IsLockedOut");
					   response.Headers.Add("Access-Control-Expose-Headers", "LastLoginDate");
					   response.Headers.Add("Access-Control-Expose-Headers", "AccountCreateDate");
					   response.Headers.Add("Access-Control-Expose-Headers", "Token");
				   }

				   return response;
			   });
		}


		private static Task<HttpResponseMessage> CreateUnauthorizedResponse(string responseMessage)
		{
			var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
			{
				Content = new StringContent(responseMessage),
				ReasonPhrase = "Exception"
			};

			var taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
			taskCompletionSource.SetResult(response);
			return taskCompletionSource.Task;
		}
	}
}