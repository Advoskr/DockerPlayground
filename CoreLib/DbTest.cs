using System;
using Npgsql;

namespace CoreLib
{
    public static class DbTest
    {
        public static (bool, string) TestConnection(string dbConnStr)
        {
            try
            {
                using (var conn = new NpgsqlConnection(dbConnStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Select 1 as bit";
                        var res = (int)cmd.ExecuteScalar();
                        return res!=1 ? (false,"db check result is not same as expected") : (true, "");
                    }
                }
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
            
        }
        
    }
}
