using BpkTech.Controllers;
using BpkTech.Data;
using BpkTech.Interfaces;
using BpkTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NUnitTestBpkTech
{

    public class DatabaseUnitTestController
    {
        private readonly IUserService _userServices;

        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }

        
        public DatabaseUnitTestController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [Fact]
        public void GetAllUsers()
        {
            var controller = new UserController(_userServices);
            var data = controller.GetUsers();
            Assert.IsNotNull(data);
        }

        [Fact]
        public void GetUserDetails()
        {
            var controller = new UserController(_userServices);
            var model = new UserModel()
            {
                Id = "905DE7D5-DB5E-4F6F-2945-08D890523FF3"
            };


            var data = controller.GetUserDetails(model);
            Assert.IsNotNull(data);
        }

        [Fact]
        public void AddNewUser()
        {
            var controller = new UserController(_userServices);
            var model = new UserModel()
            {
                FirstName = "Anup0000",
                LastName = "Pal",
                City = "Kolkata",
                PhoneNumber = "9748191610"
            };

            var data = controller.UpdateUser(model);
            Assert.IsNotNull(data);
        }

        [Fact]
        public void UpdateUser()
        {
            var controller = new UserController(_userServices);
            var model = new UserModel()
            {
                Id= "905DE7D5-DB5E-4F6F-2945-08D890523FF3",
                FirstName = "Anup0001",
                LastName = "Pal",
                City = "Kolkata",
                PhoneNumber = "9748191610"
            };

            var data = controller.UpdateUser(model);
            Assert.IsNotNull(data);
        }
    }
}
