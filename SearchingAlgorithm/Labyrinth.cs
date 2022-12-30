namespace SearchingAlgorithm
{
    internal sealed class Node
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char Character { get; set; }

        public float Heuristic { get; set; }

        public Node(int x, int y, char character)
        {
            this.X = x;
            this.Y = y;
            Character = character;
        }

        public override string ToString() => $"{this.Character}";
    }

    internal sealed class Labyrinth
    {
        private Node[,] labyrinth;

        public string Path { get; set; }

        private int width;

        private int height;

        private string labyrinthAsText = String.Empty;

        public Labyrinth(string path) { this.Path = path; }

        public Node StartNode { get; private set; }

        public Node EndNode { get; private set; }

        public Node this[int rowIndex, int columnIndex]
        {
            get { return labyrinth[rowIndex, columnIndex]; }
            set { labyrinth[rowIndex, columnIndex] = value; }
        }

        public void CreateLabyrinth()
        {
            LabyrinthIntoString();

            string[] splittedText = labyrinthAsText.Split('\n');

            bool startSet = false, endSet = false;

            width = splittedText[0].Length;
            height = splittedText.Length;

            labyrinth = new Node[height, width];

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    labyrinth[i, j] = new Node(i, j, splittedText[i][j]);

                    if (!startSet)
                    {
                        if (labyrinth[i, j].Character == 'S')
                        {
                            StartNode = labyrinth[i, j];
                            StartNode.Heuristic = 0;
                            startSet = true;
                        }
                    }

                    if (!endSet)
                    {
                        if (labyrinth[i, j].Character == 'F')
                        {
                            EndNode = labyrinth[i, j];
                            endSet = true;
                        }
                    }
                }
            }
        }

        public void Expand(Node node, List<Node> nextNodes, out bool isBlindCorner)
        {
            nextNodes.Clear();
            int wallsCounter = 0;

            if (node.X - 1 >= 0)
            {
                if (labyrinth[node.X - 1, node.Y].Character != '#')
                {
                    if (labyrinth[node.X - 1, node.Y].Character != '*' && labyrinth[node.X - 1, node.Y].Character != 'S')
                        nextNodes.Add(labyrinth[node.X - 1, node.Y]);
                }
                else
                    ++wallsCounter;
            }

            if (node.X + 1 < this.height)
            {
                if (labyrinth[node.X + 1, node.Y].Character != '#')
                {
                    if (labyrinth[node.X + 1, node.Y].Character != '*' && labyrinth[node.X + 1, node.Y].Character != 'S')
                        nextNodes.Add(labyrinth[node.X + 1, node.Y]);
                }
                else
                    ++wallsCounter;
            }

            if (node.Y + 1 < this.width)
            {
                if (labyrinth[node.X, node.Y + 1].Character != '#')
                {
                    if (labyrinth[node.X, node.Y + 1].Character != '*' && labyrinth[node.X, node.Y + 1].Character != 'S')
                        nextNodes.Add(labyrinth[node.X, node.Y + 1]);
                }
                else
                    ++wallsCounter;
            }

            if (node.Y - 1 >= 0)
            {
                if (labyrinth[node.X, node.Y - 1].Character != '#')
                {
                    if (labyrinth[node.X, node.Y - 1].Character != '*' && labyrinth[node.X, node.Y - 1].Character != 'S')
                        nextNodes.Add(labyrinth[node.X, node.Y - 1]);
                }
                else
                    ++wallsCounter;
            }

            isBlindCorner = wallsCounter > 2 ? true : false;
        }

        public void CalculateHeuristic()
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                    labyrinth[i, j].Heuristic = CalculateEuclideanDistance(labyrinth[i, j], this.EndNode);
            }

            float CalculateEuclideanDistance(Node node, Node goal) => MathF.Sqrt(MathF.Pow(node.X - goal.X, 2) + MathF.Pow(node.Y - goal.Y, 2));
        }

        private void LabyrinthIntoString()
        {
            string temp = String.Empty;

            using (StreamReader sr = new StreamReader(Path))
                temp = sr.ReadToEnd();

            for (int i = 0; i < temp.Length; ++i)
            {
                if (temp[i] != '\r')
                    labyrinthAsText += temp[i];
            }
        }

        public override string ToString()
        {
            string result = "\n";

            for (int i = 0; i < height; ++i)
            {
                result += '\t';
                for (int j = 0; j < width; ++j)
                    result += labyrinth[i, j];
                result += '\n';
            }

            return result;
        }
    }
}