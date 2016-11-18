using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Kompetensportalen
{
    public partial class Bankstaff_Startpage : System.Web.UI.Page
    {
        User currentUser = Loginpage.currentLogin;
        DateTime today = DateTime.Today;

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
            else if (currentUser.qualified == false) //Här kan vi lägga till en metodjämförelse om vi vill för att kolla om den som inte är godkänd får göra provet än. /Martin
            {
                btnStartTest.Text = "Starta licensieringstest";
                currentUser.getNewTest(1);
            }
        }

        protected void btnStartTest_Click(object sender, EventArgs e)
        {
            btnStartTest.Visible = false;

            TimerforTest.Enabled = true;
            TimerforTest.Interval = 300000;
            double counter = TimerforTest.Interval;

            //counter++;
            //if (counter >= 300000)
            //{                
            //   MessageBox.Show("Tiden är slut!");
            //}
         
            if (currentUser.qualified && currentUser.lastTestDate.Year == today.Year)
            {
                
            }
            else
            {
                renderTest(currentUser.newTest.questions);
                btnStopTest.Visible = true;
            }
           
            
            //when pressing button "End test" timer is stopped;
            //if (btnStopTest_Click = true)
            //{
            //    TimerforTest.Enabled = false;
            //}
        } 

        private void renderTest(List<Question> inputList)
        {
            foreach (Question q in inputList)
            {
                System.Web.UI.WebControls.Label activeQuestion = new System.Web.UI.WebControls.Label();
                activeQuestion.Text = q.question;
                pnlquestionWAnswer.Controls.Add(activeQuestion);
                int answerCount = 1;

                //to get radiobuttons if there is only one correct answer

                //if (q.answerList.FindAll(x => x.correct == true).ToList().Count = 1)
                //{
                //    rbList = new RadioButtonList();
                //}
                CheckBoxList cbList = new CheckBoxList();

                foreach (var answer in q.answerList)
                {         
                        ListItem li = new ListItem();

                        if (answer.text.IndexOf(".jpg")>0)
                        {
                            li.Text = answerCount.ToString();
                            li.Value = answer.id;
                            li.Selected = answer.correct;
                            Image newImg = new Image();
                            newImg.ImageUrl = answer.text;

                            cbList.Items.Add(li);
                            pnlquestionWAnswer.Controls.Add(cbList);
                            pnlquestionWAnswer.Controls.Add(newImg);
                        }
                        else
                        {
                            li.Text = answer.text;
                            li.Value = answer.id;
                            li.Selected = answer.correct;

                            cbList.Items.Add(li);
                            pnlquestionWAnswer.Controls.Add(cbList);
                        }                                          
                }                
            }
        }

        protected void btnStopTest_Click(object sender, EventArgs e)
        {
            //Här måste vi anropa metod för poängräkning och spara testet till användaren
        }
    }
}                
            








