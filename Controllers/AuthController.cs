using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiNotas.Data;
using ApiNotas.Auth;
using ApiNotas.Services;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;



namespace ApiNotas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly string _key = "clave-secreta-super-segura-1234567890";

    private readonly JwtService _jwtService;

    public AuthController(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthRequest request)
    {
        var existe = await _context.Usuarios.AnyAsync(u => u.Email == request.Email);
        if (existe)
        {
            return BadRequest(new { message = "El correo ya está registrado" });
        }

        var usuario = new Usuario
        {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(usuario);

        return Ok(new
        {
            message = "Usuario registrado",
            token
        });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
            return Unauthorized("Credenciales incorrectas (usuario no encontrado)");

        var passwordOk = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

        if (!passwordOk)
            return Unauthorized("Credenciales incorrectas (contraseña inválida)");

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }

}
