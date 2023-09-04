using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using RentApp.Collection;
using RentApp.Repository;

namespace RentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IPeopleRepository _ipeopleRepository;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(IPeopleRepository ipeopleRepository, ILogger<PeopleController> logger)
    {
        _ipeopleRepository = ipeopleRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Get all people request received.");

        var people = await _ipeopleRepository.GetAllAsync();
        _logger.LogInformation($"Returned{people.Count} people");
        return Ok(people);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        _logger.LogInformation($"Get {id} person people request received.");
        var people = await _ipeopleRepository.GetAsync(id);
        if (people == null)
        {
            _logger.LogInformation($"{id} Not found ");
            return NotFound();
        }

        _logger.LogInformation($"Return  {id}  person  request received.");
        return Ok(people);
    }

    [HttpPost]
    public async Task<IActionResult> Post(People newPeople)
    {
        await _ipeopleRepository.CreateAsync(newPeople);
        _logger.LogInformation("Post  new person !");
        return CreatedAtAction(nameof(Get), new { id = newPeople.Id }, newPeople);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(string id, People updatedPerson)
    {
        _logger.LogInformation($"Put  {id}  person  request received.");
        var person = await _ipeopleRepository.GetAsync(id);
        if (person is null)
        {
            _logger.LogInformation($"{id} Not Found ");
            return NotFound();
        }

        updatedPerson.Id = person.Id;

        await _ipeopleRepository.UpdateAsync(id, updatedPerson);
        _logger.LogInformation($"Update info about {id}");
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var people = await _ipeopleRepository.GetAsync(id);
        if (people == null) return NotFound();

        await _ipeopleRepository.RemoveAsync(id);
        return NoContent();
    }
}