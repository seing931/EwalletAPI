using System.ComponentModel.DataAnnotations;

namespace EWallet.Api.Models.Dtos
{
    public record LoginRequest([Required] string username, [Required] string password, string source_merchant_id);
    public record RegisterRequest([Required] string username, [Required] string full_name, [Required] string password, [Required] string verify_password, [Required] string Mobile, string source_merchant_id, [Required] string gender, [Required] int identification_type, [Required] string identification_id);
    public record TopupRequest([Required] DateTime ord_date, [Required] decimal ord_totalamt, [Required] string ord_mercID, [Required] string ord_mercref, [Required][Url] string ord_returnURL, [Required] string auth_token, [Required] string merchant_hashvalue);
    public record ProfileRequest(string? full_name, string? mobile_number, string? email, DateTime? date_of_birth, int? user_race, string? address, string? city, string? state, string? postcode, string? country, string? fax_number, string? otp);
    public class CheckoutRequest
    {
        public CheckoutData data { get; set; }
    }
}
