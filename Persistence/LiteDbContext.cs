using LiteDB;
using Microsoft.Extensions.Configuration;

namespace AutoServiss.Persistence
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(IConfiguration configuration)
        {
            Database = new LiteDatabase(configuration["LiteDbOptions:DatabaseLocation"]);
        }
    }
}