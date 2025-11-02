# 🎯 Capa de Aplicación - Huachin Music Store

> **Orquesta los casos de uso del sistema y coordina entre el dominio y la infraestructura**

## 📋 Tabla de Contenido

- [🎯 Propósito y Responsabilidades](#-propósito-y-responsabilidades)
- [🏛️ Arquitectura de la Capa](#-arquitectura-de-la-capa)
- [📁 Estructura Planificada](#-estructura-planificada)
- [🎨 Patrones de Diseño a Implementar](#-patrones-de-diseño-a-implementar)
- [💡 Técnicas y Buenas Prácticas](#-técnicas-y-buenas-prácticas)
- [🤔 Decisiones de Diseño](#-decisiones-de-diseño)
- [💻 Ejemplos de Implementación](#-ejemplos-de-implementación)

## 🎯 Propósito y Responsabilidades

La **Capa de Aplicación** actúa como coordinadora y contiene:

- 📝 **Casos de uso** del sistema (Use Cases)
- 🔧 **Servicios de aplicación** (Application Services)
- 📊 **Comandos y consultas** (CQRS)
- 📦 **DTOs** (Data Transfer Objects)
- 🔄 **Mappers** y transformaciones
- ✅ **Validaciones de entrada**
- ❌ **NO** contiene lógica de negocio (está en el dominio)
- ❌ **NO** contiene detalles de infraestructura

### Principios Fundamentales
- **Orquestación**: Coordina entre dominio e infraestructura
- **Abstracción**: Define contratos para la infraestructura
- **Transformación**: Convierte datos entre capas
- **Validación**: Valida datos de entrada antes del dominio

## 🏛️ Arquitectura de la Capa

```
Application/
├── Commands/                 # ✏️ Comandos (escritura)
│   ├── Concerts/
│   ├── Customers/
│   ├── Genres/
│   └── Sales/
├── Queries/                  # 📖 Consultas (lectura)
│   ├── Concerts/
│   ├── Customers/
│   ├── Genres/
│   └── Sales/
├── DTOs/                     # 📦 Data Transfer Objects
│   ├── Requests/
│   ├── Responses/
│   └── Common/
├── Services/                 # 🔧 Servicios de aplicación
│   ├── Contracts/           # Interfaces
│   └── Implementations/
├── Mappers/                  # 🔄 Transformaciones de datos
├── Validators/               # ✅ Validaciones de entrada
├── Behaviors/                # 🔀 Comportamientos transversales
└── Exceptions/               # ⚠️ Excepciones de aplicación
```

## 📁 Estructura Planificada

### Componentes a Implementar

| Componente | Descripción | Estado |
|------------|-------------|--------|
| **Commands** | Operaciones de escritura (Create, Update, Delete) | ⏳ Pendiente |
| **Queries** | Operaciones de lectura (Get, List, Search) | ⏳ Pendiente |
| **DTOs** | Objetos para transferencia de datos | ⏳ Pendiente |
| **Handlers** | Procesadores de comandos y consultas | ⏳ Pendiente |
| **Services** | Servicios de aplicación | ⏳ Pendiente |
| **Mappers** | Conversión entre entidades y DTOs | ⏳ Pendiente |
| **Validators** | Validación de datos de entrada | ⏳ Pendiente |

## 🎨 Patrones de Diseño a Implementar

### 1. 📊 **CQRS (Command Query Responsibility Segregation)**

**¿Qué es?**
Separación entre operaciones de escritura (Commands) y lectura (Queries).

**Implementación Planificada:**
```csharp
// Commands para escritura
public record CreateConcertCommand(
    int GenreId,
    string Title,
    string Description,
    string Place,
    decimal UnitPrice,
    DateTime DateEvent,
    string? ImageUrl,
    int TicketsQuantity
);

// Queries para lectura
public record GetConcertByIdQuery(Guid Id);
public record GetConcertsListQuery(int Page, int PageSize, string? Filter);
```

**Beneficios:**
- 🎯 **Separación clara** de responsabilidades
- 📈 **Escalabilidad** independiente
- ⚡ **Optimización** específica por operación
- 💡 **Claridad** en el código

### 2. 🎭 **Command Handler Pattern**

**¿Qué es?**
Cada comando tiene un handler específico que lo procesa.

**Implementación Planificada:**
```csharp
public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, ConcertResponseDto>
{
    private readonly IConcertRepository _concertRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public async Task<ConcertResponseDto> Handle(CreateConcertCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar que el género existe
        // 2. Crear la entidad usando factory method
        // 3. Persistir en repositorio
        // 4. Mapear a DTO de respuesta
    }
}
```

**Beneficios:**
- 🎯 **Una responsabilidad** por handler
- 🧪 **Fácil testing** unitario
- 📋 **Código organizado** por funcionalidad
- ♻️ **Reutilización** de lógica común

### 3. 📦 **DTO (Data Transfer Object) Pattern**

**¿Qué es?**
Objetos diseñados específicamente para transferir datos entre capas.

**Implementación Planificada:**
```csharp
// Request DTOs
public record CreateConcertRequestDto(
    int GenreId,
    string Title,
    string Description,
    string Place,
    decimal UnitPrice,
    DateTime DateEvent,
    string? ImageUrl,
    int TicketsQuantity
);

// Response DTOs
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
    GenreResponseDto Genre
);
```

**Beneficios:**
- 🛡️ **Protección** del modelo de dominio
- 📋 **Contratos** claros de API
- 🔄 **Versionado** independiente
- 🔧 **Flexibilidad** en la serialización

### 4. 🔄 **Mapper Pattern**

**¿Qué es?**
Transformación automática entre entidades del dominio y DTOs.

**Implementación Planificada:**
```csharp
public class ConcertMapper : Profile
{
    public ConcertMapper()
    {
        CreateMap<Concert, ConcertResponseDto>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));
            
        CreateMap<CreateConcertRequestDto, CreateConcertCommand>();
    }
}
```

**Beneficios:**
- 🤖 **Automatización** de conversiones
- 🔄 **Consistencia** en las transformaciones
- 🛡️ **Menos errores** manuales
- 🔧 **Fácil mantenimiento**

### 5. ✅ **Validator Pattern**

**¿Qué es?**
Validación de datos de entrada antes de procesarlos.

**Implementación Planificada:**
```csharp
public class CreateConcertCommandValidator : AbstractValidator<CreateConcertCommand>
{
    public CreateConcertCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");
            
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero");
            
        RuleFor(x => x.DateEvent)
            .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future");
    }
}
```

**Beneficios:**
- ✅ **Validación** antes del dominio
- 💬 **Mensajes** claros de error
- ♻️ **Reutilización** de reglas
- 🛡️ **Protección** de la capa de dominio

## 💡 Técnicas y Buenas Prácticas

### 1. 🔧 **Application Services Pattern**

**Implementación Planificada:**
```csharp
public interface IConcertApplicationService
{
    Task<ConcertResponseDto> CreateConcertAsync(CreateConcertRequestDto request);
    Task<ConcertResponseDto> GetConcertByIdAsync(Guid id);
    Task<PagedResponseDto<ConcertResponseDto>> GetConcertsAsync(int page, int pageSize);
    Task<ConcertResponseDto> UpdateConcertAsync(Guid id, UpdateConcertRequestDto request);
    Task DeleteConcertAsync(Guid id);
}

public class ConcertApplicationService : IConcertApplicationService
{
    private readonly IMediator _mediator;
    
    public async Task<ConcertResponseDto> CreateConcertAsync(CreateConcertRequestDto request)
    {
        var command = _mapper.Map<CreateConcertCommand>(request);
        return await _mediator.Send(command);
    }
}
```

**Beneficios:**
- 🏗️ **Fachada** simple para la capa de presentación
- 📋 **Contratos** estables
- 🧪 **Fácil testing** de integración
- 📦 **Abstracción** de MediatR

### 2. 🎭 **Mediator Pattern (MediatR)**

**Implementación Planificada:**
```csharp
// En el controller
public class ConcertsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    [HttpPost]
    public async Task<ActionResult<ConcertResponseDto>> CreateConcert(CreateConcertRequestDto request)
    {
        var command = _mapper.Map<CreateConcertCommand>(request);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetConcert), new { id = result.Id }, result);
    }
}
```

**Beneficios:**
- 🔗 **Desacoplamiento** entre controllers y handlers
- 🔀 **Comportamientos** transversales (logging, validación)
- 🧹 **Código limpio** en controllers
- 🧪 **Fácil testing** unitario

### 3. 🔀 **Pipeline Behaviors**

**Implementación Planificada:**
```csharp
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // 1. Ejecutar validaciones
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellationToken)));
        
        // 2. Si hay errores, lanzar excepción
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
        if (failures.Count != 0)
            throw new ValidationException(failures);

        // 3. Continuar con el siguiente behavior/handler
        return await next();
    }
}
```

**Beneficios:**
- ♻️ **Reutilización** de comportamientos
- 🎯 **Separación** de responsabilidades
- 🧹 **Código limpio** en handlers
- ✅ **Validación** automática

### 4. 📄 **Paged Response Pattern**

**Implementación Planificada:**
```csharp
public record PagedResponseDto<T>(
    IEnumerable<T> Items,
    int TotalCount,
    int Page,
    int PageSize,
    int TotalPages
);

public record PagedRequestDto(
    int Page = 1,
    int PageSize = 10,
    string? Search = null,
    string? SortBy = null,
    bool SortDescending = false
);
```

**Beneficios:**
- 📄 **Paginación** consistente
- ⚡ **Performance** optimizada
- 📊 **Metadatos** útiles
- 🎨 **Fácil implementación** en frontend

## 🤔 Decisiones de Diseño

### ¿Por qué CQRS?

1. **Separación clara** - Diferentes optimizaciones para lectura y escritura
2. **Escalabilidad** - Escalar queries independientemente de commands
3. **Simplicidad** - Modelos específicos para cada operación
4. **Performance** - Consultas optimizadas sin afectar escritura

### ¿Por qué MediatR?

1. **Desacoplamiento** - Controllers no conocen handlers directamente
2. **Behaviors** - Logging, validación, caching transversales
3. **Testing** - Fácil mockar IMediator
4. **Organización** - Código bien estructurado

### ¿Por qué DTOs separados?

1. **Versionado** - Cambios en API sin afectar dominio
2. **Seguridad** - No exponer propiedades internas
3. **Performance** - Solo datos necesarios
4. **Flexibilidad** - Diferentes vistas de los mismos datos

## 💻 Ejemplos de Implementación

### Comando Completo
```csharp
// 1. Command
public record CreateConcertCommand(
    int GenreId,
    string Title,
    string Description,
    string Place,
    decimal UnitPrice,
    DateTime DateEvent,
    string? ImageUrl,
    int TicketsQuantity
) : IRequest<ConcertResponseDto>;

// 2. Validator
public class CreateConcertCommandValidator : AbstractValidator<CreateConcertCommand>
{
    public CreateConcertCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
        RuleFor(x => x.DateEvent).GreaterThan(DateTime.Now);
    }
}

// 3. Handler
public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, ConcertResponseDto>
{
    private readonly IConcertRepository _concertRepository;
    private readonly IMapper _mapper;

    public async Task<ConcertResponseDto> Handle(CreateConcertCommand request, CancellationToken cancellationToken)
    {
        // Usar factory method del dominio
        var concert = Concert.Create(
            request.GenreId,
            request.Title,
            request.Description,
            request.Place,
            request.UnitPrice,
            request.DateEvent,
            request.ImageUrl,
            request.TicketsQuantity,
            false
        );

        await _concertRepository.AddAsync(concert);
        
        return _mapper.Map<ConcertResponseDto>(concert);
    }
}
```

### Query Completa
```csharp
// 1. Query
public record GetConcertByIdQuery(Guid Id) : IRequest<ConcertResponseDto>;

// 2. Handler
public class GetConcertByIdQueryHandler : IRequestHandler<GetConcertByIdQuery, ConcertResponseDto>
{
    private readonly IConcertRepository _concertRepository;
    private readonly IMapper _mapper;

    public async Task<ConcertResponseDto> Handle(GetConcertByIdQuery request, CancellationToken cancellationToken)
    {
        var concert = await _concertRepository.GetByIdAsync(request.Id);
        
        if (concert == null)
            throw new NotFoundException(nameof(Concert), request.Id);
            
        return _mapper.Map<ConcertResponseDto>(concert);
    }
}
```

---

## 🚀 Próximos Pasos

### Implementación Inmediata:

1. **Instalar paquetes NuGet**:
   - MediatR
   - FluentValidation
   - AutoMapper

2. **Crear estructura base** de folders

3. **Implementar primer caso de uso** (CreateConcert)

4. **Configurar DI** en Program.cs

### Extensiones Futuras:

- **Caching** con Redis
- **Background Jobs** con Hangfire
- **Domain Events** handling
- **Audit** logging

---

**[🏠 Volver al README Principal](../README.md)**

---

**Desarrollado siguiendo CQRS y Clean Architecture principles** 🏗️