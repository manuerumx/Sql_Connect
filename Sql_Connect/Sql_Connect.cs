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

        public string DBUser = "sa";
        public string DBPass = "";
        public string DBServer = "(local)";
        public string DBName = "TempDB";
        
        public int DBPort = 1433;
        
        public string DBInstance = "";
        public string DBDomain = "";

        public bool DBIPAddress = false;
        public bool DBPersistance = false;
        public bool DBTrusted = false;
        public bool DBConnected = false;
        public bool DBIsInstance = false;
        public bool DBExpress = false;
        public bool DBMars = false;
        public bool DBCE = false;

        private string DBError = "";
        private string DBString = "";

        
        
        //public SqlConnection conn;

        //public SqlConnection Make_Conn(string sqlConn)
        //{
        //    try 
        //    {                 
        //        conn.ConnectionString = sqlConn;
        //        conn.Open();
        //        return conn;
        //    }
        //    catch (SqlException e)
        //    {
        //        return null;
        //    }
        //}

        public string Make_String()
        {
            try
            {
                string Connection = "";
                if (DBTrusted == true)
                {
                    Connection = TrustedConnect();
                }
                else 
                {
                    //Normal Connection
                    Connection = StandardConnect();
                }

                return Connection;
            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
        }


        /**
         *  Establish a Trusted Connection |[Optional] MARS Enabled |[Optional] From CE Device
         *  
         */
        private string TrustedConnect() {
            try
            {
                if (DBServer.Length > 0 && DBName.Length > 0)
                {
                    if (DBMars == true)
                    {
                        //MARS ENABLED
                        DBString = "Data Source=" + DBServer + ";Initial Catalog=" + DBName + ";" +
                                                "Integrated Security=SSPI;MultipleActiveResultSets=true;";
                    }
                    else if (DBCE == true)
                    {
                        //CE Device connection
                        DBString = "Data Source=" + DBServer + ";Initial Catalog=" + DBName + ";Integrated Security=SSPI;" +
                                                "User ID=" + DBDomain + "\\" + DBUser + ";Password=" + DBPass + ";";
                    }
                    else if (DBIsInstance == true)
                    {
                        //Instance connection
                        DBString = "Server=" + DBServer + "\\" + DBInstance + ";Database=" + DBName + ";Trusted_Connection=True;";
                    }
                    else
                    {
                        //Normal Trusted Connection
                        DBString = "Data Source=" + DBServer + ";Initial Catalog=" + DBName + ";Integrated Security=SSPI;";
                    }
                    return DBString;
                    
                }
                else
                {
                    DBError = "The server/database is null";
                    return DBError;
                }
            }
            catch (SqlException e)
            {
                DBError = "Error " + e.ErrorCode + ": " + e.Errors;
                return DBError;
            }

        }
        
        /**
         * StandardConnect
         * Open the standard sql connection. 
         * 
         * @param User string
         * @param Pass string
         * @param Server string
         * @param Name string
         * @param Port int
         * @param Instance boolean
         * @param InstanceNm string
         */
        private string StandardConnect(){
            try{                
                if (DBIsInstance == true  && DBUser.Length > 0 && DBServer.Length > 0 && DBIPAddress == false)
                {
                    DBString = "Data Source=" + DBServer + "//" + DBInstance + ";Initial Catalog=" + DBName + ";" +
                                            "User Id=" + DBUser + ";Password=" + DBPass;
                    /*
                    DBString = "Server=" + Server + "//" + InstanceNm + ";Database=" + Name + ";UserID=" + User + ";Password=" + Pass + ";" +
                                            "Trusted_Connection=False;";
                     */
                }
                else if (DBUser.Length > 0 && DBServer.Length > 0 && DBIPAddress == false) 
                {
                    DBString = "Data Source=" + DBServer + ";Initial Catalog=" + DBName + ";" +
                                            "User Id=" + DBUser + ";Password=" + DBPass;
                    //or
                    /*
                    DBString = "Server=" + Server + ";Database=" + Name + ";UserID=" + User + ";Password=" + Pass + ";" +
                                            "Trusted_Connection=False;";
                     */
                }
                else if (DBIPAddress == true )
                {
                    DBString = "Data Source=" + DBServer + "," + DBPort + ";Network Library=DBMSSOCN;Initial Catalog=" + DBName + ";" +
                               "User ID=" + DBUser + ";Password="+ DBPass +";";
                }
                return DBString;
            }catch(Exception e){
                DBError = "Error: " + e.Message;
                return DBError;
            }
        }        
    }
}
