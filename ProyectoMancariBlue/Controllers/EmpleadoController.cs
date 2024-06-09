﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoMancariBlue.Controllers
{
    using BCrypt.Net;

    public class EmpleadoController : Controller
    {
        private readonly DBContext _context;

        public EmpleadoController(DBContext context)
        {
            _context = context;
        }

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            var dBContext =  _context.Empleados.Include(e => e.Departamento).Include(e => e.RoleEmpleado);
            return View(await dBContext.ToListAsync());
        }

        // GET: Empleado/Detalles/111111111
        public async Task<IActionResult> Detalles(string? cedEmpleado)
        {
            Console.WriteLine(cedEmpleado);
            var empleado = await _context.Empleados
                .Include(e => e.Departamento)
                .Include(e => e.RoleEmpleado)
                .FirstOrDefaultAsync(m => m.CedEmpleado == cedEmpleado);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult CrearEmpleado()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "Name");
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEmpleado([Bind("EmpleadoId,Password,CedEmpleado,Nombre,Apellido,Nacionalidad,Email,fechaNacimiento,Provincia,Canton,Distrito,fechaIngreso,Salario,Habilitado,DepartamentoId,RoleEmpleadoId")] Empleado empleado )
        {
            try
            {

                empleado.Password = BCrypt.HashPassword(empleado.Password);


                var new_empleado = await _context.Empleados.AddAsync(empleado);
                await _context.SaveChangesAsync();
            
                if (new_empleado == null) { return NotFound(); }


            } catch {

                ModelState.AddModelError("CedEmpleado", "Cédula ya en uso por otro empleado");
            }


            return View(empleado);
        }

        // GET: Empleado/Edit/5
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

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmpleadoId,CedEmpleado,Nombre,Apellido,Nacionalidad,Email,Password,FechaNacimiento,Provincia,Canton,Distrito,FechaIngreso,Salario,Habilitado,DepartamentoId,RoleEmpleadoId")] Empleado empleado)
        {
            if (id != empleado.CedEmpleado)
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
                    if (!EmpleadoExists(empleado.CedEmpleado))
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

        // GET: Empleado/Delete/5
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

        // POST: Empleado/Delete/5
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

        private bool EmpleadoExists(string cedula)
        {
          return (_context.Empleados?.Any(e => e.CedEmpleado == cedula)).GetValueOrDefault();
        }
    }
}
