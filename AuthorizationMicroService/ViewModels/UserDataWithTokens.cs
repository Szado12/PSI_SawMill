namespace AuthorizationMicroService.ViewModels
{
  public class UserDataWithTokens
  {
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int RoleId { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
  }
}
