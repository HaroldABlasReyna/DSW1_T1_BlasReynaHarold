using DSW1_T1_BlasReynaHarold.Models;
using DSW1_T1_BlasReynaHarold.Repositories;

namespace DSW1_T1_BlasReynaHarold.Services;

public class LibroService : ILibroService
{
    private readonly ILibroRepository _libroRepository;

    public LibroService(ILibroRepository libroRepository)
    {
        _libroRepository = libroRepository;
    }

    public IEnumerable<Libro> ListarLibros()
    {
        return _libroRepository.RecuperarDatos("usp_ListarLibros", new Dictionary<string, dynamic>());
    }

    public Libro? ObtenerLibroPorId(int id)
    {
        Dictionary<string, dynamic> parametros = new Dictionary<string, dynamic>();
        parametros.Add("@Id", id);
        return _libroRepository.RecuperarDatos("usp_ObtenerLibroPorId", parametros).FirstOrDefault();
    }

    public bool RegistrarLibro(Libro libro)
    {
        Dictionary<string, dynamic> parametros = new Dictionary<string, dynamic>();
        parametros.Add("@NombreLibro", libro.NombreLibro.Trim());
        parametros.Add("@Autor", libro.Autor.Trim());
        parametros.Add("@NombreEditorial", libro.NombreEditorial.Trim());
        parametros.Add("@ISDN", libro.ISDN.Trim());
        return _libroRepository.EnviarDatos("usp_RegistrarLibro", parametros);
    }

    public bool ActualizarLibro(Libro libro)
    {
        Dictionary<string, dynamic> parametros = new Dictionary<string, dynamic>();
        parametros.Add("@Id", libro.Id);
        parametros.Add("@NombreLibro", libro.NombreLibro.Trim());
        parametros.Add("@Autor", libro.Autor.Trim());
        parametros.Add("@NombreEditorial", libro.NombreEditorial.Trim());
        parametros.Add("@ISDN", libro.ISDN.Trim());
        return _libroRepository.EnviarDatos("usp_ActualizarLibro", parametros);
    }

    public bool EliminarLibro(int id)
    {
        Dictionary<string, dynamic> parametros = new Dictionary<string, dynamic>();
        parametros.Add("@Id", id);
        return _libroRepository.EnviarDatos("usp_EliminarLibro", parametros);
    }

    public IEnumerable<Libro> ListarLibrosPorAutor(string autor)
    {
        Dictionary<string, dynamic> parametros = new Dictionary<string, dynamic>();
        parametros.Add("@Autor", autor.Trim());
        return _libroRepository.RecuperarDatos("usp_ListarLibrosPorAutor", parametros);
    }
}
