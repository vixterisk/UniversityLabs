using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using System.Security;
using Npgsql;

namespace non_standart
{
    static class Program
    {
        public static string[] varcharCriterias = new string[4] { "=", "<>", "LIKE", "NOT LIKE" };
        public static string[] criterias = new string[6] { "=", "<>", ">", ">=", "<=", "<" };
        public static string connectionstring = $"Server = {ConfigurationManager.AppSettings["server"]}; Port = {ConfigurationManager.AppSettings["port"]}; User Id = {ConfigurationManager.AppSettings["userId"]}; Password={ConfigurationManager.AppSettings["password"]}; Database = {ConfigurationManager.AppSettings["database"]};";//"Server = 127.0.0.1; Port = 5432; User Id = postgres; " + "Password=a54g5x; Database=postgres;";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
        public static NpgsqlConnection npgLogin()
        {
            return new NpgsqlConnection(connectionstring);
        }
    }
   
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Table { get; set; }
        public string Type { get; set; }
        public string HeaderText { get; set; }
        public string Category { get; set; }
        public Field(int id, string name, string table, string type, string text, string category)
        {
            Id = id;
            Name = name;
            Table = table;
            Type = type;
            HeaderText = text;
            Category = category;
        }
    }
    public class FieldOrder : Field
    {
        public string OrderBy { get; set; } //ASC - по возрастанию, DESC - по убыванию
        public FieldOrder(int id, string name, string table, string type, string headerText, string category, string orderBy) : base (id, name, table, type, headerText, category)
        {
            OrderBy = orderBy;
        }
    }
    public class Condition
    {
        public Field Field { get; set; }
        public string Criteria { get; set; }
        public string Value { get; set; }
        public string Conjunction { get; set; }
        public Condition(Field field, string crit, string value, string conjunction)
        {
            Field = field;
            Criteria = crit;
            Value = value;
            Conjunction = conjunction;
        }
    }

    public class Relation
    {
        public string FK_table { get; set; }
        public string PK_table { get; set; }
        public string FK_field { get; set; }
        public string PK_field { get; set; }
        public Relation(string FK_t, string PK_t, string FK_f, string PK_f)
        {
            FK_table = FK_t;
            PK_table = PK_t;
            FK_field = FK_f;
            PK_field = PK_f;
        }
    }
    public class Node
    {
        public string Name { get; set; }
        public int Path_length { get; set; }
        public string Last_path { get; set; }
        public Node(string v)
        {
            this.Name = v;
            this.Path_length = 1000;
            this.Last_path = "";
        }
    }
}
