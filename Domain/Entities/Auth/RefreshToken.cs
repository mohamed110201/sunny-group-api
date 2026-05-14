namespace Domain.Entities.Auth;

public class RefreshToken
{
    public int Id { get; set; }
    public int AdminId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsActive => !IsRevoked && !IsExpired;
    public Admin Admin { get; set; } = null!;
}