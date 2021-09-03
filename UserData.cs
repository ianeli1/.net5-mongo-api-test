using MongoDB.Bson;

namespace SimpleMongo
{
    public class UserData
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }
    }
}