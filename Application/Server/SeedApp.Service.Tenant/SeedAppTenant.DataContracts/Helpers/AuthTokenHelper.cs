using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SeedAppTenant.DataContracts.Interfaces;

namespace SeedAppTenant.DataContracts.Helpers
{
	public static class AuthTokenHelper
	{
		/*
		 * TO GENERATE KEYS:
		 *  var rsa = new RSACryptoServiceProvider(2048);
		 *  var publicPrivateKey = rsa.ToXmlString(true);
		 *  var publicKey = rsa.ToXmlString(false);
		 */
		private const char AuthorizationHeaderSeparator = ':';
		private const String PrivateKey = "<RSAKeyValue><Modulus>wrPVzpcJgF6KfuZ2MQ/2/EgJ2+E0Oq5ZKVCVWwsi80DlvdNF0l8FcI7eBmVWPdnB65IZ79jbWXnXpiuC8WEPjiKwDFQn7t0hZGRza5fbNEXSAenE/RcEsPOxMB8v1TRBu4IlL8N6VBtK4ZY1Dm7XUWOVdLSq77LuLqkEreSSXMzA9LTnVHrgIhePPcbI+WVcJrvdgFTTKZPDA92SVwAXt+OIG5vwyoWaAsI2/1MrxsPYCoggpswg592zSFO8G3AHNXEB91wBdRR1zRTOhQTUXNDN7e55ucREJTCGWhkK/EWLyehQ6L4Rjvq0f4pixpgEqRHyjvDSwAl6isjT+ZAySw==</Modulus><Exponent>AQAB</Exponent><P>1uOvnL/wPEd2ii+HK9bP0ztjePwloILKdoMIxRRgQhfzdIpbK3BKVDZ800WXoHVE4v0gWWBhu7Zr1kv/EP/LY83FhjkKqNoG/68o3BVRkqwHRyoua9ZbSWzmkskYJFEm3dXbTxjiceqwwXJOvPbbxjdnThSO0F93kW0+BV0doCc=</P><Q>5/N8KLRhx9Fb/ZSrzWb6Qe/kHHExQmqDyZcsBqj4R7w43Z25N4v9siTeX4H1g9sFhe4qFgcTxexAVPnQVK7S5Gv2ZUQTPK2CrbWwzNDD0RS3C//bi1zme3MRDFxAoBVSjx27dvzDI7UjU/r/la3t8cELhFSfyoPuSmElQlpjTz0=</Q><DP>WbKYrzl/MeoylOPZ7HayMV3s26eEnUd685Ump/0lYPFWciL+g1dP727/E8FM1XGd25IFWslcglpXERCP26yqXXKbLLfKt0iq+zdGOve7IRolv5Lf5auIV1HZvROrvB0TuC14ab/dZees+FKag04X1tlxiaoTIu92nYGYDe0cXKk=</DP><DQ>lcmlOlwScQmAUGe78q83mXEjbEKkvvEuHfbj1YE0pI6mYmdCft6GfI5WlHLGa9n63RyKTSNQ35XLjZSttqIXSq91tLubeKnMJAWbIQkIC3NHKhaWXo8lwD70mXjRuw9J+2YF87cTBXIqWEdU2gIyENbLay+C6TL9pUbK2uioDTk=</DQ><InverseQ>vxstbmhAIsviqHm3BoZcFGKymwLVrPMIe9ttvmQzVzeQXwDJMq07yYzkozQyEyRTOLGv8gVNAegCQKQ24SqsCsGaXyufxJghMqcCIUOMzQkZA1O8ZeiOUNys1SZBnNyJAAT44UoNGX/U29mEa/DmUBZKiCtE3DBeMS2+a9D2pyo=</InverseQ><D>L5BTKPbIwW1XFA0kznOB+Lt4fRiU88jnyyn+cpFT5mzyMt9L7Up9P7QHFTToTo/FydCVOXyWiVM0lUmH4YO+kxByRXDBmJ64q5fGgoBEco6j8Qe0scdPxBKkNIdJ3iG39SCkCPkOB0HrlxRkvbPiMeXL+wivwYLKoOd1Vy3Be520VJbNib8xkaolW1P6qXATdRZBDTKv32lzw7URXzQJe5mkOmvg/oMYcQq/gwiDhjPAK7ecd0an/fVNrp+5pTfQGhlw9kf59puyvZPFawA/WEFDcFuAb2+tu7Fc+gwGsk19MwoUieEvVYrvzTqTDk8hePxGlDLQBryJ6Kg+DV1BEQ==</D></RSAKeyValue>";
		private const String PublicKey = "<RSAKeyValue><Modulus>wrPVzpcJgF6KfuZ2MQ/2/EgJ2+E0Oq5ZKVCVWwsi80DlvdNF0l8FcI7eBmVWPdnB65IZ79jbWXnXpiuC8WEPjiKwDFQn7t0hZGRza5fbNEXSAenE/RcEsPOxMB8v1TRBu4IlL8N6VBtK4ZY1Dm7XUWOVdLSq77LuLqkEreSSXMzA9LTnVHrgIhePPcbI+WVcJrvdgFTTKZPDA92SVwAXt+OIG5vwyoWaAsI2/1MrxsPYCoggpswg592zSFO8G3AHNXEB91wBdRR1zRTOhQTUXNDN7e55ucREJTCGWhkK/EWLyehQ6L4Rjvq0f4pixpgEqRHyjvDSwAl6isjT+ZAySw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

