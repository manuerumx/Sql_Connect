using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Sql_Connect
{
    public class Sql_Connect 
    {

        private string DBUser = "";
        private string DBPass = "";
        private string DBServer = "";
        private string DBName = "";
        private int DBPort = 1433;
        private string DBInstance = "";
        private string DBError = "";
        private bool DBPersistance = false;
        private bool DBConnected = false;
        public string DBQuery = "";

        public SqlConnection conn;
        public SqlDataReader reader;

        /**
         *  Establish a Trusted Connection |[Optional] MARS Enabled |[Optional] From CE Device
         *  
         *  @param Server string
         *  @param Database string
         *  @param MARS boolean
         *  @param CEDevice boolean 
         *  @param domain string    |CE Device
         *  @param user string      |CE Device
         *  @param pass string      |CE Device
         */
        public bool TrustedConnect(string Server = "", string Database = "", bool MARS = false, bool CEDevice = false,
                                   bool instance = false, string nameInstance = "",
                                   string domain = "", string user = "", string pass = "") {
            try
            {
                if (Server.Length > 0 && Database.Length > 0)
                {
                    if (MARS == true)
                    {
                        //MARS ENABLED
                        conn.ConnectionString = "Data Source=" + Server + ";Initial Catalog=" + Database + ";" +
                                                "Integrated Security=SSPI;MultipleActiveResultSets=true;";
                    }
                    else if (CEDevice == true)
                    {
                        //CE Device connection
                        conn.ConnectionString = "Data Source=" + Server + ";Initial Catalog=" + Database + ";Integrated Security=SSPI;" +
                                                "User ID=" + domain + "\\" + user + ";Password=" + pass + ";";
                    }
                    else if (instance == true)
                    {
                        //Instance connection
                        conn.ConnectionString = "Server=" + Server + "\\" + nameInstance + ";Database=" + Database + ";Trusted_Connection=True;";
                    }
                    else
                    {
                        //Normal Trusted Connection
                        conn.ConnectionString = "Data Source=" + Server + ";Initial Catalog=" + Database + ";Integrated Security=SSPI;";
                    }
                    conn.Open();
                    return true;
                }
                else
                {
                    DBError = "The server/database is null";
                    return false;
                }
            }
            catch (SqlException e)
            {
                DBError = "Error " + e.ErrorCode + ": " + e.Errors;
                return false;
            }

        }
        /**
         * Establish a Instance Connection
         * 
         * 
         */
        public bool InstanceConnect() {
            return false;
        }
        /**
         * Establish a Express Instance Connection
         * 
         */
        public bool InstanceExConnect() {
            return false;
        }

        public bool StandardConnect(string User, string Pass,  string Server, string Name, int Port, bool Instance){
            try{

                //Standard Security
                conn.ConnectionString = "Data Source=urServerAddress;Initial Catalog=urDataBase;" +
                                        "User Id=urUsername;Password=urPassword";
                //or
                conn.ConnectionString = "Server=urServerAddress;Database=urDataBase;UserID=urUsername;Password=urPassword;" +
                                        "Trusted_Connection=False;";
                
                conn.Open();

                return true;            
            }catch(SqlException sqle){
                DBError = "Error " + sqle.ErrorCode + ": " + sqle.Errors;
                return false;
            }
        }        
    }
}
