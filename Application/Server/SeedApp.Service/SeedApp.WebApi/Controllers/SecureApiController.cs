using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using SeedApp.DataContracts.Interfaces;
using SeedApp.WebApi.Attributes;
using StructureMap;

namespace SeedApp.WebApi.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Authorize]
	[ApiExceptionHandling]
	public class SecureApiController : BaseApiController
	{
		/// <summary>
		/// IUserModel established after registering or authentication
		/// </summary>
		public IUserModel CurrentUser;

		/// <summary>
		/// BASE CONTROLLER
		/// </summary>
		public SecureApiController()
		{
			CurrentUser = ObjectFactory.GetInstance<IUserModel>();
		}
	}
}