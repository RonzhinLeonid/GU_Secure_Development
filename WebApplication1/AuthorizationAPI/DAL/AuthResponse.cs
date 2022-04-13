namespace AuthorizationAPI.DAL
{
    public sealed class AuthResponse
    {
        public string Password { get; set; }
        public string Salt { get; set; }

        public RefreshToken LatestRefreshToken { get; set; }
    }
}
