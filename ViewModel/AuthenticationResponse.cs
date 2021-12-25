namespace IotAdminAPI.ViewModel
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(string access_token, string token_type, double expires_in)
        {
            this.access_token = access_token;
            this.token_type = token_type;
            this.expires_in = expires_in;
        }

        public string access_token { get; set; }
        public string token_type { get; set; }

        public double expires_in { get; set; }
    }
}
