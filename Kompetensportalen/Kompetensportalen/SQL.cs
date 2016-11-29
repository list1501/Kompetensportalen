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
using System.IO;

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

        #region Send XML to Database
        public void saveTestToDB(Test test)
        {
            string employee = test.employee;
            DateTime testDate = test.date;
            int testType = test.testType;
            bool passed = test.passed;
            int totalPoints = test.totalPoints;
            int category1 = test.category1;
            int category2 = test.category2;
            int category3 = test.category3;
            XmlDocument xml = test.sourceFile;

            StringWriter sw = new StringWriter();
            XmlTextWriter tw = new XmlTextWriter(sw);
            xml.WriteTo(tw);

            string xmlString = tw.ToString();

            string sql = "INSERT INTO finished_tests (employee, date, type, passed, total_points, points_category1, points_category2, points_category3, xml)" +
                "values (@employee, @date, @type, @passed, @total, @cat1, @cat2, @cat3, @xml)";

            openConn();
            _cmd = new NpgsqlCommand(sql, _conn);
            _cmd.Parameters.AddWithValue("employee", employee);
            _cmd.Parameters.AddWithValue("date", testDate);
            _cmd.Parameters.AddWithValue("type", testType);
            _cmd.Parameters.AddWithValue("passed", passed);
            _cmd.Parameters.AddWithValue("total", totalPoints);
            _cmd.Parameters.AddWithValue("cat1", category1);
            _cmd.Parameters.AddWithValue("cat2", category2);
            _cmd.Parameters.AddWithValue("cat3", category3);
            _cmd.Parameters.AddWithValue("xml", xmlString);  //Lite osäker på om den måste vara .ToString() för att databasen ska ta emot eller inte. Kan kolla upp detta.

            _cmd.ExecuteNonQuery();
            closeConn();

        }

        #endregion

        #region Get test history
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
        #endregion

        public List<Test> getallUsersAdmin()
        {
            openConn();
            string sql = "SELECT employee, date, type, passed, total_points, points_category1, points_category2, points_category3 FROM finished_tests ORDER BY date DESC";
            _cmd = new NpgsqlCommand(sql, _conn);
            _dr = sqlQuery();
            Test newTest;

            List<Test> testlist = new List<Test>();
            while (_dr.Read())
            {
                newTest = new Test()
                {
                    employee = _dr["employee"].ToString(),
                    date = (DateTime)_dr["date"],
                    testType = (int)_dr["type"],
                    passed = (bool)_dr["passed"],
                    totalPoints = (int)_dr["total_points"],
                    category1 = (int)_dr["points_category1"],
                    category2 = (int)_dr["points_category2"],
                    category3 = (int)_dr["points_category3"],
                };
                testlist.Add(newTest);
            }
            closeConn();
            return testlist;
        }

        public List<User> GetUsersForAnnualCheck()
        {        
            openConn();
            string sql = "SELECT * FROM employee left join finished_tests on employee.username = finished_tests.employee where employee.latest_test < CURRENT_TIMESTAMP - INTERVAL '1' YEAR AND employee.qualified = true ORDER BY date DESC";
            _cmd = new NpgsqlCommand(sql, _conn);
            _dr = sqlQuery();
            User annualtest;

            List<User> annuallist = new List<User>();
            while (_dr.Read())
            {
                annualtest = new User()
                {
                    username = _dr["employee"].ToString(),
                    lastTestDate = (DateTime)_dr["date"],
                    usertype = (int)_dr["type"],
                    qualified = (bool)_dr["passed"],

                };
                annuallist.Add(annualtest);
            }
            closeConn();
            return annuallist;
        }

        public List<User> GetNotCertifiedUsersAdmin()
        {
            openConn();
            string sql = "SELECT * FROM employee left join finished_tests on employee.username = finished_tests.employee where passed = false ORDER BY date DESC";
            _cmd = new NpgsqlCommand(sql, _conn);
            _dr = sqlQuery();
            User uncertified;

            List<User> uncertifiedlist = new List<User>();
            while (_dr.Read())
            {
                uncertified = new User()
                {
                    username = _dr["employee"].ToString(),
                    lastTestDate = (DateTime)_dr["date"],
                    usertype = (int)_dr["type"],
                    qualified = (bool)_dr["passed"],

                };
                uncertifiedlist.Add(uncertified);
            }
            closeConn();
            return uncertifiedlist;
        }
        public List<User> GetCertifiedUsersAdmin()
        {
            openConn();
            string sql = "SELECT * FROM employee left join finished_tests on employee.username = finished_tests.employee where passed = true ORDER BY date DESC";
            _cmd = new NpgsqlCommand(sql, _conn);
            _dr = sqlQuery();
            User uncertified;

            List<User> uncertifiedlist = new List<User>();
            while (_dr.Read())
            {
                uncertified = new User()
                {
                    username = _dr["employee"].ToString(),
                    lastTestDate = (DateTime)_dr["date"],
                    usertype = (int)_dr["type"],
                    qualified = (bool)_dr["passed"],

                };
                uncertifiedlist.Add(uncertified);
            }
            closeConn();
            return uncertifiedlist;
        }
    }
}