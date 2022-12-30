namespace SearchingAlgorithm
{
    internal sealed class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string path = GetLabyrinthPath();

            Labyrinth labyrinth = new Labyrinth(path);
            labyrinth.CreateLabyrinth();
            Console.WriteLine(labyrinth);

            string algorithmName = ChooseAlgorithm().ToLower();

            switch (algorithmName)
            {
                case "rbfs":
                    SearchingAlgorithms.RBFS(labyrinth);
                    break;
                case "ldfs":
                    int limit = GetLimitValue();
                    SearchingAlgorithms.LDFS(labyrinth, limit);
                break;
            }
        }

        private static string GetLabyrinthPath()
        {
            string mazeName;
            string path;

            do
            {
                Console.Write("Введіть назву лабіринта, який бажаєте дослідити: ");

                mazeName = Console.ReadLine();
                path = $"Mazes\\{mazeName}.txt";

                if (File.Exists(path))
                    return path;
                
                Console.WriteLine("Обраного лабіринту не існує!");

            } while (true);
        }

        private static string ChooseAlgorithm()
        {
            string algorithmName = String.Empty;

            do
            {
                Console.Write("Введіть алгоритм пошуку, яким бажаєте скористатися (RBFS/LDFS): ");
                algorithmName = Console.ReadLine();

                if (algorithmName.Equals("rbfs", StringComparison.OrdinalIgnoreCase) || algorithmName.Equals("ldfs", StringComparison.OrdinalIgnoreCase))
                    return algorithmName;

               Console.WriteLine("Обраного алгоритму не існує!");

            } while (true);
        }

        private static int GetLimitValue()
        {
            int limit;

            do
            {
                Console.Write("Введіть глибину пошуку: ");

                if (Int32.TryParse(Console.ReadLine(), out limit))
                {
                    if (limit > 0)
                        return limit;

                    Console.WriteLine("Введене значення має бути більше 0!");
                }
                else
                    Console.WriteLine("Введене значення має бути числом!");

            } while (true);
        }
    }
}