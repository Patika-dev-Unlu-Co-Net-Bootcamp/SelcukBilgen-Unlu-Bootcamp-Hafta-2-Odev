using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UserOperations.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly CounselingCenterDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommand(CounselingCenterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == Model.Email);

            if (user is not null)
                throw new InvalidOperationException("Kullanıcı daha önce kayıt olmuş");

            user = _mapper.Map<User>(Model); // Model ile gelen veriyi User objesine convert et.

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