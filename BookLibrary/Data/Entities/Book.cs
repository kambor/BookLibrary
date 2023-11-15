using System.Text;

namespace BookLibrary.Entities;

public class Book : EntityBase
{
    public string? Isbn { get; set; }
    public string? Author { get; set; }
    public int? PublicationYear { get; set; }
    public string? Title { get; set; }
    public double? AverageRating { get; set; }
    public string? ImageUrl { get; set; }


    public override string ToString()
    {
        StringBuilder sb = new(1024);
        sb.AppendLine($"ID: {Id}, Isbn: {Isbn}");
        sb.AppendLine($"    Title: {Title}  Authors: {Author}");
        sb.AppendLine($"    Relase: {PublicationYear}   Rating: {AverageRating}");
      
        return sb.ToString();
    }
}
