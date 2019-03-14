using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project_Try1.Models {
    public class DBUtils {
        public static SqlConnection GetConnection() {
            try {
                return new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
            } catch (Exception e) {
                throw e;
            }

        }
    }
}