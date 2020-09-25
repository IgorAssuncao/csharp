using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class StateService : IStateService
    {
        private IStateRepository StateRepository { get; set; }
        private ICountryService CountryService { get; set; }
        private IImageService ImageService { get; set; }

        public StateService(IStateRepository stateRepository, ICountryService countryService, IImageService imageService)
        {
            StateRepository = stateRepository;
            CountryService = countryService;
            ImageService = imageService;
        }

        public async Task<List<State>> GetAll()
        {
            return await StateRepository.GetAll();
        }

        public async Task<State> GetById(Guid id)
        {
            return await StateRepository.GetById(id);
        }

        public async Task<bool> Add(StateRequest stateReq)
        {
            Guid Id = Guid.NewGuid();
            string url = await ImageService.UploadImage(Id, stateReq.PhotoUrl);

            State state = new State()
            {
                Id = Id,
                Name = stateReq.Name,
                CountryId = stateReq.CountryId,
                Country = stateReq.Country,
                Friends = stateReq.Friends,
                PhotoUrl = url
            };
            return await StateRepository.Add(state);
        }

        public async Task<bool> Update(Guid id, State state)
        {
            State stateFound = await StateRepository.GetById(id);

            if (stateFound == null)
                return false;

            state.Id = stateFound.Id;

            return await StateRepository.Update(state);
        }

        public async Task<bool> Delete(Guid id)
        {
            State state = await StateRepository.GetById(id);

            if (state == null)
                return false;

            return await StateRepository.Delete(state);
        }
    }
}
