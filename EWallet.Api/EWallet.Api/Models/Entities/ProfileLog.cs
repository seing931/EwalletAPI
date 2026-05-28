using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EWallet.Api.Models.Entities
{
    [Table("profileslogs")]
    public class ProfileLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(255)]
        public string? FullName { get; set; }

        [MaxLength(20)]
        public string? MobileNumber { get; set; }   // O

        [MaxLength(255)]
        public string? Email { get; set; }          // O

        public DateTime? DateOfBirth { get; set; }  // O (YYYY-MM-DD)

        public int? UserRace { get; set; }          // 1 = Malay, 2 = Chinese, 3 = Indian, 4 = Others

        [MaxLength(500)]
        public string? Address { get; set; }        // O

        [MaxLength(100)]
        public string? City { get; set; }           // O

        [MaxLength(100)]
        public string? State { get; set; }          // O

        [MaxLength(10)]
        public string? Postcode { get; set; }       // O

        [MaxLength(2)]
        public string? Country { get; set; }        // O (ISO 3166-1 alpha-2)

        [MaxLength(20)]
        public string? FaxNumber { get; set; }      // O

        [MaxLength(10)]
        public string? Otp { get; set; }             // O (used when update email/mobile)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Token { get; set; }
        // Navigation
    }
}
