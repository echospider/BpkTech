using BpkTech.Data;
using BpkTech.Entities;
using BpkTech.Interfaces;
using BpkTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpkTech.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<UserViewModel> AddUserAsync(UserModel model)
        {
            var user = new Users
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                PhoneNumber = model.PhoneNumber
            };
            await _applicationDbContext.Users.AddAsync(user);

            _applicationDbContext.SaveChanges();
            return await GetUserDetailsAsync(model);
        }

        public async Task<int> DeleteUserAsync(DeleteModel model)
        {
            var retval = 0;
            var result = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == model.Id);
            if (result != null)
            {
                _applicationDbContext.Users.Remove(result);
                retval = _applicationDbContext.SaveChanges();
            }
            return retval;
        }

        public async Task<UserViewModel> GetUserById(string id)
        {
            var users = from user in _applicationDbContext.Users.Where(u => u.Id.ToString() == id)
                        select new UserViewModel()
                        {
                            Id = user.Id.ToString(),
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            City = user.City,
                            PhoneNumber = user.PhoneNumber
                        };
            return await users.FirstOrDefaultAsync();
        }

        public async Task<UserViewModel> GetUserDetailsAsync(UserModel model)
        {
            var users = from user in _applicationDbContext.Users.Where(u => u.Id.ToString() == model.Id)
                            select new UserViewModel()
                            {
                                Id = user.Id.ToString(),
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                City = user.City,
                                PhoneNumber = user.PhoneNumber
                            };
            return await users.FirstOrDefaultAsync();
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var users = from user in _applicationDbContext.Users
                            select new UserViewModel()
                            {
                                Id = user.Id.ToString(),
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                City = user.City,
                                PhoneNumber = user.PhoneNumber
                            };
            return await users.ToListAsync();
        }

        public bool IsUserExistsAsync(UserModel model)
        {
            var retval = false;
            if (string.IsNullOrEmpty(model.Id))
            {
                //For adding
                var result = _applicationDbContext.Users.FirstOrDefault(u => u.FirstName == model.FirstName);
                if (result != null)
                {
                    retval = true;
                }
            }
            else
            {
                //For editing
                var result = _applicationDbContext.Users.FirstOrDefault(u => (u.FirstName == model.FirstName && u.Id.ToString() != model.Id) && u.Id.ToString() != model.Id);
                if (result != null)
                {
                    retval = true;
                }
            }

            return retval;
        }

        public async Task<UserViewModel> UpdateUserAsync(UserModel model)
        {
            var retval = 0;
            var result = await _applicationDbContext.Users.FirstOrDefaultAsync(s => s.Id.ToString() == model.Id);
            if (result != null)
            {
                result.FirstName = model.FirstName;
                result.LastName = model.LastName;
                result.City = model.City;
                result.PhoneNumber = model.PhoneNumber;
                _applicationDbContext.Users.Update(result);
                retval = _applicationDbContext.SaveChanges();

            }
            return await GetUserDetailsAsync(model);
        }
    }
}
