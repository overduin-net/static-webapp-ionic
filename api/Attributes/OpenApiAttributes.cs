using System.Net;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiParameterAttribute<T>(string name, ParameterLocation location, bool required = false) :
    OpenApiParameterAttribute(name)
{
    public override ParameterLocation In { get; set; } = location;
    public override Type Type { get; set; } = typeof(T);
    public override bool Required { get; set; } = required;
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiRequestAttribute<T>() :
    OpenApiRequestBodyAttribute(typeof(T).DetermineContentType(), typeof(T));

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiResponseWithBodyAttribute<T>(HttpStatusCode statusCode, string? description = null) :
    OpenApiResponseWithBodyAttribute(statusCode, typeof(T).DetermineContentType(), typeof(T).DetermineResponseType())
{
    public override string? Description { get; set; } = description;
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiResponseOkAttribute<T>(string? description = null, string? contentType = null) :
    OpenApiResponseWithBodyAttribute<T>(HttpStatusCode.OK, description)
{
    public override string ContentType { get; } = contentType ?? typeof(T).DetermineContentType();
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiResponseCreatedAttribute(string? description = "Created successfully") :
    OpenApiResponseWithBodyAttribute<string>(HttpStatusCode.Created, description);

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiResponseNoContentAttribute() :
    OpenApiResponseWithoutBodyAttribute(HttpStatusCode.NoContent);

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiResponseNotFoundAttribute(string? description = "Not found") :
    OpenApiResponseWithBodyAttribute<string>(HttpStatusCode.NotFound, description);

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiResponseBadRequestAttribute(string? description = "Bad request") :
    OpenApiResponseWithBodyAttribute<string>(HttpStatusCode.BadRequest, description);

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class OpenApiResponseConflictAttribute(string? description = "Conflict") :
    OpenApiResponseWithBodyAttribute<string>(HttpStatusCode.Conflict, description);
