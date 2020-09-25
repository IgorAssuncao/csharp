using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ICountryRepository
    {
        Task<bool> Add(Country country);
        Task<bool> Delete(Country country);
        Task<List<Country>> GetAll();
        Task<Country> GetById(Guid Id);
        Task<bool> Update(Country country);
    }
}