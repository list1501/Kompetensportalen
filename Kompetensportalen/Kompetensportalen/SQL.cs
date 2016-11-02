using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mono.Security;
using Npgsql;
using System.Web.Configuration;
using System.Web.UI;

namespace Kompetensportalen
{
    public class SQL
    {
        public NpgsqlConnection _conn;
        public NpgsqlCommand _cmd;
        public NpgsqlDataReader _dr;

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
            _conn.Close();
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
        
        //Method to get user info
        public User getLogin(string usr)
        {
            User newUser = new Kompetensportalen.User();
            string user = usr;
            string sql = "SELECT * FROM users WHERE username = '" + user + "'";
            _dr = sqlQuery(sql);
            
            while (_dr.Read())
            {
                newUser.username = _dr["username"].ToString();
                newUser.usertype = int.Parse(_dr["type"].ToString());

                if (newUser.usertype == 2)
                {
                    string sqlEmployee = "SELECT latest_test, passed FROM employee WHERE username = '" + user + "'";
                    _dr = sqlQuery(sqlEmployee);
                    
                    while (_dr.Read())
                    {
                        newUser.latestTest = (DateTime)_dr["latest_test"];
                        newUser.qualified = (bool)_dr["qualified"];
                    }
                }
            }
            return newUser;          
        }
        
        //Method to get selected user's test history
        public List<Test> getTestHistory(string usr)
        {
            List<Test> testHistory = new List<Test>();
            string user = usr;
            string sql = "SELECT * FROM finished_tests WHERE employee = '" + user + "' SORT BY username ASC ORDER BY date DESC";
            _dr = sqlQuery(sql);

            while (_dr.Read())
            {
                Test newTest = new Kompetensportalen.Test()
                {
                    employee = _dr["employee"].ToString(),                    
                    date = (DateTime)_dr["date"],
                    testType = (int)_dr["type"],
                    passed = (bool)_dr["passed"]
                };
                testHistory.Add(newTest);
            }
            return testHistory;
        }        
    }
}