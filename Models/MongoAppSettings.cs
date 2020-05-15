using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl.Models
{
    public class MongoAppSettings : IMongoAppSettings
    {
        public string Collections { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

    public interface IMongoAppSettings
    {
        string Collections { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
