using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Data;

public class ListaZadanRepozytorium
{
    DbConfig db;
    public ListaZadanRepozytorium(IConfiguration csg)
    {
        db = new DbConfig();
        csg.GetSection("DB").Bind(db);
    }
    public List<Zadanie> GetTaskForUsers(Uzytkownik u)
    {
        string sql = "EXEC GetTaskForUsers '" + u.login + "', '" + u.haslo + "'";

        using (var connection = new SqlConnection(db.CS))
        {
            var res = connection.Query<Zadanie>(sql).ToList();

            return res;
        }
    }

    public void AddTaskForUser(Zadanie z, Uzytkownik u)
    {
        //string sql = "EXEC  '" + u.login + "', " + u.haslo;

        using (var connection = new SqlConnection(db.CS))
        {
            var affectedRows = connection.Execute("AddTaskForUser",
            new
            {
                login = u.login,
                haslo = u.haslo,
                trescZadania = z.trescZadania,
                terminWykonania = z.terminWykonania
            },
            commandType: CommandType.StoredProcedure);
        }
    }

    public Uzytkownik GetUserById(int idUzytkownika)
    {
        string sql = "SELECT Id, login, haslo FROM Uzytkownicy WHERE Id=" + idUzytkownika;

        using (var connection = new SqlConnection(db.CS))
        {
            var res = connection.Query<Uzytkownik>(sql).FirstOrDefault();
            return res;
        }
    }
    public void OznaczJakoWykonane(int idZadania)
    {
        string sql = "UPDATE Zadania SET wykonano=1 WHERE Id=" + idZadania;

        using (var connection = new SqlConnection(db.CS))
        {
            var res = connection.Execute(sql);
            
        }
    }
}
public class DbConfig
{
    public string CS { get; set; }
}
