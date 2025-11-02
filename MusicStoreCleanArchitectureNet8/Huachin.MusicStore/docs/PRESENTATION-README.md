# ??? Capa de Presentación - Huachin Music Store

> **Interfaz de usuario y API - La puerta de entrada al sistema para usuarios externos**

## ?? Tabla de Contenido

- [?? Propósito y Responsabilidades](#-propósito-y-responsabilidades)
- [??? Arquitectura de la Capa](#?-arquitectura-de-la-capa)
- [?? Estructura Planificada](#-estructura-planificada)
- [?? Patrones de Diseño a Implementar](#-patrones-de-diseño-a-implementar)
- [?? Técnicas y Buenas Prácticas](#-técnicas-y-buenas-prácticas)
- [?? Decisiones de Diseño](#-decisiones-de-diseño)
- [?? Ejemplos de Implementación](#-ejemplos-de-implementación)

## ?? Propósito y Responsabilidades

La **Capa de Presentación** es la interfaz con el mundo exterior y contiene:

- ? **API Controllers** (RESTful API)
- ? **Middleware** personalizado
- ? **Filtros** de acción y autorización
- ? **Validación** de entrada (Data Annotations)
- ? **Serialización** JSON
- ? **Configuración** de servicios
- ? **Swagger/OpenAPI** documentación
- ? **NO** contiene lógica de negocio
- ? **NO** accede directamente a datos

### Principios Fundamentales
- **Delgada**: Solo coordinación, no lógica
- **Stateless**: Sin estado entre requests
- **RESTful**: Seguir convenciones REST
- **Segura**: Autenticación y autorización

## ??? Arquitectura de la Capa

```
Presentation/
??? Controllers/              # ?? Controladores API
?   ??? v1/                  # Versionado de API
?   ?   ??? ConcertsController.cs
?   ?   ??? CustomersController.cs
?   ?   ??? GenresController.cs
?   ?   ??? SalesController.cs
?   ??? Base/                # Controladores base
??? Middleware/              # ?? Middleware personalizado
?   ??? ExceptionHandlingMiddleware.cs
?   ??? PerformanceMiddleware.cs
?   ??? RequestLoggingMiddleware.cs
??? Filters/                 # ?? Filtros de acción
?   ??? ValidationFilter.cs
?   ??? AuthorizationFilter.cs
?   ??? CacheFilter.cs
??? Extensions/              # ?? Extensiones de configuración
?   ??? ServiceCollectionExtensions.cs
?   ??? ApplicationBuilderExtensions.cs
?   ??? SwaggerExtensions.cs
??? Models/                  # ?? View Models y DTOs
?   ??? Requests/           # Request models
?   ??? Responses/          # Response models
?   ??? Common/             # Modelos comunes
??? Validators/              # ? Validadores de entrada
??? Configuration/           # ?? Configuraciones
    ??? ApiVersioning.cs
    ??? SwaggerConfig.cs
    ??? CorsConfig.cs
```

## ?? Estructura Planificada

### Componentes a Implementar

| Componente | Descripción | Estado |
|------------|-------------|--------|
| **Controllers** | Endpoints de la API REST | ?? Pendiente |
| **Middleware** | Procesamiento de requests | ?? Pendiente |
| **Filters** | Filtros transversales | ?? Pendiente |
| **Validation** | Validación de entrada | ?? Pendiente |
| **Swagger** | Documentación de API | ?? Pendiente |
| **Versioning** | Versionado de API | ?? Pendiente |
| **CORS** | Configuración de CORS | ?? Pendiente |

## ?? Patrones de Diseño a Implementar

### 1. ?? **Controller Pattern (MVC)**

**¿Qué es?**
Controladores que manejan requests HTTP y coordinan con la capa de aplicación.

**Implementación Planificada:**
```csharp
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ConcertsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ConcertsController> _logger;

    public ConcertsController(IMediator mediator, ILogger<ConcertsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all concerts with pagination
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResponseDto<ConcertResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResponseDto<ConcertResponseDto>>> GetConcerts(
        [FromQuery] GetConcertsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get concert by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ConcertResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConcertResponseDto>> GetConcert(Guid id)
    {
        var query = new GetConcertByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create a new concert
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ConcertResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConcertResponseDto>> CreateConcert(
        [FromBody] CreateConcertRequestDto request)
    {
        var command = new CreateConcertCommand(
            request.GenreId,
            request.Title,
            request.Description,
            request.Place,
            request.UnitPrice,
            request.DateEvent,
            request.ImageUrl,
            request.TicketsQuantity
        );

        var result = await _mediator.Send(command);
        
        return CreatedAtAction(
            nameof(GetConcert), 
            new { id = result.Id }, 
            result);
    }
}
```

**Beneficios:**
- ?? **Responsabilidad única** por controller
- ?? **RESTful** design
- ?? **Testing** fácil
- ?? **Documentación** automática

### 2. ?? **Middleware Pattern**

**¿Qué es?**
Componentes que procesan requests y responses en el pipeline de ASP.NET Core.

**Implementación Planificada:**
```csharp
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            ValidationException => (StatusCodes.Status400BadRequest, exception.Message),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            ForbiddenException => (StatusCodes.Status403Forbidden, "Forbidden"),
            _ => (StatusCodes.Status500InternalServerError, "An error occurred while processing your request")
        };

        context.Response.StatusCode = statusCode;

        var response = new
        {
            error = new
            {
                message,
                statusCode,
                timestamp = DateTime.UtcNow
            }
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
```

**Beneficios:**
- ??? **Manejo centralizado** de errores
- ?? **Logging** consistente
- ?? **Respuestas** estandarizadas
- ?? **Reutilización** en toda la aplicación

### 3. ?? **Filter Pattern**

**¿Qué es?**
Filtros que se ejecutan antes o después de las acciones del controller.

**Implementación Planificada:**
```csharp
public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Validar ModelState
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var response = new ValidationProblemDetails(context.ModelState)
            {
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest,
                Detail = "One or more validation errors occurred"
            };

            context.Result = new BadRequestObjectResult(response);
            return;
        }

        // Continuar con la acción
        await next();
    }
}

public class PerformanceFilter : IAsyncActionFilter
{
    private readonly ILogger<PerformanceFilter> _logger;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        var executedContext = await next();

        stopwatch.Stop();

        var actionName = $"{context.Controller.GetType().Name}.{context.ActionDescriptor.DisplayName}";
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500) // Log slow requests
        {
            _logger.LogWarning("Slow request: {ActionName} took {ElapsedMilliseconds}ms", 
                actionName, elapsedMilliseconds);
        }
    }
}
```

**Beneficios:**
- ?? **Aspectos transversales** implementados
- ?? **Reutilización** entre controllers
- ?? **Monitoreo** automático
- ? **Validación** centralizada

### 4. ?? **API Documentation Pattern (Swagger)**

**¿Qué es?**
Documentación automática de la API usando OpenAPI/Swagger.

**Implementación Planificada:**
```csharp
public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Huachin Music Store API",
                Version = "v1",
                Description = "A clean architecture music store API built with .NET 8",
                Contact = new OpenApiContact
                {
                    Name = "Music Store Team",
                    Email = "support@musicstore.com"
                }
            });

            // Include XML comments
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            // JWT Bearer configuration
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Music Store API v1");
            options.RoutePrefix = string.Empty; // Swagger UI at root
            options.DisplayRequestDuration();
            options.EnableFilter();
            options.EnableDeepLinking();
        });

        return app;
    }
}
```

**Beneficios:**
- ?? **Documentación** automática
- ?? **Testing** interactivo
- ?? **Client generation** automático
- ?? **Contratos** claros

### 5. ?? **API Versioning Pattern**

**¿Qué es?**
Versionado de API para mantener compatibilidad hacia atrás.

**Implementación Planificada:**
```csharp
public static class ApiVersioningExtensions
{
    public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Version"),
                new QueryStringApiVersionReader("version")
            );
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}

// Uso en controllers
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ConcertsController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> GetConcertsV1() { /* implementación v1 */ }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult> GetConcertsV2() { /* implementación v2 */ }
}
```

**Beneficios:**
- ?? **Evolución** de API sin romper clientes
- ?? **Múltiples versiones** simultáneas
- ?? **Flexibilidad** en el versionado
- ??? **Compatibilidad** hacia atrás

## ?? Técnicas y Buenas Prácticas

### 1. ?? **Request/Response Models**

**Implementación Planificada:**
```csharp
// Request Models
public record CreateConcertRequestDto(
    [Required] int GenreId,
    [Required, StringLength(200)] string Title,
    [Required, StringLength(1000)] string Description,
    [Required, StringLength(300)] string Place,
    [Range(0.01, double.MaxValue)] decimal UnitPrice,
    [Required] DateTime DateEvent,
    [Url] string? ImageUrl,
    [Range(1, int.MaxValue)] int TicketsQuantity
);

public record UpdateConcertRequestDto(
    [Required, StringLength(200)] string Title,
    [Required, StringLength(1000)] string Description,
    [Required, StringLength(300)] string Place,
    [Range(0.01, double.MaxValue)] decimal UnitPrice,
    [Required] DateTime DateEvent,
    [Url] string? ImageUrl,
    [Range(1, int.MaxValue)] int TicketsQuantity,
    bool Finalized
);

// Response Models
public record ConcertResponseDto(
    Guid Id,
    string Title,
    string Description,
    string Place,
    decimal UnitPrice,
    DateTime DateEvent,
    string? ImageUrl,
    int TicketsQuantity,
    bool Finalized,
    GenreResponseDto Genre,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

// Common Models
public record PagedRequestDto(
    [Range(1, int.MaxValue)] int Page = 1,
    [Range(1, 100)] int PageSize = 10,
    string? Search = null,
    string? SortBy = null,
    bool SortDescending = false
);

public record ApiErrorResponse(
    string Message,
    int StatusCode,
    string? Detail = null,
    DateTime Timestamp = default
);
```

**Beneficios:**
- ?? **Contratos** claros
- ? **Validación** automática
- ??? **Protección** del dominio
- ?? **Documentación** automática

### 2. ?? **Security Configuration**

**Implementación Planificada:**
```csharp
public static class SecurityExtensions
{
    public static IServiceCollection AddSecurityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // JWT Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

        // Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => 
                policy.RequireRole("Admin"));
            options.AddPolicy("CustomerOnly", policy => 
                policy.RequireRole("Customer"));
        });

        return services;
    }
}

// Uso en controllers
[Authorize(Policy = "AdminOnly")]
[HttpPost]
public async Task<ActionResult> CreateConcert([FromBody] CreateConcertRequestDto request)
{
    // Solo administradores pueden crear conciertos
}

[Authorize(Policy = "CustomerOnly")]
[HttpPost("purchase")]
public async Task<ActionResult> PurchaseTickets([FromBody] PurchaseTicketsRequestDto request)
{
    // Solo customers pueden comprar tickets
}
```

**Beneficios:**
- ?? **Autenticación** robusta
- ??? **Autorización** granular
- ?? **Políticas** reutilizables
- ?? **Configuración** centralizada

### 3. ?? **CORS Configuration**

**Implementación Planificada:**
```csharp
public static class CorsExtensions
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", builder =>
            {
                var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
                
                builder
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            options.AddPolicy("DevelopmentPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}
```

**Beneficios:**
- ?? **Cross-origin** support
- ?? **Seguridad** configurada
- ?? **Políticas** por ambiente
- ?? **Flexibilidad** de configuración

### 4. ?? **Health Checks**

**Implementación Planificada:**
```csharp
public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddDbContext<MusicStoreDbContext>(options =>
            {
                options.Tags.Add("database");
            })
            .AddCheck("self", () => HealthCheckResult.Healthy("API is running"))
            .AddUrlGroup(new Uri("https://www.google.com"), "google", tags: new[] { "external" });

        return services;
    }
}

// Endpoint personalizado
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    [HttpGet]
    public async Task<ActionResult> GetHealth()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                duration = entry.Value.Duration,
                description = entry.Value.Description
            }),
            totalDuration = report.TotalDuration
        };

        return report.Status == HealthStatus.Healthy ? Ok(response) : StatusCode(503, response);
    }
}
```

**Beneficios:**
- ?? **Monitoreo** automático
- ?? **Diagnóstico** de problemas
- ?? **Alertas** proactivas
- ?? **Información** detallada

## ?? Decisiones de Diseño

### ¿Por qué API REST sobre GraphQL?

1. **Simplicidad** - Más fácil de implementar y consumir
2. **Caching** - Mejor soporte en CDNs y proxies
3. **Estándares** - Ampliamente adoptado en la industria
4. **Tooling** - Mejor soporte de herramientas

### ¿Por qué Versionado en URL?

1. **Claridad** - Versión visible en la URL
2. **Caching** - Diferentes versiones en caché separados
3. **Simplicidad** - Fácil de entender y usar
4. **Compatibilidad** - Funciona con todos los clientes

### ¿Por qué Records para DTOs?

1. **Inmutabilidad** - DTOs no deberían cambiar
2. **Concisión** - Menos código boilerplate
3. **Value equality** - Comparación por valor
4. **Pattern matching** - Mejor para transformaciones

## ?? Ejemplos de Implementación

### Program.cs Completo
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<PerformanceFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddSecurityConfiguration(builder.Configuration);
builder.Services.AddHealthChecksConfiguration(builder.Configuration);

// Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
    app.UseCors("DevelopmentPolicy");
}
else
{
    app.UseCors("AllowSpecificOrigins");
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

await app.RunAsync();
```

### Controller Base
```csharp
[ApiController]
[Produces("application/json")]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<T> HandleResult<T>(T result)
    {
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    protected ActionResult<PagedResponseDto<T>> HandlePagedResult<T>(PagedResponseDto<T> result)
    {
        Response.Headers.Add("X-Total-Count", result.TotalCount.ToString());
        Response.Headers.Add("X-Page", result.Page.ToString());
        Response.Headers.Add("X-Page-Size", result.PageSize.ToString());

        return Ok(result);
    }
}
```

---

## ?? Próximos Pasos

### Implementación Inmediata:

1. **Crear estructura** de controllers y modelos
2. **Configurar Swagger** y documentación
3. **Implementar middleware** básico
4. **Configurar CORS** y seguridad

### Extensiones Futuras:

- **Rate limiting** para prevenir abuso
- **Response caching** para mejor performance
- **Real-time updates** con SignalR
- **File upload** para imágenes de conciertos

---

**[?? Volver al README Principal](../README.md)**

---

**Desarrollado siguiendo RESTful API best practices y Clean Architecture principles** ???