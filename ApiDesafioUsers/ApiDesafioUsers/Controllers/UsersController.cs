using ApiDesafioUsers.Models;
using ApiDesafioUsers.Models.Helpers;
using ApiDesafioUsers.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace ApiDesafioUsers.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private string _cacheKey = "usuarios";
    public UsersController(IMemoryCache cache)
    {
        _cache = cache;
    }

    [DisableRequestSizeLimit]
    [HttpPost("users")]
    public async Task<IActionResult> PostUsersAsync([FromForm] FileUploadRequest upload)
    {
        if (upload == null || upload.File is null || upload.File.Length == 0)
            return BadRequest("O arquivo é obrigatório.");

        var result = new StringBuilder();
        using (var reader = new StreamReader(upload.File.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
                result.AppendLine(await reader.ReadLineAsync());
        }

        var users = JsonConvert.DeserializeObject<List<User>>(result.ToString());

        if (users is null)
            return BadRequest("O arquivo é obrigatório.");

        _cache.Set(_cacheKey, users);

        return Ok(new ResponsePostUsers("Arquivo recebido com sucesso", users.Count));
    }

    [HttpGet("superusers")]
    public IActionResult GetSuperUsers()
    {
        DateTime init = DateTime.Now;
        if (!_cache.TryGetValue(_cacheKey, out List<User>? users))
            return BadRequest("Não foi possível encontrar os usuários");

        var superUsers = users!.Where(e => e.Score >= 900 && e.Ativo).Select(e => new UserModel()
        {
            Id = e.Id,
            Nome = e.Nome,
            Ativo = e.Ativo,
            Idade = e.Idade,
            Pais = e.Pais,
            Score = e.Score,
        });

        DateTime end = DateTime.Now;

        var obj = new ResponseSuperUsers()
        {
            Timestamp = init,
            ExecutionTimems = (end - init).TotalMilliseconds,
            Data = [.. superUsers],
        };

        return Ok(obj);
    }

    /// Agrupa os superusuários por país e retorna os 5 com mais usuários.
    [HttpGet("top-countries")]
    public IActionResult GetTopCountries()
    {
        DateTime init = DateTime.Now;
        if (!_cache.TryGetValue(_cacheKey, out List<User>? users))
            return BadRequest("Não foi possível encontrar os usuários");

        var superUsers = users!.Where(e => e.Score >= 900 && e.Ativo);

        var agroupedCountries = superUsers.GroupBy(e => e.Pais).Select(g => new Country()
        {
            CountryName = g.Key,
            Total = g.Count(),
        }).OrderByDescending(x => x.Total).Take(5);

        DateTime end = DateTime.Now;

        return Ok(new ResponseTopCountries()
        {
            Timestamp = init,
            ExecutionTimems = (end - init).TotalMilliseconds,
            Countries = [.. agroupedCountries],
        });
    }

    /// Retorna estatísticas por equipe com base nos membros, projetos e liderança.
    [HttpGet("team-insights")]
    public IActionResult GetTeamInsights()
    {
        DateTime init = DateTime.Now;
        if (!_cache.TryGetValue(_cacheKey, out List<User>? users))
            return BadRequest("Não foi possível encontrar os usuários");

        var agroupedTeams = users!.GroupBy(e => e.Equipe.Nome).Select(g => new Equipe()
        {
            Team = g.Key,
            TotalMembers = g.Count(),
            Leaders = g.Count(e => e.Equipe.Lider),
            CompletedProjects = g.Count(e => e.Equipe.Projetos.Any(p => p.Concluido)),
            ActivePercentage = g.Average(e => e.Equipe.PorcentagemProjetos) * 100.0,
        });

        DateTime end = DateTime.Now;

        return Ok(new ResponseTeam()
        {
            Timestamp = init,
            ExecutionTimems = (end - init).TotalMilliseconds,
            Teams = [.. agroupedTeams],
        });
    }

    /// Retorna o número total de logins por data.
    [HttpGet("active-users-per-day")]
    public IActionResult GetActiveUsersPerDay()
    {
        DateTime init = DateTime.Now;
        if (!_cache.TryGetValue(_cacheKey, out List<User>? users))
            return BadRequest("Não foi possível encontrar os usuários");

        var logs = users!.Select(e => e.Logs.Select(f => f.Data));
        var logDias = (from list in logs
                       from item in list
                       select item).GroupBy(g => g).Select(s => new LogDia()
                       {
                           Date = s.Key,
                           Total = users!.Where(e => e.Logs.Any(l => l.Data == s.Key)).Count()
                       }
            );

        DateTime end = DateTime.Now;

        return Ok(new ResponseDate()
        {
            Timestamp = init,
            ExecutionTimems = (end - init).TotalMilliseconds,
            Logins = [.. logDias],
        });
    }

    /// Executa testes automáticos nos endpoints da própria API e retorna um relatório de pontuação.
    [HttpGet("evaluation")]
    public IActionResult Getevaluation()
    {
        var tests = new List<EndPointTest>();

        var responseUsers = GetSuperUsers();
        var testusers = new EndPointTest() { Name = "/superusuarios" };
        if (responseUsers != null && responseUsers is OkObjectResult)
        {
            var responseSuperUsers = (responseUsers! as OkObjectResult)!.Value as ResponseSuperUsers;
            testusers.Status = 200;
            testusers.Timems = responseSuperUsers!.ExecutionTimems;
            testusers.ValidResponse = true;

        }
        tests.Add(testusers);


        var countries = GetTopCountries();
        var testCountries = new EndPointTest() { Name = "/top-countries" };
        if (countries != null && countries is OkObjectResult)
        {
            var responseCountries = (countries! as OkObjectResult)!.Value as ResponseTopCountries;
            testCountries.Status = 200;
            testCountries.Timems = responseCountries!.ExecutionTimems;
            testCountries.ValidResponse = true;

        }
        tests.Add(testCountries);



        var teamInsights = GetTeamInsights();
        var testteamInsights = new EndPointTest() { Name = "/team-insights" };
        if (teamInsights != null && teamInsights is OkObjectResult)
        {
            var responseTeamInsigths = (teamInsights! as OkObjectResult)!.Value as ResponseTeam;
            testteamInsights.Status = 200;
            testteamInsights.Timems = responseTeamInsigths!.ExecutionTimems;
            testteamInsights.ValidResponse = true;

        }
        tests.Add(testteamInsights);
        

        var logDia = GetActiveUsersPerDay();
        var testLogDia = new EndPointTest() { Name = "/usuarios-ativos-por-dia" };
        if (logDia != null && logDia is OkObjectResult)
        {
            var responseLogDia = (logDia! as OkObjectResult)!.Value as ResponseDate;
            testLogDia.Status = 200;
            testLogDia.Timems = responseLogDia!.ExecutionTimems;
            testLogDia.ValidResponse = true;

        }
        tests.Add(testLogDia);

        return Ok(new ResponseEvaluation()
        {
            TestedEndpoints = tests
        });
    }

}
