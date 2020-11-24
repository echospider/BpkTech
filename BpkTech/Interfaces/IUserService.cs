using BpkTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpkTech.Interfaces
{
    public interface IUserService
    {
        #region Users
        Task<List<UserViewModel>> GetUsersAsync();
        Task<UserViewModel> GetUserDetailsAsync(UserModel model);
        Task<UserViewModel> GetUserById(string id);
        bool IsUserExistsAsync(UserModel model);
        Task<UserViewModel> AddUserAsync(UserModel model);
        Task<UserViewModel> UpdateUserAsync(UserModel model);
        Task<int> DeleteUserAsync(DeleteModel model);
        #endregion
    }
}
