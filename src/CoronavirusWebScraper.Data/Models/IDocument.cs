namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Interface which hold base information about each document in mongodb.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Gets or sets id of current document.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
