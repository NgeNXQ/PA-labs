namespace BackpackProblem
{
    internal class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            int capacity = InputInt("Введіть значення місткості рюкзака. Значення згідно до варіанту - 500. Введіть значення: ", 0, Int32.MaxValue);
            int numberOfItems = InputInt("Введіть значення кількості предметів. Значення згідно до варіанту - 100. Введіть значення: ", 0, Int32.MaxValue);

            int minPrice = InputInt("Введіть мінімальну цінність предметів у рюкзаку. Значення згідно до варіанту - 2. Введіть значення: ", 0, Int32.MaxValue);
            int maxPrice = InputInt("Введіть максимальну цінність предметів у рюкзаку. Значення згідно до варіанту - 30. Введіть значення: ", 0, Int32.MaxValue);

            while (minPrice > maxPrice)
            {
                Console.WriteLine("Мінімальна ціна має бути менше максимальної!");
                minPrice = InputInt("Введіть мінімальну цінність предметів у рюкзаку. Значення згідно до варіанту - 2. Введіть значення: ", 0, Int32.MaxValue);
                maxPrice = InputInt("Введіть максимальну цінність предметів у рюкзаку. Значення згідно до варіанту - 30. Введіть значення: ", 0, Int32.MaxValue);
            }

            int minWeight = InputInt("Введіть мінімальну вагу предметів у рюкзаку. Значення згідно до варіанту - 1. Введіть значення: ", 0, Int32.MaxValue);
            int maxWeight = InputInt("Введіть максимальну вагу предметів у рюкзаку. Значення згідно до варіанту - 20. Введіть значення: ", 0, Int32.MaxValue);

            while (minWeight > maxWeight)
            {
                Console.WriteLine("Мінімальна вага має бути менше максимальної!");
                minWeight = InputInt("Введіть мінімальну вагу предметів у рюкзаку. Значення згідно до варіанту - 1. Введіть значення: ", 0, Int32.MaxValue);
                maxWeight = InputInt("Введіть максимальну вагу предметів у рюкзаку. Значення згідно до варіанту - 20. Введіть значення: ", 0, Int32.MaxValue);
            }

            int numberOfScouts = InputInt("Введіть кількість розвідників: ", 0, Int32.MaxValue);
            int numberOfForagers = InputInt("Введіть кількість фуражирів: ", 0, Int32.MaxValue);
            int numberOfFields = InputInt("Введіть кількість ділянок для дослілдення: ", 0, Int32.MaxValue);

            Backpack backpack = new Backpack(capacity);
            List<Item> items = new List<Item>(numberOfItems);

            for (int i = 0; i < numberOfItems; ++i)
                items.Add(Item.GenerateItemsParameters(minWeight, maxWeight, minPrice, maxPrice));

            Console.WriteLine();
            Console.WriteLine($"\nМаксимальна ціна стандартним методом: {backpack.Fill(items)}");
            Console.WriteLine();
            Console.WriteLine($"\nМаксимальна ціна за допомогою бджолиного алгоритму: {backpack.Fill(items, numberOfScouts, numberOfForagers, numberOfFields)}");
        }

        private static int InputInt(string text, int min, int max)
        {
            int value;

            do
            {
                Console.Write(text);

                if (Int32.TryParse(Console.ReadLine(), out value))
                {
                    if (value <= min)
                    {
                        Console.WriteLine($"Введене значення має бути більше {min}!");
                        continue;
                    }

                    if (value >= max)
                    {
                        Console.WriteLine($"Введене значення має бути менше {max}!");
                        continue;
                    }

                    return value;
                }

                Console.WriteLine("Введена величина має бути числом!");

            } while (true);
        }
    }
}