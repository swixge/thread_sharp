using System;
using System.Threading;

namespace thread_sum
{   
    class Program
    {
        /// <summary>
        /// ОСНОВНИЙ МЕТОД
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Vvedit` krok: ");
            int step = int.Parse(Console.ReadLine());   //крок
            Console.WriteLine("Vvedit` kilkist` potokiv: ");
            byte numThreads = byte.Parse(Console.ReadLine());     //кількість потоків
            double [] sums = new double[numThreads];   // сума кожного з потоків
            double[] numver_of_counts = new double[numThreads];   //кількість доданків
            bool canStop = false;
            
            Thread[] threads = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                int index = i;  //позицію потоку в масиві
                threads[i] = new Thread(() => Calculator(index, step, sums, numver_of_counts, ref canStop));
                threads[i].Start();
            }

            Thread stopperThread = new Thread(() => Stopper(ref canStop));
            stopperThread.Start();

            stopperThread.Join();
            for (int i = 0; i < numThreads; i++)
            {
                Console.WriteLine($"Thread {i + 1}: sum = {sums[i]} count = {numver_of_counts[i]}");
            }
        }

        /// <summary>
        /// МЕТОД ОБЧИСЛЕННЯ
        /// </summary>
        /// <param name="index">позицію потоку в масиві</param>
        /// <param name="step">крок</param>
        /// <param name="sums">сума потоку</param>
        /// <param name="counts">/кількість доданків</param>
        /// <param name="canStop">Зміна відповідна за зупинку потокік</param>
        static void Calculator(int index, int step, double[] sums, double[] counts, ref bool canStop)
        {
            double sum = 0;
            double count = 0;

            for (int i = 0; !canStop; i += step)
            {
                sum += i;
                count++;
            }

            sums[index] = sum;
            counts[index] = count;
        }
        
        /// <summary>
        /// МЕТОД СТОПЕР
        /// </summary>
        /// <param name="canStop"></param>
        static void Stopper(ref bool canStop)
        {
            Thread.Sleep(10000);
            canStop = true;
        }
    }
}