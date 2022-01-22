using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public enum UserRole
    {
        Client,
        Specialist
    }

    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required] public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }
}