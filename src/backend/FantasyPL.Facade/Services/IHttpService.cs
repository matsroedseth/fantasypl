using FantasyPL.Facade.Models.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace FantasyPL.Facade.Clients;

public interface IHttpService
{
    Task<T> GetAsync<T>(Uri uri, Dictionary<string, string> queryParameters = null, Dictionary<string, string> additionalHeaders = null);
    Task<T> PostAsync<T>(Uri uri, string payload, Dictionary<string, string> queryParameters = null, Dictionary<string, string> additionalHeaders = null);
    Task<T> PatchAsync<T>(Uri uri, string payload, Dictionary<string, string> queryParameters = null, Dictionary<string, string> additionalHeaders = null);
    Task<T> PutAsync<T>(Uri uri, string payload, Dictionary<string, string> queryParameters = null, Dictionary<string, string> additionalHeaders = null);
    Task<T> DeleteAsync<T>(Uri uri, Dictionary<string, string> additionalHeaders = null);
}

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpService> _logger;

    public HttpService(ILogger<HttpService> logger, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<T> DeleteAsync<T>(Uri uri, Dictionary<string, string> additionalHeaders = null)
    {
        try
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri))
            {
                if (additionalHeaders != null && additionalHeaders.Any())
                {
                    AddHeaders(requestMessage, additionalHeaders);
                }

                _logger.LogDebug($"URI: {requestMessage.RequestUri}");

                return await SendAsync<T>(requestMessage);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteAsync.");
            throw;
        }
    }

    public async Task<T> GetAsync<T>(Uri uri, Dictionary<string, string> queryParameters = null,
        Dictionary<string, string> additionalHeaders = null)
    {
        if (queryParameters != null)
        {
            uri = AppendQueryParameters(uri, queryParameters);
        }

        try
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                if (additionalHeaders != null && additionalHeaders.Any())
                {
                    AddHeaders(requestMessage, additionalHeaders);
                }

                return await SendAsync<T>(requestMessage);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAsync<{T}>.", typeof(T).Name);
            throw;
        }
    }

    public async Task<T> PostAsync<T>(Uri uri, string body, Dictionary<string, string> queryParameters, Dictionary<string, string> additionalHeaders)
    {
        if (queryParameters != null)
        {
            uri = AppendQueryParameters(uri, queryParameters);
        }

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri))
        using (requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json"))
        {
            try
            {
                if (additionalHeaders != null && additionalHeaders.Any())
                {
                    AddHeaders(requestMessage, additionalHeaders);
                }

                _logger.LogDebug($"URI: {requestMessage.RequestUri} with body {body}");
                return await SendAsync<T>(requestMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PostAsync.");
                throw;
            }
        }
    }

    public async Task<T> PatchAsync<T>(Uri uri, string body, Dictionary<string, string> queryParameters, Dictionary<string, string> additionalHeaders)
    {
        if (queryParameters != null)
        {
            uri = AppendQueryParameters(uri, queryParameters);
        }

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Patch, uri))
        using (requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json"))
        {
            try
            {
                if (additionalHeaders != null && additionalHeaders.Any())
                {
                    AddHeaders(requestMessage, additionalHeaders);
                }

                _logger.LogDebug($"URI: {requestMessage.RequestUri} with body {body}");
                return await SendAsync<T>(requestMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PatchAsync.");
                throw;
            }
        }
    }

    public async Task<T> PutAsync<T>(Uri uri, string body, Dictionary<string, string> queryParameters, Dictionary<string, string> additionalHeaders)
    {
        if (queryParameters != null)
        {
            uri = AppendQueryParameters(uri, queryParameters);
        }

        using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri))
        using (requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json"))
        {
            try
            {
                if (additionalHeaders != null && additionalHeaders.Any())
                {
                    AddHeaders(requestMessage, additionalHeaders);
                }

                _logger.LogDebug($"URI: {requestMessage.RequestUri} with body {body}");
                return await SendAsync<T>(requestMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PutAsync.");
                throw;
            }
        }
    }

    private async Task<T> SendAsync<T>(HttpRequestMessage requestMessage)
    {
        var responseMessage = await _httpClient.SendAsync(requestMessage);
        var jsonResult = await responseMessage.Content.ReadAsStringAsync();
        if (responseMessage.IsSuccessStatusCode)
        {
            if (string.IsNullOrEmpty(jsonResult))
            {
                return default(T);
            }

            _logger.LogDebug($"Status code: {responseMessage.StatusCode}, body {jsonResult}");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<T>(jsonResult, options);
        }
        else if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException();
        }
        else if (responseMessage.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            throw new ForbiddenException();
        }
        else if (responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new KeyNotFoundException();
        }
        else if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return default(T);
        }
        else
        {
            throw new Exception($"Status code: {responseMessage.StatusCode}, body: {jsonResult}");
        }
    }

    private static Uri AppendQueryParameters(Uri uri, Dictionary<string, string> queryParameters)
    {
        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        foreach (var pair in queryParameters)
        {
            query.Add(pair.Key, pair.Value);
        }

        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }

    private static void AddHeaders(HttpRequestMessage requestMessage, Dictionary<string, string> headers)
    {
        foreach (var pair in headers)
        {
            requestMessage.Headers.Add(pair.Key, pair.Value);
        }
    }
}