using EWallet.Api.Clients;
using EWallet.Api.Data;
using EWallet.Api.Models.Dtos;
using EWallet.Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;


namespace EWallet.Api.Services
{
    public interface IUserService
    {
        Task<regResponse> RegisterUserAsync(User user, string password);
        Task<tokenResponse> LoginUserAsync(string username, string password,string source_merchant_id);
        Task<profileResponse> GetProfileAsync(string token);
        Task<UpdprofileResponse> UpdateProfileAsync(string token, ProfileLog updatedProfile);
    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IEWalletApiClient _apiClient;
        private readonly IConfiguration _config;
        public UserService(AppDbContext db, IEWalletApiClient apiClient, IConfiguration config)
        {
            _db = db;
            _apiClient = apiClient;
            _config = config;
        }
        public async Task<regResponse> RegisterUserAsync(User user, string password)
        {
            var payload = new
            {
                username = user.Username,
                full_name = user.FullName,
                password = password,
                verify_password = password,
                mobile = user.MobileNumber,
                source_merchant_id = user.SourceMercId,
                gender = user.Gender,
                identification_type = user.IdentificationType,
                identification_id = user.IdentificationId
            };

            var response = await _apiClient.PostAsync<regResponse>( $"{_config["EWallet:ThirdPartyUrl"]}/register",payload);

            if (response.success)
            {
                user.Username = response.data.username;
                user.FullName = response.data.full_name;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                user.MobileNumber = payload.mobile;
                user.SourceMercId = payload.source_merchant_id;
                user.Gender = payload.gender;
                user.IdentificationType = payload.identification_type;
                user.IdentificationId = payload.identification_id;
                user.RegistrationDate = DateTime.Parse(response.data.registration_date);
                user.Status = response.data.status;
                user.Token = response.data.token;
                user.RefreshToken = response.data.refresh_token;
                user.TokenExpiry = DateTime.Parse(response.data.token_expiry);
                user.WalletId = response.data.wallet_id;

                _db.Users.Add(user);              
                await _db.SaveChangesAsync();     
            }

            return response;
        }
        public async Task<tokenResponse> LoginUserAsync(string username, string password,string source_merchant_id)
        {
            var payload = new
            {
                username = username,
                password = password,
                source_merchant_id = source_merchant_id
            };

            var response = await _apiClient.PostAsync<tokenResponse>($"{_config["EWallet:ThirdPartyUrl"]}/token", payload);

            return response;
        }
        public async Task<profileResponse> GetProfileAsync(string token)
        {
            var response = await _apiClient.GetAsync<profileResponse>($"{_config["EWallet:ThirdPartyUrl"]}/profile.json", token);
            return response;
        }
        public async Task<UpdprofileResponse> UpdateProfileAsync(string token, ProfileLog profile)
        {

            var payload = new
            {
                full_name = profile.FullName,
                mobile_number = profile.MobileNumber,
                email = profile.Email,
                date_of_birth = profile.DateOfBirth,
                user_race = profile.UserRace,
                address = profile.Address,
                city = profile.City,
                state = profile.State,
                postcode = profile.Postcode,
                country = profile.Country,
                fax_number = profile.FaxNumber,
                otp = profile.Otp
            };

            var response = await _apiClient.PutAsync<UpdprofileResponse>($"{_config["EWallet:ThirdPartyUrl"]}/profile.json", token, payload);

            if (response.success)
            {
                profile.FullName = response.data.full_name;
                profile.MobileNumber = response.data.mobile_number;
                profile.Email = payload.email;
                profile.DateOfBirth = payload.date_of_birth;
                profile.UserRace = payload.user_race;
                profile.Address = payload.address;
                profile.City = payload.city;
                profile.State = payload.state;
                profile.Postcode = payload.postcode;
                profile.Country = payload.country;
                profile.FaxNumber = payload.fax_number;
                profile.Otp = payload.otp;
                profile.Token = token;
                _db.ProfilesLogs.Add(profile);
                await _db.SaveChangesAsync();
            }
            return response;
        }
    }
}
