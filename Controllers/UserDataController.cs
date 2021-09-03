using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;

//mongodb+srv://admin:ObzJYU35f7twe6MQ@cluster0.lgloz.mongodb.net/discordJsSession?retryWrites=true&w=majority

namespace SimpleMongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {

        private readonly ILogger<UserDataController> _logger;
        private readonly MongoClient client;
        private readonly IMongoDatabase db;

        private readonly IMongoCollection<UserData> collection;

        public UserDataController(ILogger<UserDataController> logger)
        {
            _logger = logger;
            client = new MongoClient(Environment.GetEnvironmentVariable("CONN_STRING"));
            db = client.GetDatabase("test");
            collection = db.GetCollection<UserData>("userData");
        }

        [HttpGet("/all")]
        public IEnumerable<UserData> GetAll()
        {
            _logger.Log(LogLevel.Information, "A request to Mongo has been requested");
            return collection.Find(new BsonDocument()).ToEnumerable();
        }

        [HttpPost("/create")]
        public UserData CreateUser([FromBody] UserData newUser)
        {
            collection.InsertOne(newUser);
            return newUser;
        }

        [HttpGet("/get/{name}")]
        public ActionResult<UserData> GetOne([FromRoute] string name)
        {
            var document = collection.Find(x => x.Name == name);
            if (document.CountDocuments() == 0)
            {
                return NotFound();
            }
            _logger.Log(LogLevel.Information, $"Looking for {name}");
            return Ok(document.First());
        }


    }
}