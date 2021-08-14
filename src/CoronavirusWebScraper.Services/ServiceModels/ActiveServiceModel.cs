namespace CoronavirusWebScraper.Services.ServiceModels
{
    /// <summary>
    /// Hold currently infected by covid19.
    /// </summary>
    public class ActiveServiceModel
    {
        /// <summary>
        /// Gets or sets count of current infected people.
        /// </summary>
        public int Curent { get; set; }

        /// <summary>
        /// Gets or sets count of current infected people by type of care.
        /// </summary>
        public ActiveTypesServiceModel CurrentByType { get; set; }
    }
}
