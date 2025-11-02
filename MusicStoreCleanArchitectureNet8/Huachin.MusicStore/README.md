# ?? Huachin Music Store - Clean Architecture .NET 8

> **Sistema de gestión de tienda de música implementado con Clean Architecture, Domain-Driven Design (DDD) y las mejores prácticas de desarrollo en .NET 8**

## ?? Tabla de Contenido

- [??? Arquitectura General](#?-arquitectura-general)
- [?? Tecnologías Utilizadas](#-tecnologías-utilizadas)
- [?? Estructura del Proyecto](#-estructura-del-proyecto)
- [?? Documentación por Capas](#-documentación-por-capas)
- [?? Patrones y Principios Aplicados](#-patrones-y-principios-aplicados)
- [?? Configuración y Ejecución](#-configuración-y-ejecución)

## ??? Arquitectura General

Este proyecto implementa **Clean Architecture** (Arquitectura Limpia) dividida en cuatro capas principales, siguiendo los principios SOLID y Domain-Driven Design:

```
???????????????????????????????????????
?           Presentation              ? ? Interfaz de Usuario
???????????????????????????????????????
?           Application               ? ? Casos de Uso y Servicios
???????????????????????????????????????
?           Infrastructure            ? ? Implementaciones Técnicas
???????????????????????????????????????
?             Domain                  ? ? Lógica de Negocio Central
???????????????????????????????????????
```

### Principios de Dependencia
- Las capas externas dependen de las internas
- El dominio es independiente de cualquier framework
- Las abstracciones no dependen de detalles

## ?? Tecnologías Utilizadas

- **.NET 8** - Framework principal
- **C# 12** - Lenguaje de programación
- **Entity Framework Core** - ORM para acceso a datos
- **Clean Architecture** - Patrón arquitectónico
- **Domain-Driven Design (DDD)** - Enfoque de diseño
- **CQRS** - Command Query Responsibility Segregation
- **Repository Pattern** - Patrón de repositorio

## ?? Estructura del Proyecto

```
Huachin.MusicStore/
??? src/
?   ??? Huachin.MusicStore.Domain/          # ??? Capa de Dominio
?   ?   ??? Entities/
?   ?       ??? MusicStore/
?   ??? Huachin.MusicStore.Application/     # ?? Capa de Aplicación
?   ??? Huachin.MusicStore.Infrastructure/  # ??? Capa de Infraestructura
?   ??? Huachin.MusicStore.Presentation/    # ??? Capa de Presentación
??? docs/                                   # ?? Documentación adicional
??? README.md                              # ?? Este archivo
```

## ?? Documentación por Capas

### ??? [Capa de Dominio](docs/DOMAIN-README.md)
**La capa más importante del sistema - Contiene la lógica de negocio pura**

- ? **Entidades** con Factory Methods
- ? **Rich Domain Model** 
- ? **Encapsulación** total
- ? **Nullable Reference Types**
- ? **Invariant Protection**

[Ver documentación completa ?](docs/DOMAIN-README.md)

### ?? [Capa de Aplicación](docs/APPLICATION-README.md)
**Orquesta los casos de uso del sistema**

- ?? **CQRS Pattern**
- ?? **Command/Query Handlers**
- ?? **Application Services**
- ?? **DTOs y Mappers**

[Ver documentación completa ?](docs/APPLICATION-README.md)

### ??? [Capa de Infraestructura](docs/INFRASTRUCTURE-README.md)
**Implementaciones técnicas y acceso a datos**

- ?? **Entity Framework Core**
- ?? **Repository Pattern**
- ?? **Unit of Work**
- ?? **Configuraciones de EF**

[Ver documentación completa ?](docs/INFRASTRUCTURE-README.md)

### ??? [Capa de Presentación](docs/PRESENTATION-README.md)
**Interfaz de usuario y controllers**

- ?? **API Controllers**
- ?? **Dependency Injection**
- ?? **Middleware**
- ?? **API Versioning**

[Ver documentación completa ?](docs/PRESENTATION-README.md)

## ?? Patrones y Principios Aplicados

### ??? Domain-Driven Design (DDD)
- **Entidades** con identidad única
- **Value Objects** para conceptos sin identidad
- **Aggregate Roots** para consistencia
- **Domain Events** para comunicación

### ?? Patrones de Diseño
- **Factory Method** - Creación controlada de entidades
- **Repository** - Abstracción de acceso a datos
- **Unit of Work** - Gestión de transacciones
- **CQRS** - Separación de comandos y consultas

### ?? Principios SOLID
- **S**ingle Responsibility - Una razón para cambiar
- **O**pen/Closed - Abierto para extensión, cerrado para modificación
- **L**iskov Substitution - Objetos sustituibles por instancias de subtipos
- **I**nterface Segregation - Interfaces específicas y cohesivas
- **D**ependency Inversion - Dependencias hacia abstracciones

### ?? Clean Code
- **Nombres descriptivos** y claros
- **Funciones pequeñas** y específicas
- **Comentarios** cuando añaden valor
- **Manejo de errores** consistente

## ?? Configuración y Ejecución

### Prerrequisitos
- .NET 8 SDK
- SQL Server (LocalDB o instancia completa)
- Visual Studio 2022 o VS Code

### Instalación
```bash
# Clonar el repositorio
git clone [URL_DEL_REPOSITORIO]

# Restaurar paquetes
dotnet restore

# Aplicar migraciones
dotnet ef database update

# Ejecutar el proyecto
dotnet run
```

---

## ?? Contacto y Contribución

Este proyecto está desarrollado siguiendo las mejores prácticas de la industria. Para contribuciones o consultas, por favor revisa la documentación específica de cada capa.

**Desarrollado con ?? usando Clean Architecture y .NET 8**