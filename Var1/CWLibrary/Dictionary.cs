using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

namespace CWLibrary
{
    //    Итератор, позволяющий перебирать с помощью цикла foreach все пары словаря в алфавитном порядке
    //локали(т.е.если локаль 0, то перебираем в алфавитном порядке по русским словам, если локаль 1, то
    //перебираем в алфавитном порядке по английским словам);

    //• Именнованный итератор, позволяющий перебирать с помощью цикла foreach все пары словаря в
    //алфавитном порядке локали(т.е.если локаль 0, то перебираем в алфавитном порядке по русским словам,
    //если локаль 1, то перебираем в алфавитном порядке по английским словам), начинающиеся с
    //определенной буквы(задается в качестве параметра);

    //    Hint: Нужна будет сортировка с помощью лямбда-выражения для U item2
    //• Метод void MySerialize(string path) для выполнения бинарной сериализации текущего объекта класса
    //Journal, path – путь к файлу, лежащему в одной папке с*.exe.
    //• Статический метод Dictionary MyDeserialize(string path) для выполнения бинарной десериализации
    //объекта
    [Serializable]
    public class Dictionary : IEnumerable
    {
        static Random rnd = new Random();
        int locale;
        List<Pair<string, string>> words;

        public Dictionary(List<Pair<string, string>> words)
        {
            this.words = words;
            locale = rnd.Next(0, 2);
        }

        public Dictionary()
        {
            words = new List<Pair<string, string>>();
        }

        public void Add(Pair<string, string> addition)
        {
            words.Add(addition);
        }

        public void Add(string word1, string word2)
        {
            Pair<string, string> newPair = new Pair<string, string>(word1, word2);
            words.Add(newPair);
        }
        public IEnumerator GetEnumerator()
        {
            words.Sort(); //сортируем
            return words.GetEnumerator();
        }


        public IEnumerable MyEnumerator(char letter)
        {

            if (letter < 'я' + 1 && letter > 'а' - 1)  //если русская буква
            {
                List<Pair<string, string>> lst = new List<Pair<string, string>>();
                foreach (var w in words)
                {
                    if (w.Item1[0] == letter) lst.Add(w);
                }
                foreach (var el in lst) yield return el;
            }
            else //если английская
            {
                words.Sort((x, y) => x.Item2.CompareTo(y.Item2)); //сортируем по второму
                foreach (var w in words)
                {
                    yield return w;
                }
            }
        }

        public void MySerialize(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, this);
            }
        }

        public static Dictionary MyDeserialize(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Dictionary)bf.Deserialize(fs);
            }
        }


    }
}

