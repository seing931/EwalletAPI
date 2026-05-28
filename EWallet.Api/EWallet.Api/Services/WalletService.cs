using EWallet.Api.Clients;
using EWallet.Api.Data;
using EWallet.Api.Models.Dtos;
using EWallet.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace EWallet.Api.Services
{
    public interface IWalletService
    {
        Task<WebcashTopupInitResponse> TopupAsync(TopupTransaction topup);
    }
    public class WalletService : IWalletService
    {
        private readonly AppDbContext _db;
        private readonly IEWalletApiClient _apiClient;
        private readonly IConfiguration _config;

        public WalletService(AppDbContext db, IEWalletApiClient apiClient, IConfiguration config)
        {
            _db = db;
            _apiClient = apiClient;
            _config = config;
        }

        public async Task<WebcashTopupInitResponse> TopupAsync(TopupTransaction topup)
        {

            var orderRef = Guid.NewGuid().ToString("N");
            var ordTotalAmt = ((int)(topup.OrderAmount * 100)).ToString();

            var merchantSecret = _config["EWallet:MerchantSecret"];
            var merchantId = _config["EWallet:ClientId"];

            // Generate hash
            var rawHash = $"{merchantSecret}{merchantId}{orderRef}{ordTotalAmt}";
            using var sha256 = SHA256.Create();
            var merchantHashValue = Convert.ToHexString(sha256.ComputeHash(Encoding.UTF8.GetBytes(rawHash))).ToLower();

            var payload = new
            {
                ord_date = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                ord_totalamt = topup.OrderAmount,
                ord_mercID = merchantId,
                ord_mercref = orderRef,
                ord_returnURL = topup.OrderUrl,
                merchant_hashvalue = merchantHashValue
            };

            var response = await _apiClient.PostAsync<WebcashTopupInitResponse>($"{_config["EWallet:BaseUrl"]}/gateway/topup", payload);

            return response;
        }
    }
}
