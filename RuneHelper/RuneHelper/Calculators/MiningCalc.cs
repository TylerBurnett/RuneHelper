﻿using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace RuneHelper
{
    public partial class MiningCalc : MetroForm
    {
        public MiningCalc()
        {
            InitializeComponent();
        }

        private string result;
        private string[] MArray;
        private string[] LevelArray;

        public static float[] OreXPArray = new float[] { 5, 17.5f, 25, 26.5f, 35, 40, 50, 62.4702f, 40, 65, 60, 80, 95, 90, 50, 65, 70, 125, 296.7f, 445.5f, 692.5f, 5250 };

        #region Load And Close Functions

        private void MiningCalc_Load(object sender, EventArgs e)
        {
            StyleManager.Theme = MainForm.data.Theme;
            StyleManager.Style = MainForm.data.Colour;
            this.Theme = StyleManager.Theme;
        }

        #endregion Load And Close Functions

        #region Form Buttons

        private void CharSearchButton_Click(object sender, EventArgs e)
        {
            string Name = CharNameBox.Text;
            string result = GetPlayerMiningXP(Name);
            XPBox.Text = result;
            ShowBoxes();
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            if (API.IntParse(MArray[1]) < API.IntParse(TargetBox.Text))
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
            CharNameBox.Text = MainForm.data.Name;
            result = GetPlayerMiningXP(MainForm.data.Name);
            XPBox.Text = result;
            ShowBoxes();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Form Buttons

        #region Functions

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
                LevelArray = API.GetStats(Name);
                MArray = LevelArray[15].Split(',');
                return MArray[2];
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

                int XP = API.IntParse(XPBox.Text) + API.IntParse(BonusBox.Text);

                NeededXP = API.LevelXpArray[API.IntParse(TargetBox.Text)] - XP;

                AmountBox.Text = Convert.ToString(Math.Round(NeededXP / OreXPArray[TypeBox.SelectedIndex]));
                AmountBox.Visible = true;
            }
            catch
            {
                MessageBox.Show("Could not run calculation, please check all fields have been filled with no text", "ERROR");
            }
        }

        #endregion Functions
    }
}