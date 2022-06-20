using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace raspisanie
{
    class Task
    {
        string name;
        int priority;
        List<string> prevList;
        int prevCount;
        List<string> nextList;
        int nextCount;
        List<int> mark;

        public string Name { get => name; set => name = value; }
        public int Priority { get => priority; set => priority = value; }
        public List<string> PrevList { get => prevList; set => prevList = value; }
        public int PrevCount { get => prevCount; set => prevCount = value; }
        public List<string> NextList { get => nextList; set => nextList = value; }
        public int NextCount { get => nextCount; set => nextCount = value; }
        public List<int> Mark { get => mark; set => mark = value; }
        public Task(string name, List<string> prevList, List<string> nextList)
        {
            this.name = name;
            this.prevList = prevList;
            this.prevCount = prevList.Count;
            this.nextList = nextList;
            this.nextCount = nextList.Count;
            mark = new List<int>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var n = int.Parse(input[0].Split()[0]);
            var m = int.Parse(input[0].Split()[1]);
            var tasksInput = input[1].Split();
            var depend = new List<Tuple<string, string>>();
            //var depend = new Dictionary<string, string>();
            for (int i = 2; i < input.Length; i++)
                depend.Add(Tuple.Create(input[i].Split()[0], input[i].Split()[1]));
            var tasks = new List<Task>();
            foreach (var taskInput in tasksInput)
            {
                var prevList = new List<string>();
                var nextList = new List<string>();
                for (int i = 0; i < depend.Count; i++)
                {
                    if (depend[i].Item2 == taskInput)
                        prevList.Add(depend[i].Item1);
                    if (depend[i].Item1 == taskInput)
                        nextList.Add(depend[i].Item2);
                }
                tasks.Add(new Task(taskInput, prevList, nextList));
            }
            var LList = new List<Task>();
            foreach (var task in tasks)
                if (task.NextCount == 0)
                    LList.Add(task);
            var p = 1;
            while (LList.Count > 0)
            {
                var task = LList[0];
                LList.RemoveAt(0);
                task.Priority = p;
                foreach (var taskName in task.PrevList)
                {
                    var taskPrev = tasks.Find(x => x.Name == taskName);
                    taskPrev.Mark.Insert(0, p);
                    taskPrev.NextCount--;
                    if (taskPrev.NextCount == 0)
                    {
                        int i = LList.Count - 1;
                        while (i >= 0 && LList[i].Mark.Count > 0 && LList[i].Mark[0] > taskPrev.Mark[0])
                            i--;
                        LList.Insert(i + 1, taskPrev);
                        //LList.Add(taskPrev);
                        //LList.Sort(CompareMarks);
                    }
                }
                p++;
            }
            foreach (var task in tasks)
                if (task.Priority == 0)
                {
                    Console.WriteLine("ошибка: указанный набор заданий не может быть выполнен");
                    return;
                }
            tasks.Sort(ComparePriority);
            LList = new List<Task>();
            foreach (var task in tasks)
            {
                if (task.PrevCount == 0) 
                    LList.Add(task);
            }
            var step = 1;
            var result = new StringBuilder();
            while (LList.Count > 0)
            {
                var curTasks = new List<Task>();
                var curM = m;
                while (LList.Count > 0 && curM > 0)
                {
                    curTasks.Add(LList[0]);
                    LList.RemoveAt(0);
                    curM--;
                }
                result.Append("Период времени: " + step + "\n");
                foreach (var curTask in curTasks)
                {
                    result.Append("Исполняется задача " + curTask.Name + "\n");
                    foreach (var taskName in curTask.NextList)
                    {
                        var taskNext = tasks.Find(x => x.Name == taskName);
                        taskNext.PrevCount--;
                        if (taskNext.PrevCount == 0)
                        {
                            LList.Add(taskNext);
                            LList.Sort(ComparePriority);
                        }
                    }
                }
                step++;
            }
            File.WriteAllText("output.txt", result.ToString());
        }

        private static int ComparePriority(Task x, Task y)
        {
            return -x.Priority.CompareTo(y.Priority);
        }
    }
}
