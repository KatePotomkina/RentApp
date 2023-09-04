using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;
using RentApp.Collection;
using RentApp.Dtos;

namespace RentApp.Service;

public class UserService
{
    private readonly IMongoCollection<People> _usersCollection;

    public UserService(DataContext dataContext)
    {
        _usersCollection = dataContext.Users;
    }

    public People Authenticate(string username, string password)
    {
        var user = _usersCollection.Find(u => u.UserName == username).FirstOrDefault();

        if (user != null && VerifyPasswordHash(password, user.Password))
        {
            return user;
        }

        return null;
    }

    public People Create(People user)
    {
        _usersCollection.InsertOne(user);
        return user;
    }

    private static bool VerifyPasswordHash(string password, string storedHash)
    {
        using (var hmac = new HMACSHA512())
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i])
                {
                    return false;
                }
            }
        }

        return true;
    }
}