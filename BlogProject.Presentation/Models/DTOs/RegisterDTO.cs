using BlogProject.Domain.Enum;

namespace BlogProject.Presentation.Models.DTOs
{
    public class RegisterDTO
    {
        //Todo: DataAnnotaions, Validations
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
