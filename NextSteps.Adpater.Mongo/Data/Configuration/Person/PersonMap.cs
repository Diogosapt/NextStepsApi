using MongoDB.Bson.Serialization;

namespace NextSteps.Adpater.Mongo.Data.Configuration
{
    public class PersonMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Models.Person>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Name).SetIsRequired(true);
            });
        }
    }
}