namespace Shoping.Domain;

public class RefreshToken
{
    public int RefreshTokenId { get; set; }
    public string RefreshTokenValue { get; set; }=string.Empty;
    public bool Active { get; set; }
    public DateTime Expiration { get; set; }
    public bool Used { get; set; }
    public User User { get; set; }= new User();
    public string UserId { get; set; } =string.Empty;
}