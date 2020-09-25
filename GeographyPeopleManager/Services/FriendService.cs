using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class FriendService : IFriendService
    {
        private IFriendRepository FriendRepository { get; set; }
        private IImageService ImageService { get; set; }

        public FriendService(IFriendRepository friendRepository, IImageService imageService)
        {
            FriendRepository = friendRepository;
            ImageService = imageService;
        }

        public async Task<List<Friend>> GetAll()
        {
            return await FriendRepository.GetAll();
        }

        public async Task<List<Friend>> GetAllButMe(Guid id)
        {
            return await FriendRepository.GetAllButMe(id);
        }

        public async Task<Friend> GetById(Guid id)
        {
            return await FriendRepository.GetById(id);
        }

        public async Task<bool> Add(FriendRequest friendRequest)
        {
            Guid id = Guid.NewGuid();
            string url = await ImageService.UploadImage(id, friendRequest.PhotoURL);

            Friend friend = new Friend()
            {
                Id = id,
                Name = friendRequest.Name,
                Lastname = friendRequest.Lastname,
                Email = friendRequest.Email,
                Birthday = friendRequest.Birthday,
                Phone = friendRequest.Phone,
                PhotoURL = url,
                Country = friendRequest.Country,
                CountryId = friendRequest.CountryId,
                State = friendRequest.State,
                StateId = friendRequest.StateId,
                Friends = friendRequest.Friends
            };

            return await FriendRepository.Add(friend);
        }

        public async Task<bool> Update(Guid id, Friend friend)
        {
            Friend friendFound = await FriendRepository.GetById(id);

            if (friendFound == null)
                return false;

            friend.Id = friendFound.Id;

            return await FriendRepository.Update(friend);
        }

        public async Task<bool> Delete(Guid id)
        {
            Friend friend = await FriendRepository.GetById(id);

            if (friend == null)
                return false;

            return await FriendRepository.Delete(friend);
        }
    }
}
