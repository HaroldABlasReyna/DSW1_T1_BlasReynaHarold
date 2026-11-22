using System.Data;
using DSW1_T1_BlasReynaHarold.Models;
using Microsoft.Data.SqlClient;

namespace DSW1_T1_BlasReynaHarold.Repositories;

public class LibroRepository : ILibroRepository
{
    private readonly IConfiguration _configuration;

    public LibroRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Libro> RecuperarDatos(string storeProcedure, Dictionary<string, dynamic> parametros)
    {
        List<Libro> listaLibros = new List<Libro>();

        using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("BD")))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(storeProcedure, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, dynamic> param in parametros)
            {
                sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
            }

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    listaLibros.Add(new Libro
                    {
                        Id = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Id")),
                        NombreLibro = sqlDataReader.GetString(sqlDataReader.GetOrdinal("NombreLibro")),
                        Autor = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Autor")),
                        NombreEditorial = sqlDataReader.GetString(sqlDataReader.GetOrdinal("NombreEditorial")),
                        ISDN = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ISDN"))
                    });
                }
            }
        }

        return listaLibros;
    }

    public bool EnviarDatos(string storeProcedure, Dictionary<string, dynamic> parametros)
    {
        using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("BD")))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(storeProcedure, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, dynamic> param in parametros)
            {
                sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
            }

            return sqlCommand.ExecuteNonQuery() > 0;
        }
    }
}
