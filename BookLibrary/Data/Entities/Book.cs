using System.Drawing;
using System.Text;

namespace BookLibrary.Entities;

public class Book : EntityBase
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public DateTime? RelaseData { get; set; }  
    public Category Category { get; set; }
    public bool IsAvailable { get; set; }
     

    public override string ToString()
    {
        StringBuilder sb = new(1024);
        sb.AppendLine($"ID: {Id}");
        sb.AppendLine($"    Title: {Title}  Authors: {Author}");
        sb.AppendLine($"    Category: {Category}  Relase: {RelaseData.Value.ToShortDateString()}");
      
        return sb.ToString();
    }
}
