namespace BookLibrary.Services;

public abstract class UserComunicationBase
{
    protected string GetInputFromUser(string comment)
    {
        Console.Write(comment);
        var userInput = Console.ReadLine();
        return userInput;
    }

    protected T? GetValueFromUser<T>(string comment) where T : struct
    {
        while (true)
        {
            var input = GetInputFromUser(comment);

            try
            {
                if (typeof(T) == typeof(int))
                {
                    return (T)(object)int.Parse(input);
                }
                else if (typeof(T) == typeof(double))
                {
                    return (T)(object)double.Parse(input);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter correct value.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter correct value.");
            }
        }
    }
}
