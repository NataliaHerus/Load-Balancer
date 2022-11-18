using System.ComponentModel.DataAnnotations;
using static LoadBalancer.IdenityServer.Validators.Validation;

namespace LoadBalancerIdentityServer.Models.Identity
{
    public class LoginRequestModel
    {
        [Required]
        [Email]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
