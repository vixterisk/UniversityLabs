using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HuffmanFileWork;

namespace Huffman
{
    class HuffmanTree
    {
        public char ch { get; private set; }
        public double freq { get; private set; }
        public bool isTerminal { get; private set; }
        public HuffmanTree left { get; private set; }
        public HuffmanTree rigth { get; private set; }
        public HuffmanTree(char c, double frequency)
        {
            ch = c;
            freq = frequency;
            isTerminal = true;
            left = rigth = null;
        }
        public HuffmanTree(HuffmanTree l, HuffmanTree r)
        {
            freq = l.freq + r.freq;
            isTerminal = false;
            left = l;
            rigth = r;
        }
    }
    class HuffmanInfo
    {
        HuffmanTree Tree; // дерево кода Хаффмана, потребуется для распаковки
        Dictionary<char, string> Table; // словарь, хранящий коды всех символов, будет удобен для сжатия
        List<HuffmanTree> freqList = new List<HuffmanTree>();

        HuffmanTree FindMin()
        {
            HuffmanTree minItem = new HuffmanTree('~', 1.0);
            for (int i = 0; i < freqList.Count; i++)
                if (freqList[i].freq <= minItem.freq)
                {
                    minItem = freqList[i];
                }
            return minItem;
        }
        void getCodes(HuffmanTree tree, Dictionary<char, string> Table, string curCode)
        {
            if (tree.isTerminal == true)
            {
                Table.Add(tree.ch, curCode);
                return;
            }
            else
            getCodes(tree.left, Table, curCode + "0");
            getCodes(tree.rigth, Table, curCode + "1");
        }

        public HuffmanInfo(string fileName)
        {
            string line;
            StreamReader sr = new StreamReader(fileName, Encoding.Unicode);
            // считать информацию о частотах символов
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Length == 0)
                {
                    //TODO: отдельная обработка строки, которой соответствует частота символа "конец строки" 
                    line = sr.ReadLine();
                    double freq = Convert.ToDouble(line.Split()[1]);
                    freqList.Add(new HuffmanTree('\n', freq));
                }
                else
                {
                    //TODO: создаем вершину (лист) дерева с частотой очередного символа
                    double freq = Convert.ToDouble(line.Substring(3));
                    freqList.Add(new HuffmanTree(line[0], freq));
                }
            }
            sr.Close();
            // TODO: добавить еще одну вершину-лист, соответствующую символу с кодом 0 ('\0'), который будет означать конец файла. Частота такого символа, очевидно, должна быть очень маленькой, т.к. такой символ встречается только 1 раз во всем файле (можно даже сделать частоту = 0)
            freqList.Add(new HuffmanTree('\0', 0.0));
            // TODO: построить дерево кода Хаффмана путем последовательного объединения листьев
            // Tree = ...
            while (freqList.Count > 1)
            {
                var min = FindMin();
                freqList.Remove(min);
                var min2 = FindMin();
                freqList.Remove(min2);
                freqList.Add(new HuffmanTree(min, min2));
            }
            Table = new Dictionary<char, string>();
            var tree = freqList[0];
            // TODO: заполнить таблицу кодирования Table на основе обхода построенного дерева
            getCodes(tree, Table, "");
            //Table.Add('\0', "0"); // это временная заглушка!!! Эту строчку нужно будет потом убрать, т.к. признак конца файла должен быть уже добавлен в таблицу, как и все остальные символы
        }
        public void Compress(string inpFile, string outFile)
        {
            var sr = new StreamReader(inpFile, Encoding.Unicode);
            var sw = new ArchWriter(outFile); //нужна побитовая запись, поэтому использовать StreamWriter напрямую нельзя
            string line;
            StringBuilder str = new StringBuilder();
            while ((line = sr.ReadLine()) != null)
            {
                for (int i = 0; i < line.Count(); i++)
                {
                    str.Append(Table[line[i]]);
                }
                str.Append(Table['\n']);
                // TODO: посимвольно обрабатываем строку, кодируем, пишем в sw
            }
            str.Append(Table['\0']);
            var additZer = Math.Ceiling(str.Length / 8.0) * 8 - str.Length;
            for (int i = 0; i < additZer; i++)
                str.Append('0');
            sw.WriteWord(str.ToString());
            sr.Close();
            //sw.WriteWord(Table['\0']); // записываем признак конца файла
            sw.Finish();
        }
        char GetKey(Dictionary<char, string> dict, string value)
        {
            foreach (KeyValuePair<char, string> kvp in dict)
            {
                    if (kvp.Value == value) return kvp.Key;
            }
            return '\0';
        }
        public void Decompress(string archFile, string txtFile)
        {
            var sr = new ArchReader(archFile); // нужно побитовое чтение
            var sw = new StreamWriter(txtFile, false, Encoding.Unicode);
            byte curBit;
            StringBuilder str = new StringBuilder();
            while (sr.ReadBit(out curBit))
            {
                str.Append(curBit.ToString());
                // TODO: побитово (!) разбираем архив
            }
            int i = 0;
            while (str != null)
            {
                StringBuilder word = new StringBuilder();
                while (!Table.ContainsValue(word.ToString()))
                {
                    word.Append(str[i]);
                    i++;
                };
                var symbol = GetKey(Table, word.ToString());
                sw.Write(symbol);
                if (symbol == '\0') break;
            }
            sr.Finish();
            sw.Close();
        }
    }

    class Huffman
    {
        static void Main(string[] args)
        {
            if (!File.Exists("freq.txt"))
            {
                Console.WriteLine("Не найден файл с частотами символов!");
                return;
            }
            if (args.Length == 0)
            {
                var hi = new HuffmanInfo("freq.txt");
                hi.Compress("etalon.txt", "etalon.arc");
                hi.Decompress("etalon.arc", "etalon_dec.txt");
                return;
            }
            if (args.Length != 3 || args[0] != "zip" && args[0] != "unzip")
            {
                Console.WriteLine("Синтаксис:");
                Console.WriteLine("Huffman.exe zip <имя исходного файла> <имя файла для архива>");
                Console.WriteLine("либо");
                Console.WriteLine("Huffman.exe unzip <имя файла с архивом> <имя файла для текста>");
                Console.WriteLine("Пример:");
                Console.WriteLine("Huffman.exe zip text.txt text.arc");
                return;
            }
            var HI = new HuffmanInfo("freq.txt");
            if (args[0] == "zip")
            {
                if (!File.Exists(args[1]))
                {
                    Console.WriteLine("Не найден файл с исходным текстом!");
                    return;
                }
                HI.Compress(args[1], args[2]);
            }
            else
            {
                if (!File.Exists(args[1]))
                {
                    Console.WriteLine("Не найден файл с архивом!");
                    return;
                }
                HI.Decompress(args[1], args[2]);
            }
            Console.WriteLine("Операция успешно завершена!");
        }
    }
}
