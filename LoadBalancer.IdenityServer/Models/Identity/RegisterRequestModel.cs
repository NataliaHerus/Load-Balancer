using System.ComponentModel.DataAnnotations;
using static LoadBalancer.IdenityServer.Validators.Validation;

namespace LoadBalancerIdentityServer.Models.Identity
{
    public class RegisterRequestModel
    {
        [Required]
        [Name]
        public string? FirstName { get; set; }

        [Required]
        [Name]
        public string? LastName { get; set; }

        [Required]
        [Email]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
