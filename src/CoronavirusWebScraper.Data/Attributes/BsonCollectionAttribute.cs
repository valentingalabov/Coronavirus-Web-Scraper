namespace CoronavirusWebScraper.Data.Attributes
{
    using System;

    /// <summary>
    /// Atribute specifies the collection name in mongo db document.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
       /// <summary>
       /// Gets collection name.
       /// </summary>
        public string CollectionName { get; }

        /// <summary>
        /// Constructor which set collection name.
        /// </summary>
        /// <param name="collectionName">Contain name in attribute as string.</param>
        public BsonCollectionAttribute(string collectionName)
        {
            this.CollectionName = collectionName;
        }
    }
}
