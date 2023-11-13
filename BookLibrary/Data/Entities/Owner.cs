namespace BookLibrary.Entities;

public  class Owner : EntityBase
{
    public string? Name { get; set; }

    public override string ToString() => $"Id: {Id}, Name: {Name}";


}
