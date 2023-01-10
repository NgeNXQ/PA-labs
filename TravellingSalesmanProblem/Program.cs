using TravellingSalesmanProblem;

namespace AntColony
{
    internal class AntColonyProgram
    {
        private static void Main()
        {
            System.Console.InputEncoding = System.Text.Encoding.Unicode;
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;

            int a = InputInt("Введіть значення АЛЬФА. Стандарте значення згідно до варіанту - 2. Введіть число: ", 0, Int32.MaxValue);
            int b = InputInt("Введіть значення БЕТА. Стандарте значення згідно до варіанту - 4. Введіть число: ", 0, Int32.MaxValue);
            float p = InputFloat("Введіть значення випаровування ферамону. Стандарте значення згідно до варіанту - 0.4. Введіть число: ", 0.0f, 1.0f);

            int numberOfIterations = InputInt("Введіть кількість ітерцій: ", 0, Int32.MaxValue);
            int numberOfAnts = InputInt("Введіть кількість мурах. Стандарте значення згідно до варіанту - 30. Введіть число: ", 0, Int32.MaxValue);
            int numberOfCities = InputInt("Введіть кількість міст. Стандарте значення згідно до варіанту - 50. Введіть число: ", 1, Int32.MaxValue);

            int MinDistance = InputInt("Введіть мінімальну відстань між містами. Стандарте значення згідно до варіанту - 5. Введіть число: ", 0, Int32.MaxValue);
            int MaxDistance = InputInt("Введіть максимальну відстань між містами. Стандарте значення згідно до варіанту - 50. Введіть число: ", 0, Int32.MaxValue);

            while (MinDistance > MaxDistance)
            {
                Console.WriteLine("Мінімальна відстань має бути менше максимальної!");
                MinDistance = InputInt("Введіть мінімальну відстань між містами. Стандарте значення згідно до варіанту - 5. Введіть число: ", 0, Int32.MaxValue);
                MaxDistance = InputInt("Введіть максимальну відстань між містами. Стандарте значення згідно до варіанту - 50. Введіть число: ", 0, Int32.MaxValue);
            }

            Graph graph = new Graph(numberOfCities);
            graph.GenerateDistanceMatrix(MinDistance, MaxDistance);
            graph.GeneratePheromoneMatrix(0.0f, 0.1f);

            Ant[] ants = new Ant[numberOfAnts];
            Ant.AllCities = graph.GetAllCities();

            for (int i = 0; i < numberOfAnts; ++i)
                ants[i] = new Ant(graph, Ant.GetRandomCity());

            List<int> distances = new List<int>();

            for (int i = 0; i < numberOfIterations; ++i)
            {
                Console.Write($"{i + 1}. ");

                foreach (Ant ant in ants)
                    ant.Move(a, b);

                distances.Add(ants.OrderBy(ant => ant.TravelledDistance).First().TravelledDistance);
                graph.UpdatePheromoneMatrix(p, distances[distances.Count - 1], ants);
                Console.WriteLine($"Мінімальна відстань поточної ітерації: {distances[distances.Count - 1]}.");
            }

            Console.WriteLine($"\nМінімальна відстань за результатом всіх ітерацій: {distances.OrderBy(distance => distance).First()}");
            graph.PrintPath();
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

        private static float InputFloat(string text, float min, float max)
        {
            float value;

            do
            {
                Console.Write(text);

                if (Single.TryParse(Console.ReadLine(), out value))
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
