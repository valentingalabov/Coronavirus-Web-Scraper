namespace CoronavirusWebScraper.Data.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            this.CollectionName = collectionName;
        }
    }
}
