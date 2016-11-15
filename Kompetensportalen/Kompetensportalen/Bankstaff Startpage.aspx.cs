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
            TimerforTest.Enabled = true;
            TimerforTest.Interval = 300000;
            double counter = TimerforTest.Interval;

            counter++;
            if (counter >= 300000)
            {                
               MessageBox.Show("Tiden är slut!");
            }


            //when pressing button "End test" timer is stopped;
            //if (btnStopTest_Click = true)
            //{
            //    TimerforTest.Enabled = false;
            //}
       } 

        public void DisplayTest(Question test, int questionnumber)
        {
            
            labelQuestion.Text = "";

            //rBAnswers.Items.Clear();
            chBAnswers.Items.Clear();

            var currentQuestion = test.question[questionnumber];

            //if (NrOfCorrectAnswersForQuest(currentQuestion) == 1) //Finns det bara 1 rätt svar blir det radiobutton
            //{
            //    foreach (var answer in currentQuestion.answers)
            //    {
            //        rBAnswers.Items.Add(answer.AnswerText);

            //    }
            //}
            //else if (NrOfCorrectAnswersForQuest(currentQuestion) > 1) //Finns det fler än 1 rätt svar blir det checkbox
            //{
            //    foreach (var answer in currentQuestion.answers)
            //    {
            //        chBAnswers.Items.Add(answer.AnswerText);
            //    }
            //    labelQuestion.Text = "ID: " + currentQuestion.id +  "Fråga: " + currentQuestion.text;
        }

        public void showTest()
        {
            int c = currentUser.newTest.questions.Count;

            for (int i = 0; i < c; i++)
            {
                Question q = currentUser.newTest.questions[i];

            }
        }
    }
}
    
    





