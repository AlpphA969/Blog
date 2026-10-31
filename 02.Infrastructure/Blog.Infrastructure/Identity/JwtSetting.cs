namespace Blog.Infrastructure.Identity;

public class JwtSetting
{
    public const string SectionName = "JwtSettings";
    
    public string Key { get; set; } = string.Empty;
    
    public string Issuer { get; set; } = string.Empty;
    
    public string Audience { get; set; } = string.Empty;
    
    public int ExpiryMinutes { get; set; }
    
    
}