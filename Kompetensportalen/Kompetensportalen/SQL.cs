using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mono.Security;
using Npgsql;
using System.Web.Configuration;
using System.Web.UI;
using System.Data;

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
        
        //Method to get user info
        public User getLogin(string usr)
        {
            User newUser = new User();
            string user = usr;
            string sql = "SELECT * FROM users WHERE username = '" + user + "'";

            openConn();
            _dr = sqlQuery(sql);
            
            while (_dr.Read())
            {
                newUser.username = _dr["username"].ToString();
                newUser.usertype = int.Parse(_dr["type"].ToString());

                if (newUser.usertype == 2)
                {
                    string sqlEmployee = "SELECT latest_test, qualified FROM employee WHERE username = '" + user + "'";
                    NpgsqlDataReader _dr1 = sqlQuery(sqlEmployee);
                    
                    while (_dr1.Read())
                    {
                        newUser.latestTest = (DateTime)_dr1["latest_test"];
                        newUser.qualified = (bool)_dr1["qualified"];
                    }
                }
            }
            closeConn();
            return newUser;
        }
        
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
                Test newTest = new Kompetensportalen.Test()
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

        //Method to get test question with multiple answers
        public Question getQuestion(int dbID, int type)
        {
            Question newQuestion = new Question();
            string sql;
            string id = dbID.ToString();
            int testType = type;

            if (testType == 1)
            {
                sql = "SELECT * FROM questions_qualifying WHERE id = '" + id + "'";

                openConn();
                _dr = sqlQuery(sql);

                while (_dr.Read())
                {
                    newQuestion.id = (int)_dr["id"];
                    newQuestion.question = _dr["question"].ToString();
                    newQuestion.category = (int)_dr["category"];

                    int arrayLengthCorrect;

                    string sqlCorrect = "SELECT array_length(correct_answer, 1) FROM questions_qualifying WHERE id = '" + id + "'";
                    NpgsqlDataReader _correct = sqlQuery(sqlCorrect);

                    while (_correct.Read())
                    {
                        arrayLengthCorrect = (int)_correct["array_length"];

                        int c = 0;

                        while (c != arrayLengthCorrect)
                        {
                            Answer newCorrectAnswer = new Answer()
                            {
                                type = 0,
                                text = _dr["correct_answer[" + c + "]"].ToString(),
                            };
                            newQuestion.correctAnswer.Add(newCorrectAnswer);
                            c++;
                        }
                    }

                    int arrayLengthWrong;
                    string sqlWrong = "SELECT array_length(wrong_answer, 1) FROM questions_qualifying WHERE id = '" + id + "'";
                    NpgsqlDataReader _wrong = sqlQuery(sqlWrong);

                    while (_wrong.Read())
                    {
                        arrayLengthWrong = (int)_wrong["array_length"];

                        int w = 0;

                        while (w != arrayLengthWrong)
                        {
                            Answer newWrongAnswer = new Answer()
                            {
                                type = 1,
                                text = _dr["wrong_answer[" + w + "]"].ToString(),
                            };
                            newQuestion.wrongAnswer.Add(newWrongAnswer);
                            w++;
                        }
                    }
                }
                closeConn();
            }
            else if (testType == 2)
            {
                sql = "SELECT * FROM questions_competence WHERE id = '" + id + "'";

                openConn();
                _dr = sqlQuery(sql);

                while (_dr.Read())
                {
                    newQuestion.id = (int)_dr["id"];
                    newQuestion.question = _dr["question"].ToString();
                    newQuestion.category = (int)_dr["category"];

                    int arrayLengthCorrect;

                    string sqlCorrect = "SELECT array_length(correct_answer, 1) FROM questions_competence WHERE id = '" + id + "'";
                    NpgsqlDataReader _correct = sqlQuery(sqlCorrect);

                    while (_correct.Read())
                    {
                        arrayLengthCorrect = (int)_correct["array_length"];

                        int c = 0;

                        while (c != arrayLengthCorrect)
                        {
                            Answer newCorrectAnswer = new Answer()
                            {
                                type = 0,
                                text = _dr["correct_answer[" + c + "]"].ToString(),
                            };
                            newQuestion.correctAnswer.Add(newCorrectAnswer);
                            c++;
                        }
                    }

                    int arrayLengthWrong;
                    string sqlWrong = "SELECT array_length(wrong_answer, 1) FROM questions_competence WHERE id = '" + id + "'";
                    NpgsqlDataReader _wrong = sqlQuery(sqlWrong);

                    while (_wrong.Read())
                    {
                        arrayLengthWrong = (int)_wrong["array_length"];

                        int w = 0;
                        while (w != arrayLengthWrong)
                        {
                            Answer newWrongAnswer = new Answer()
                            {
                                type = 1,
                                text = _dr["wrong_answer[" + w + "]"].ToString(),
                            };
                            newQuestion.wrongAnswer.Add(newWrongAnswer);
                            w++;
                        }
                    }
                }
                closeConn();
            }
            return newQuestion;
        }
    }
}