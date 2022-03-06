using JwtAPI.Helpers;
using JwtAPI.Models;
using JwtAPI.Service;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace JwtAPITests.Service
{
    public class TokenServiceTests
    {
        [Fact]
        public void GenerateToken_ReturnsTokenWhenCalled()
        {
            var secret = "Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA==";

            var foo = Options.Create(new AppSettings());

            foo.Value.Secret = secret;

            var tokenService = new TokenService(foo);

            var user = new User()
            {
                Id = new Guid("c12e0fdc-faef-4314-af77-dbeff471f38e"),
                FirstName = "Test",
                LastName = "Test",
                Password = "Test",
                Role = "Admin",
                UserName = "Test"

            };

            var bar = new DateTime(2021,01,01).ToUniversalTime();

            var dto = new DateTimeOffset(bar).ToUnixTimeMilliseconds();

            var coo = TimeSpan.FromMinutes(15);

            var result = tokenService.generateJWTToken(user);

            Assert.Equal("0", result);
        }
    }
}
