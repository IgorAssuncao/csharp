using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryService : ICountryService
    {
        private ICountryRepository CountryRepository { get; set; }
        private IImageService ImageService { get; set; }

        public CountryService(ICountryRepository countryRepository, IImageService imageService)
        {
            CountryRepository = countryRepository;
            ImageService = imageService;
        }

        public async Task<List<Country>> GetAll()
        {
            return await CountryRepository.GetAll();
        }

        public async Task<Country> GetById(Guid id)
        {
            return await CountryRepository.GetById(id);
        }

        public async Task<bool> Add(CountryRequest countryReq)
        {
            Guid Id = Guid.NewGuid();
            string url = await ImageService.UploadImage(Id, countryReq.PhotoUrl);

            Country country = new Country()
            {
                Id = Id,
                Name = countryReq.Name,
                States = countryReq.States,
                Friends = countryReq.Friends,
                PhotoUrl = url
            };
            return await CountryRepository.Add(country);
        }

        public async Task<bool> Update(Guid id, Country country)
        {
            Country countryFound = await CountryRepository.GetById(id);

            if (countryFound == null)
                return false;

            country.Id = countryFound.Id;

            return await CountryRepository.Update(country);
        }

        public async Task<bool> Delete(Guid id)
        {
            Country country = await CountryRepository.GetById(id);

            if (country == null)
                return false;

            return await CountryRepository.Delete(country);
        }
    }
}
