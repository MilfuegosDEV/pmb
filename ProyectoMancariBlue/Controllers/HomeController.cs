using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProyectoMancariBlue.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _context;

        public HomeController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.EmpleadosCount = _context.Empleados.Count();
            return View();
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
                IQueryable<Empleado> empleadosQuery = _context.Empleados.AsQueryable();
                var pwd = await empleadosQuery.Where(e => e.Email == Email).Select(e => e.Password).FirstOrDefaultAsync();

                if (pwd != null && BCrypt.Net.BCrypt.Verify(Password, pwd))
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos.");
            return View("Login");
        }


        public IActionResult RestablecerContrasena()
        {
            TempData["AlertMessage"] = "Se restablecio su contraseña, verifique el corrreo";
            TempData["AlertType"] = "success";
            return RedirectToAction("Login");
        }

        public IActionResult CambiarContrasena()
        {
      
            return View();
        }

        public IActionResult CambiarContrasenaS()
        {

            TempData["AlertMessage"] = "Se cambio la contraseña correctamente";
            TempData["AlertType"] = "success";
            return RedirectToAction("Login");
        }

        public IActionResult CerrarSesion()
        {
            TempData["AlertMessage"] = "Se cerró correctamente la sesión";
            TempData["AlertType"] = "success";
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
