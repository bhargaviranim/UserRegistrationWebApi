using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistrationAPI.Model
{
    [Table("User", Schema = "Security")]
    public partial class UserRegistrationUser
    {
        [Required()]
        [Key()]
        public Guid UserId { get; set; }

        [Required()]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required()]
        [StringLength(255)]
        public string Password { get; set; }
    }
}

