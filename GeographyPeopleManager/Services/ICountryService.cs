using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface ICountryService
    {
        Task<bool> Add(CountryRequest countryReq);
        Task<bool> Delete(Guid id);
        Task<List<Country>> GetAll();
        Task<Country> GetById(Guid id);
        Task<bool> Update(Guid id, Country country);
    }
}