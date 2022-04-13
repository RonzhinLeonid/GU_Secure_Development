using AuthorizationAPI.DAL;
using AuthorizationAPI.Interface;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AuthorizationAPI
{
    public class UserService : IUserService
    {
        Random rnd = new Random();
        private readonly ApplicationDataContext _context;

        public UserService(ApplicationDataContext context) => _context = context;

        private KeyValuePair<string, AuthResponse> _users;// = new KeyValuePair<string, AuthResponse>();

        public const string SecretCode = "THIS IS SOME VERY SECRET STRING!!! Im blue da ba dee da ba di da ba dee da ba di da d ba dee da ba di da ba dee";


        public TokenResponse Authenticate(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var user = _context.Users.FirstOrDefault(t => t.Login == login);
            if (user == null) return null;
            //_users.Add(user.Login, new AuthResponse() { Password = user.Password, Salt = user.Salt });
            _users = new KeyValuePair<string, AuthResponse>(user.Login, new AuthResponse() { Password = user.Password, Salt = user.Salt });

            TokenResponse tokenResponse = new TokenResponse();

            if (string.CompareOrdinal(_users.Key, login) == 0 && string.CompareOrdinal(_users.Value.Password, GetPassword(password, _users.Value.Salt)) == 0)
            {
                tokenResponse.Token = GenerateJwtToken(15);
                RefreshToken refreshToken = GenerateRefreshToken();
                _users.Value.LatestRefreshToken = refreshToken;
                tokenResponse.RefreshToken = refreshToken.Token;
                return tokenResponse;
            }
            return null;
        }
        public string RefreshToken(string token)
        {
            int i = 0;
            i++;
            if (string.CompareOrdinal(_users.Value.LatestRefreshToken.Token, token) == 0
                && _users.Value.LatestRefreshToken.IsExpired is false)
            {
                _users.Value.LatestRefreshToken = GenerateRefreshToken();
                return _users.Value.LatestRefreshToken.Token;
            }

            return string.Empty;
        }
        private string GenerateJwtToken(int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "")
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();
            refreshToken.Expires = DateTime.Now.AddMinutes(360);
            refreshToken.Token = GenerateJwtToken(360);
            return refreshToken;
        }

        public async Task Registration(string user, string password)
        {
            _context.Users.Add(Generete(user, password));
            await _context.SaveChangesAsync();
        }

        User Generete(string user, string password)
        {
            var salt = rnd.Next(999999).ToString();
            var tmpPassword = password + salt;
            string hash;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(tmpPassword);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
            return new User() { Login = user, Salt = salt, Password = hash };
        }

        string GetPassword(string password, string salt)
        {
            var tmpPassword = password + salt;
            string hash;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(tmpPassword);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
            return hash;
        }

        public async Task<IList<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
