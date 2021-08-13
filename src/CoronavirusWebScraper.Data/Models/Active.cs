namespace CoronavirusWebScraper.Data.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Hold currently infected by covid19.
    /// </summary>
    public class Active
    {

        /// <summary>
        /// Gets or sets count of current infected people.
        /// </summary>
        [BsonElement("current")]
        public int Curent { get; set; }

        /// <summary>
        /// Gets or sets count of current infected people by type of care.
        /// </summary>
        [BsonElement("current_by_type")]
        public ActiveTypes CurrentByType { get; set; }
    }
}
