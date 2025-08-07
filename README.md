# 📝 ApiNotas - .NET 9

API RESTful para gestionar notas personales. Incluye autenticación de usuarios mediante JWT, rutas protegidas, persistencia con SQLite y operaciones completas de CRUD. Proyecto ideal para incluir en tu portafolio como desarrollador backend .NET.

---

## 🚀 Tecnologías usadas

- [.NET 9](https://dotnet.microsoft.com/)
- Entity Framework Core
- SQLite
- JWT (JSON Web Token)
- BCrypt.Net
- Insomnia o Postman (para pruebas)

---

## 📦 Instalación

```bash
# 1. Clona el repositorio
git clone https://github.com/tuusuario/ApiNotas.git
cd ApiNotas

# 2. Restaura los paquetes
dotnet restore

# 3. Aplica las migraciones y crea la base de datos
dotnet ef migrations add Init
dotnet ef database update

# 4. Ejecuta el proyecto
dotnet run
```

> 💡 Asegúrate de tener instalada la herramienta dotnet-ef:
```bash
dotnet tool install --global dotnet-ef
```

---

## ⚙️ Configuración

Edita tu archivo `appsettings.json`:

```json
"Jwt": {
  "Key": "TuSuperClaveSecretaCon32CaracteresOmas123!",
  "Issuer": "ApiNotas",
  "Audience": "ApiNotasUsers"
}
```

---

## 🔐 Autenticación JWT

### Registro

```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "usuario@ejemplo.com",
  "password": "123456"
}
```

#### ✅ Respuesta
```json
{
  "message": "Usuario registrado",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### Login

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "usuario@ejemplo.com",
  "password": "123456"
}
```

#### ✅ Respuesta
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

> Usa este token como Bearer Token en tus peticiones protegidas.

---

## 🛡️ Rutas protegidas

Cualquier endpoint de `/api/notas` requiere autenticación:

```http
Authorization: Bearer TU_TOKEN
```

---

## 📒 Endpoints del CRUD de Notas

| Método | Ruta                  | Descripción                   |
|--------|-----------------------|-------------------------------|
| GET    | /api/notas            | Obtener todas las notas       |
| GET    | /api/notas/{id}       | Obtener nota por ID           |
| POST   | /api/notas            | Crear nueva nota              |
| PUT    | /api/notas/{id}       | Actualizar nota existente     |
| DELETE | /api/notas/{id}       | Eliminar nota                 |
| GET    | /api/notas/buscar?q=  | Buscar notas por título       |

---

## 🗂 Modelo de datos

```csharp
public class Nota
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Contenido { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}
```

---

## 🧠 Lecciones demostradas

✅ CRUD completo con Entity Framework Core  
✅ JWT y validación con `[Authorize]`  
✅ Hasheo de contraseñas con BCrypt  
✅ SQLite para almacenamiento local  
✅ Buenas prácticas REST (status codes, DTOs, seguridad)

---

## 📌 Mejoras futuras

- Relación Usuario - Notas (cada nota pertenece a un usuario)
- Paginación en listado de notas
- Filtros avanzados (por fecha, contenido, etc.)

---

## 📚 Autor

**Tu Nombre** - [@tuusuario](https://github.com/tuusuario)