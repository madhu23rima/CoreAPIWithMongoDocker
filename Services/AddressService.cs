using LocationApi.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace LocationApi.Services
{
    public class AddressService : IAddressService
    {
        private readonly IMongoCollection<Address> _addresses;
        private ILogger<AddressService> _logger { get; }
        private string _dbName = "locationDB";
        private string _collectionName = "address" ;

        public AddressService(IDatabaseSettings settings, ILogger<AddressService> logger)
        {
            _logger = logger;
            _logger.LogInformation("Connection string is :" + settings.AdminLocationConnectionString);
           
            var client = new MongoClient(settings.AdminLocationConnectionString);
            var database = client.GetDatabase(_dbName);
            _addresses = database.GetCollection<Address>(_collectionName);
        }

        public List<Address> Get()
        {
            return _addresses.Find(address => true).ToList();
        }


        public Address Get(string id)
        {
            return _addresses.Find<Address>(book => book.Id == id).FirstOrDefault();
        }


        public Address Create(Address book)
        {
            _addresses.InsertOne(book);
            return book;
        }

        public void Update(string id, Address addressIn)
        {
            _addresses.ReplaceOne(book => book.Id == id, addressIn);
        }


        public void Remove(Address addressIn)
        {
            _addresses.DeleteOne(book => book.Id == addressIn.Id);
        }


        public void Remove(string id)
        {
            _addresses.DeleteOne(book => book.Id == id);
        }

    }

    public interface IAddressService
    {

        List<Address> Get();

        Address Get(string id);

        Address Create(Address book);

        void Update(string id, Address addressIn);

        void Remove(Address addressIn);

        void Remove(string id);

    }

}