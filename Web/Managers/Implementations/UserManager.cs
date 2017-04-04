using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Managers.Base.Interfaces;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Models.DTO;
using Web.Repositories.Base.Interfaces;

namespace Web.Managers.Implementations
{
    public class UserManager: IUserManager
    {
        public IUnitOfWork UnitOfWork { get; }
        public IObjectMapper ObjectMapper { get; }

        public UserManager(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
        {
            UnitOfWork = unitOfWork;
            ObjectMapper = objectMapper;
        }
        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        public async Task<ApplicationUserModel> GetAsync(string id)
        {
            return ObjectMapper.Map<ApplicationUser, ApplicationUserModel>(await UnitOfWork.UserRepository.Get(id));
        }

        public async Task<List<ApplicationUserModel>> GetAsync()
        {
            return
                ObjectMapper.Map<IEnumerable<ApplicationUser>, List<ApplicationUserModel>>(
                    await UnitOfWork.UserRepository.GetAll());
        }

        public Task<int> InsertAsync(ApplicationUserModel item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUserModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await UnitOfWork.UserRepository.Get(id);
            await this.UnitOfWork.UserRepository.Delete(user);
            await UnitOfWork.SaveAsync();
        }
    }
}