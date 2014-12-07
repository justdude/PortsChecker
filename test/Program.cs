/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 26.10.2014
 */
using System;
using System.Threading;
namespace test
{
class Program
    {
        public delegate int DisplayHandler();
        static void Main(string[] args)
        {
            DisplayHandler handler = new DisplayHandler(Display);
            int result = handler.Invoke();
 
            Console.WriteLine("Продолжается работа метода Main");
            Console.WriteLine("Результат равен {0}", result);
 
            
            Console.ReadLine();
        }
 
        static int Display()
        {
            Console.WriteLine("Начинается работа метода Display....");
 
            int result = 0;
            for (int i = 1; i < 10; i++)
            {
                result += i * i;
            }
            Thread.Sleep(3000);
            Console.WriteLine("Завершается работа метода Display....");
            return result;
        }
    }
}