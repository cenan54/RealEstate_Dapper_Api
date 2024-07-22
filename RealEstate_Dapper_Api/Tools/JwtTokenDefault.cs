namespace RealEstate_Dapper_Api.Tools
{
    public class JwtTokenDefault
    {
        //token kim tarafından dinlenilecek kısmı
        public const string ValidAudience = "https://localhost";
        //token kim tarafından yayınlanacak kısmı
        public const string ValidIssuer = "https://localhost";
        //tokenın geçerli olabilmesi için her tokenda bulunması gereken secret key
        public const string Key = "REALestate..0102030405Asp.NetCore.8.0.1++--*/*/";
        //token geçerlilik süresi dakika cinsinden
        public const int Expire = 5;
    }
}
