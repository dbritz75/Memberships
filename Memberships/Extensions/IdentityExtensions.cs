using Memberships.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Memberships.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFullName(this IIdentity identity)
        {
            var db = ApplicationDbContext.Create();
            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(identity.Name));

            return user != null ? user.FirstName + ' ' + user.LastName : String.Empty;
        }

        public static async Task GetUsers(this List<UserViewModel> users)
        {
            //Convert users to user view model objects

            var db = ApplicationDbContext.Create();
            users.AddRange(await
                            (from u in db.Users
                             select new UserViewModel
                             {
                                 userID = u.Id,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                             }
                             ).OrderBy(o => o.LastName).ThenBy(o => o.FirstName).ToListAsync()
                           );

        }
    }
}