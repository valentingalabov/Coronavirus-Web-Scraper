namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson;

    /// <summary>
    /// Abstract class that contains base information about each document in mongodb.
    /// </summary>
    public abstract class Document : IDocument
    {
        /// <summary>
        /// Gets or sets id of current document.
        /// </summary>
        public ObjectId Id { get; set; }
    }
}
