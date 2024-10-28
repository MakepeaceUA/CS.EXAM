using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;

namespace ConsoleApp17
{
    internal class DictionaryManager
    {
        private List<Words> dictionaries = new List<Words>();
        public DictionaryManager()
        {
            Load();
        }
        public void Create()
        {
            Console.Write("Введите тип словаря (например, English-Russian): ");
            string name = Console.ReadLine();
            if (name.Length >= 5)
            {
                string filePath = $"Dict.{name}.json";
                if (File.Exists(filePath))
                {
                    Console.WriteLine($"Словарь с именем '{name}' уже существует."); Thread.Sleep(2000); Console.Clear();
                    return;
                }
                var dictionary = new Words(name);
                dictionaries.Add(dictionary);
                dictionary.SaveToFile(filePath);
                Console.WriteLine($"Словарь '{name}' создан и сохранен в файл '{filePath}'."); Thread.Sleep(2000); Console.Clear();
                DictionaryMenu(dictionary);
            }
            else
            {
                Console.WriteLine("Имя словаря слишком маленькое"); Thread.Sleep(2000); Console.Clear();
                return;
            }

        }
        public void Select()
        {
            if (dictionaries.Count == 0)
            {
                Console.WriteLine("Словарей пока нет."); Thread.Sleep(1000); Console.Clear();
                return;
            }
            Console.Write("Введите тип словаря, который хотите изменить: ");
            string type = Console.ReadLine();
            var dictionary = dictionaries.Find(d => d.dict_type.Equals(type));

            if (dictionary != null)
            {
                Console.Clear();
                Console.WriteLine($"Выбран словарь '{type}' для редактирования."); Thread.Sleep(1000); Console.Clear();
                DictionaryMenu(dictionary);
            }
            else
            {
                Console.WriteLine("Словарь не найден."); Thread.Sleep(1000); Console.Clear();
            }
        }
        public void DictionaryMenu(Words dictionary)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню словаря:");
                Console.WriteLine("1. Добавить слово");
                Console.WriteLine("2. Добавить перевод к слову");
                Console.WriteLine("3. Заменить слово или перевод");
                Console.WriteLine("4. Удалить слово или перевод");
                Console.WriteLine("5. Найти перевод слова");
                Console.WriteLine("6. Вернуться в главное меню");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Выбрано 'Добавить слово'"); Thread.Sleep(1000); Console.Clear();
                        Add(dictionary);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Выбрано 'Добавить перевод к слову'"); Thread.Sleep(1000); Console.Clear();
                        NewTranslation(dictionary);
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Выбрано 'Заменить слово или перевод'"); Thread.Sleep(1000); Console.Clear();
                        Replace(dictionary);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Выбрано 'Удалить слово или перевод'"); Thread.Sleep(1000); Console.Clear();
                        Delete(dictionary);
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Выбрано 'Найти перевод слова'"); Thread.Sleep(1000); Console.Clear();
                        Find(dictionary);
                        break;
                    case "6":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
        public void Add(Words name)
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();
            Console.Write("Введите перевод: ");
            string translation = Console.ReadLine();
            name.AddWord(word, translation); 
            name.SaveToFile($"Dict.{name.dict_type}.json");
            Console.WriteLine($"Слово '{word}' с переводом {translation} добавлен и сохранён в словарь."); Thread.Sleep(2000); Console.Clear();
        }
        public void NewTranslation(Words name)
        {
            Console.Write("Введите слово, для которого нужно добавить перевод: ");
            string word = Console.ReadLine();
            Console.Write("Введите новый перевод: ");
            string translation = Console.ReadLine();
            name.AddTranslation(word, translation);
            name.SaveToFile($"Dict.{name.dict_type}.json");
            Console.WriteLine($"Перевод '{translation}' добавлен к слову '{word}' в словарь."); Thread.Sleep(1000); Console.Clear();
        }
        public void Replace(Words name)
        {
            string word;
            Console.WriteLine("Выберите действие:\n1:Изменить Слово\n2:Изменить Перевод\n3:Назад ");
            switch (Console.ReadLine())
            {
                case "1":
                    Thread.Sleep(1000); Console.Clear();
                    Console.Write("Введите старое слово: ");
                    word = Console.ReadLine();
                    Console.Write("Введите новое слово: ");
                    string newWord = Console.ReadLine();
                    name.ReplaceWord(word, newWord);
                    name.SaveToFile($"Dict.{name.dict_type}.json");
                    Console.WriteLine($"Слово '{word}' заменено на '{newWord}' в словаре."); Thread.Sleep(1000); Console.Clear();
                    break;
                case "2":
                    Thread.Sleep(1000); Console.Clear();
                    Console.Write("Введите слово: ");
                    word = Console.ReadLine();
                    Console.Write("Введите старый перевод: ");
                    string oldTranslation = Console.ReadLine();
                    Console.Write("Введите новый перевод: ");
                    string newTranslation = Console.ReadLine();
                    name.ReplaceTranslation(word, oldTranslation, newTranslation);
                    name.SaveToFile($"Dict.{name.dict_type}.json");
                    Console.WriteLine($"Перевод '{oldTranslation}' заменен на '{newTranslation}' в словаре."); Thread.Sleep(1000); Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Неправильный выбор. Возвращение в главное меню."); Thread.Sleep(2000); Console.Clear();
                    break;
            }

        }
        public void Delete(Words name)
        {
            Console.WriteLine("Выберите действие:\n1:Удалить Слово\n2:Удалить Перевод\n3:Назад");
            switch (Console.ReadLine())
            {
                case "1":
                    Thread.Sleep(1000); Console.Clear();
                    Console.Write("Введите слово для удаления: ");
                    string word = Console.ReadLine();
                    name.DeleteWord(word);
                    name.SaveToFile($"Dict.{name.dict_type}.json");
                    Console.WriteLine($"Слово '{word}' удалено из словаря."); Thread.Sleep(1000); Console.Clear();
                    break;
                case "2":
                    Thread.Sleep(1000); Console.Clear();
                    Console.Write("Введите слово, из которого нужно удалить перевод: ");
                    string translate_word = Console.ReadLine();
                    Console.Write("Введите перевод для удаления: ");
                    string del_translate = Console.ReadLine();
                    name.DeleteTranslation(translate_word, del_translate);
                    name.SaveToFile($"Dict.{name.dict_type}.json");
                    Console.WriteLine($"Перевод '{del_translate}' удалено из словаря."); Thread.Sleep(1000); Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Неправильный выбор. Возвращение в главное меню."); Thread.Sleep(2000); Console.Clear();
                    break;
            }
        }
        public void Find(Words name)
        {
            Console.Write("Введите слово для поиска перевода: ");
            string word = Console.ReadLine();
            var translations = name.FindTranslation(word);
            if (translations != null)
            {
                Console.WriteLine($"Переводы слова '{word}': {string.Join(", ", translations)}");
                Console.ReadLine(); Console.Clear();
            }
            else
            {
                Console.WriteLine("Переводы не найдены."); Thread.Sleep(1000); Console.Clear();
            }
        }
        public void Display()
        {
            if (dictionaries.Count == 0)
            {
                Console.WriteLine("Словарей пока нет."); Thread.Sleep(1000); Console.Clear();
                return;
            }
            foreach (var dictionary in dictionaries)
            {
                Console.WriteLine($"\nСловарь: {dictionary.dict_type}");
                foreach (var word in dictionary.dict)
                {
                    Console.WriteLine($"Слово: {word.Key}");
                    Console.WriteLine("Переводы: " + string.Join(", ", word.Value));
                }
            }
            Console.ReadLine();
            Console.Clear();
        }
        private void Load()
        {
            var jsonFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "Dict.*.json");
            foreach (var file in jsonFiles)
            {
                var jsonData = File.ReadAllText(file);
                var dictionary = JsonSerializer.Deserialize<Words>(jsonData);
                if (dictionary != null)
                {
                    dictionaries.Add(dictionary);
                }
                else
                {
                    Console.WriteLine("Ошибка."); Thread.Sleep(2000); Console.Clear();
                }
            }
        }
    }
}
