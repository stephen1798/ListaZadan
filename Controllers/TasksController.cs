using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ListaZadanRepozytorium _repo;

    public TasksController(ListaZadanRepozytorium repo)
    {
        _repo = repo;
    }



    // GET api/values
    [HttpGet]
    public ActionResult<string> Get()
    {

        return "OK1";
    }

    // POBIERANIE ZADAN
    // GET api/values/5
    [HttpGet("{idUzytkownika}")]
    public ActionResult<IEnumerable<Zadanie>> Get(int idUzytkownika)
    {
        var user = _repo.GetUserById(idUzytkownika);
        var res = _repo.GetTaskForUsers(user);
        return res;
    }

    // POST api/values
    [HttpPost("{idZadania}")]
    public void Post(int idZadania)
    {
        _repo.OznaczJakoWykonane(idZadania);
    }

    // DODAWANIE ZADAN
    // PUT api/values/5
    [HttpPut("{idUzytkownika}")]
    public void Put(int idUzytkownika, [FromBody] Zadanie zadanie)
    {
        var user = _repo.GetUserById(idUzytkownika);
        _repo.AddTaskForUser(zadanie, user);
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
