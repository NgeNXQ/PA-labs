namespace TravellingSalesmanProblem
{
    public sealed class Ant
    {
        public static List<int> AllCities { get; set; } = new List<int>();

        private Graph graph;
        private int nextCity;
        private int currentCity;
        private List<int> citiesToVisit;

        public int StartCity { get; }

        public int TravelledDistance { get; private set; }

        public List<int> VisitedCities { get; private set; }

        public Ant(Graph graph, int startCity)
        {
            this.graph = graph;
            this.StartCity = startCity;
            this.currentCity = startCity;
            this.VisitedCities = new List<int>();
            citiesToVisit = new List<int>();
        }

        public void Move(int a, int b)
        {
            this.VisitedCities.Clear();
            this.TravelledDistance = 0;
            this.citiesToVisit = graph.GetAllCities();

            float denominator;
            var probabilities = new[] { new { city = -1, probability = 0.0f } }.ToList();

            while (true)
            {
                denominator = 0.0f;
                probabilities.Clear();
                this.VisitedCities.Add(currentCity);
                this.citiesToVisit.Remove(currentCity);

                for (int i = 0, length = this.citiesToVisit.Count; i < length; ++i)
                        denominator += MathF.Pow(graph.PheromoneMatrix[currentCity, citiesToVisit[i]], a) * MathF.Pow(1.0f / graph.DistanceMatrix[currentCity, citiesToVisit[i]], b);

                foreach (int city in this.citiesToVisit)
                    probabilities.Add(new { city = city, probability = GetProbability(currentCity, city, denominator) });

                if (probabilities.Count > 1)
                    probabilities = probabilities.OrderByDescending(record => record.probability).ToList();

                if (this.citiesToVisit.Count < 1)
                {
                    this.TravelledDistance += graph.DistanceMatrix[currentCity, StartCity];
                    this.VisitedCities.Add(this.StartCity);
                    break;
                }

                nextCity = probabilities.First().city;
                this.TravelledDistance += graph.DistanceMatrix[currentCity, nextCity];
                currentCity = nextCity;
            }

            float GetProbability(int currentCity, int nextCity, float denominator) =>
                MathF.Pow(graph.PheromoneMatrix[currentCity, nextCity], a) * MathF.Pow(1.0f / graph.DistanceMatrix[currentCity, nextCity], b) / denominator;
        }

        public static int GetRandomCity()
        {
            int city = AllCities[new Random().Next(0, AllCities.Count)];
            AllCities.Remove(city);
            return city;
        }
    }
}