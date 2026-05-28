using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EWallet.Api.Models.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // User identity
        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(20)]
        public string MobileNumber { get; set; }

        public string? SourceMercId { get; set; }

        [MaxLength(1)]
        public string Gender { get; set; }

        public int IdentificationType { get; set; } // 1=NRIC, 2=Passport

        [Required]
        [MaxLength(50)]
        public string IdentificationId { get; set; }

        // Account status
        public int? Status { get; set; } = 0; // 0=inactive, 1=active

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenExpiry { get; set; }

        public string? WalletId { get; set; }
    }
}
