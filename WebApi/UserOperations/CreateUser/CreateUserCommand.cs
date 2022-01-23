using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UserOperations.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly CounselingCenterDbContext _dbContext;

        public CreateUserCommand(CounselingCenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == Model.Email);

            if (user is not null)
                throw new InvalidOperationException("Kullanıcı daha önce kayıt olmuş");

            user = new User();
            user.FirstName = Model.FirstName;
            user.LastName = Model.LastName;
            user.Email = Model.Email;
            user.UserRole = (UserEnum) Model.UserRole;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }

    public class CreateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserRole { get; set; }
    }
}