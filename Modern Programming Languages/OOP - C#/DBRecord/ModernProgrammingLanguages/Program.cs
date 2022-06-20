using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernProgrammingLanguages
{
    public class Database
    {
        List<Dictionary<char, string>> db;

        public Database()
        {
            db = new List<Dictionary<char, string>>();
        }

        public string GetValue(int rowNaturalNumber, char column)
        {
            if (rowNaturalNumber >= 1 && rowNaturalNumber <= db.Count && column >= 'A' && column <= 'G')
                return db[rowNaturalNumber - 1][column];
            return null;
        }

        public void CreateRecord(int rowNaturalNumber, string[] value)
        {
            var dict = new Dictionary<char, string>();
            char ch = 'A';
            for (int i = 0; i < 7; i++, ch++)
                dict[ch] = value[i];
            db.Insert(rowNaturalNumber - 1,dict);
        }

        public void CreateRecord(string[] value)
        {
            CreateRecord(db.Count + 1, value);
        }

        //public void ShowRecord(int rowNaturalNumber)
        //{
        //    Console.WriteLine("{}   %s  %s   %s   %s    %s   %s", )
        //}

        //public void ShowRecord()
        //{
        //    for (int i = 0; i < db.Count; i++)
        //        ShowRecord(i);
        //}
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database();
            db.CreateRecord(1, new string[] { "1", "2", "3", "4", "5", "6", "7"});
            db.CreateRecord(new string[] { "A", "A", "3", "4", "5", "6", "7" });
            db.CreateRecord(2, new string[] { "B", "B", "B", "4", "5", "6", "7" });
        }
    }
}
