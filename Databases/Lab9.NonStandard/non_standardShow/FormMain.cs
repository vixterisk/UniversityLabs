using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using Npgsql;
using System.Text.RegularExpressions;

namespace non_standart
{
    public partial class FormMain : Form
    {
        public static List<Field> fieldsList = new List<Field>(); //коллекция с инф о всех полях
        public static List<Field> selectedFields = new List<Field>(); //коллекция с инф о выбранных полях
        public static List<Condition> conditionList = new List<Condition>(); // коллекция с условиями
        public static List<Relation> allRelations = new List<Relation>(); //коллекция со всеми связями таблиц по внешнему ключу
        public static List<string> tablesList = new List<string>(); //все таблицы
        public static List<Relation> neededRelations = new List<Relation>(); //нужные связи
        public static List<string> neededTables = new List<string>(); //нужные таблицы
        public static List<FieldOrder> selectionOrder = new List<FieldOrder>(); //поля, выбранные в каком-либо порядке
        public FormMain()
        {
            InitializeComponent();
            fillFieldsList();
            FillRelations();
        }

        void fillFieldsList()
        {
            using (NpgsqlConnection sConn = Program.npgLogin())
            {
                sConn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select  helpers.fields.id as id, helpers.fields.field_name as f_name, helpers.fields.table_name as t_name, DATA_TYPE as f_type, helpers.fields.transl_fn as headerText, helpers.fields.category_name as category " +
                                                " from INFORMATION_SCHEMA.COLUMNS, helpers.fields " +
                                                " where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = helpers.fields.table_name " +
                                                " and INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = helpers.fields.field_name", sConn);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    fieldsList.Add(new Field((int)reader["id"], (string)reader["f_name"], (string)reader["t_name"], (string)reader["f_type"], (string)reader["headerText"], (string)reader["category"]));
                    if (!tablesList.Contains((string)reader["t_name"]))
                        tablesList.Add((string)reader["t_name"]);
                }
                reader.Close();

                //command = new NpgsqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '" + ConfigurationManager.AppSettings["database"] + "'", sConn);
                //reader = command.ExecuteReader();

                //while (reader.Read())
                //{
                //    tablesList.Add((string)reader["TABLE_NAME"]);
                //}
                //reader.Close();

