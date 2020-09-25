using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private Context Context { get; set; }

        public CountryRepository(Context context)
        {
            Context = context;
        }

        public async Task<List<Country>> GetAll()
        {
            return await Context.CountryDb.ToListAsync();
        }

        public async Task<Country> GetById(Guid Id)
        {
            return await Context.CountryDb.Include(entity => entity.States).FirstOrDefaultAsync(entity => entity.Id == Id);
        }

        public async Task<bool> Add(Country country)
        {
            try
            {
                await Context.CountryDb.AddAsync(country);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Country country)
        {
            try
            {
                Context.CountryDb.Update(country);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(Country country)
        {
            try
            {
                Context.CountryDb.Remove(country);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
