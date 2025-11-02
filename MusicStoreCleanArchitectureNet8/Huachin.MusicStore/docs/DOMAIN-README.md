# 🏗️ Capa de Dominio - Huachin Music Store

> **La capa más importante del sistema - Contiene la lógica de negocio pura y las reglas de dominio**

## 📋 Tabla de Contenido

- [🎯 Propósito y Responsabilidades](#-propósito-y-responsabilidades)
- [🏛️ Arquitectura de la Capa](#-arquitectura-de-la-capa)
- [📁 Estructura de Archivos](#-estructura-de-archivos)
- [🎨 Patrones de Diseño Implementados](#-patrones-de-diseño-implementados)
- [💡 Técnicas y Buenas Prácticas](#-técnicas-y-buenas-prácticas)
- [📊 Entidades del Dominio](#-entidades-del-dominio)
- [🤔 Decisiones de Diseño](#-decisiones-de-diseño)
- [💻 Ejemplos de Código](#-ejemplos-de-código)

## 🎯 Propósito y Responsabilidades

La **Capa de Dominio** es el corazón del sistema y contiene:

- 🏢 **Entidades de negocio** con comportamiento rico
- 📏 **Reglas de negocio** y validaciones
- 🛡️ **Invariantes del dominio** 
- 💼 **Lógica empresarial** pura
- ❌ **NO** contiene dependencias externas
- ❌ **NO** conoce sobre frameworks o infraestructura

### Principios Fundamentales
- **Independencia**: No depende de ninguna capa externa
- **Riqueza**: Entidades con comportamiento, no solo datos
- **Inmutabilidad**: Estado protegido y controlado
- **Consistencia**: Invariantes siempre válidas

## 🏛️ Arquitectura de la Capa

```
Domain/
├── Entities/
│   └── MusicStore/
│   │   ├── BaseEntity.cs      # 🏗️ Entidad base
│   │   ├── Concert.cs         # 🎵 Entidad Concert
│   │   ├── Customer.cs        # 👤 Entidad Customer
│   │   ├── Genre.cs           # 🎭 Entidad Genre
│   │   └── Sale.cs           # 💰 Entidad Sale
├── ValueObjects/              # 💎 Objetos de valor (futuro)
├── DomainEvents/             # 📢 Eventos de dominio (futuro)
├── Exceptions/               # ⚠️ Excepciones de dominio (futuro)
└── Interfaces/               # 📋 Contratos del dominio (futuro)
```

## 📁 Estructura de Archivos

### Entidades Implementadas

| Archivo | Entidad | Responsabilidad |
|---------|---------|-----------------|
| `BaseEntity.cs` | Entidad Base | Propiedades comunes y comportamiento base |
| `Concert.cs` | Concierto | Gestión de eventos musicales |
| `Customer.cs` | Cliente | Información de usuarios |
| `Genre.cs` | Género Musical | Categorización de música |
| `Sale.cs` | Venta | Transacciones comerciales |

## 🎨 Patrones de Diseño Implementados

### 1. 🏭 **Factory Method Pattern**

**¿Qué es?**
Patrón que proporciona una interfaz para crear objetos sin especificar la clase exacta a crear.

**Implementación:**
```csharp
// 🎯 Método estático para creación controlada
public static Genre Create(string name)
{
    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Genre name cannot be null or empty.", nameof(name));
    
    return new Genre(name);
}

// 🔒 Constructor privado para forzar uso del factory
private Genre(string name)
{
    Name = name;
}
```

**Beneficios:**
- 🎛️ **Control total** sobre la creación
- ✅ **Validaciones** centralizadas
- 📦 **Encapsulación** del proceso de construcción
- 🔄 **Consistencia** en las reglas de negocio

### 2. 💰 **Rich Domain Model**

**¿Qué es?**
Las entidades contienen tanto datos como comportamiento, no son solo contenedores de datos.

**Implementación:**
```csharp
public class Customer : BaseEntity
{
    // 🔐 Datos protegidos
    public string Email { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;

    // 🎭 Comportamiento en la entidad
    public static Customer Create(string email, string fullName)
    {
        // Lógica de negocio aquí
        return new Customer(email, fullName);
    }
}
```

**Beneficios:**
- 🤝 **Cohesión** alta entre datos y comportamiento
- 🎯 **Lógica centralizada** en el lugar correcto
- 🛡️ **Protección** del estado interno
- 💬 **Expresividad** del modelo de negocio

### 3. 📦 **Encapsulation (Encapsulación)**

**¿Qué es?**
Ocultación del estado interno y exposición controlada a través de métodos.

**Implementación:**
```csharp
public class Concert : BaseEntity
{
    // 🔐 Propiedades con setter privado
    public string Title { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    
    // 📚 Collections protegidas
    private readonly List<Sale> _sales = new();
    public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();
    
    // 🔓 Constructor público solo para ORM
    public Concert() { }
    
    // 🔒 Constructor privado para lógica de negocio
    private Concert(/* parámetros */) { /* inicialización */ }
}
```

**Beneficios:**
- 🛡️ **Estado protegido** contra modificaciones indebidas
- 🎛️ **Acceso controlado** a las propiedades
- 🔄 **Invariantes** siempre mantenidas
- ✅ **Integridad** de los datos garantizada

### 4. 🛡️ **Invariant Protection**

**¿Qué es?**
Garantía de que las reglas de negocio siempre se cumplan en las entidades.

**Implementación:**
```csharp
public static Concert Create(/* parámetros */)
{
    // ✅ Validaciones de invariantes
    if (string.IsNullOrWhiteSpace(title))
        throw new ArgumentException("Concert title cannot be null or empty.", nameof(title));
    
    if (unitPrice < 0)
        throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");
    
    if (ticketsQuantity < 0)
        throw new ArgumentOutOfRangeException(nameof(ticketsQuantity), "Tickets quantity cannot be negative.");
    
    return new Concert(/* parámetros validados */);
}
```

**Beneficios:**
- 🔄 **Consistencia** garantizada
- ❌ **Estados inválidos** imposibles
- 🎯 **Validaciones** centralizadas
- 💪 **Robustez** del sistema

## 💡 Técnicas y Buenas Prácticas

### 1. 🚫 **Nullable Reference Types**

**Implementación:**
```csharp
// 📝 Para datos de negocio obligatorios
public string Title { get; private set; } = string.Empty;

// ❓ Para datos opcionales
public string? ImageUrl { get; private set; }

// 🔗 Para navigation properties (EF Core las inicializará)
public Genre Genre { get; private set; } = null!;
```

**Beneficios:**
- 🛡️ **Prevención** de NullReferenceException
- 📋 **Claridad** en el contrato de la API
- 🔍 **Detección temprana** de problemas
- 💡 **Mejor IntelliSense** y debugging

### 2. 🏗️ **Constructor Separation**

**Patrón Implementado:**
```csharp
// 🔓 Constructor público para Entity Framework
public Concert() { }

// 🔒 Constructor privado para lógica de negocio
private Concert(/* parámetros */) { /* inicialización controlada */ }

// 🏭 Factory method para uso del dominio
public static Concert Create(/* parámetros */) { /* validaciones + creación */ }
```

**Beneficios:**
- 🤝 **Compatibilidad** con ORMs
- 🎛️ **Control** sobre la creación
- 📋 **Separación** de responsabilidades
- 💡 **Claridad** en el uso

### 3. 🔒 **Collection Protection**

**Implementación:**
```csharp
// 🔐 Campo privado mutable
private readonly List<Sale> _sales = new();

// 📖 Propiedad pública inmutable
public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();
```

**Beneficios:**
- 🛡️ **Protección** contra modificaciones externas
- 🎛️ **Control** sobre las operaciones de colección
- ✅ **Integridad** de las relaciones
- 📦 **Encapsulación** completa

### 4. 🏗️ **BaseEntity Pattern**

**Implementación:**
```csharp
public class BaseEntity
{
    public Guid Id { get; protected set; }
    public bool IsActive { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}
```

**Beneficios:**
- ♻️ **Reutilización** de propiedades comunes
- 🔄 **Gestión automática** de identificadores
- 📊 **Auditoría** básica incorporada
- 🔄 **Consistencia** en todas las entidades

## 📊 Entidades del Dominio

### 🎵 Concert (Concierto)
**Responsabilidad:** Gestionar eventos musicales y sus propiedades

**Propiedades Clave:**
- `Title`, `Description`, `Place` - Información del evento
- `UnitPrice`, `TicketsQuantity` - Aspectos comerciales
- `DateEvent` - Programación temporal
- `Finalized` - Estado del evento

**Relaciones:**
- Pertenece a un `Genre`
- Puede tener múltiples `Sales`

### 👤 Customer (Cliente)
**Responsabilidad:** Representar usuarios del sistema

**Propiedades Clave:**
- `Email` - Identificación única
- `FullName` - Información personal

**Relaciones:**
- Puede realizar múltiples `Sales`

### 🎭 Genre (Género Musical)
**Responsabilidad:** Categorizar tipos de música

**Propiedades Clave:**
- `Name` - Nombre del género

**Relaciones:**
- Puede tener múltiples `Concerts`

### 💰 Sale (Venta)
**Responsabilidad:** Registrar transacciones comerciales

**Propiedades Clave:**
- `SaleDate`, `OperationNumber` - Información transaccional
- `Total`, `Quantity` - Detalles comerciales

**Relaciones:**
- Pertenece a un `Customer`
- Está asociada a un `Concert`

## 🤔 Decisiones de Diseño

### ¿Por qué Factory Methods en lugar de constructores públicos?

1. **Control de validaciones** - Todas las reglas en un lugar
2. **Flexibilidad futura** - Fácil extensión sin romper el API
3. **Claridad semántica** - `Concert.Create()` es más expresivo
4. **Compatibilidad con ORMs** - Constructor público separado

### ¿Por qué setters privados?

1. **Inmutabilidad controlada** - Solo cambios internos
2. **Protección de invariantes** - Estado siempre consistente
3. **Encapsulación real** - No hay "setters públicos accidentales"
4. **Mejor mantenibilidad** - Cambios controlados

### ¿Por qué Guid como Id?

1. **Distribución** - Funciona en sistemas distribuidos
2. **Unicidad garantizada** - Sin colisiones
3. **Independencia de BD** - No requiere sequences
4. **Seguridad** - No secuencial, difícil de adivinar

## 💻 Ejemplos de Código

### Creación de una Entidad
```csharp
// ✅ Forma CORRECTA - usando factory method
var concert = Concert.Create(
    idGenre: 1,
    title: "Rock Concert 2024",
    description: "Amazing rock event",
    place: "Madison Square Garden",
    unitPrice: 99.99m,
    dateEvent: DateTime.Now.AddDays(30),
    imageUrl: "https://example.com/image.jpg",
    ticketsQuantity: 1000,
    finalized: false
);

// ❌ Forma INCORRECTA - constructor directo
// var concert = new Concert(); // No valida, estado inconsistente
```

### Manejo de Collections
```csharp
// 📖 Acceso de solo lectura
var sales = concert.Sales; // IReadOnlyCollection<Sale>

// 🚫 No se puede modificar directamente
// concert.Sales.Add(newSale); // Compilador ERROR

// 🎛️ La entidad controla las modificaciones internas
// (Método AddSale podría añadirse en el futuro)
```

### Validaciones en Factory Methods
```csharp
public static Genre Create(string name)
{
    // ✅ Validación exhaustiva antes de crear
    if (string.IsNullOrWhiteSpace(name))
    {
        throw new ArgumentException("Genre name cannot be null or empty.", nameof(name));
    }
    
    // Validaciones adicionales futuras:
    // - Longitud máxima
    // - Caracteres permitidos
    // - Unicidad (si requerida)
    
    return new Genre(name);
}
```

---

## 🚀 Próximos Pasos

### Mejoras Futuras Planificadas:

1. **Value Objects** - Para conceptos como Email, Money, etc.
2. **Domain Events** - Para comunicación entre agregados
3. **Specifications** - Para consultas complejas
4. **Domain Services** - Para lógica que no pertenece a entidades

### Extensiones Recomendadas:

```csharp
// Ejemplo futuro: Value Object para Email
public record Email
{
    public string Value { get; }
    
    private Email(string value) => Value = value;
    
    public static Email Create(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid email format.");
        return new Email(value);
    }
    
    private static bool IsValid(string email) => /* validación regex */;
}
```

---

**[🏠 Volver al README Principal](../README.md)**

---

**Desarrollado siguiendo Domain-Driven Design y Clean Architecture principles** 🏗️