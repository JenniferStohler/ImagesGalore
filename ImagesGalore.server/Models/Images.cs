using System;

namespace ImagesGalore.server.Models
{
  public class Images
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Image { get; set; } = "http://placehold.it150x150";
  }
}