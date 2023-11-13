using System.Text;

namespace BookLibrary.Entities;

public class Car : EntityBase
{
    public string Name { get; set; }
    public string Color { get; set; }
    public decimal StandardCost { get; set; }
    public decimal ListPrice { get; set; }
    public string Type { get; set; }


    public int? NameLength { get; set; }
    public decimal? TotalSales { get; set; }

    #region ToString Override
    public override string ToString()
    {
        StringBuilder sb = new(1024);
        sb.AppendLine($"{Name} ID: {Id}");
        sb.AppendLine($"    Color: {Color}  Type: {(Type ?? "n/a")}");
        sb.AppendLine($"    Cpst: {StandardCost:c}  Price: {ListPrice:c}");
        if(NameLength.HasValue)
        {
            sb.AppendLine($"    Name Length: {NameLength}");
        }
        if(TotalSales.HasValue) 
        {
            sb.AppendLine($"    Total Sales: {TotalSales}");
        }
        return sb.ToString();
    }
    #endregion
}
