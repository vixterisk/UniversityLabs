using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GistForms
{
    public partial class FormAuth : Form
    {
        List<Gist> Gists;
        public FormAuth()
        {
            InitializeComponent();
            ButtonsProgramStart();
            TryLogIn();
        }

        void ButtonsProgramStart()
        {
            loginButton.Enabled = true;
            addButton.Enabled = deleteButton.Enabled = patchButton.Enabled = logoutButton.Enabled = false;
        }
        void ButtonsProgramAuthorized()
        {
            addButton.Enabled = deleteButton.Enabled = patchButton.Enabled = logoutButton.Enabled = true;
            loginButton.Enabled = false;
        }
        private async Task TryLogIn()
        {
            if (!string.IsNullOrEmpty(ConnectionHandler.Token))
            {
                await Refill_ListViewAsync();
                ButtonsProgramAuthorized();
            }
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            await Authorize();
            if (string.IsNullOrEmpty(ConnectionHandler.Token))
                MessageBox.Show("Авторизация не удалась");
        }

        async Task Authorize()
        {
            var handler = new OAuthAuthorizationHandler(ConnectionHandler.clientID, ConnectionHandler.clientSecret);
            await handler.GetToken();
            await TryLogIn();
        }

        private async Task Refill_ListViewAsync()
        {
            GistLV.Items.Clear();
            var lvItemsList = new List<ListViewItem>();
            Gists = await GistClient.GetGists();
            foreach (var gist in Gists)
            {
                var item = new ListViewItem(gist.id);
                item.SubItems.Add(gist.description);
                lvItemsList.Add(item);
            }
            GistLV.Items.AddRange(lvItemsList.ToArray());
        }

        private Tuple<string, string>[] FilesToTuples(FormAddChange form, List<GistFile> files)
        {
            var tuples = new List<Tuple<string, string>>();
            foreach (var file in files)
                tuples.Add(Tuple.Create(file.filename, file.content));
            return tuples.ToArray();
        }

        private async void AddButton_ClickAsync(object sender, EventArgs e)
        {
            var form = new FormAddChange();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.GistPatched.files.RemoveAll(x => x.content == "");
                await GistClient.CreateGist(form.GistPatched.description, true, FilesToTuples(form, form.GistPatched.files));
                await Refill_ListViewAsync();
            }
            form.Close();
        }
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (GistLV.SelectedItems.Count > 0)
            {
                var item = GistLV.SelectedItems[0];
                await GistClient.DeleteGist(item.Text);
                await Refill_ListViewAsync();
            }
        }

        private async void PatchButton_ClickAsync(object sender, EventArgs e)
        {
            if (GistLV.SelectedIndices.Count > 0)
            {
                var gist = Gists[GistLV.SelectedIndices[0]];
                var form = new FormAddChange(gist);
                if (form.ShowDialog() == DialogResult.OK)
                    await GistClient.EditGist(form.GistPatched.id, form.GistPatched.description, FilesToTuples(form, gist.files));
                form.Close();
                await Refill_ListViewAsync();
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            ButtonsProgramStart();
            GistLV.Items.Clear();
            ConnectionHandler.RemoveTokenHeader();
        }
    }
}
