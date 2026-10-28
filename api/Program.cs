var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {
      builder.UseMiddleware<SwaValidationMiddleware>();
    })
    .ConfigureOpenApi()
    .ConfigureAppConfiguration((hostContext, services) =>
    {

    })
    .ConfigureServices((hostContext, services) =>
    {
      services.AddSingleton<IOpenApiConfigurationOptions, OpenApiConfigurationOptions>();
      services.AddHttpContextAccessor();
      services.AddScoped<JwtSecurityTokenHandler>();
      services.AddScoped<ITableStorageRepository, TableStorageRepository>();
      services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();
      services.AddScoped<ILoggerHelper, LoggerHelper>();
      services.AddScoped<IAdUserHelper, AdUserHelper>();
      var mapsterConfig = new TypeAdapterConfig();
      mapsterConfig.Scan(typeof(Program).Assembly);
      services.AddSingleton(mapsterConfig);
      services.AddScoped<IMapper, ServiceMapper>();
    })
    .Build();

host.Run();
