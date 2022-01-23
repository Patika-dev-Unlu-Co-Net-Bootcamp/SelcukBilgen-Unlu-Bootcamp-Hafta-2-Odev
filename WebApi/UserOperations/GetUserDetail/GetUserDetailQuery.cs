using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.UserOperations.GetUserDetail
{
    public class GetUserDetailQuery
    {
        private readonly CounselingCenterDbContext _dbContext;
        public int UserId { get; set; }

        public GetUserDetailQuery(CounselingCenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserDetailViewModel Handle()
        {
            var user = _dbContext.Users.Where(u => u.Id == UserId).SingleOrDefault();
            if (user is null)
                throw new InvalidOperationException("User bulunamadı");
            
            UserDetailViewModel vm = new UserDetailViewModel();
            vm.UserName = $"{user.FirstName} {user.LastName}";
            vm.Email = user.Email;
            vm.UserRole = user.UserRole.ToString();

            return vm;
        }
    }

    public class UserDetailViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}