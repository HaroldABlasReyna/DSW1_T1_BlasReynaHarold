using DSW1_T1_BlasReynaHarold.Models;
using DSW1_T1_BlasReynaHarold.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DSW1_T1_BlasReynaHarold.Controllers;

public class LibroController : Controller
{
    private readonly ILibroService _libroService;

    public LibroController(ILibroService libroService)
    {
        _libroService = libroService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Mensaje = TempData["Mensaje"];
        ViewBag.TipoMensaje = TempData["TipoMensaje"];

        try
        {
            return View(_libroService.ListarLibros());
        }
        catch (SqlException)
        {
            ViewBag.Mensaje = "No se pudo conectar con la base de datos. Verifique la cadena de conexión.";
            ViewBag.TipoMensaje = "danger";
            return View(Enumerable.Empty<Libro>());
        }
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new Libro());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Libro libro)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Mensaje = "Complete correctamente todos los campos.";
            ViewBag.TipoMensaje = "warning";
            return View(libro);
        }

        try
        {
            if (_libroService.RegistrarLibro(libro))
            {
                TempData["Mensaje"] = $"El libro \"{libro.NombreLibro}\" se registró correctamente.";
                TempData["TipoMensaje"] = "success";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensaje = "No se pudo registrar el libro.";
        }
        catch (SqlException)
        {
            ViewBag.Mensaje = "Ocurrió un error en la base de datos al registrar el libro.";
        }

        ViewBag.TipoMensaje = "danger";
        return View(libro);
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        Libro? libro = _libroService.ObtenerLibroPorId(id);

        if (libro == null)
        {
            TempData["Mensaje"] = "El libro que intenta editar no existe.";
            TempData["TipoMensaje"] = "warning";
            return RedirectToAction(nameof(Index));
        }

        return View(libro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Libro libro)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Mensaje = "Complete correctamente todos los campos.";
            ViewBag.TipoMensaje = "warning";
            return View(libro);
        }

        try
        {
            if (_libroService.ActualizarLibro(libro))
            {
                TempData["Mensaje"] = $"El libro \"{libro.NombreLibro}\" se actualizó correctamente.";
                TempData["TipoMensaje"] = "success";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensaje = "No se pudo actualizar: el libro no existe.";
        }
        catch (SqlException)
        {
            ViewBag.Mensaje = "Ocurrió un error en la base de datos al actualizar el libro.";
        }

        ViewBag.TipoMensaje = "danger";
        return View(libro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        try
        {
            if (_libroService.EliminarLibro(id))
            {
                TempData["Mensaje"] = "El libro se eliminó correctamente.";
                TempData["TipoMensaje"] = "success";
            }
            else
            {
                TempData["Mensaje"] = "No se pudo eliminar: el libro no existe.";
                TempData["TipoMensaje"] = "warning";
            }
        }
        catch (SqlException)
        {
            TempData["Mensaje"] = "No se pudo eliminar el libro porque tiene registros relacionados o hubo un error en la base de datos.";
            TempData["TipoMensaje"] = "danger";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult BuscarPorAutor(string? autor)
    {
        ViewBag.Autor = autor;

        if (string.IsNullOrWhiteSpace(autor))
        {
            ViewBag.Mensaje = "Ingrese el nombre (o parte del nombre) del autor.";
            ViewBag.TipoMensaje = "info";
            return View(Enumerable.Empty<Libro>());
        }

        IEnumerable<Libro> libros = _libroService.ListarLibrosPorAutor(autor);

        if (!libros.Any())
        {
            ViewBag.Mensaje = $"No se encontraron libros para el autor \"{autor}\".";
            ViewBag.TipoMensaje = "warning";
        }

        return View(libros);
    }
}