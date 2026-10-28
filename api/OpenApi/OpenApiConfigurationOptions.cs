using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
    public override OpenApiInfo Info { get; set; } = new()
    {
        Version = "v1",
        Title = "Static WebApp Ionic API",
        Description = "OpenAPI documentation for the Static WebApp Ionic Azure Functions API.",
        Contact = new OpenApiContact
        {
            Name = "Static WebApp Ionic",
            Email = string.Empty
        }
    };

    public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
    public override bool IncludeRequestingHostName { get; set; } = false;
    public override List<OpenApiServer> Servers { get; set; } = [];
    public override bool ForceHttp { get; set; } = false;
    public override bool ForceHttps { get; set; } = false;
}
