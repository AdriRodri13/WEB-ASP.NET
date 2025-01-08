using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_ASP.NET.Data;
using WEB_ASP.NET.Models;

namespace WEB_ASP.NET.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ServiciosContext _context;

        public ServiciosController(ServiciosContext context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Servicios.ToListAsync());
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicios = await _context.Servicios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (servicios == null)
            {
                return NotFound();
            }

            return View(servicios);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titulo,Contenido,ImagenURL")] Servicios servicios, IFormFile ImagenURL)
        {
            if (ModelState.IsValid)
            {
                if (ImagenURL != null && ImagenURL.Length > 0)
                {
                    // Definir el nombre del archivo y la ruta de destino
                    var fileName = Path.GetFileName(ImagenURL.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                    // Guardar la imagen en la carpeta ~/img/
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagenURL.CopyToAsync(stream);
                    }

                    // Asignar el nombre del archivo al campo ImagenURL
                    servicios.ImagenURL = "/img/" + fileName;
                }

                // Guardar el objeto servicio en la base de datos
                _context.Add(servicios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(servicios);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicios = await _context.Servicios.FindAsync(id);
            if (servicios == null)
            {
                return NotFound();
            }
            return View(servicios);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titulo,Contenido,ImagenURL")] Servicios servicios)
        {
            if (id != servicios.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiciosExists(servicios.ID))
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
            return View(servicios);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicios = await _context.Servicios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (servicios == null)
            {
                return NotFound();
            }

            return View(servicios);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Buscar el servicio en la base de datos
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                // Eliminar la imagen del servidor si existe
                if (!string.IsNullOrEmpty(servicio.ImagenURL))
                {
                    // Obtener solo el nombre del archivo de la URL
                    var fileName = Path.GetFileName(servicio.ImagenURL);
                    // Construir la ruta completa del archivo en el servidor
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                    // Verificar si el archivo existe y eliminarlo
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath); // Eliminar la imagen
                    }
                }

                // Eliminar el servicio de la base de datos
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }

            // Redirigir a la lista de servicios
            return RedirectToAction(nameof(Index));
        }

        private bool ServiciosExists(int id)
        {
            return _context.Servicios.Any(e => e.ID == id);
        }
    }
}
