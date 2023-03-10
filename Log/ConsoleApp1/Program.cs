using Serilog;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

            Log.Information("пользователь вводит деньги");

            var curs = 30;
            var tryparse = false;
            var value=0;
            while (!tryparse)
            {
                Console.Write("Сколько у вас долларов: ");
                var dollar = Console.ReadLine();
                tryparse = int.TryParse(dollar, out value);
                if (!tryparse)
                {
                    Log.Error("пользователь ошибся :(");
                }
            }
            Log.Information("пользователь ввёл {0}", value); 
            var rubli = value * curs;
            Console.WriteLine("Курс: 1 доллар = " + curs+" Р");
            if(value<500)
            {
                Console.WriteLine("Учитывая коммисию 0,8 p, у вас: " + (rubli-8)+" p");
            }
            else
            {
                Console.WriteLine("Учитывая коммисию 3%, у вас: " + (rubli - ((rubli * 0.8) / 100)));
            }
            Log.Warning("операция выполнена");
            Log.CloseAndFlush();
        }
    }
}
