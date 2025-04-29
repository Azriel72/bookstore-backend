# BookStore.API - Backend RESTful API

## Descripción

Este es un proyecto de **API RESTful** desarrollado en **C# .NET 9.0**, utilizando **Clean Architecture**. La API actúa como un **proxy** entre el frontend y la **FakeRestAPI** externa para obtener información de libros y autores.

### Tecnologías

- **Backend**: C# .NET 9.0
- **Patrón arquitectónico**: Clean Architecture
- **Paquetes NuGet**:
  - `Microsoft.AspNetCore.OpenApi` (versión 9.0.4)
  - `Swashbuckle.AspNetCore` (versión 8.1.1)
  
### Estructura del Proyecto

Este proyecto sigue la arquitectura de **Clean Architecture**, con las siguientes capas:

- **Domain**: Contiene las entidades y las interfaces.
- **Application**: Contiene los DTOs y los servicios de la lógica de negocio.
- **Infrastructure**: Se encarga de la comunicación con la **FakeRestAPI** externa y las implementaciones de repositorios.
- **API**: Exposición de la API RESTful mediante controladores.

### Endpoints

#### Libros (Books)

- **GET /api/books**: Obtiene la lista de libros desde la FakeRestAPI.
- **GET /api/books/{id}**: Obtiene un solo libro por ID.
- **POST /api/books**: Agrega un nuevo libro.
- **PUT /api/books/{id}**: Actualiza un libro existente.
- **DELETE /api/books/{id}**: Elimina un libro.

#### Autores (Authors)

- **GET /api/authors**: Obtiene la lista de autores desde la FakeRestAPI.
- **GET /api/authors/{id}**: Obtiene un solo autor por ID.
- **POST /api/authors**: Agrega un nuevo autor.
- **PUT /api/authors/{id}**: Actualiza un autor existente.
- **DELETE /api/authors/{id}**: Elimina un autor.

### Instrucciones para Ejecutar el Proyecto

1. **Clonar el repositorio**:

   ```bash
   git clone https://github.com/tu-usuario/bookstore-backend.git
   cd bookstore-backend
