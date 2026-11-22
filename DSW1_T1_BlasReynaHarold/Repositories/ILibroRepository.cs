using DSW1_T1_BlasReynaHarold.Models;

namespace DSW1_T1_BlasReynaHarold.Repositories;

public interface ILibroRepository
{
    IEnumerable<Libro> RecuperarDatos(string storeProcedure, Dictionary<string, dynamic> parametros);

    bool EnviarDatos(string storeProcedure, Dictionary<string, dynamic> parametros);
}
