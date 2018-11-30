using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApp
{
    [Route("api")]
    public class LoginController : Controller
    {
        private readonly IAccountDatabase _db;

        public LoginController(IAccountDatabase db)
        {
            _db = db;
        }

        [HttpPost("sign-in")]
        public async Task Login(string userName)
        {
            var account = await _db.FindByUserNameAsync(userName);

            if (account != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, account.ExternalId));

                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, account.Role);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
                Response.Redirect("/api/account");
                //TODO 1 (completed): Generate auth cookie for user 'userName' with external id
            }
            else
                Response.StatusCode = 404;
            //TODO 2 (completed): return 404 not found if user not found
        }
    }
}