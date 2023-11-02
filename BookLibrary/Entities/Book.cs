namespace BookLibrary.Entities;

public class Book : EntityBase
{
    public string? Title { get; set; }
    public string? Authors { get; set; }
    public DateTime? RelaseData { get; set; }  
    public Category Category { get; set; }
    public bool IsAvailable { get; set; }
      

    public override string ToString() => $"Id: {Id}, Title: {Title}, Authors: {Authors}, Category: {Category}, Relase Data: {RelaseData.Value.ToShortDateString()}";


}
