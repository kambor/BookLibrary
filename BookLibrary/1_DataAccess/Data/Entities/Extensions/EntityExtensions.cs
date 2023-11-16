using System.Text.Json;

namespace BookLibrary.DataAccess.Data.Entities.Extensions;

public static class EntityExtensions
{
    public static T? Coppy<T>(this T itemToCopy)where T: IEntity
    {
        var json = JsonSerializer.Serialize<T>(itemToCopy);
        return JsonSerializer.Deserialize<T>(json);
    }
}