		private static readonly UnicodeEncoding Encoder = new UnicodeEncoding();

		public static String GenerateAuthToken(IUserModel userModel, ITenantDataContract tenant)
		{
			return Encrypt(userModel.GlobalId.ToString(), tenant.Id);
		}

		public static String UnPackAuthToken(String token, out DateTime timeStampCreated, out int tenantId, out DateTime timeStampExpired)
		{
			var decryptedToken = Decrypt(token);
			var credentialBytes = Convert.FromBase64String(decryptedToken);
			var credentials = Encoding.ASCII.GetString(credentialBytes);
			var credentialParts = credentials.Split(AuthorizationHeaderSeparator);
						
			//TOKENS SHOULD HAVE 3 PARTS: USER KEY, TIMESTAMP, AND TENANT ID
			if (credentialParts.Length == 4)
			{
				//0. GLOBAL ID (UNIQUELY IDENTIFY USER)
				var globalId = credentialParts[0].Trim();

				//1. TIMESTAMP WHEN TOKEN WAS CREATED
				long ticks;
				long.TryParse(credentialParts[1].Trim(), out ticks);				
				//TODO: DETERMINE AMOUNT OF TIME SINCE TOKEN WAS CREATED TO SEE IF IT'S STILL VALID.
				timeStampCreated = new DateTime(ticks);

				//2. ID OF TENANT USED TO CREATE TOKEN
				Int32.TryParse(credentialParts[2].Trim(), out tenantId);

				//3. TIMESTAMP WHEN TOKEN EXPIRES
				long ticks2;
				long.TryParse(credentialParts[3].Trim(), out ticks2);
				//TODO: DETERMINE AMOUNT OF TIME SINCE TOKEN WAS CREATED TO SEE IF IT'S STILL VALID.
				timeStampExpired = new DateTime(ticks2);

				return globalId;
			}

			timeStampCreated = new DateTime();
			tenantId = 0;
			timeStampExpired = new DateTime();

			return null;
		}


		#region PRIVATE
		private static String Decrypt(String data)
		{
			var rsa = new RSACryptoServiceProvider();
			var dataArray = data.Split(new char[] { ',' });
			var dataByte = new byte[dataArray.Length];

			for (var i = 0; i < dataArray.Length; i++)
			{
				dataByte[i] = Convert.ToByte(dataArray[i]);
			}

			rsa.FromXmlString(PrivateKey);

			var decryptedByte = rsa.Decrypt(dataByte, false);

			return Encoder.GetString(decryptedByte);
		}

		private static String Encrypt(String globalId, Int32 tenantId)
		{
			var tokenExpiresHours = int.Parse(ConfigurationManager.AppSettings["Max_Token_Expires_Hours"]);
			var tokenCreated = DateTime.Now.Ticks;
			var tokenExpiration = DateTime.Now.AddHours(tokenExpiresHours).Ticks;

			//ENCODE GLOBAL ID, TOKEN CREATED, TENANTID, AND TOKEN EXPIRATION
			var encodedValue = EncodeTo64(String.Format("{0}:{1}:{2}:{3}", globalId, tokenCreated, tenantId, tokenExpiration));

			var rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(PublicKey);

			var dataToEncrypt = Encoder.GetBytes(encodedValue);
			var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
			var length = encryptedByteArray.Count();
			var item = 0;
			var sb = new StringBuilder();

			foreach (var x in encryptedByteArray)
			{
				item++;
				sb.Append(x);

				if (item < length)
					sb.Append(",");
			}

			return sb.ToString();
		}

		private static String EncodeTo64(String toEncode)
		{
			var toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
			return Convert.ToBase64String(toEncodeAsBytes);
		}
		#endregion PRIVATE
	}
}
