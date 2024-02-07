/*Объедините две предыдущих работы (практические работы 2 и 3): 
* поиск файла и поиск текста в файле написав утилиту которая ищет файлы определенного 
* расширения с указанным текстом. Рекурсивно. Пример вызова утилиты: utility.exe txt текст.*/

using System.Text;

namespace FileSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 3 && Path.Exists(args[0]))
            {

                SeekFileToExtensionsAndText(new DirectoryInfo(args[0]), args[1], args[2]);
            }

            Console.ReadLine();
        }

        static public void SeekFileToExtensionsAndText(DirectoryInfo dirStart, string extensions, string text)
        {

            var dir = dirStart.EnumerateDirectories();
            var file = dirStart.EnumerateFiles();

            foreach (var item in dir)
            {
                SeekFileToExtensionsAndText(item, extensions, text);
            }
            foreach (var item in file)
            {
                if (item.Extension == extensions)
                {
                    using (var fileStream = new FileStream(item.FullName, FileMode.Open))
                    {
                        byte[] bytes = new byte[fileStream.Length];
                        int cnt = fileStream.Read(bytes, 0, bytes.Length);
                        if (cnt == bytes.Length)
                        {
                            string str = Encoding.Default.GetString(bytes);
                            if (str.Contains(text))
                            {
                                Console.WriteLine($"{item.FullName}");
                            }
                        }
                    }
                }

            }

        }
    }
}