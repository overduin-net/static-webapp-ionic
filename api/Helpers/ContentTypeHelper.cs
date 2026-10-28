internal static class ContentTypeHelper
{
    internal static string DetermineContentType(this Type type) => type switch
    {
        not null when type == typeof(string) => "text/plain",
        not null when type == typeof(Stream) || typeof(Stream).IsAssignableFrom(type) => "application/octet-stream",
        _ => "application/json"
    };

    internal static Type? DetermineResponseType(this Type type) => type switch
    {
        not null when type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>) => ToKeyValuePairEnumerable(type),
        _ => type
    };

    private static Type ToKeyValuePairEnumerable(Type dictionary)
    {
        var genericArguments = dictionary.GetGenericArguments();
        var keyValuePairType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
        return typeof(IEnumerable<>).MakeGenericType(keyValuePairType);
    }
}
