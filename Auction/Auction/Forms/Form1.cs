using Auction.Forms;
using System;
using System.Windows.Forms;

namespace Auction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            sellsToolStripMenuItem.Visible = false;
            btnSignIn.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult x = MessageBox.Show("Вы действительно хотите закрыть приложение?",
                 "Выйти", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (x == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            SignIn sign = new SignIn(this);
            sign.Show();
        }

        private void objectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Objects objects = new Objects();
            objects.Show();
        }

        public void ShowSell()
        {
            sellsToolStripMenuItem.Visible = true;
        }
        public void SignInEnabled()
        {
            btnSignIn.Enabled = false;
        }

        private void contactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contacts contacts = new Contacts();
            contacts.Show();
        }

        private void sellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sells sells = new Sells();
            sells.Show();
        }
    }
}
