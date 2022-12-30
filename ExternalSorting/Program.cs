namespace ExternalSorting
{
    public sealed class ExternalSorting
    {
        private const int DEFAULT = 0;
        private const int MODIFICATION = 1;

        private static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string pathA = "A.txt";
            string pathB = "B.txt";
            string pathC = "C.txt";

            GenerateFile(pathA, 2_000_000, 1, 2_000_000);

            Console.WriteLine($"Можна переглянути вміст згенерованого файлу за відповідним шляхом: {pathA}");

            if (ChooseApproach() == MODIFICATION)
                PreSort(pathA, GetSize(pathA));

            while (!IsFileSorted(pathA))
            {
                ManageData(pathA, pathB, pathC);
                MergeData(pathA, pathB, pathC);
            }

            File.Delete(pathB);
            File.Delete(pathC);

            Console.WriteLine("Файл відсортовано.");
        }

        private static void GenerateFile(string path, int quantity, int min, int max)
        {
            File.WriteAllText(path, String.Empty);

            using (StreamWriter sw = new StreamWriter(File.OpenWrite(path)))
            {
                Random random = new Random();

                for (int i = 0; i < quantity; ++i)
                    sw.Write($"{random.Next(min, max)} ");                  
            }
        }

        private static int ChooseApproach()
        {
            int value;

            do
            {
                Console.Write($"Оберіть спосіб сортування. Звичайне зовнішнє сортування ({DEFAULT}), або зовнішнє сортування з модифікацією ({MODIFICATION}): ");

                if (Int32.TryParse(Console.ReadLine(), out value))
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

        private static int GetSize(string path)
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

        private static bool TryGetNumber(StreamReader reader, out int number)
        {
            if (reader.Peek() == -1)
            {
                number = -1;
                return false;
            }

            string temp = String.Empty;

            while (reader.Peek() != ' ' && reader.Peek() != -1)
                temp += (char)reader.Read();

            if (reader.Peek() != -1)
                reader.Read();

            number = Int32.Parse(temp);
            return true;       
        }

        private static void ManageData(string pathA, string pathB, string pathC)
        {
            File.WriteAllText(pathB, string.Empty);
            File.WriteAllText(pathC, string.Empty);

            using (StreamReader sr = new StreamReader(File.OpenRead(pathA)))
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

        private static void MergeData(string pathA, string pathB, string pathC)
        {
            File.WriteAllText(pathA, string.Empty);

            using (StreamReader readerB = new StreamReader(File.OpenRead(pathB)), readerC = new StreamReader(File.OpenRead(pathC)))
            {
                List<int> numbers = new List<int>();
                int lastB, lastC;

                numbers.AddRange(GetNumbers(readerB, out lastB));
                numbers.AddRange(GetNumbers(readerC, out lastC));
                numbers.Sort();

                using (StreamWriter sw = new StreamWriter(File.OpenWrite(pathA)))
                {
                    for (int i = 0, length = numbers.Count; i < length; ++i)
                        sw.Write($"{numbers[i]} ");

                    while (true)
                    {
                        numbers.Clear();

                        if (lastB != -1)
                        {
                            numbers.Add(lastB);
                            lastB = -1;
                        }

                        numbers.AddRange(GetNumbers(readerB, out lastB));

                        if (lastC != -1)
                        {
                            numbers.Add(lastC);
                            lastC = -1;
                        }

                        numbers.AddRange(GetNumbers(readerC, out lastC));

                        if (numbers.Count == 0)
                            break;

                        numbers.Sort();

                        for (int i = 0, length = numbers.Count; i < length; ++i)
                            sw.Write($"{numbers[i]} ");
                    }
                }
            }

            List<int> GetNumbers(StreamReader reader, out int lastNumber)
            {
                lastNumber = -1;
                int currentNumber;
                int previousNumber;
                List<int> numbers = new List<int>();

                if (!TryGetNumber(reader, out previousNumber))
                    return numbers;

                numbers.Add(previousNumber);

                while (TryGetNumber(reader, out currentNumber))
                {
                    if (previousNumber <= currentNumber)
                        numbers.Add(currentNumber);
                    else
                    {
                        lastNumber = currentNumber;
                        break;
                    }

                    previousNumber = currentNumber;
                }

                return numbers;
            }
        }

        private static bool IsFileSorted(string path)
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

        private static void PreSort(string path, int size)
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