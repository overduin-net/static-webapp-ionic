using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Functions.Worker.Middleware;

public class SwaValidationMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var method = GetHttpTriggerMethod(context);
        var httpContext = context.GetHttpContext();
        if (method == null || httpContext == null)
        {
            await next(context);
            return;
        }

        var attributes = method.GetCustomAttributes<AuthorizeAttribute>(true)
            .Concat(method.DeclaringType?.GetCustomAttributes<AuthorizeAttribute>(true) ?? [])
            .ToList();

        if (attributes.Count == 0)
        {
            await next(context);
            return;
        }

        var adUserHelper = context.InstanceServices.GetRequiredService<IAdUserHelper>();
        var principal = adUserHelper.GetStaticWebAppClientPrincipal(httpContext.Request);
        var userRoles = principal.UserRoles?.ToList() ?? [];

        if (userRoles.Count == 0)
        {
            Reject(context, StatusCodes.Status401Unauthorized);
            return;
        }

        var allowed = attributes.All(attribute =>
        {
            var roles = (attribute.Roles ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (roles.Length == 0)
            {
                return userRoles.Contains(Constants.Role.Authenticated, StringComparer.OrdinalIgnoreCase);
            }

            return userRoles.Contains(Constants.Role.Administrator, StringComparer.OrdinalIgnoreCase)
                || roles.Any(role => userRoles.Contains(role, StringComparer.OrdinalIgnoreCase));
        });

        if (!allowed)
        {
            Reject(context, StatusCodes.Status403Forbidden);
            return;
        }

        await next(context);
    }

    private static void Reject(FunctionContext context, int statusCode)
    {
        context.GetInvocationResult().Value = new StatusCodeResult(statusCode);
    }

    private static MethodInfo? GetHttpTriggerMethod(FunctionContext context)
    {
        var entryPoint = context.FunctionDefinition.EntryPoint;
        var split = entryPoint.LastIndexOf('.');
        var method = Assembly
            .LoadFrom(context.FunctionDefinition.PathToAssembly)
            .GetType(entryPoint[..split])
            ?.GetMethod(entryPoint[(split + 1)..]);

        return method?.GetParameters().Any(parameter => parameter.GetCustomAttribute<HttpTriggerAttribute>() != null) == true
            ? method
            : null;
    }
}
