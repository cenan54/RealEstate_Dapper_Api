namespace RealEstate_Dapper_Api.Tools
{
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }

        //Token yanıtı için çalışacak viewmodel

        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
