namespace ExternalSorting
{
    public class ExternalSorting
    {
        private const int DEFAULT = 0;
        private const int MODIFICATION = 1;

        private static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string path = "Files\\A.txt";

            if (ChooseApproach() == MODIFICATION)
                PreSort(path, GetSize(path));

            while (!IsFileSorted(path))
            {
                ManageData(path);
                MergeData(path);
            }

            Console.WriteLine("Файл відсортовано!");
        }

        public static int ChooseApproach()
        {
            do
            {
                Console.Write($"Оберіть спосіб сортування. Звичайне зовнішнє сортування ({DEFAULT}), або зовнішнє сортування з модифікацією ({MODIFICATION}): ");

                if (Int32.TryParse(Console.ReadLine(), out int value))
                {
                    if (value > 1 || value < 0)
                    {
                        Console.WriteLine($"Допустимі значення лише {DEFAULT} або {MODIFICATION}!");
                        continue;
                    }

                    return value;
                }
                else
                    Console.WriteLine("Введіть коректні дані!");

            } while (true);
        }

        public static int GetSize(string path)
        {
            long fileSize = new FileInfo(path).Length;
            int size;

            do
            {
                Console.Write("Скільки байтів файлу Ви бажаєте попередньо впорядкувати: ");

                if (Int32.TryParse(Console.ReadLine(), out size))
                {
                    if (size > fileSize)
                    {
                        Console.WriteLine("Розмір послідовності має бути меншим за розмір файлу!");
                        continue;
                    }

                    return size /= sizeof(Int32);
                }
                else
                    Console.WriteLine("Введіть коректні дані.");

            } while (true);
        }

        public static bool TryGetNumber(StreamReader sr, out int number)
        {
            string temp = String.Empty;

            while (sr.Peek() != ' ' && sr.Peek() != -1)
                temp += (char)sr.Read();

            if (sr.Peek() != -1)
                sr.Read();

            if (Int32.TryParse(temp, out number))
                return true;
            else
                return false;
        }

        public static void ManageData(string path)
        {
            string pathB = path.Replace("A.txt", "B.txt");
            string pathC = path.Replace("A.txt", "C.txt");

            File.WriteAllText(pathB, string.Empty);
            File.WriteAllText(pathC, string.Empty);

            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                using (StreamWriter sw1 = new StreamWriter(File.OpenWrite(pathB)), sw2 = new StreamWriter(File.OpenWrite(pathC)))
                {
                    int numberOfSeries = 0;
                    int previousNumber, currentNumber;

                    TryGetNumber(sr, out previousNumber);
                    sw1.Write($"{previousNumber} ");

                    while (TryGetNumber(sr, out currentNumber))
                    {
                        if (previousNumber > currentNumber)
                            ++numberOfSeries;

                        previousNumber = currentNumber;

                        if (numberOfSeries % 2 == 0)
                            sw1.Write($"{currentNumber} ");
                        else
                            sw2.Write($"{currentNumber} ");
                    }
                }
            }
        }

        public static void MergeData(string path)
        {
            string pathB = path.Replace("A.txt", "B.txt");
            string pathC = path.Replace("A.txt", "C.txt");

            File.WriteAllText(path, string.Empty);

            using (StreamReader sr1 = new StreamReader(File.OpenRead(pathB)), sr2 = new StreamReader(File.OpenRead(pathC)))
            {
                List<int> numbers = new List<int>();

                int previousNumber, currentNumber;
                int? lastAFile = null, lastBFile = null;

                TryGetNumber(sr1, out previousNumber);
                numbers.Add(previousNumber);

                while (TryGetNumber(sr1, out currentNumber))
                {
                    if (previousNumber <= currentNumber)
                        numbers.Add(currentNumber);
                    else
                    {
                        lastAFile = currentNumber;
                        break;
                    }

                    previousNumber = currentNumber;
                }

                TryGetNumber(sr2, out previousNumber);
                numbers.Add(previousNumber);

                while (TryGetNumber(sr2, out currentNumber))
                {
                    if (previousNumber <= currentNumber)
                        numbers.Add(currentNumber);
                    else
                    {
                        lastBFile = currentNumber;
                        break;
                    }

                    previousNumber = currentNumber;
                }

                numbers.Sort();

                using (StreamWriter sw = new StreamWriter(File.OpenWrite(path)))
                {
                    for (int i = 0, length = numbers.Count; i < length; ++i)
                        sw.Write($"{numbers[i]} ");

                    numbers.Clear();

                    while (true)
                    {
                        if (lastAFile != null)
                        {
                            previousNumber = lastAFile.Value;
                            numbers.Add(previousNumber);
                            lastAFile = null;
                        }

                        while (TryGetNumber(sr1, out currentNumber))
                        {
                            if (previousNumber <= currentNumber)
                                numbers.Add(currentNumber);
                            else
                            {
                                lastAFile = currentNumber;
                                break;
                            }

                            previousNumber = currentNumber;
                        }

                        if (lastBFile != null)
                        {
                            previousNumber = lastBFile.Value;
                            numbers.Add(previousNumber);
                            lastBFile = null;
                        }

                        while (TryGetNumber(sr2, out currentNumber))
                        {
                            if (previousNumber <= currentNumber)
                                numbers.Add(currentNumber);
                            else
                            {
                                lastBFile = currentNumber;
                                break;
                            }

                            previousNumber = currentNumber;
                        }

                        if (numbers.Count != 0)
                        {
                            numbers.Sort();

                            for (int i = 0, length = numbers.Count; i < length; ++i)
                                sw.Write($"{numbers[i]} ");

                            numbers.Clear();
                        }
                        else
                            break;
                    }
                }
            }
        }

        public static bool IsFileSorted(string path)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                int previousNumber, currentNumber;

                TryGetNumber(sr, out previousNumber);

                while (TryGetNumber(sr, out currentNumber))
                {
                    if (previousNumber > currentNumber)
                        return false;

                    previousNumber = currentNumber;
                }
            }

            return true;
        }

        public static void PreSort(string path, int size)
        {
            string tempPath = path.Replace("A.txt", "Temp.txt");

            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                using (StreamWriter sw = new StreamWriter(File.OpenWrite(tempPath)))
                {
                    List<int> sequence = new List<int>(size);

                    int counter = 0;
                    int currentNumber;

                    while (true)
                    {
                        while (counter < size)
                        {
                            if (TryGetNumber(sr, out currentNumber))
                                sequence.Add(currentNumber);

                            ++counter;
                        }

                        sequence.Sort();

                        for (int i = 0, length = sequence.Count; i < length; ++i)
                            sw.Write($"{sequence[i]} ");

                        sequence.Clear();

                        counter = 0;

                        if (sr.Peek() == -1)
                            break;
                    }
                }
            }

            File.Delete(path);
            File.Move(tempPath, path);
        }
    }
}