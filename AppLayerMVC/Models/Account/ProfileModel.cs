using System.ComponentModel.DataAnnotations;

namespace AppLayerMVC.Models.Account
{
    public class ProfileModel
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string AccountStatus { get; set; } = null!;
    }
}
