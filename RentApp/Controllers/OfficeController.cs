using Microsoft.AspNetCore.Mvc;
using RentApp.Collection;
using RentApp.Repository;

namespace RentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class OfficeController : ControllerBase
{
    private readonly IRealEstateRepository _realEstateRepository;
    private readonly ILogger<OfficeController> _logger;

    public OfficeController(IRealEstateRepository realEstateRepository, ILogger<OfficeController> logger)
    {
        _realEstateRepository = realEstateRepository;
        _logger = logger;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetOffices()
    {
        _logger.LogInformation("Get all offices from collection  request received.");
        var offices = await _realEstateRepository.GeyAllOfficesAsync();
        _logger.LogInformation($"Returned{offices.Count} offices ");
        return Ok(offices);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdOffice(string id)
    {
        _logger.LogInformation($"Get {id} office request received.");
        var people = await _realEstateRepository.GetByIdOffice(id);
        if (people == null)
        {
            _logger.LogInformation($"{id} Not found ");
            return NotFound();
        }

        _logger.LogInformation($"Return  {id}  office  request received.");
        return Ok(people);
    }

    [HttpPost]
    public async Task<IActionResult> PostOffice(Office newOffice)
    {
        await _realEstateRepository.CreateOfficeAsync(newOffice);
        _logger.LogInformation("Post  new office !");
        return CreatedAtAction(nameof(GetByIdOffice), new { id = newOffice.Id }, newOffice);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutOffice(string id, Office updatedOffice)
    {
        _logger.LogInformation($"Put  {id}  office  request received.");
        var office = await _realEstateRepository.GetByIdOffice(id);
        if (office is null)
        {
            _logger.LogInformation($"{id} Not Found ");
            return NotFound();
        }

        updatedOffice.Id = office.Id;

        await _realEstateRepository.UpdateOfficeAsync(id, updatedOffice);
        _logger.LogInformation($"Update info about {id}");
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOffice(string id)
    {
        var offices = await _realEstateRepository.GetByIdOffice(id);
        if (offices == null) return NotFound();

        await _realEstateRepository.RemoveOfficeAsync(id);
        return NoContent();
    }
}