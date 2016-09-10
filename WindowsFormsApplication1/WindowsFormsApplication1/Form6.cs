﻿using System;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace RsThing
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        string result;
        string[] AgilArray;
        string[] LevelArray;


        public static float[] CourseXPArray = new float[] { 79.5f, 86.5f, 750, 1014, 775, 139.5f, 580, 571.5f, 380, 540, 704, 880, 1056, 1184, 1328, 2375, 725, 740.7f};
         /// <summary>
         /// Forms Buttons
         /// </summary>
        private void CharSearchButton_Click(object sender, EventArgs e)
        {
            string Name = CharNameBox.Text;
            string result = GetPlayerMiningXP(Name);
            XPBox.Text = result;
            ShowBoxes();
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(AgilArray[1]) < Convert.ToInt32(TargetBox.Text))
            {
                Calculate();
            }
            else
            {
                MessageBox.Show("Target Level Cant be under or same as current level", "ERROR");
            }
            
        }

        private void CachedName_Click(object sender, EventArgs e)
        {
            CharNameBox.Text = MainForm.Username;
            result = GetPlayerMiningXP(MainForm.Username);
            XPBox.Text = result;
            ShowBoxes();
        }

        /// <summary>
        /// Functions
        /// </summary>
        private void ShowBoxes()
        {
            TargetLevelLabel.Visible = true;
            TargetBox.Visible = true;
            BonusXPlabel.Visible = true;
            BonusBox.Visible = true;
            TypeBox.Visible = true;
            CalculateBtn.Visible = true;
        }

        private String GetPlayerMiningXP(string Name)
        {
            try
            {
                string url = "http://services.runescape.com/m=hiscore/index_lite.ws?player=X";
                string NewUrl = url.Replace("X", Name);
                string textFromFile = (new System.Net.WebClient()).DownloadString(NewUrl);
                LevelArray = textFromFile.Split('\n');
                AgilArray = LevelArray[17].Split(',');
                return AgilArray[2];
            }
            catch
            {
                MessageBox.Show("Username was not found in the runescape highscores. Or highscores or are offline", "ERROR");
            }
            return null;
        }

        private void Calculate()
        {
            try
            {
                double NeededXP;

                int XP = Convert.ToInt32(XPBox.Text) + Convert.ToInt32(BonusBox.Text);


                NeededXP = API.LevelXpArray[Convert.ToInt32(TargetBox.Text)] - XP;


                AmountBox.Text = Convert.ToString(Math.Round(NeededXP / CourseXPArray[TypeBox.SelectedIndex]));
                AmountBox.Visible = true;
                LapsLabel.Visible = true;
            }
            catch
            {
                MessageBox.Show("Could not run calculation, please check all fields have been filled with no text", "ERROR");
            }
                
            }    
        

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
