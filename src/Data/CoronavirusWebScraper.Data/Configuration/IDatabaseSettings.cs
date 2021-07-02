using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusWebScraper.Data.Configuration
{
   public interface  IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
