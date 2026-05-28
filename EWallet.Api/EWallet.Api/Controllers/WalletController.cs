using EWallet.Api.Models.Dtos;
using EWallet.Api.Models.Entities;
using EWallet.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace EWallet.Api.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<WalletController> _logger;

        public WalletController(IWalletService walletService, ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        [HttpPost("topup")]
        public async Task<IActionResult> Topup([FromBody] TopupRequest request)
        {
            try
            {
                var topUptrans = new TopupTransaction
                {
                    OrderDate = request.ord_date,
                    OrderAmount = request.ord_totalamt,
                    MerchantId = request.ord_mercID,
                    OrderRef = request.ord_mercref,
                    OrderUrl = request.ord_returnURL,
                    AuthToken = request.auth_token,
                    MerchantHashValue = request.merchant_hashvalue
                };
                var topup = await _walletService.TopupAsync(topUptrans);
                return Ok(topup);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Top up failed | OrderRef: {OrderRef} | OrderTotal : {totalamt} | Error: {ErrorMessage}", request.ord_mercref, request.ord_totalamt, ex.Message);
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
