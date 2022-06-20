using Microsoft.Data.Sqlite;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TreeViewDatabase
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            InitializeCatsTree();
        }

        private void InitializeCatsTree()
        {
            var sConnectionStr = new SQLiteConnectionStringBuilder();
            sConnectionStr.DataSource = @"C:\Users\vixterisk\db.sqlite";
            sConnectionStr.ForeignKeys = true;
            var connStr = sConnectionStr.ConnectionString;
            using (var sConnection = new SQLiteConnection(connStr))
            {
                sConnection.Open();
                var sCommand = new SQLiteCommand();
                sCommand.Connection = sConnection;
                sCommand.CommandText = @"
                SELECT subfamily.name AS subf_name, subfamily.id AS subf_id, genus.name AS genus_name, genus.id AS genus_id, species.name AS species_name, species.id AS species_id
                FROM subfamily 
                    LEFT OUTER JOIN genus on subfamily.id = genus.subfamily_id
                        LEFT OUTER JOIN species on genus.id = species.genus_id
                ORDER BY  subfamily.name, subfamily.id,  genus.name, genus.id, species.name,species.id
                ";
                using (var reader = sCommand.ExecuteReader())
                {
                    long lastSubfamilyId = -1, lastGenusId = -1, lastSpeciesId = -1;
                    TreeNode subfamilyNode = null, genusNode = null;
                    while (reader.Read())
                    {
                        var subfamilyName = (string)reader["subf_name"];
                        long subfamilyId = (long)reader["subf_id"];
                        var genusName = reader["genus_name"];
                        var genusId = reader["genus_id"];
                        var speciesName = reader["species_name"];
                        var speciesId = reader["species_id"];
                        if (subfamilyId != lastSubfamilyId)
                        {
                            subfamilyNode = catsTree.Nodes.Add(subfamilyName);
                            lastSubfamilyId = subfamilyId;
                        }
                        if (!(genusId is DBNull))
                        {
                            if ((long)genusId != lastGenusId)
                            {
                                genusNode = subfamilyNode.Nodes.Add($"{(string)genusName}");
                                lastGenusId = (long)genusId;
                            }
                            if (!(speciesId is DBNull))
                                if ((long)speciesId != lastSpeciesId)
                                {
                                    genusNode.Nodes.Add($"{(string)speciesName}");
                                    lastSpeciesId = (long)speciesId;
                                }
                        }
                    }
                }
            };
        }

        private void catsTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            var curNode = e.Node;
            curNode.Text = curNode.Text.Split(new Char[] { '–' })[0];
        }

        private void catsTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var curNode = e.Node;
            if (curNode.Parent is null)
            {
                var singleChildNodesCount = 0;
                foreach (TreeNode childNode in curNode.Nodes)
                    if (childNode.Nodes.Count == 0) singleChildNodesCount++;
                var prevText = curNode.Text;
                var nodesWithChildren = curNode.Nodes.Count - singleChildNodesCount;
                var text = prevText + " – " + nodesWithChildren + @" / " + singleChildNodesCount;
                curNode.Text = text;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
