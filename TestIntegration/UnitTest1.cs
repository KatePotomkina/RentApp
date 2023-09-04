using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RentApp.Collection;
using RentApp.Models;
using RentApp.Repository;

namespace TestIntegration;

public class UnitTest1:IDisposable
{
     private readonly IMongoDatabase _mongoDatabase;
        private readonly IRealEstateRepository _repository;

        public UnitTest1()
        {
            // Подключение к MongoDB и создание временной базы данных для тестов
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = mongoClient.GetDatabase("TestDatabase");

            // Инициализация репозитория с настройками для временной базы данных
            var options = Options.Create(new MongoDBSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestDatabase",
                ApartmentCollectionName = "Apartments",
                HouseCollectionName = "Houses",
                OfficceCollectionName = "Offices"
            });

            _repository = new RealEstateRepository(options);
        }

        public void Dispose()
        {
            // Удаление временной базы данных после выполнения всех тестов
            _mongoDatabase.Client.DropDatabase(_mongoDatabase.DatabaseNamespace.DatabaseName);
        }

        [Fact]
        public async Task GetAllPropertiesAsync_ShouldReturnAllProperties()
        {
            // Arrange
            await InsertTestData(); // Вставка тестовых данных в базу данных

            // Act
            var result = await _repository.GetAllPropertiesAsync();

            // Assert
            Assert.Equal(3, result.Count); // Проверка, что возвращено ожидаемое количество объектов
        }

        private async Task InsertTestData()
        {
            // Вставка тестовых данных в базу данных
            var apartments = new List<Apartament>
            {
                new Apartament { Id = ObjectId.GenerateNewId().ToString(), Name = "Apartment 1" },
                new Apartament { Id = ObjectId.GenerateNewId().ToString(), Name = "Apartment 2" },
            };

            var houses = new List<House>
            {
                new House { Id = ObjectId.GenerateNewId().ToString(), Name = "House 1" },
            };

            var offices = new List<Office>
            {
                new Office { Id = ObjectId.GenerateNewId().ToString(), Name = "Office 1" },
            };

            await _mongoDatabase.GetCollection<Apartament>("Apartments").InsertManyAsync(apartments);
            await _mongoDatabase.GetCollection<House>("Houses").InsertManyAsync(houses);
            await _mongoDatabase.GetCollection<Office>("Offices").InsertManyAsync(offices);
        }
    }

