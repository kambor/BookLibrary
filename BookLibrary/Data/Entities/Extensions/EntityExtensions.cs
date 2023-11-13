using System.Text.Json;

namespace BookLibrary.Entities.Extensions;

public static class EntityExtensions
{
    //kopia obiektu
    public static T? Coppy<T>(this T itemToCopy)where T: IEntity
    {
        var json = JsonSerializer.Serialize<T>(itemToCopy);//zamiana na stringa
        return JsonSerializer.Deserialize<T>(json);
    }
}
