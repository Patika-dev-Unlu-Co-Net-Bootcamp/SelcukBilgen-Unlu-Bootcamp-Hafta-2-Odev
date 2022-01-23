using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UserOperations.GetUsers
{
    public class GetUsersQuery
    {
        private readonly CounselingCenterDbContext _dbContext;

        public GetUsersQuery(CounselingCenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserViewModel> Handle()
        {
            var userList = _dbContext.Users.OrderBy(u => u.Id).ToList<User>();
            List<UserViewModel> vm = new List<UserViewModel>();
            foreach (var user in userList)
            {
                vm.Add(new UserViewModel()
                {
                    Email = user.Email,
                    UserName = $"{user.FirstName} {user.LastName}",
                    UserRole = (user.UserRole).ToString()
                });
            }

            return vm;
        }
    }

    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}