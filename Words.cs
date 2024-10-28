using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    internal class Words
    {
        public string dict_type { get; set; }
        public Dictionary<string, List<string>> dict { get; set; }

        public Words()
        {
            dict = new Dictionary<string, List<string>>();
        }

        public Words(string type) : this() 
        {
            dict_type = type;
        }

        public void AddWord(string word, string translation)
        {
            if (!dict.ContainsKey(word))
            {
                dict[word] = new List<string>();
            }
            else
            {
                Console.WriteLine($"Слово '{word}' уже существует."); 
            }
            if (!dict[word].Contains(translation))
            {
                dict[word].Add(translation);
            }
            else
            {
                Console.WriteLine($"Перевод '{translation}' для слова '{word}' уже существует."); 
            }
        }
        public void AddTranslation(string word, string translation)
        {
            if (dict.ContainsKey(word))
            {
                if (!dict[word].Contains(translation))
                {
                    dict[word].Add(translation);
                }
                else
                {
                    Console.WriteLine("Этот перевод уже существует."); Thread.Sleep(2000); Console.Clear();
                }
            }
            else
            {
                AddWord(word, translation);
            }
        }
        public void ReplaceWord(string oldWord, string newWord)
        {
            if (dict.ContainsKey(oldWord))
            {
                var translations = dict[oldWord];
                dict.Remove(oldWord);  
                dict[newWord] = translations; 
            }
        }
        public void ReplaceTranslation(string word, string oldTranslation, string newTranslation)
        {
            if (dict.ContainsKey(word))
            {
                var translations = dict[word];
                int index = translations.IndexOf(oldTranslation);
                if (index != -1)
                {
                    translations[index] = newTranslation;
                }
            }
        }
        public void DeleteWord(string word)
        {
            if (dict.ContainsKey(word))
            {
                dict.Remove(word);
            }
        }
        public void DeleteTranslation(string word, string translation)
        {
            if (dict.ContainsKey(word))
            {
                var translations = dict[word];
                if (translations.Count > 1)
                {
                    translations.Remove(translation);
                }
            }
        }
        public List<string> FindTranslation(string word)
        {
            return dict.ContainsKey(word) ? dict[word] : null;
        }
        public void SaveToFile(string filePath)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(this));
        }
    }
}
