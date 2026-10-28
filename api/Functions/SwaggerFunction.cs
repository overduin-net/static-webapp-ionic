public class SwaggerFunction
{
  private readonly ISwashBuckleClient _swashBuckleClient;

  public SwaggerFunction(ISwashBuckleClient swashBuckleClient)
  {
    _swashBuckleClient = swashBuckleClient;
  }

  [SwaggerIgnore]
  [Function("SwaggerJson")]
  public async Task<HttpResponseData> SwaggerJson([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/json")] HttpRequestData req)
  {
    return await _swashBuckleClient.CreateSwaggerJsonDocumentResponse(req);
  }

  [SwaggerIgnore]
  [Function("SwaggerYaml")]
  public async Task<HttpResponseData> SwaggerYaml([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/yaml")] HttpRequestData req)
  {
    return await _swashBuckleClient.CreateSwaggerYamlDocumentResponse(req);
  }

  [SwaggerIgnore]
  [Function("SwaggerUi")]
  public async Task<HttpResponseData> SwaggerUi([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequestData req)
  {
    return await _swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json");
  }

  [SwaggerIgnore]
  [Function("SwaggerOAuth2Redirect")]
  public async Task<HttpResponseData> SwaggerOAuth2Redirect([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/oauth2-redirect")] HttpRequestData req)
  {
    return await _swashBuckleClient.CreateSwaggerOAuth2RedirectResponse(req);
  }
}
