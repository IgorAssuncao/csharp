using Microsoft.EntityFrameworkCore;
using TP3_Auth.Models;

namespace TP3_Auth.Context
{
    public interface IApplicationContext
    {
        DbSet<Friend> Friend { get; set; }
    }
}