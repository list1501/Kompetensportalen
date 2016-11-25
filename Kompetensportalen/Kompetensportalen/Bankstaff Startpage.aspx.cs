using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using System.Web.UI.HtmlControls;


namespace Kompetensportalen
{
    public partial class Bankstaff_Startpage : System.Web.UI.Page
    {
        User currentUser = Loginpage.currentLogin;
        DateTime today = DateTime.Today;
        SQL newSQL = new SQL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if logged in user is qualified and load correct test to object
            if (currentUser.qualified && currentUser.lastTestDate.Year == today.Year)
            {
                btnStartTest.Text = "Titta på senaste testet";
                currentUser.getLastTest();
            }
            else if (currentUser.qualified && currentUser.lastTestDate.Year != today.Year)
            {
                btnStartTest.Text = "Starta ÅKU-test";
                currentUser.getNewTest(2);
            }
            else if (!currentUser.qualified) //Här kan vi lägga till en metodjämförelse om vi vill för att kolla om den som inte är godkänd får göra provet än. /Martin
            {
                btnStartTest.Text = "Starta licensieringstest";
                currentUser.getNewTest(1);
            }

            renderTest(currentUser.newTest.questions);
        }

        protected void btnStartTest_Click(object sender, EventArgs e)
        {               
            //if (currentUser.qualified && currentUser.lastTestDate.Year == today.Year)
            //{
            //    //Do the method for showing tests instead              
            //}
            //else
            //{
            //    btnStartTest.Visible = false;
            //    renderTest(currentUser.newTest.questions);
            //    btnStopTest.Visible = true;
            //}
        }
        #region Show Test on Page method
        private void renderTest(List<Question> inputList)
        {
            foreach (Question q in inputList)
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("ID", q.id.ToString());

                Label activeCategory = new Label();
                Label activeQuestion = new Label();
                string catequest;

                if (q.category == 1)
                {
                    catequest = "Produkter och hantering av kundens affärer. ";
                }
                else if (q.category == 2)
                {
                    catequest = "Ekonomi. ";
                }
                else
                {
                    catequest = "Etik och regelverk. ";
                }
                activeCategory.Text = catequest;
                activeQuestion.Text = q.question;
                div.Controls.Add(activeCategory);
                div.Controls.Add(activeQuestion);

                testContent.Controls.Add(div);

                //Get radiobuttons if there is only one correct answer
                if (q.answerList.FindAll(x => x.correct == true).ToList().Count == 1)
                {
                    RadioButtonList rbList = new RadioButtonList();
                    rbList.ID = q.id.ToString();

                    foreach (var answer in q.answerList)
                    {
                        ListItem li = new ListItem();

                        if (answer.text.IndexOf(".jpg") > 0)
                        {
                            Image newImg = new Image();
                            newImg.ImageUrl = answer.text;
                            li.Value = answer.id;
                            li.Text = "<img href='" + newImg.ImageUrl + "'>";

                           
                            rbList.Items.Add(li);
                            div.Controls.Add(rbList);
                            div.Controls.Add(newImg);
                        }
                        else
                        {
                            li.Text = answer.text;
                            li.Value = answer.id;

                            rbList.Items.Add(li);
                            div.Controls.Add(rbList);
                        }
                    }
                }
                //Get checkbuttons if there is more than one correct answer
                else
                {
                    CheckBoxList cbList = new CheckBoxList();
                    cbList.ID = q.id.ToString();

                    foreach (var answer in q.answerList)
                    {
                        ListItem li = new ListItem();

                        if (answer.text.IndexOf(".jpg") > 0)
                        {
                            Image newImg = new Image();
                            newImg.ImageUrl = answer.text;
                            li.Value = answer.id;
                            li.Text = "<img href='" + newImg.ImageUrl + "'>";
                            //li.Value = answer.id;
                            //Image newImg = new Image();
                            //newImg.ImageUrl = answer.text;

                            cbList.Items.Add(li);
                            div.Controls.Add(cbList);
                        }
                        else
                        {
                            li.Text = answer.text;
                            li.Value = answer.id;

                            cbList.Items.Add(li);
                            div.Controls.Add(cbList);
                        }
                    }
                }
            }
        }

        #endregion Show test on page method

        #region End test and save to file
        protected void btnStopTest_Click(object sender, EventArgs e)
        {
            //Run method to add user's answers to correct questions
            addAnswersToQuestion();

            //Run method to export test-xml to DB
            newSQL.saveTestToDB(currentUser.newTest);

            //Check if user is now allowed to see last test and show button with appropriate text
            //Also pre-load appropriate test for user
            if (currentUser.qualified)
            {            
                btnStartTest.Text = "Titta på senaste testet";
                currentUser.getLastTest();
            }
            else if (!currentUser.qualified)
            {
                btnStartTest.Text = "Starta licensieringstest";
                currentUser.getNewTest(1);
            }
            
        }

        //Method to add user's answers to correct questions
        public void addAnswersToQuestion()
        {            
            List<Question> questions = currentUser.newTest.questions;
            int c = questions.Count();

            for (int i = 0; i < c; i++)
            {
                Question currentQuestion = questions[i];
                List<Answer> currentAnswers = currentQuestion.answerList;
                List<Answer> userAnswers = new List<Answer>();

                int qId = questions[i].id;
                Control div = FindControl(qId.ToString());
                
                foreach (Control ctrl in div.Controls)
                {
                    if (ctrl is RadioButtonList)
                    {
                        RadioButtonList rblist = (RadioButtonList)ctrl;

                        for (int rb = 0; rb < rblist.Items.Count; rb++)
                        {
                            if (rblist.Items[rb].Selected)
                            {
                                ListItem li = rblist.Items[rb];
                                Answer currentAnswer = currentAnswers[rb];
                                Answer userAnswer = new Answer()
                                {
                                    id = li.Value,
                                    correct = currentAnswer.correct,
                                    text = currentAnswer.text
                                };
                                userAnswers.Add(userAnswer);
                            }
                        }
                    }
                    else if (ctrl is CheckBoxList)
                    {
                        CheckBoxList cblist = (CheckBoxList)ctrl;

                        for (int cb = 0; cb < cblist.Items.Count; cb++)
                        {
                            if (cblist.Items[cb].Selected)
                            {
                                ListItem li = cblist.Items[cb];
                                Answer currentAnswer = currentAnswers[cb];
                                Answer userAnswer = new Answer()
                                {
                                    id = li.Value,
                                    correct = currentAnswer.correct,
                                    text = currentAnswer.text
                                };
                                userAnswers.Add(userAnswer);
                            }
                        }
                    }
                    currentQuestion.userAnswerList = userAnswers;
                }
                questions[i] = currentQuestion;
            }
            currentUser.newTest.questions = questions;

            System.Diagnostics.Debug.WriteLine(currentUser.newTest.questions[4].userAnswerList[0].id);
        }

        public void addAnswersToXML()
        {
            XmlDocument doc = currentUser.newTest.sourceFile;
            XmlNodeList xQlist = doc.SelectNodes("Test/question");

            int c = currentUser.newTest.questions.Count;

            for (int i = 0; i < c; i++)
            {
                Question q = currentUser.newTest.questions[i];
                int qID = q.id;


            }
        }

        #endregion End test and save to file
    }
}                
            








