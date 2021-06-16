namespace ImagesGalore.server.Models
{
  public class Account
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
  }
}