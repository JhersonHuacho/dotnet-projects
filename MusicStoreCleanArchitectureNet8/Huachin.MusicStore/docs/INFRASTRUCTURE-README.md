# ??? Capa de Infraestructura - Huachin Music Store

> **Implementaciones técnicas y acceso a datos - La capa que conecta con el mundo exterior**

## ?? Tabla de Contenido

- [?? Propósito y Responsabilidades](#-propósito-y-responsabilidades)
- [??? Arquitectura de la Capa](#?-arquitectura-de-la-capa)
- [?? Estructura Planificada](#-estructura-planificada)
- [?? Patrones de Diseño a Implementar](#-patrones-de-diseño-a-implementar)
- [?? Técnicas y Buenas Prácticas](#-técnicas-y-buenas-prácticas)
- [?? Decisiones de Diseño](#-decisiones-de-diseño)
- [?? Ejemplos de Implementación](#-ejemplos-de-implementación)

## ?? Propósito y Responsabilidades

La **Capa de Infraestructura** proporciona implementaciones concretas y contiene:

- ? **Acceso a datos** (Entity Framework Core)
- ? **Repositorios** implementaciones concretas
- ? **Configuraciones de EF** (Fluent API)
- ? **Servicios externos** (APIs, Email, etc.)
- ? **Implementación de interfaces** del dominio/aplicación
- ? **Migraciones** de base de datos
- ? **NO** contiene lógica de negocio
- ? **NO** expone detalles técnicos a capas superiores

### Principios Fundamentales
- **Implementación**: Concreta las abstracciones del dominio
- **Persistencia**: Maneja el almacenamiento de datos
- **Integración**: Conecta con servicios externos
- **Configuración**: Configura frameworks y herramientas

## ??? Arquitectura de la Capa

```
Infrastructure/
??? Data/                     # ??? Acceso a datos
?   ??? Context/             # DbContext y configuración
?   ??? Configurations/      # Configuraciones de entidades
?   ??? Repositories/        # Implementaciones de repositorios
?   ??? Migrations/          # Migraciones de EF Core
?   ??? Seeds/               # Datos iniciales
??? Services/                # ?? Servicios externos
?   ??? Email/              # Servicio de email
?   ??? Storage/            # Almacenamiento de archivos
?   ??? External/           # APIs externas
??? Identity/                # ?? Autenticación y autorización
?   ??? Models/
?   ??? Services/
?   ??? Configurations/
??? Caching/                 # ?? Caché (Redis)
??? Logging/                 # ?? Logging avanzado
??? BackgroundJobs/          # ? Trabajos en segundo plano
??? DependencyInjection/     # ?? Registro de servicios
```

## ?? Estructura Planificada

### Componentes a Implementar

| Componente | Descripción | Estado |
|------------|-------------|--------|
| **DbContext** | Contexto de Entity Framework | ?? Pendiente |
| **Configurations** | Configuración Fluent API | ?? Pendiente |
| **Repositories** | Implementaciones concretas | ?? Pendiente |
| **Unit of Work** | Gestión de transacciones | ?? Pendiente |
| **Migrations** | Scripts de base de datos | ?? Pendiente |
| **Services** | Servicios de infraestructura | ?? Pendiente |

## ?? Patrones de Diseño a Implementar

### 1. ?? **Repository Pattern**

**¿Qué es?**
Abstrae el acceso a datos proporcionando una interfaz similar a una colección en memoria.

**Implementación Planificada:**
```csharp
// Interface en Application o Domain
public interface IConcertRepository
{
    Task<Concert?> GetByIdAsync(Guid id);
    Task<IEnumerable<Concert>> GetAllAsync();
    Task<Concert> AddAsync(Concert concert);
    Task UpdateAsync(Concert concert);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Concert>> GetByGenreAsync(int genreId);
    Task<bool> ExistsAsync(Guid id);
}

// Implementación en Infrastructure
public class ConcertRepository : IConcertRepository
{
    private readonly MusicStoreDbContext _context;

    public ConcertRepository(MusicStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Concert?> GetByIdAsync(Guid id)
    {
        return await _context.Concerts
            .Include(c => c.Genre)
            .Include(c => c.Sales)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    // Más implementaciones...
}
```

**Beneficios:**
- ?? **Abstracción** del acceso a datos
- ?? **Testing** fácil con mocks
- ?? **Consistencia** en operaciones CRUD
- ??? **Encapsulación** de consultas complejas

### 2. ?? **Unit of Work Pattern**

**¿Qué es?**
Mantiene una lista de objetos afectados por una transacción y coordina la escritura de cambios.

**Implementación Planificada:**
```csharp
public interface IUnitOfWork : IDisposable
{
    IConcertRepository Concerts { get; }
    ICustomerRepository Customers { get; }
    IGenreRepository Genres { get; }
    ISaleRepository Sales { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly MusicStoreDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(MusicStoreDbContext context)
    {
        _context = context;
        Concerts = new ConcertRepository(_context);
        Customers = new CustomerRepository(_context);
        // Más repositorios...
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    // Más implementaciones...
}
```

**Beneficios:**
- ?? **Transacciones** coordinadas
- ?? **Consistencia** de datos
- ?? **Menos código** repetitivo
- ??? **Gestión automática** de conexiones

### 3. ??? **DbContext Pattern (Entity Framework)**

**¿Qué es?**
Contexto que representa una sesión con la base de datos para consultar y guardar entidades.

**Implementación Planificada:**
```csharp
public class MusicStoreDbContext : DbContext
{
    public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplicar todas las configuraciones
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicStoreDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuraciones adicionales si es necesario
        base.OnConfiguring(optionsBuilder);
    }
}
```

**Beneficios:**
- ??? **ORM completo** para .NET
- ?? **Change tracking** automático
- ?? **Migraciones** automáticas
- ?? **LINQ** para consultas

### 4. ?? **Configuration Pattern (Fluent API)**

**¿Qué es?**
Configuración de entidades usando Fluent API en lugar de atributos.

**Implementación Planificada:**
```csharp
public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
{
    public void Configure(EntityTypeBuilder<Concert> builder)
    {
        // Tabla
        builder.ToTable("Concerts");

        // Clave primaria
        builder.HasKey(c => c.Id);

        // Propiedades
        builder.Property(c => c.Id)
            .ValueGeneratedNever(); // Guid generado en dominio

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(c => c.Place)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(c => c.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.ImageUrl)
            .HasMaxLength(500);

        // Relaciones
        builder.HasOne(c => c.Genre)
            .WithMany(g => g.Concerts)
            .HasForeignKey(c => c.IdGenre)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Sales)
            .WithOne(s => s.Concert)
            .HasForeignKey(s => s.IdConcert)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(c => c.IdGenre);
        builder.HasIndex(c => c.DateEvent);
        builder.HasIndex(c => c.Finalized);
    }
}
```

**Beneficios:**
- ?? **Separación** de responsabilidades
- ?? **Configuración** explícita y clara
- ?? **Mantenimiento** más fácil
- ?? **Documentación** viva del schema

### 5. ?? **Seeding Pattern**

**¿Qué es?**
Inicialización de datos maestros y de prueba en la base de datos.

**Implementación Planificada:**
```csharp
public static class MusicStoreDbContextSeed
{
    public static async Task SeedAsync(MusicStoreDbContext context)
    {
        // Verificar si ya hay datos
        if (await context.Genres.AnyAsync())
            return;

        // Crear géneros iniciales
        var genres = new List<Genre>
        {
            Genre.Create("Rock"),
            Genre.Create("Pop"),
            Genre.Create("Jazz"),
            Genre.Create("Classical"),
            Genre.Create("Electronic")
        };

        await context.Genres.AddRangeAsync(genres);
        await context.SaveChangesAsync();

        // Crear conciertos de ejemplo
        var concerts = new List<Concert>
        {
            Concert.Create(
                genres[0].Id, // Rock
                "Rock Night 2024",
                "Amazing rock concert with top artists",
                "Madison Square Garden",
                99.99m,
                DateTime.Now.AddDays(30),
                "https://example.com/rock-concert.jpg",
                1000,
                false
            )
        };

        await context.Concerts.AddRangeAsync(concerts);
        await context.SaveChangesAsync();
    }
}
```

**Beneficios:**
- ?? **Datos iniciales** consistentes
- ?? **Testing** con datos conocidos
- ?? **Demo** con contenido real
- ?? **Desarrollo** más rápido

## ?? Técnicas y Buenas Prácticas

### 1. ?? **Dependency Injection Configuration**

**Implementación Planificada:**
```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<MusicStoreDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(MusicStoreDbContext).Assembly.FullName)
            ));

        // Repositories
        services.AddScoped<IConcertRepository, ConcertRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }
}
```

**Beneficios:**
- ?? **Registro** centralizado
- ?? **Configuración** clara
- ?? **Extensibilidad** fácil
- ?? **Testing** simplificado

### 2. ?? **Query Optimization**

**Implementación Planificada:**
```csharp
public class ConcertRepository : IConcertRepository
{
    // Consulta optimizada con Include explícito
    public async Task<Concert?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Concerts
            .Include(c => c.Genre)
            .Include(c => c.Sales)
                .ThenInclude(s => s.Customer)
            .AsNoTracking() // Para consultas de solo lectura
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    // Consulta paginada optimizada
    public async Task<PagedResult<Concert>> GetPagedAsync(int page, int pageSize, string? filter = null)
    {
        var query = _context.Concerts
            .Include(c => c.Genre)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(c => 
                c.Title.Contains(filter) || 
                c.Description.Contains(filter) ||
                c.Place.Contains(filter));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(c => c.DateEvent)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PagedResult<Concert>(items, totalCount, page, pageSize);
    }
}
```

**Beneficios:**
- ?? **Performance** optimizada
- ?? **Consultas** eficientes
- ?? **Memoria** controlada
- ?? **N+1** problem evitado

### 3. ?? **Transaction Management**

**Implementación Planificada:**
```csharp
public class ConcertApplicationService : IConcertApplicationService
{
    private readonly IUnitOfWork _unitOfWork;

    public async Task<ConcertResponseDto> CreateConcertWithSalesAsync(CreateConcertWithSalesRequestDto request)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            // 1. Crear concierto
            var concert = Concert.Create(/* parámetros */);
            await _unitOfWork.Concerts.AddAsync(concert);
            await _unitOfWork.SaveChangesAsync();

            // 2. Crear ventas iniciales si las hay
            if (request.InitialSales?.Any() == true)
            {
                foreach (var saleRequest in request.InitialSales)
                {
                    var sale = Sale.Create(/* parámetros */);
                    await _unitOfWork.Sales.AddAsync(sale);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            return _mapper.Map<ConcertResponseDto>(concert);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
```

**Beneficios:**
- ?? **Consistencia** de datos
- ??? **Rollback** automático
- ?? **Operaciones** atómicas
- ?? **Gestión** centralizada

### 4. ?? **Caching Strategy**

**Implementación Planificada:**
```csharp
public class CachedGenreRepository : IGenreRepository
{
    private readonly IGenreRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        const string cacheKey = "all-genres";
        
        if (_cache.TryGetValue(cacheKey, out IEnumerable<Genre>? cachedGenres))
            return cachedGenres!;

        var genres = await _repository.GetAllAsync();
        
        _cache.Set(cacheKey, genres, _cacheDuration);
        
        return genres;
    }

    public async Task<Genre> AddAsync(Genre genre)
    {
        var result = await _repository.AddAsync(genre);
        
        // Invalidar caché
        _cache.Remove("all-genres");
        
        return result;
    }
}
```

**Beneficios:**
- ?? **Performance** mejorada
- ?? **Reducción** de consultas DB
- ?? **Gestión** automática de caché
- ?? **Invalidación** inteligente

## ?? Decisiones de Diseño

### ¿Por qué Entity Framework Core?

1. **ORM maduro** - Ampliamente adoptado en .NET
2. **Code First** - Control desde el código
3. **Migraciones** - Versionado de base de datos
4. **Performance** - Optimizaciones continuas

### ¿Por qué Repository + Unit of Work?

1. **Testabilidad** - Fácil mockear para testing
2. **Abstracción** - Independencia del ORM
3. **Transacciones** - Gestión coordinada
4. **Consistencia** - Operaciones estandarizadas

### ¿Por qué Fluent API sobre Atributos?

1. **Separación** - Configuración fuera de entidades
2. **Flexibilidad** - Configuraciones complejas
3. **Mantenimiento** - Cambios sin tocar dominio
4. **Legibilidad** - Configuración explícita

## ?? Ejemplos de Implementación

### DbContext Completo
```csharp
public class MusicStoreDbContext : DbContext
{
    public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplicar configuraciones
        modelBuilder.ApplyConfiguration(new ConcertConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new SaleConfiguration());

        // Configuraciones globales
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Convención para DeleteBehavior
            foreach (var foreignKey in entityType.GetForeignKeys())
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Convención para propiedades string
            foreach (var property in entityType.GetProperties().Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                    property.SetMaxLength(256);
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Auditoría automática
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
```

### Configuración de DI
```csharp
// En Program.cs
builder.Services.AddInfrastructure(builder.Configuration);

// Seeding en Program.cs después de Build
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MusicStoreDbContext>();
    await context.Database.MigrateAsync();
    await MusicStoreDbContextSeed.SeedAsync(context);
}
```

---

## ?? Próximos Pasos

### Implementación Inmediata:

1. **Instalar paquetes NuGet**:
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.EntityFrameworkCore.Tools
   - Microsoft.EntityFrameworkCore.Design

2. **Crear DbContext y configuraciones**

3. **Implementar repositorios base**

4. **Configurar migraciones**

### Extensiones Futuras:

- **Redis** para caching distribuido
- **Elasticsearch** para búsquedas avanzadas
- **Message queues** para eventos
- **Health checks** para monitoreo

---

**[?? Volver al README Principal](../README.md)**

---

**Desarrollado siguiendo Repository Pattern y Clean Architecture principles** ???