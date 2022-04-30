public static class Program
{
    public static void Main(string[] args)
    {
        var card = new MessageCard()
        {
            Title = args[1],
            Summary = args[2],
            Text = args[3],
            ThemeColor = args[4],
            Sections = ParseCollection<Section>(args[5]),
            Actions = ParseCollection<BaseAction>(args[6])
        };

        var converted = JsonConvert.SerializeObject(card);

        var message = (string)null;
        var requestUri = args[0];

        using (var client = new HttpClient())
        using (var content = new StringContent(converted, Encoding.UTF8, "application/json"))
        using (var response = await client.PostAsync(requestUri, content).ConfigureAwait(false))
        {
            response.EnsureSuccessStatusCode();
        }
    }

    private static List<T> ParseCollection<T>(string value)
    {
        var parsed = string.IsNullOrWhiteSpace(value)
                     ? null
                     : JsonConvert.DeserializeObject<List<T>>(value, settings);

        return parsed;
    }
}