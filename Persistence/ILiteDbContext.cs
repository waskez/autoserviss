using LiteDB;

namespace AutoServiss.Persistence
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}