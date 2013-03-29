using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SquirrelThePirate2
{
    public enum MenuChoices
    {
        Cancel,
        Submit,
        Restart
    }

    public partial class MenuForm : Form
    {
        private MenuChoices menuChoice  = MenuChoices.Cancel;

        public MenuForm()
        {
            InitializeComponent();
            PasswordStatusLabel.Text = "";
        }

        public MenuChoices MenuChoice { get { return menuChoice; }}
        public string Password { get { return PasswordTextBox.Text.Trim(); }}

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            menuChoice = MenuChoices.Cancel;
            this.Close();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (!LevelManager.IsPassword(this.Password))
            {
                PasswordStatusLabel.Text = "Invalid password";
                return;
            }
            menuChoice = MenuChoices.Submit;
            this.Close();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            menuChoice = MenuChoices.Restart;
            this.Close();
        }
    }
}
