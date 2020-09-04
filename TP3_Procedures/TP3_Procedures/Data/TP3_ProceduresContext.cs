using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP3_Procedures.Models;

namespace TP3_Procedures.Data
{
    public class TP3_ProceduresContext : DbContext, ITP3_ProceduresContext
    {
        public TP3_ProceduresContext(DbContextOptions<TP3_ProceduresContext> options)
            : base(options)
        {
        }

        public DbSet<Friend> Friend { get; set; }

        public async Task<Friend> ExecuteSqlSelectOneFriendAsync(FormattableString sql)
        {
            var friend = (await Friend.FromSqlInterpolated(sql).ToListAsync()).FirstOrDefault();
            return friend;
        }

        public async Task<List<Friend>> ExecuteSqlSelectManyFriendsAsync(string sql)
        {
            var friends = await Friend.FromSqlRaw(sql).ToListAsync();
            return friends;
        }

        public async Task<int> ExecuteSqlInsertFriendAsync(FormattableString sql)
        {
            var result = await Database.ExecuteSqlInterpolatedAsync(sql);
            return result;
        }

        public async Task<int> ExecuteSqlUpdateFriendAsync(FormattableString sql)
        {
            var result = await Database.ExecuteSqlInterpolatedAsync(sql);
            return result;
        }
        public async Task<int> ExecuteSqlDeleteFriendAsync(FormattableString sql)
        {
            var result = await Database.ExecuteSqlInterpolatedAsync(sql);
            return result;
        }
    }
}
