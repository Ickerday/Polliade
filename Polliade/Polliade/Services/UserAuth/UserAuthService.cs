using Akavache;
using Polliade.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Polliade.Services.UserAuth
{
    public class UserAuthService : IUserAuthService
    {
        private static string UserCacheKey => $"{nameof(User)}_logged_in";
        private readonly DateTimeOffset DefaultExpiryDate = DateTime.Now + TimeSpan.FromDays(1d);

        private IBlobCache _cache = BlobCache.Secure;

        private static IUserAuthService _instance;
        public static IUserAuthService Instance => _instance ?? (_instance = new UserAuthService());

        public async Task<User> GetLoggedInUserAsync()
        {
            try { return await _cache.GetObject<User>(UserCacheKey); }
            catch (KeyNotFoundException) { return null; }
        }

        public async Task<bool> IsUserLoggedIn() =>
            await GetLoggedInUserAsync() != null;

        public async Task Logout() =>
            await _cache.Invalidate(UserCacheKey);

        private async Task SaveLoggedInUser(User user) =>
            await _cache.InsertObject(UserCacheKey, user, DefaultExpiryDate);
    }
}
