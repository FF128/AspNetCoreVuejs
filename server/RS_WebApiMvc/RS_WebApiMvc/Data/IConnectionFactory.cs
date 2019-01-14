using System.Data;

namespace RS_WebApiMvc.Data
{
    public interface IConnectionFactory
    {
        IDbConnection Connection { get; }

    }
}