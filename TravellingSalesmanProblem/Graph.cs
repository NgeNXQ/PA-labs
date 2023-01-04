namespace TravellingSalesmanProblem
{
    public sealed class Graph
    {
        public int NumberOfCities { get; }

        public int[,] DistanceMatrix { get; private set; }

        public float[,] PheromoneMatrix { get; set; }

        public Graph(int numberOfCities)
        { 
            this.NumberOfCities = numberOfCities;
            this.DistanceMatrix = new int[NumberOfCities, NumberOfCities];
            this.PheromoneMatrix = new float[NumberOfCities, NumberOfCities];
        }

        public List<int> GetAllCities() => Enumerable.Range(0, NumberOfCities).ToList();

        public void GenerateDistanceMatrix(int min, int max)
        {
            ++max;
            Random random = new Random();

            for (int i = 0; i < NumberOfCities; ++i)
            {
                for (int j = 0; j < NumberOfCities; ++j)
                {
                    if (i == j)
                        DistanceMatrix[i, j] = 0;
                    else
                        DistanceMatrix[i, j] = random.Next(min, max);
                }
            }
        }

        public void GeneratePheromoneMatrix(float min, float max)
        {
            Random random = new Random();

            for (int i = 0; i < NumberOfCities; ++i)
            {
                for (int j = 0; j < NumberOfCities; ++j)
                {
                    if (i == j)
                        PheromoneMatrix[i, j] = 0.0f;
                    else
                        PheromoneMatrix[i, j] = random.NextSingle() * (max - min) + min;
                }
            }
        }

        public void UpdatePheromoneMatrix(float p, int Lmin, Ant[] ants)
        {
            int cityIndex;
            float delta;

            for (int i = 0; i < NumberOfCities; ++i)
            {
                for (int j = 0; j < NumberOfCities; ++j)
                {
                    if (i == j)
                        continue;

                    delta = 0;

                    for (int k = 0; k < ants.Length; ++k)
                    {
                        cityIndex = ants[k].VisitedCities.IndexOf(i);

                        if (cityIndex + 1 < ants[k].VisitedCities.Count)
                        {
                            if (ants[k].VisitedCities[cityIndex + 1] == j)
                                delta += (float)Lmin / ants[k].TravelledDistance;
                        }
                    }

                    this.PheromoneMatrix[i, j] = (1.0f - p) * this.PheromoneMatrix[i, j] + delta;
                }
            }
        }

        public void PrintPath()
        {
            string sequence = String.Empty;
            HashSet<int> visitedCities = new HashSet<int>();
            var row = new[] { new { city = -1, pheromone = 0.0f } }.ToList();

            for (int i = 0; i < NumberOfCities; ++i)
            {
                row.Clear();

                for (int j = 0; j < NumberOfCities; ++j)
                    row.Add(new { city = j, pheromone = this.PheromoneMatrix[i, j] });

                row = row.OrderByDescending(edge => edge.pheromone).ToList();

                for (int j = 0; j < NumberOfCities; ++j)
                {
                    if (visitedCities.Add(row[j].city))
                    {
                        sequence += $"{row[j].city} -> ";
                        break;
                    }
                }
            }

            Console.WriteLine(sequence.Remove(sequence.Length - 4));
        }
    }
}