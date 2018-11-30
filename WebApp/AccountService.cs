using System.Threading.Tasks;

namespace WebApp
{
    class AccountService : IAccountService
    {
        // TODO 4 : keep the cache up to date in accordance with DB (data about accounts in db and cache should be fully synced) 
        private readonly IAccountCache _cache;
        private readonly IAccountDatabase _db;

        public AccountService(IAccountCache cache, IAccountDatabase db)
        {
            _cache = cache;
            _db = db;
        }
        
        public Account GetFromCache(long id)
        {
            if (_cache.TryGetValue(id, out var account))
            {
                return account;
            }

            return null;
        }
        
        public async ValueTask<Account> LoadOrCreateAsync(string id)
        {
            var account1 = await _db.GetOrCreateAccountAsync(id);
            if (!_cache.TryGetValue(id, out var account))
            {
                _cache.AddOrUpdate(account1);
            }

            if (!account1.Equals(account))
                account = await _db.Update(account1.ExternalId, account);

            return account;
        }

        public async ValueTask<Account> LoadOrCreateAsync(long id)
        {
            var account1 = await _db.GetOrCreateAccountAsync(id);
            if (!_cache.TryGetValue(id, out var account))
            {
                _cache.AddOrUpdate(account1);
            }

            if (!account1.Equals(account))
                account = await _db.Update(account1.ExternalId, account);

            return account;
        }
    }
}