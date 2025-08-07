using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiNotas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    // Ruta protegida
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var email = User.FindFirstValue(ClaimTypes.Name);
        var rol = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new
        {
            message = "Acceso autorizado",
            email,
            rol
        });
    }
}
