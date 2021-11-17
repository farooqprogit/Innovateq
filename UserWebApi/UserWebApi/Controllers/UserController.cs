using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebApi.Models;
using UserWebApi.Context;
using Microsoft.AspNetCore.Hosting;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserContext _userContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserController(UserContext userContext, IWebHostEnvironment hostingEnvironment)
        {
            _userContext = userContext;
            _hostingEnvironment = hostingEnvironment;
        }
   
        [Route("GetUsers")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userContext.Users);
        }

 
        [Route("GetUserById/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(_userContext.Users.FirstOrDefault(u => u.UserId == id));
        }
      
        [Route("SearchUser/{name}")]
        [HttpGet]
        public IActionResult SearchUser(string  name)
        {
            return Ok(_userContext.Users.Where(s => s.Name == name));
        }

        [Route("SortUser/{sortorder}")]
        [HttpGet]
        public IActionResult SortUser(string sortorder)
        {
            var users=(dynamic)null;
            if (sortorder == "asc" || sortorder == "ascending")
                users=_userContext.Users.OrderBy(s => s.Name);
            else if(sortorder == "desc" || sortorder == "descending")
                users = _userContext.Users.OrderByDescending(s => s.Name);
            return Ok(users);
        }

        [Route("UserPaging/{pageno}")]
        [HttpGet]
        public IActionResult UserPaging(int pageno)
        {
            int pageSize = 3;
            return Ok(_userContext.Users.Skip((pageno - 1) * pageSize).Take(pageSize));
        }

        [Route("SaveUser")]
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
             _userContext.Users.Add(value);
             _userContext.SaveChanges();
              return Ok();         
        }


        [Route("UpdateUser/{id}")]
        [HttpPut]
        public IActionResult Put(int id, [FromBody] User value)
        {
            var user = _userContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return StatusCode(500, "User is not available");
            }
            _userContext.Entry<User>(user).CurrentValues.SetValues(value);
            _userContext.SaveChanges();
            return Ok();
        }


        [Route("DeleteUser/{id}")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return StatusCode(500, "User is not available");
            }
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
            return Ok();
        }

        [Route("GetUserImage")]
        [HttpGet]
        public IActionResult GetUserImage()
        {
            var path = _hostingEnvironment.ContentRootFileProvider.GetFileInfo("Images/UserPhoto.png").PhysicalPath;
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, "image/png");
        }

    }
}
