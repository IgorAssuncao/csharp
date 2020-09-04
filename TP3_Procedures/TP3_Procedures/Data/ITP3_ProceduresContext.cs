using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP3_Procedures.Models;

namespace TP3_Procedures.Data
{
    public interface ITP3_ProceduresContext
    {
        DbSet<Friend> Friend { get; set; }

        public Task<Friend> ExecuteSqlSelectOneFriendAsync(FormattableString sql);

        public Task<List<Friend>> ExecuteSqlSelectManyFriendsAsync(string sql);

        public Task<int> ExecuteSqlInsertFriendAsync(FormattableString sql);

        public Task<int> ExecuteSqlUpdateFriendAsync(FormattableString sql);

        public Task<int> ExecuteSqlDeleteFriendAsync(FormattableString sql);
    }
}