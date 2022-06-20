using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernProgrammingLanguages
{
    public class Stack
    {
        int[] array;
        int limit;
        int size;

        public Stack(int n)
        {
            limit = n;
            size = 0;
            array = new int[n];
        }

        public bool Push(int item)
        {
            if (size == limit) return false;
            size++;
            array[size - 1] = item;
            return true;
        }

        public int Pop()
        {
            if (size == 0) throw new InvalidOperationException();
            return array[--size];
        }
        public void Clear()
        {
            size = 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack(10);
            for (int i = 0; i < 10; i++) stack.Push(Int32.Parse(Console.ReadLine()));
            for (int i = 0; i < 10; i++) Console.WriteLine(stack.Pop());
            Console.ReadLine();
        }
    }
}
