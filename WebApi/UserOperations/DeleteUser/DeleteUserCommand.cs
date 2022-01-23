using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.UserOperations.DeleteUser
{
    public class DeleteUserCommand
    {
        private readonly CounselingCenterDbContext _dbContext;
        public int UserId { get; set; }

        public DeleteUserCommand(CounselingCenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == UserId);
            if (user is null)
                throw new InvalidOperationException("User bulunamadı");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}