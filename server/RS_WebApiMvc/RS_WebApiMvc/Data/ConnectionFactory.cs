using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RS_WebApiMvc.Data
{
    public class ConnectionFactory : IConnectionFactory
    { 

        public IDbConnection Connection => new SqlConnection(ConfigurationManager.ConnectionStrings["RSContext"].ConnectionString);
    }
}