using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PopsApp.WebApi.Tests
{
	[TestClass]
	public class RSAKeyGeneratorTest
	{
		[TestMethod]
		public void TestRSAKeyLarge()
		{
			var rsa = new RSACryptoServiceProvider(2048);
			var privateKey = rsa.ToXmlString(true);
			var publicKey = rsa.ToXmlString(false);

			Assert.IsNotNull(privateKey);
			Assert.IsNotNull(publicKey);
		}

		[TestMethod]
		public void TestRSAKeySmall()
		{
			var rsa = new RSACryptoServiceProvider();
			var privateKey = rsa.ToXmlString(true);
			var publicKey = rsa.ToXmlString(false);

			Assert.IsNotNull(privateKey);
			Assert.IsNotNull(publicKey);
		}
	}
}
