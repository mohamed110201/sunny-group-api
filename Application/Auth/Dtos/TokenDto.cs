namespace Application.Auth.Dtos;

using System.Text.Json.Serialization;

public class TokenDto
{
    public string AccessToken { get; set; } = string.Empty;

    [JsonIgnore]
    public string RefreshToken { get; set; } = string.Empty;
}