                fieldsList.Sort(delegate (Field f1, Field f2)
                { return f1.Category.CompareTo(f2.Category); });
                string category = "";
                foreach (Field curField in fieldsList)
                {
                    if (curField.Category == category)
                        listBoxFields.Items.Add(curField.HeaderText);
                    else
                    {
                        category = curField.Category;
                        listBoxFields.Items.Add("-----" + category + "-----");
                        listBoxFields.Items.Add(curField.HeaderText);
                    }
                    comboBoxFields.Items.Add(curField.HeaderText + " (" + curField.Category + ") ");

                }
                sConn.Close();
            }
        }

        void FillRelations()
        {
            using (NpgsqlConnection sConn = Program.npgLogin())
            {
                sConn.Open();
                //NpgsqlCommand comm = new NpgsqlCommand("select kcu.table_name as FK_Table, " +
                //"string_agg(kcu.column_name, ', ') as Field_in_FK, " +
                //"rel_tco.table_name as PK_Table, " +
                //"kcu.column_name as Field_in_PK " +
                //"from information_schema.table_constraints tco " +
                //"join information_schema.key_column_usage kcu " +
                //"on tco.constraint_schema = kcu.constraint_schema " +
                //"and tco.constraint_name = kcu.constraint_name " +
                //"join information_schema.referential_constraints rco " +
                //"on tco.constraint_schema = rco.constraint_schema " +
                //"and tco.constraint_name = rco.constraint_name " +
                //"join information_schema.table_constraints rel_tco " +
                //"on rco.unique_constraint_schema = rel_tco.constraint_schema " +
                //"and rco.unique_constraint_name = rel_tco.constraint_name " +
                //@"where tco.constraint_type = 'FOREIGN KEY' " +
                //"group by kcu.table_schema, " +
                //"kcu.table_name, " +
                //"rel_tco.table_name, " +
                //"rel_tco.table_schema, " +
                //"kcu.column_name " +
                //"order by kcu.table_schema, " +
                //"kcu.table_name; ", sConn);
                NpgsqlCommand comm = new NpgsqlCommand("select table1, table2, relations, via from helpers.rel_table;", sConn);
                NpgsqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["relations"] != DBNull.Value)
                    {
                        var table1Key = GetFieldName((string)reader["relations"], (string)reader["table1"]);
                        var table2Key = GetFieldName((string)reader["relations"], (string)reader["table2"]);
                        allRelations.Add(new Relation((string)reader["table1"], (string)reader["table2"], table1Key, table2Key));
                    }
                    //all_rel_s.Add(new relation( (string)reader["FK_Table"], (string)reader["PK_Table"], (string)reader["Field_in_FK"], (string)reader["Field_in_PK"]));
                }
                reader.Close();
                sConn.Close();
            }
        }

        static string GetFieldName(string relationString, string tableName)
        {
            relationString = relationString.Replace((string)tableName + ".", "?");
            var startIndex = relationString.IndexOf("?") + 1;
            int Length = 0;
            while (startIndex + Length < relationString.Length && relationString[startIndex + Length] != ' ' && relationString[startIndex + Length] != '=')
                Length++;
            return relationString.Substring(startIndex, Length);
        }

        //нахождение пути соединения с помощью алгоритма дийкстры
        static void SPF(List<Relation> allRelations, List<string> tablesList, string start)
        {

            bool[] used = new bool[tablesList.Count];
            List<Node> nodeList = new List<Node>();
            foreach (string table in tablesList)
            {
                nodeList.Add(new Node(table));
            }
            for (int i = 0; i < nodeList.Count; i++)
            {
                used[i] = false;
              
                if (nodeList[i].Name == start)
                    nodeList[i].Path_length = 0;
            }
            for (int i = 0; i < nodeList.Count; i++)
            {
                int v = -1;
                for (int j = 0; j < nodeList.Count; j++)
                    if (!used[j] && (v < 0 || nodeList[j].Path_length < nodeList[v].Path_length))
                        v = j;
                if (nodeList[v].Path_length == 1000)
                    break;
                used[v] = true;

                foreach (Relation r in allRelations)
                {

                    if (r.PK_table == nodeList[v].Name)
                    {

                        if (nodeList[v].Path_length + 1 < nodeList.Find(x => x.Name == r.FK_table).Path_length)
                        {
                            nodeList.Find(x => x.Name == r.FK_table).Path_length = nodeList[v].Path_length + 1;
                            nodeList.Find(x => x.Name == r.FK_table).Last_path = nodeList[v].Name;
                        }
                    }
                    if (r.FK_table == nodeList[v].Name)
                    {

                        if (nodeList[v].Path_length + 1 < nodeList.Find(x => x.Name == r.PK_table).Path_length)
                        {
                            nodeList.Find(x => x.Name == r.PK_table).Path_length = nodeList[v].Path_length + 1;
                            nodeList.Find(x => x.Name == r.PK_table).Last_path = nodeList[v].Name;
                        }
                    }
                }
            }
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].Name != start && neededTables.Find(x=>x==nodeList[i].Name)!=null)
                {
                   
                    AddRelation(nodeList, start, nodeList[i].Name);
                }
            }
        }

        //возвращение по найденному пути и добавление связей, если они еще не добавлены
        static void AddRelation(List<Node> v_s, string start, string end)
        {
            string cur = end;

            while (cur != start)
            {
                var curNode = v_s.Find(x => x.Name == cur);
                if (curNode == null) break;
                string t = curNode.Last_path;
                if (neededRelations.Find(x =>x.PK_table == cur && x.FK_table == t) == null && neededRelations.Find(x =>x.FK_table == cur && x.PK_table == t) == null)
                {
                    if (allRelations.Find(x => x.PK_table == cur && x.FK_table == t) != null)
                        neededRelations.Add(allRelations.Find(x => x.PK_table == cur && x.FK_table == t));
                    if (allRelations.Find(x => x.FK_table == cur && x.PK_table == t) != null)
                        neededRelations.Add(allRelations.Find(x => x.FK_table == cur && x.PK_table == t));
                }
                cur = t;
            }
        }

        private void AddQueryButton_Click(object sender, EventArgs e)
        {
            if (listBoxFields.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не выбрали поле", "Ошибка добавления поля"); return;
            }
            string str = listBoxFields.SelectedItem.ToString();
            if (str[0] == '-')
            {
                MessageBox.Show("Нельзя выбрать название категории", "Ошибка добавления поля"); return;
            }
            string cat  = "";
            int j = 0;
            while (j < listBoxFields.SelectedIndex)
            {
                string s = (string)listBoxFields.Items[j];
                if (s[0] == '-' && s[s.Length - 1] == '-')
                    cat = s.Substring(5, s.Length - 10); 
                j++;
            }
            if (selectedFields.Find(x => x.HeaderText == str && x.Category==cat) != null)
            {
                {
                    MessageBox.Show("Это поле уже выбрано", "Ошибка добавления поля"); return;
                }
            }
            int index = 0;
            int i = 0;
            while (i != listBoxFields.SelectedIndex)
            {
                string s = (string)listBoxFields.Items[i];
                if (s[0] != '-' && s[s.Length - 1] != '-')
                    index++;
                i++;
            }
            selectedFields.Add(fieldsList[index]);
            listBoxSelectedFields.Items.Add(str + "(" + cat + ")");
            listBoxAllOrder.Items.Add(str + "(" + cat + ")");
        }

        private void RemoveFieldButton_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedFields.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не выбрали поле", "Ошибка удаления поля"); return;
            }

            int index = listBoxSelectedFields.SelectedIndex;
            listBoxAllOrder.Items.Remove(listBoxAllOrder.Items[index]);
            string removalCandidte = "";
            bool removeFlag = false;
            foreach (string curOrder in listBoxSelectedOrder.Items)
            {
                string s1 = Convert.ToString(curOrder);
                string s2 = (Convert.ToString(listBoxSelectedFields.Items[index]));
                if ((s1 == (s2)) || (s1 == (s2)))
                {
                    removalCandidte = curOrder;
                    removeFlag = true;
                }
            }
            if (removeFlag)
            {
                selectionOrder.Remove(selectionOrder[listBoxSelectedOrder.Items.IndexOf(removalCandidte)]);
                listBoxSelectedOrder.Items.Remove(removalCandidte);
            }
            listBoxSelectedFields.Items.Remove(listBoxSelectedFields.Items[index]); 
            selectedFields.Remove(selectedFields[index]);
            
        }

        private void SelectAllFieldsButton_Click(object sender, EventArgs e)
        {
            foreach (Field field in fieldsList)
            {
                if (selectedFields.Find(x => x.HeaderText == field.HeaderText) == null)
                {
                    selectedFields.Add(field);
                    listBoxSelectedFields.Items.Add(field.HeaderText + "(" + field.Category + ")");
                    listBoxAllOrder.Items.Add(field.HeaderText + "(" + field.Category + ")");

                }
                
            }

        }

        private void RemoveOldFieldsButton_Click(object sender, EventArgs e)
        {
            listBoxAllOrder.Items.Clear();
            selectionOrder.Clear();
            listBoxSelectedOrder.Items.Clear();
            int i = listBoxSelectedFields.Items.Count - 1;
            while (i >= 0)
            {
                listBoxSelectedFields.Items.Remove(listBoxSelectedFields.Items[i]);
                selectedFields.Remove(selectedFields[i]);
                i--;
            }

        }

        private void ComboBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            comboBoxValue.Text="";
            
            comboBoxCriterias.Items.Clear();
            Field cur = fieldsList[comboBoxFields.SelectedIndex];
            if(cur.Type == "date")
            {
                dateTimePickerValue.Text = "";
                comboBoxValue.Text = Convert.ToString(dateTimePickerValue.Value.ToShortDateString());
                dateTimePickerValue.Visible = true;
                comboBoxValue.Visible = false;
            }
            else
            {
                dateTimePickerValue.Visible = false;
                comboBoxValue.Visible = true;
            }
                if (cur.Type != "character varying")
                foreach (string str in Program.criterias)
                {
                    comboBoxCriterias.Items.Add(str);
                }
                else
                foreach (string str in Program.varcharCriterias)
                {
                    comboBoxCriterias.Items.Add(str);
                }
            using (NpgsqlConnection sConn = Program.npgLogin())
            {
                sConn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select distinct \"" + cur.Name + "\" as field from \"" + cur.Table + "\"", sConn);
                NpgsqlDataReader reader = command.ExecuteReader();
                comboBoxValue.Items.Clear();
                while (reader.Read())
                {
                    comboBoxValue.Items.Add(reader["field"]);
                }
                reader.Close();
                sConn.Close();
            }
        }

        private void AddConditionButton_Click(object sender, EventArgs e)
        {
            
            if (comboBoxFields.Text == "" || comboBoxCriterias.Text == "" || comboBoxValue.Text == "" || comboBoxConjunction.SelectedIndex < 0)
            {
                MessageBox.Show("Не все поля заполнены"); return;
            }
            if (comboBoxConjunction.Text == "-")
                AddConditionButton.Enabled = false;
            String fieldString = comboBoxFields.Text;
            String criteriaString = comboBoxCriterias.Text;
            String valueString = comboBoxValue.Text;
            String conjunctionString = comboBoxConjunction.Text;
            ListViewItem lvi = new ListViewItem(fieldString);
            lvi.SubItems.Add(criteriaString);
            lvi.SubItems.Add(valueString);
            lvi.SubItems.Add(conjunctionString);
            listViewCondition.Items.Add(lvi);
            conditionList.Add(new Condition(fieldsList[comboBoxFields.SelectedIndex],criteriaString,valueString,conjunctionString));
        }

        private void RemoveConditionButton_Click(object sender, EventArgs e)
        {
            if (listViewCondition.SelectedItems.Count == 0)
            {
                MessageBox.Show("Вы не выбрали условие для удаления", "Ошибка удаления условия"); return;
            }
            else
            {
                foreach (ListViewItem item in listViewCondition.SelectedItems)
                {
                    if (conditionList[listViewCondition.SelectedItems[0].Index].Conjunction == "-")
                        AddConditionButton.Enabled = true;
                    conditionList.Remove(conditionList[listViewCondition.SelectedItems[0].Index]);
                    listViewCondition.Items.Remove(listViewCondition.Items[listViewCondition.SelectedItems[0].Index]);
                }
            }
           
        }
      
        private string CreateQueryString()
        {
            neededTables.Clear();
            neededRelations.Clear();
            //таблицы из которых выбираются поля
            foreach (Field a in selectedFields)
            {
                if (neededTables.Find(x => x == a.Table) == null)
                    neededTables.Add(a.Table);
            }
            //добавим таблицы из условий. для этих таблиц надо найти связи
            foreach (Condition a in conditionList)
            {
                if (neededTables.Find(x => x == a.Field.Table) == null)
                    neededTables.Add(a.Field.Table);
            }
            for (int i = 0; i < neededTables.Count; i++)
                SPF(allRelations, tablesList, neededTables[i]);
            //теперь надо еще добавить те таблицы, через которые могли соединять
            foreach (Relation a in neededRelations)
            {
                if (neededTables.Find(x => x == a.PK_table) == null)
                    neededTables.Add(a.PK_table);
                if (neededTables.Find(x => x == a.FK_table) == null)
                    neededTables.Add(a.FK_table);
            }
            //using (NpgsqlConnection sConn = Program.login_as())
            //{
                if (selectedFields.Count == 0)
                {
                    MessageBox.Show("Вы не выбрали поля", "Ошибка формирования запроса"); return null;
                }
                //sConn.Open();
                var query = @"SELECT DISTINCT
";
                //выбранные поля
                for (int i = 0; i < selectedFields.Count - 1; i++)
                {
                    string headerText = selectedFields[i].HeaderText;
                    headerText = headerText.Replace(" ", "_");
                    query += "" + selectedFields[i].Table + "." + selectedFields[i].Name + " AS " + headerText + @",
";
                }
                string header = selectedFields[selectedFields.Count - 1].HeaderText;

                header = header.Replace(" ", "_");

                query += "\"" + selectedFields[selectedFields.Count - 1].Table + "\".\"" + selectedFields[selectedFields.Count - 1].Name + "\" AS " + header + @" 

FROM ";
                //из таблиц
                for (int i = 0; i < neededTables.Count - 1; i++)
                {
                    query += "\"" + neededTables[i] + "\"" + ", ";
                }

                query += "\"" + neededTables[neededTables.Count - 1] + "\"";

                //условия + соединения
                if (conditionList.Count > 0 || neededTables.Count > 1)
                {
                    query += @" 

WHERE ";
                }
                //соединения
                for (int i = 0; i < neededRelations.Count - 1; i++)
                {
                    query += "" + neededRelations[i].PK_table + "." + neededRelations[i].PK_field + " = " + neededRelations[i].FK_table + "." + neededRelations[i].FK_field + " AND ";
                }
                if (neededRelations.Count > 0)
                {
                    query += "" + neededRelations[neededRelations.Count - 1].PK_table + "." + neededRelations[neededRelations.Count - 1].PK_field + " = " + neededRelations[neededRelations.Count - 1].FK_table + "." + neededRelations[neededRelations.Count - 1].FK_field + " ";
                }

                if (conditionList.Count > 0 && neededRelations.Count > 0)
                {
                    query += "AND (";
                }
                //условия пользователя
                for (int i = 0; i < conditionList.Count - 1; i++)
                {
                    query += "" + conditionList[i].Field.Table + "." + conditionList[i].Field.Name + " " + conditionList[i].Criteria + " ";
                    if (conditionList[i].Field.Type == "character varying" || conditionList[i].Field.Type == "date")
                    {
                        string curValue = conditionList[i].Value;
                        query += "'";
                        for (int k = 0; k < curValue.Length; k++)
                        {
                            query += curValue[k];
                            if (curValue[k] == '\'')
                            {
                                query += "'";
                            }
                        }
                        query += "' ";
                    }
                    else
                    {
                        query += conditionList[i].Value + " "; 
                    }

                    if (conditionList[i].Conjunction == "-")
                    {
                        MessageBox.Show("Способ соединения условий может отсутствовать только у  последнего соединения", "Ошибка формирования запроса");
                        return null;
                    }
                    if (conditionList[i].Conjunction == "ИЛИ")
                    {
                        query += "OR ";
                    }
                    else
                    {
                        query += "AND ";
                    }
                }
                if (conditionList.Count > 0)
                {
                    query += "" + conditionList[conditionList.Count - 1].Field.Table + "." + conditionList[conditionList.Count - 1].Field.Name + " " + conditionList[conditionList.Count - 1].Criteria + " "; //Тут прилетают условия
                    if (conditionList[conditionList.Count - 1].Field.Type == "character varying" || conditionList[conditionList.Count - 1].Field.Type == "date")
                    {
                        string curValue = conditionList[conditionList.Count - 1].Value;
                        query += "'";
                        for (int k = 0; k < curValue.Length; k++)
                        {
                            query += curValue[k];
                            if (curValue[k] == '\'')
                            {
                                query += "'";
                            }
                        }
                        if (neededRelations.Count > 0)
                            query += "' )";
                        else
                            query += "' ";
                    }
                    else
                    {
                        if (conditionList[conditionList.Count - 1].Value.IndexOf(",") != -1)
                        {
                            conditionList[conditionList.Count - 1].Value = conditionList[conditionList.Count - 1].Value.Replace(',', '.');
                        }
                        if (neededRelations.Count > 0)
                            query += @"'" + conditionList[conditionList.Count - 1].Value + "' )";
                        else
                            query += @"'" + conditionList[conditionList.Count - 1].Value + "' ";
                    }
                }

                //сортировка
                if (selectionOrder.Count > 0)
                {
                    query += @" 

ORDER BY ";
                }
                for (int i = 0; i < selectionOrder.Count - 1; i++)
                {
                    query += "" + selectionOrder[i].Table + "." + selectionOrder[i].Name + " " + selectionOrder[i].OrderBy + @",
";
                }
                if (selectionOrder.Count > 0)
                {
                    query += "" + selectionOrder[selectionOrder.Count - 1].Table + "." + selectionOrder[selectionOrder.Count - 1].Name + " " + selectionOrder[selectionOrder.Count - 1].OrderBy;
                }
                return query;
            //}
        }

        private void ExecuteQueryButton_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection sConn = Program.npgLogin())
            {
                var query = CreateQueryString();
                if (query == null) return;
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter();
                BindingSource bindingSource1 = new BindingSource();
                dataAdapter = new NpgsqlDataAdapter(query, sConn);
                NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                try
                {
                    dataAdapter.Fill(ds);
                }
                catch { MessageBox.Show("Некорректное значение условия", "Ошибка Выполнения запроса"); return; }
                dataGridView_RES.ReadOnly = true;
                dataGridView_RES.DataSource = ds.Tables[0];

                sConn.Close();
            }
            tabControl.SelectedIndex = 3;
        }

        private void AddOrderButton_Click(object sender, EventArgs e)
        {
            if (listBoxAllOrder.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не выбрали поле", "Ошибка сортировки"); return;
            }
       
            Field newField = selectedFields[listBoxAllOrder.SelectedIndex];
            if (selectionOrder.Find(x => x.Category == newField.Category && x.Name == newField.Name)!=null)
            {
                MessageBox.Show("Такое поле уже добавлено в поля сортировок", "Ошибка сортировки");
                return;
            }
            selectionOrder.Add(new FieldOrder(newField.Id, newField.Name, newField.Table, newField.Type, newField.HeaderText, newField.Category, "ASC"));
            listBoxSelectedOrder.Items.Add(newField.HeaderText +"("+ newField.Category + ")");
        }

        private void AddAllOrdersButton_Click(object sender, EventArgs e)
        {
            foreach (Field field in selectedFields)
            {
                if (selectionOrder.Find(x => x.Category == field.Category && x.Name == field.Name) == null)
                {
                    selectionOrder.Add(new FieldOrder(field.Id, field.Name, field.Table, field.Type, field.HeaderText, field.Category, "ASC"));
                    listBoxSelectedOrder.Items.Add(field.HeaderText + "(" + field.Category + ")");

                }

            }
        }
        private void DeleteOrderButton_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedOrder.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не выбрали поле", "Ошибка сортировки"); return;
            }
            int removeIndex = listBoxSelectedOrder.SelectedIndex;
            listBoxSelectedOrder.Items.Remove(listBoxSelectedOrder.Items[removeIndex]);       
            selectionOrder.Remove(selectionOrder[removeIndex]);
        }
        private void DeleteAllOrdersButton_Click(object sender, EventArgs e)
        {
            selectionOrder.Clear();
            listBoxSelectedOrder.Items.Clear();
        }

        private void OrderChangeButton_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedOrder.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не выбрали поле", "Ошибка сортировки");
                return;
            }
            int index = listBoxSelectedOrder.SelectedIndex;
            if (radioButtonASC.Checked)
            {
                selectionOrder[index].OrderBy = "ASC";
                listBoxSelectedOrder.Items[index] = selectionOrder[index].HeaderText + "(" + selectionOrder[index].Category + ")";
            }
            else
            {
                selectionOrder[index].OrderBy = "DESC";
                listBoxSelectedOrder.Items[index] = selectionOrder[index].HeaderText + "(" + selectionOrder[index].Category + ")";
            
            }
            listBoxSelectedOrder.SelectedIndex = index;
        }

        private void listBoxSelectedOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int x = listBoxSelectedOrder.SelectedIndex;
            if (x < 0) return;
            if (selectionOrder[x].OrderBy == "ASC")
                radioButtonASC.Checked = true;
            else
                radioButtonDESC.Checked = true;

        }

        private void ShowQuery_Click(object sender, EventArgs e)
        {
            var query = CreateQueryString();
            if (query != null)
            { 
                if (query == "")
                    MessageBox.Show("Запрос пуст", "Ошибка предпросмотра запроса");
                else
                    MessageBox.Show(query, "Текст составленного запроса");
            }
        }

        private void dateTimePicker_value_ValueChanged(object sender, EventArgs e)
        {
            comboBoxValue.Text = Convert.ToString(dateTimePickerValue.Value.ToShortDateString());
        }

        private void splitContainer4_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer12_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}