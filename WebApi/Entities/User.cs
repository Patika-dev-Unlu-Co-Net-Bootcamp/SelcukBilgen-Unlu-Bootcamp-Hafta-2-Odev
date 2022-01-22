using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public enum UserRole
    {
        Client,
        Specialist
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required] public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }
}