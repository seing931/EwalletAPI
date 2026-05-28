using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EWallet.Api.Models.Entities
{
    public class TopupTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(100)]
        public string OrderRef { get; set; } // ord_mercref

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderAmount { get; set; }

        public string OrderUrl { get; set; }

        [MaxLength(50)]
        public string MerchantId { get; set; }

        [Required]
        public string AuthToken { get; set; }

        [Required]
        public string MerchantHashValue { get; set; }

        public string checkout2 { get; set; }

        public string wcID { get; set; }

        [MaxLength(10)]
        public string ReturnCode { get; set; }

        public string orderKey { get; set; } // ord_key / wcID

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
