using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TokenJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IHttpContextAccessor _httpContext;
        public LoginController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        // GET: api/<LoginController>
        [HttpGet]
        public string Get()
        {
            Microsoft.Extensions.Primitives.StringValues SecureToken = string.Empty;
            //HttpContext httpContext = null;
            _httpContext.HttpContext.Request.Headers.TryGetValue("UserName", out SecureToken);
            return SecureToken;
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        public string LoginUser(string UserName)
        {
            string Role = "Admin";
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecureKeyRequiredforvalidationAdmin"));

            var authClaims = new[]

                         {

                         new Claim("UserName", UserName),
                         new Claim("Role", Role)

                         };
            var token = new JwtSecurityToken(

                issuer: "https://localhost:44316",
                audience: "https://localhost:44393/GatewayAPI",
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

                );

                return new JwtSecurityTokenHandler().WriteToken(token);

        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
