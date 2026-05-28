using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EWallet.Api.Models.Dtos
{
    public class regResponse
    {
        public bool success { get; set; }
        public regData data { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
    public class regData
    {
        public string username { get; set; }
        public string full_name { get; set; }

        [JsonPropertyName("registration_date")]
        public string registration_date { get; set; }
        public int? status { get; set; }
        public string? refresh_token { get; set; }
        public string? token { get; set; }

        [JsonPropertyName("token_expiry")]
        public string? token_expiry { get; set; }
        public string? wallet_id { get; set; }
    }
    public class tokenResponse
    {
        public bool success { get; set; }
        public tokenData data { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
    public class tokenData
    {
        public string? token { get; set; }

        [JsonPropertyName("token_expiry")]
        public string? token_expiry { get; set; }
        public decimal? account_balance { get; set; }
        public long? wallet_id { get; set; }
        public string? refresh_token { get; set; }
    }
    public class profileResponse
    {
        public bool success { get; set; }
        public profileData data { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
    public class profileData
    {
        public string username { get; set; }
        public string full_name { get; set; }
        public string mobile_number { get; set; }
        public string email { get; set; }
        public DateTime? date_of_birth { get; set; }
        public int? gender { get; set; } // 1=Male, 2=Female?
        public int? user_race { get; set; } // 1=Malay, 2=Chinese, 3=Indian, 4=Others
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postcode { get; set; }
        public string? country { get; set; }
        public string? nationality { get; set; }
        public string? contact_number { get; set; }
        public string? fax_number { get; set; }
        public string? avatar { get; set; }
        public string? identification_id { get; set; }
        public int? identification_type { get; set; }
        public int? status { get; set; }
        public long? wallet_id { get; set; }
        public bool? has_wallet { get; set; }
        public bool? has_pin { get; set; }
    }
    public class UpdprofileResponse
    {
        public bool success { get; set; }
        public UpdprofileData data { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
    public class UpdprofileData
    {
        public long? wallet_id { get; set; }
        public string full_name { get; set; }
        public string mobile_number { get; set; }
        public string? nationality { get; set; }
    }
    public class WebcashTopupInitResponse
    {
        public CheckoutRequest checkoutRequest { get; set; }
        public bool displayBanks { get; set; }
        public bool displayWallets { get; set; }
        public bool displayCreditCard { get; set; }
    }
    public class CheckoutData
    {
        public string ord_mercref { get; set; }
        public decimal ord_totalamt { get; set; }
        public long ord_mercID { get; set; }
    }
    public class CheckoutResponse
    {
        public string ord_mercref { get; set; }
        public string ord_date { get; set; }
        public decimal ord_totalamt { get; set; }
        public string ord_mercID { get; set; }
        public string auth_token { get; set; }
        public string? checkout2 { get; set; }
        public string? wcID { get; set; }
        public string? returncode { get; set; }
    }
}
