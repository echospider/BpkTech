using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BpkTech.Core;
using BpkTech.Interfaces;
using BpkTech.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BpkTech.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [Route("Users")]
        public IActionResult GetUsers()
        {
            var users = _userServices.GetUsersAsync().Result;
            var response = new ApiResult<List<UserViewModel>>(users, null, true, Ok().StatusCode, users.Count);
            return Ok(response);
        }

        [HttpPost]
        [Route("UserDetails")]
        public IActionResult GetUserDetails([FromBody] UserModel model)
        {
            var result = _userServices.GetUserDetailsAsync(model).Result;
            var response = new ApiResult<UserViewModel>(result, null, true, Ok().StatusCode, 1);
            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
            {
                IList<ModelError> modelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                var collection = new List<ErrorList>();
                modelErrors.ToList().ForEach(m => { collection.Add(new ErrorList() { ErrorMessage = m.ErrorMessage }); });
                var modelError = new ApiResult<BadRequestResult>(null, collection, false, BadRequest().StatusCode);
                return Ok(modelError);
            }
            var exists = _userServices.IsUserExistsAsync(model);
            if (exists == true)
            {
                var result = new ApiResult<BadRequestResult>(null, new List<ErrorList> { new ErrorList() { ErrorMessage = "Already exists!" } }, false, BadRequest().StatusCode);
                return Ok(result);
            }
            else
            {
                if (string.IsNullOrEmpty(model.Id))
                {
                    var result = _userServices.AddUserAsync(model).Result;
                    var response = new ApiResult<UserViewModel>(result, null, true, Ok().StatusCode, 1);
                    return Ok(response);
                }
                else
                {
                    //Update records to database
                    var result = _userServices.UpdateUserAsync(model).Result;
                    var response = new ApiResult<UserViewModel>(result, null, true, Ok().StatusCode, 1);
                    return Ok(response);
                }
            }
        }
    }
}
