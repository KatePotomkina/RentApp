using Microsoft.AspNetCore.Mvc;
using RentApp.Collection;
using RentApp.Repository;

namespace RentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ApartmentController : Controller
{
    private readonly IRealEstateRepository _realEstateRepository;
    private readonly ILogger<ApartmentController> _logger;

    public ApartmentController(IRealEstateRepository realEstateRepository, ILogger<ApartmentController> logger)
    {
        _realEstateRepository = realEstateRepository ?? throw new ArgumentNullException(nameof(realEstateRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    // GET
    [HttpGet]
    public async Task<IActionResult> GetApartments()
    {
        _logger.LogInformation("Get all apartments from collection  request received.");
        var apartments = await _realEstateRepository.GetAllApartmentsAsync();
        _logger.LogInformation($"Returned{apartments.Count} apartments ");
        return Ok(apartments);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDetailsApartments(string id)
    {
        _logger.LogInformation($"Get {id} apartments request received.");
        var people = await _realEstateRepository.GetByIdApartaments(id);
        if (people == null)
        {
            _logger.LogInformation($"{id} Not found ");
            return NotFound();
        }

        _logger.LogInformation($"Return  {id}  person  request received.");
        return Ok(people);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Apartament newApartment)
    {
        await _realEstateRepository.CreateAsync(newApartment);
        _logger.LogInformation("Post  new person !");
        return CreatedAtAction(nameof(GetDetailsApartments), new { id = newApartment.Id }, newApartment);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(string id, Apartament updatedApartament)
    {
        _logger.LogInformation($"Put  {id}  apartmebnt  request received.");
        var person = await _realEstateRepository.GetByIdApartaments(id);
        if (person is null)
        {
            _logger.LogInformation($"{id} Not Found ");
            return NotFound();
        }

        updatedApartament.Id = person.Id;


        await _realEstateRepository.UpdateAsync(id, updatedApartament);
        _logger.LogInformation($"Update info about {id}");
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var people = await _realEstateRepository.GetByIdApartaments(id);
        if (people == null) return NotFound();

        await _realEstateRepository.RemoveAsync(id);
        return NoContent();
    }
}