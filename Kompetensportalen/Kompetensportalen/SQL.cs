using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mono.Security;
using Npgsql;
using System.Web.Configuration;
using System.Web.UI;
using System.Data;
using System.Xml;

namespace Kompetensportalen
{
    public class SQL
    {
        public NpgsqlConnection _conn;
        public NpgsqlCommand _cmd;
        public NpgsqlDataReader _dr;
        public User currentUser = Loginpage.currentLogin;

        #region Open, Close and execute Query SQL
        //Method to open DB connection
        public void openConn()
        {
            try
            {
                _conn = new NpgsqlConnection(WebConfigurationManager.ConnectionStrings["interaktiva_g12"].ConnectionString);
                _conn.Open();
            }
            catch
            {
                _conn = new NpgsqlConnection(WebConfigurationManager.ConnectionStrings["interaktiva_g12_trust"].ConnectionString);
                _conn.Open();
            }
        }

        //Method to close DB connection
        public void closeConn()
        {
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        //Method to execute Query in DB
        public NpgsqlDataReader sqlQuery(string sql)
        {
            try
            {
                _cmd = new NpgsqlCommand(sql, _conn);
                _dr = _cmd.ExecuteReader();
                return _dr;
            }
            catch (NpgsqlException ex)
            {
                //HÄR SKA DET STÅ NÅGOT FÖR FELMEDDELANDE
                return null;
            }
        }

        //Method to execute Non Query in DB
        public void sqlNonQuery(string sql)
        {
            try
            {
                _cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                //HÄR SKA DET STÅ NÅGOT FÖR FELMEDDELANDE
            }
        }

        #endregion Open, Close and execute Query SQL

        #region Get User info

        //Method to get user info
        public User getLogin(string usr)
        {
            User newUser = new User();
            string user = usr;
            string sql = "SELECT * FROM users WHERE username = '" + user + "'";

            openConn();
            _dr = sqlQuery(sql);

            if (_dr.Read())
            {
                while (_dr.Read())
                {
                    newUser.username = _dr["username"].ToString();
                    newUser.usertype = (int)_dr["type"];
                }
            }
            closeConn();

            if (newUser.usertype == 2)
            {
                string sqlEmployee = "SELECT latest_test, qualified FROM employee WHERE username = '" + user + "'";
                
                openConn();
                _dr = sqlQuery(sqlEmployee);

                if (_dr.Read())
                {
                    while (_dr.Read())
                    {
                        if (_dr["latest_test"] != null)
                        {
                            newUser.lastTestDate = (DateTime)_dr["latest_test"];
                        }
                        if (_dr["qualified"] != null)
                        {
                            newUser.qualified = Convert.ToBoolean(_dr["qualified"].ToString());
                        }
                    }
                }
                closeConn();
            }

            return newUser;
        }

        #endregion Get User info    


        #region Get XML from Database
        //Method to get xml from database to asp.net
        public XmlDocument DbToXml(int testIDs)
        {
            XmlDocument doc = new XmlDocument();          

            string getTest = "SELECT * FROM tests WHERE id = @id";
            openConn();
            _cmd = new NpgsqlCommand(getTest, _conn);
            _cmd.Parameters.AddWithValue("id", Convert.ToInt16(testIDs));        

            _dr = _cmd.ExecuteReader();

            if (_dr.Read())
            {
                doc.LoadXml(_dr["xml"].ToString());
            }
            closeConn();
            return doc;
        }
        
        public Test getLastTest(string user, DateTime date)
        {
            Test newTest = new Test();
            XmlDocument doc = new XmlDocument();
            string username = user;
            string testDate = date.ToShortDateString();
            string sql = "SELECT * FROM finished_tests WHERE username = @username AND date = @date";

            openConn();
            _cmd = new NpgsqlCommand(sql, _conn);
            _cmd.Parameters.AddWithValue("username", username);
            _cmd.Parameters.AddWithValue("date", testDate);

            _dr = _cmd.ExecuteReader();

            if (_dr.Read())
            {
                while (_dr.Read())
                {
                    newTest.employee = _dr["username"].ToString();
                    newTest.date = (DateTime)_dr["date"];
                    newTest.testType = (int)_dr["type"];
                    newTest.passed = Convert.ToBoolean(_dr["passed"]);
                    newTest.totalPoints = (int)_dr["total_points"];
                    newTest.category1 = (int)_dr["points_category1"];
                    newTest.category2 = (int)_dr["points_category2"];
                    newTest.category3 = (int)_dr["points_category3"];
                    doc.LoadXml(_dr["xml"].ToString());
                    newTest.sourceFile = doc;
                }
            }
            closeConn();

            return newTest;
        }   
        #endregion Get XML from Database

        //Method to get selected user's test history
        public List<Test> getTestHistory(string usr)
        {
            List<Test> testHistory = new List<Test>();
            string user = usr;
            string sql = "SELECT * FROM finished_tests WHERE employee = '" + user + "' SORT BY username ASC ORDER BY date DESC";

            openConn();
            _dr = sqlQuery(sql);

            while (_dr.Read())
            {
                Test newTest = new Test()
                {
                    employee = _dr["employee"].ToString(),
                    date = (DateTime)_dr["date"],
                    testType = (int)_dr["type"],
                    passed = (bool)_dr["passed"]
                };
                testHistory.Add(newTest);
            }
            closeConn();
            return testHistory;
        }
    }
}