using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApp
{
    // TODO 5(completed): unauthorized users should receive 401 status code and should be redirected to Login endpoint
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accManeger;
        
        public AccountController(IAccountService accManeger)
        {
            _accManeger = accManeger;
        }

        [Authorize]
        [HttpGet]
        public ValueTask<Account> Get()
        {
            var id = User.Identity.Name;
            return _accManeger.LoadOrCreateAsync(id/* TODO 3 (completed): Get user id from cookie */);
        }
        
        //TODO 6 (completed?): Get user id from cookie
        //TODO 7 (completed?): Endpoint should works only for users with "Admin" Role
        [Authorize (Roles = "Admin")]
        [HttpGet("{id}")]
        public Account GetByInternalId([FromRoute] int id)
        {
            return _accManeger.GetFromCache(id);
        }

        [Authorize]
        [HttpPost("counter")]
        public async Task UpdateAccount()
        {
            //Update account in cache, don't bother saving to DB, this is not an objective of this task.
            var account = await Get();

            account.Counter++;        
            
        }
    }
}