using CWLibrary;
using System;
using System.Collections.Generic;
using System.IO;

namespace Var1
{
    class Program
    {
        public static int ReadInt()
        {
            int n = 0;
            Console.WriteLine("Введите количество слов в словаре");
            while(!int.TryParse(Console.ReadLine(), out n) || n <=0 )
            {
                Console.WriteLine("Ошибочные данные. Введите положительное целое число");
            }
            return n;
        }

        public static List<Pair<string, string>> GetPairs(string path)
        {
            List<Pair<string, string>> res = new List<Pair<string, string>>();
            string[] lines = File.ReadAllLines(path);
            foreach(string s in lines)
            {
                string w1 = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string w2 = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                Pair<string, string> p = new Pair<string, string>(w1, w2);
                res.Add(p);
            }
            return res;
        }

        public static string GetStr()
        {
            Console.WriteLine("Введите первое русское слово");
            string w1 = Console.ReadLine();
            
            Console.WriteLine("Введите второе английское слово");
            string w2 = Console.ReadLine();

            return (w1 + " " + w2);
        }
        static void Main(string[] args)
        {
            string path = "dictionary.txt";
            try
            {
                int n = ReadInt();
                
                File.WriteAllText(path, ""); //обновляем файл
                string[] lines = new string[n];
                for (int i = 0; i < n; i++)
                {
                    lines[i] = GetStr();
                }
                File.AppendAllLines(path, lines); //добавляем в файл

            }
            catch (ArgumentException e ) { Console.WriteLine(e.Message); }
            catch (IOException e ) { Console.WriteLine(e.Message); }
            catch (Exception e ) { Console.WriteLine(e.Message); }

            try
            {
                List<Pair<string, string>> lst = GetPairs(path);
                Dictionary dict = new Dictionary(lst);

                dict.MySerialize("out.bin"); //сериализация
                Dictionary dict2 = Dictionary.MyDeserialize("out.bin");
            


            foreach (var el in dict2)
            {
                Console.WriteLine(el);
            }

            Console.WriteLine("Вывожу пары с русскими словами на букву а");

            foreach (var el in dict2.MyEnumerator('а'))
            {
                Console.WriteLine(el);
            }
            }
            catch (ArgumentException e) { Console.WriteLine(e.Message); }
            catch (IOException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
