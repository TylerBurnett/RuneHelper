﻿using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace RuneHelper
{
    public partial class SettingsForm : MetroForm
    {
        public SettingsForm()
        {
            InitializeComponent();
        }
        #region Form Load
        private void Form3_Load(object sender, EventArgs e)
        {
            UsernameTextBox.Text = MainForm.SaveData[0];
        }
        #endregion

        #region Form Controls
        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.SaveData[0] = UsernameTextBox.Text;
            API.StreamWriter(string.Join(",",MainForm.SaveData), @"C:\Users\" + Environment.UserName + @"\AppData\Local\RsThing\Data.txt");
            MainForm.SaveData[0] = UsernameTextBox.Text;
            this.Close();
        }
        #endregion

        private void DeleteXP_Click(object sender, EventArgs e)
        {
            
            if(MetroMessageBox.Show(this, "Clicking ok will delete all cached data stored over this last month. Are you sure? ", "Delete saved XP data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int count = 2;

                while (count < MainForm.SaveData.Length)
                {
                    MainForm.SaveData[count] = "0";
                    count++;
                }
            }
        }
    }
}