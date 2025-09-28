using System.Text.Json;

namespace NetChat.Services.Extras;

public static class Serializator
{
    public static T Deserialize<T>(string text)
    {
        var result = JsonSerializer.Deserialize<T>(text);
        if (result == null) throw new Exception("Error when serialize");
        return result;
    }
}