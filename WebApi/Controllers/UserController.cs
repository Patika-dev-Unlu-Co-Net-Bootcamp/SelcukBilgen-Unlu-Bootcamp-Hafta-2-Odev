using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // readonly uygulama içerisinden değiştirilemez. Yalnızca constructor ile set edilebilir.
        private readonly CounselingCenterDbContext _context; 

        public UserController(CounselingCenterDbContext context) // constructor ile inject etme.
        {
            _context = context;
        }

        // Tüm kullanıcıları getir
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.OrderBy(u => u.Id).ToList();
            return Ok(users);
        }
        
        // id'ye göre kullanıcı getir. FromRoute
        [HttpGet("{id}")]
        public ActionResult<User> GetById([FromRoute] int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return NotFound();

            return Ok(user);
        }
        
        // yeni kullanıcı ekleme
        [HttpPost]
        public IActionResult AddUser([FromBody] User newUser)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == newUser.Email);

            if (user is not null)
                return BadRequest("Kullanıcı daha önce kayıt olmuş");

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return StatusCode(201);
        }
        
        // var olan kullanıcıyı güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return BadRequest("Kullanıcı bulunamadı");

            user.Email = updatedUser.Email != default ? updatedUser.Email : user.Email;
            user.UserName = updatedUser.UserName != default ? updatedUser.UserName : user.UserName;
            user.UserRole = updatedUser.UserRole != default ? updatedUser.UserRole : user.UserRole;

            _context.SaveChanges();

            return Ok();
        }
        
        // From Query ile kullanıcı silme
        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return BadRequest();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
        
        // tek bir alanı güncelleme için patch yöntemi.
        [HttpPatch("{id}")]
        public IActionResult UpdateUserEmail(int id, [FromBody] string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return BadRequest();

            user.Email = email != default ? email : user.Email;
            _context.SaveChanges();

            return Ok();
        }
        
        // Örn: /api/products/list?name=abc

        
        
    }
}