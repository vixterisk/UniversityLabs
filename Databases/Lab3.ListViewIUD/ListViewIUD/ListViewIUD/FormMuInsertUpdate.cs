using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ListViewIUD
{
    public partial class FormMuInsertUpdate : Form
    {
        private int id;

        public enum FormType
        {
            Insert,
            Update
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public TextBox TbName
        {
            get { return tbName; }
            set { tbName = value; }
        }

        public string Login
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public string Password
        {
            get { return tbHead.Text; }
            set { tbHead.Text = value; }
        }

        public string Salt
        {
            get { return tbAddress.Text; }
            set { tbAddress.Text = value; }
        }

        public DateTime Reg_date
        {
            get { return dtpModifiedDate.Value; }
            set { dtpModifiedDate.Value = value; }
        }

        public Button BtOK
        {
            get { return btOK; }
            set { btOK = value; }
        }

        public ErrorProvider ErrorProvider
        {
            get { return errorProviderLogin; }
            set { errorProviderLogin = value; }
        }

        public FormMuInsertUpdate(FormType frmType, int id)
        {
            InitializeComponent();
            switch (frmType)
            {
                case FormType.Insert:
                    btOK.Text = @"Добавить";
                    break;
                case FormType.Update:
                    btOK.Text = @"Изменить";
                    break;
            }
            this.id = id;
        }

        private bool isChangesAllowed()
        {
            return UserTextCheck.LoginCheck(UserTextCheck.WhitespaceFormat(Login), id) && UserTextCheck.WhitespaceFormat(Password).Length >= 8;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (isChangesAllowed())
            { btOK.Enabled = true; errorProviderLogin.Clear(); return; }
            btOK.Enabled = false;
            if (!UserTextCheck.LoginCheck(UserTextCheck.WhitespaceFormat(Login), id))
                errorProviderLogin.SetError(tbName, "Такой логин недопустим (Только русско-английская раскладка, повторения логинов запрещены).");
            else
                errorProviderLogin.Clear();
        }

        private void tbHead_TextChanged(object sender, EventArgs e)
        {
            if (isChangesAllowed())
            { btOK.Enabled = true; errorProviderPassword.Clear();  return; }
            btOK.Enabled = false;
            if (UserTextCheck.WhitespaceFormat(Password).Length < 8)
                errorProviderPassword.SetError(tbHead, "Такой пароль недопустим (Требуется не менее 8 символов).");
            else
                errorProviderPassword.Clear();
        }
    }
}
