using Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Auction.Forms
{
    public partial class SignIn : Form
    {
        private Form1 mainForm;
        public SignIn(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtLogin.Text;
            string password = txtPassword.Text;

            try
            {
                List<User> users = Program.context.Users.ToList();
                User u = users.FirstOrDefault(p => p.UserName == username && p.Password == password);

                if (u != null)
                {
                    mainForm.ShowSell();
                    this.Hide();
                    mainForm.SignInEnabled();
                    txtPassword.Clear();
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void pictureBoxUnVisible_Click(object sender, EventArgs e)
        {
            pictureBoxUnVisible.Visible = false;
            pictureBoxVisible.Visible = true;
            txtPassword.PasswordChar = '*';
        }

        private void pictureBoxVisible_Click(object sender, EventArgs e)
        {
            pictureBoxUnVisible.Visible = true;
            pictureBoxVisible.Visible = false;
            txtPassword.PasswordChar = '\0';
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
