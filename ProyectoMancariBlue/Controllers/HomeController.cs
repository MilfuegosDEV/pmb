using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ProyectoMancariBlue.Controllers
{
    public class LoginForm
    {
        public string Email { get; set; } = string.Empty.ToString();
        public string Password { get; set; } = string.Empty.ToString();
    }


    public class LoginModel
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class HomeController : Controller
    {
        private readonly DBContext _context;

        public HomeController(DBContext context)
        {
            _context = context;
        }


        static private string CreateJWT(LoginModel user )
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("396B5DD9-CC75-411C-9311-5B6E1F391B89"));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<LoginModel?> AuthenticateAsync(LoginForm login)
        {
            var user = await _context.Empleados
                                     .Where(e => e.Email == login.Email)
                                     .Select(e => new
                                     {
                                         e.Email,
                                         e.Password, 
                                         e.Nombre
                                     })
                                     .FirstOrDefaultAsync();


            if (user != null && user.Password != null &&  BCrypt.Net.BCrypt.Verify(login.Password, user.Password) == true)
            {
                return new LoginModel{ Email = user.Email, Name = user.Nombre};
            }

            return null;
        }

        [HttpPost("api/home/post")]
        public async Task<IActionResult> Authenticate([FromBody] LoginForm login)
        {
            var user = await AuthenticateAsync(login);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = CreateJWT(user);
            return Ok(new { token });
        }

        [HttpGet("api/home/index")]
        [Authorize]
        public IActionResult Index()
        {
            var empleadosCount = _context.Empleados.Count();
            return Ok(new { empleadosCount });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAut(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Empleados.FirstOrDefaultAsync(e => e.Email == Email);

                if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.Password))
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos.");
            return View("Login");
        }

        public IActionResult RestablecerContrasena()
        {
            TempData["AlertMessage"] = "Se restableció su contraseña, verifique el correo";
            TempData["AlertType"] = "success";
            return RedirectToAction("Login");
        }

        public IActionResult CambiarContrasena()
        {
            return View();
        }

        public IActionResult CambiarContrasenaS()
        {
            TempData["AlertMessage"] = "Se cambió la contraseña correctamente";
            TempData["AlertType"] = "success";
            return RedirectToAction("Login");
        }

        public IActionResult CerrarSesion()
        {
            TempData["AlertMessage"] = "Se cerró correctamente la sesión";
            TempData["AlertType"] = "success";
            return RedirectToAction("Login");
        }
    }
}
