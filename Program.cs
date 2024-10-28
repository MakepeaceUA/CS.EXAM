namespace ConsoleApp17
{
    internal class Program
    {

        static void Main(string[] args)
        {
            DictionaryManager manager = new DictionaryManager();
            Menu(manager);
        }
        public static void Menu(DictionaryManager manager)
        {
            while (true)
            {
                Console.WriteLine("Главное меню:");
                Console.WriteLine("1. Создать новый словарь");
                Console.WriteLine("2. Изменить словарь");
                Console.WriteLine("3. Показать все словари");
                Console.WriteLine("4. Выход");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Выбрано 'Создать новый словарь'"); Thread.Sleep(2000); Console.Clear();
                        manager.Create();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Выбрано 'Изменить словарь'"); Thread.Sleep(2000); Console.Clear();
                        manager.Select();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Выбрано 'Показать все словари'"); Thread.Sleep(2000); Console.Clear();
                        manager.Display();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Выход"); Thread.Sleep(2000); Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
    }
}