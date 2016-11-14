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
        public NpgsqlDataReader sqlQuery()
        {
            try
            {
                _dr = _cmd.ExecuteReader();
                return _dr;
            }
            catch (NpgsqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return _dr;
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

            openConn();
            string sql = "SELECT * FROM users WHERE username = @user";
            _cmd = new NpgsqlCommand(sql, _conn);
            _cmd.Parameters.AddWithValue("user", user);
            
            _dr = sqlQuery();

          
            while (_dr.Read())
            {
                newUser.username = _dr["username"].ToString();
                newUser.usertype = (int)_dr["type"];
            }
            closeConn();

            if (newUser.usertype == 2)
            {
                openConn();
                string sqlEmployee = "SELECT latest_test, qualified FROM employee WHERE username = @user";
                _cmd = new NpgsqlCommand(sqlEmployee, _conn);
                _cmd.Parameters.AddWithValue("user", user);
                
                _dr = sqlQuery();
                
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
            Test oldTest = new Test();
            XmlDocument doc = new XmlDocument();
            string username = user;
            DateTime testDate = date;
            string sql = "SELECT * FROM finished_tests WHERE employee = @username AND date = @date";

            openConn();
            _cmd = new NpgsqlCommand(sql, _conn);
            _cmd.Parameters.AddWithValue("username", username);
            _cmd.Parameters.AddWithValue("date", testDate);

            _dr = _cmd.ExecuteReader();

            
            while (_dr.Read())
            {
                oldTest.employee = _dr["employee"].ToString();
                oldTest.date = (DateTime)_dr["date"];
                oldTest.testType = (int)_dr["type"];
                oldTest.passed = Convert.ToBoolean(_dr["passed"]);
                oldTest.totalPoints = (int)_dr["total_points"];
                oldTest.category1 = (int)_dr["points_category1"];
                oldTest.category2 = (int)_dr["points_category2"];
                oldTest.category3 = (int)_dr["points_category3"];
                doc.LoadXml(_dr["xml"].ToString());
                oldTest.sourceFile = doc;
            }
            closeConn();

            return oldTest;
        }   
        #endregion Get XML from Database

        //Method to get selected user's test history
        public List<Test> getTestHistory(string usr)
        {
            List<Test> testHistory = new List<Test>();
            string user = usr;
            openConn();
            string sql = "SELECT * FROM finished_tests WHERE employee = @user SORT BY username ASC ORDER BY date DESC";
            _cmd = new NpgsqlCommand(sql, _conn);
            _cmd.Parameters.AddWithValue("user", user);

            _dr = sqlQuery();

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

        public List<Test> getTestsAdmin()
        {
            List<Test> userTestsforAdmin = new List<Test>();
            openConn();
            string sql = "SELECT employee, date, passed, total_points, points_category1, points_category2, points_category3 FROM finished_tests ORDER BY date DESC";
            _cmd = new NpgsqlCommand(sql, _conn);

            _dr = sqlQuery();

            while (_dr.Read())
            {
                Test seeTests = new Test()
                {
                    employee = _dr["employee"].ToString(),
                    date = (DateTime)_dr["date"],
                    passed = (bool)_dr["passed"],
                    totalPoints = (int)_dr["total_points"],
                    category1 = (int)_dr["points_category1"],
                    category2 = (int)_dr["points_category2"],
                    category3 = (int)_dr["points_category3"],
                   
            };
                userTestsforAdmin.Add(seeTests);
            }
            closeConn();
            return userTestsforAdmin;
        }
        
    }
}