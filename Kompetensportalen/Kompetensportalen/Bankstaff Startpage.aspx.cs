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
            if (currentUser.qualified && currentUser.lastTestDate.Year == today.Year)
            {               
            }
            else
            {
                btnStartTest.Visible = false;
                renderTest(currentUser.newTest.questions);
                btnStopTest.Visible = true;
            }
        }
        #region Show Test on Page method
        private void renderTest(List<Question> inputList)
        {
            foreach (Question q in inputList)
            {
                System.Web.UI.WebControls.Label activeQuestion = new System.Web.UI.WebControls.Label();
                string catequest = q.category.ToString();

                if (q.category == 1)
                {
                    catequest = "Produkter och hantering av kundens affärer";
                    activeQuestion.Text = catequest + ". " + q.question;
                }
                else if (q.category == 2)
                {
                    catequest = "Ekonomi";
                    activeQuestion.Text = catequest + ". " + q.question;
                }
                else if (q.category == 3)
                {
                    catequest = "Etik och regelverk";
                    activeQuestion.Text = catequest + ". " + q.question;
                }

                pnlquestionWAnswer.Controls.Add(activeQuestion);

                int answerCount = 1;                

                //to get radiobuttons if there is only one correct answer
                if (q.answerList.FindAll(x => x.correct == true).ToList().Count == 1)
                {
                    RadioButtonList rbList = new RadioButtonList();

                    foreach (var answer in q.answerList)
                    {
                        ListItem li = new ListItem();

                        if (answer.text.IndexOf(".jpg") > 0)
                        {
                            li.Text = answerCount.ToString();
                            li.Value = answer.id;
                            li.Selected = answer.correct;
                            Image newImg = new Image();
                            newImg.ImageUrl = answer.text;

                            rbList.Items.Add(li);
                            pnlquestionWAnswer.Controls.Add(rbList);
                            pnlquestionWAnswer.Controls.Add(newImg);
                        }
                        else
                        {
                            li.Text = answer.text;
                            li.Value = answer.id;
                            li.Selected = answer.correct;

                            rbList.Items.Add(li);
                            pnlquestionWAnswer.Controls.Add(rbList);
                        }
                    }
                }
                //to get checkbuttons if there is more than one correct answer
                else
                {
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
                        }}}}}
        #endregion Show test on page method
        protected void btnStopTest_Click(object sender, EventArgs e)
        {
            //Här måste vi anropa metod för poängräkning och spara testet till användaren
        }
    }
}                
            








