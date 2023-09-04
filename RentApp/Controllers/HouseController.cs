using Microsoft.AspNetCore.Mvc;
using RentApp.Collection;
using RentApp.Repository;

namespace RentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class HouseController : Controller
{
    private readonly IRealEstateRepository _realEstateRepository;
    private readonly ILogger<HouseController> _logger;

    public HouseController(IRealEstateRepository realEstateRepository, ILogger<HouseController> logger)
    {
        _realEstateRepository = realEstateRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetHouses()
    {
        _logger.LogInformation("Get all houses from collection  request received.");
        var houses = await _realEstateRepository.GeyAllHousesAsync();
        _logger.LogInformation($"Returned{houses.Count} houses ");
        return Ok(houses);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdHouses(string id)
    {
        _logger.LogInformation($"Get {id} house request received.");
        var people = await _realEstateRepository.GetByIdHouses(id);
        if (people == null)
        {
            _logger.LogInformation($"{id} Not found ");
            return NotFound();
        }

        _logger.LogInformation($"Return  {id}  house  request received.");
        return Ok(people);
    }

    [HttpPost]
    public async Task<IActionResult> PostHouse(House newHouse)
    {
        await _realEstateRepository.CreateHouseAsync(newHouse);
        _logger.LogInformation("Post  new house !");
        return CreatedAtAction(nameof(GetByIdHouses), new { id = newHouse.Id }, newHouse);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutHouse(string id, House updatedHouse)
    {
        _logger.LogInformation($"Put  {id}  house  request received.");
        var house = await _realEstateRepository.GetByIdHouses(id);
        if (house is null)
        {
            _logger.LogInformation($"{id} Not Found ");
            return NotFound();
        }

        updatedHouse.Id = house.Id;

        await _realEstateRepository.UpdateHouseAsync(id, updatedHouse);
        _logger.LogInformation($"Update info about {id}");
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteHouse(string id)
    {
        var people = await _realEstateRepository.GetByIdHouses(id);
        if (people == null) return NotFound();

        await _realEstateRepository.RemoveAsync(id);
        return NoContent();
    }
}