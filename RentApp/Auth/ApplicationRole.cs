using AspNetCore.Identity.MongoDbCore.Models;

namespace RentApp.Auth;

public class ApplicationRole : MongoIdentityRole<Guid>
{
}