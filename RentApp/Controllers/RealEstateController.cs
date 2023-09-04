using Microsoft.AspNetCore.Mvc;
using RentApp.Collection;
using RentApp.Repository;

namespace RentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class RealEstateController : ControllerBase
{
    private readonly IRealEstateRepository _realEstateRepository;
    private readonly ILogger<RealEstateController> _logger;

    public RealEstateController(IRealEstateRepository realEstateRepository, ILogger<RealEstateController> logger)
    {
        _realEstateRepository = realEstateRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetProperties()
    {
        _logger.LogInformation("Get all objects from collection  request received.");

        var properties = await _realEstateRepository.GetAllPropertiesAsync();
        _logger.LogInformation($"Returned{properties.Count} objects ");
        return Ok(properties);
    }
}