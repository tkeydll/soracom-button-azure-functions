#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    string action = data?.value1;

    log.LogInformation(requestBody);

    switch (action)
    {
        case "SINGLE":
            break;
        case "DOUBLE":
            await PostAsync("", log);
            break;
        case "LONG":
            await PostAsync("", log);
            break;
    }

    string responseMessage = string.IsNullOrEmpty(action)
        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Event is {action}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
}

private static HttpClient _client = new HttpClient();

public static async Task PostAsync(string url, ILogger log)
{
    var parameters = new Dictionary<string, string>()
    {
        { "foo", "bar" }
    };
    var content = new FormUrlEncodedContent(parameters);
    var response = await _client.PostAsync(url, content);

    log.LogInformation($"Response = {response}");
}
