namespace SearchingAlgorithm
{
    internal static class SearchingAlgorithms
    {
        private static int iterations = 0;
        public static int Iterations { get => iterations; }

        private static int states = 0;
        public static int States { get => states; }

        private static int statesInMemory = 0;
        public static int StatesInMemory { get => statesInMemory; }

        private static int blindCorners = 0;
        public static int BlindCorners { get => blindCorners; }

        private static bool isBlindCorner;

        public static void LDFS(Labyrinth labyrinth, int limit)
        {
            Stack<Node> toVisit = new Stack<Node>();
            List<Node> next = new List<Node>();

            if (Recursive_LDFS(labyrinth.StartNode, limit))
                PrintResult(labyrinth, true, toVisit.Count);
            else
                PrintResult(labyrinth, false, toVisit.Count);

            bool Recursive_LDFS(Node node, int depth)
            {
                ++states;

                if (node == labyrinth.EndNode)
                    return true;

                if (depth < 1)
                    return false;

                ++iterations;

                if (node != labyrinth.StartNode)
                    node.Character = '*';

                labyrinth.Expand(node, next, out isBlindCorner);

                if (isBlindCorner)
                    ++blindCorners;

                if (next.Count < 1)
                {
                    if (toVisit.Count < 1)
                        return false;

                    return Recursive_LDFS(toVisit.Pop(), --depth);
                }

                if (next.Count > 1)
                {
                    for (int i = 1, length = next.Count; i < length; ++i)
                        toVisit.Push(next[i]);
                }

                return Recursive_LDFS(next[0], --depth);
            }
        }

        public static void RBFS(Labyrinth labyrinth)
        {
            List<Node> next = new List<Node>();
            labyrinth.CalculateHeuristic();

            if (Recursive_RBFS(labyrinth.StartNode, float.MaxValue))
                PrintResult(labyrinth, true, statesInMemory);
            else
                PrintResult(labyrinth, false, statesInMemory);

            bool Recursive_RBFS(Node node, float f_limit)
            {
                ++states;

                if (node == labyrinth.EndNode)
                    return true;

                if (node != labyrinth.StartNode)
                    node.Character = '*';

                ++iterations;

                while (true)
                {
                    labyrinth.Expand(node, next, out isBlindCorner);

                    if (isBlindCorner)
                        ++blindCorners;

                    if (next.Count < 1)
                        return false;

                    if (next.Count > 1)
                    {
                        next = next.OrderBy(node => node.Heuristic).ToList();
                        statesInMemory += next.Count - 1;
                    }                      

                    Node best = next[0];

                    if (best.Heuristic > f_limit)
                        return false;     

                    float alternative = float.MaxValue;

                    if (next.Count > 1)
                        alternative = next[1].Heuristic;

                    bool result = Recursive_RBFS(best, MathF.Min(f_limit, alternative));

                    if (result)
                        return result;  
                }
            }
        }

        private static void PrintResult(Labyrinth labyrinth, bool found, int savedStates)
        {
            statesInMemory = savedStates;

            Console.Clear();
            Console.WriteLine(labyrinth);
            Console.WriteLine(found ? "\tЗнайдено!\n" : "\tНе знайдено!\n");
            Console.WriteLine($"\tКількість ітераці: {Iterations}\n\tКількість глухих кутів: {BlindCorners}\n" +
                $"\tЗагальна кількість станів: {States}\n\tКількість станів у пам'яті: {StatesInMemory}");

            Reset();
        }

        private static void Reset()
        {
            states = 0;
            iterations = 0;
            blindCorners = 0;
            statesInMemory = 0;
        }      
    }
}