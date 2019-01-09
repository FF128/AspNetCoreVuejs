using System.Data;

namespace WebAPI.Data
{
    public interface IConnectionFactory
    {
        IDbConnection Connection { get; }

    }
}