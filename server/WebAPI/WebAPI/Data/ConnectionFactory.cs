
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration config;
        public ConnectionFactory(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                //var connStr = @"Server=.\SQL2017; Initial Catalog=RS_DB; Integrated Security=false; User Id=sa; Password=128TechInc";
                //var test = new SqlConnection(connStr);

                return new SqlConnection(config.GetConnectionString("ConnStr"));

            }
        }
    }
}
