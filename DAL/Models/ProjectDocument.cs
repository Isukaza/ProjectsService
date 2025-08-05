using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models;

public class ProjectDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public int UserId { get; set; }
    public string Name { get; set; }
    public List<ChartDocument> Charts { get; set; } = [];
}