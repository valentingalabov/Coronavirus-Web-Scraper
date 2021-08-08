namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson;

    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}
