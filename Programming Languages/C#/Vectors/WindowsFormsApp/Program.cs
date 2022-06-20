using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
        class Vector
        {
            readonly double x;
            readonly double y;
            
            public double X
            {
            get { return x; }
            }

            public double Y
            {
                get { return y; }
            }

            public Vector()
            {
                x = 0;
                y = 0;
            }

            public Vector(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public double GetLength()
            {
                return Math.Sqrt(x * x + y * y);
            }

            public Vector Add(Vector vectorToAdd)
            {
                return new Vector(x + vectorToAdd.x, y + vectorToAdd.y);
            }

            public Vector Substract(Vector vectorToSubstract)
            {
                return new Vector(x - vectorToSubstract.x, y - vectorToSubstract.y);
            }

            public double ScalarMultiplication(Vector vectorToMultiplicate)
            {
                return x * vectorToMultiplicate.x + y * vectorToMultiplicate.y;
            }

            public double CosBetweenVectors(Vector secondVector)
            {
                return ScalarMultiplication(secondVector) / (GetLength() * secondVector.GetLength());
            }
        }

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
