using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoMancariBlue.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Cryptography;
using System.Security.Cryptography;

namespace ProyectoMancariBlue.Controllers
{
    public class EMPLEADOTEST : Controller
    {
        private readonly DBContext _context;

        public EMPLEADOTEST(DBContext context)
        {
            _context = context;
        }

        // GET: EMPLEADOTEST
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Empleados.Include(e => e.Departamento).Include(e => e.RoleEmpleado);
            return View(await dBContext.ToListAsync());
        }

        // GET: EMPLEADOTEST/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Departamento)
                .Include(e => e.RoleEmpleado)
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: EMPLEADOTEST/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoName"] = new SelectList(_context.Departamentos, "DepartamentoId", "Name");
            ViewData["RoleName"] = new SelectList(_context.RolEmpleados, "RolEmpleadoId", "Name");
            return View();
        }

        // POST: EMPLEADOTEST/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoId,CedEmpleado,Nombre,Apellido,Nacionalidad,Email,Password,FechaNacimiento,Provincia,Canton,Distrito,FechaIngreso,Salario,Habilitado,DepartamentoId,RoleEmpleadoId")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
               
            
            rng.GetBytes(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: ,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));


            ViewData["DepartamentoName"] = new SelectList(_context.Departamentos, "DepartamentoId", "Name", empleado.Departamento);
            ViewData["RoleName"] = new SelectList(_context.RolEmpleados, "RolEmpleadoId", "Name", empleado.RoleEmpleado);
            return View(empleado);
        }

        // GET: EMPLEADOTEST/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "DepartamentoId", empleado.DepartamentoId);
            ViewData["RoleEmpleadoId"] = new SelectList(_context.RolEmpleados, "RolEmpleadoId", "RolEmpleadoId", empleado.RoleEmpleadoId);
            return View(empleado);
        }

        // POST: EMPLEADOTEST/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,CedEmpleado,Nombre,Apellido,Nacionalidad,Email,Password,FechaNacimiento,Provincia,Canton,Distrito,FechaIngreso,Salario,Habilitado,DepartamentoId,RoleEmpleadoId")] Empleado empleado)
        {
            if (id != empleado.EmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.EmpleadoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "DepartamentoId", empleado.DepartamentoId);
            ViewData["RoleEmpleadoId"] = new SelectList(_context.RolEmpleados, "RolEmpleadoId", "RolEmpleadoId", empleado.RoleEmpleadoId);
            return View(empleado);
        }

        // GET: EMPLEADOTEST/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Departamento)
                .Include(e => e.RoleEmpleado)
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: EMPLEADOTEST/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'DBContext.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return (_context.Empleados?.Any(e => e.EmpleadoId == id)).GetValueOrDefault();
        }
    }
}
