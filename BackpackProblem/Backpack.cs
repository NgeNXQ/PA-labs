namespace BackpackProblem
{
    internal sealed class Backpack
    {
        public readonly int Capacity;

        public int Weight { get; private set; }

        public int TotalPrice { get; private set; }

        private List<Item> items;

        public Backpack(int capacity)
        {
            this.Capacity = capacity;
            this.items = new List<Item>();
        }

        public void Add(Item item)
        {
            if (item.Weight + this.Weight <= this.Capacity)
            {
                items.Add(item);
                this.Weight += item.Weight;
                this.TotalPrice += item.Price;
            }
        }

        public int Fill(List<Item> items)
        {
            int weight = 0;
            List<Item> selectedItems = new List<Item>();
            items = items.OrderByDescending(item => item.Nectar).ToList();

            foreach (Item item in items)
            {
                if (weight + item.Weight <= this.Capacity)
                {
                    weight += item.Weight;
                    selectedItems.Add(item);
                    Console.WriteLine($"Був доданий новий предмет. Вага: {item.Weight}. Ціна: {item.Price}");
                }
                else
                    break;
            }

            return selectedItems.Sum(item => item.Price);
        }

        public int Fill(List<Item> items, int numberOfScouts, int numberOfForagers, int numberOfFields)
        {
            List<Item> scoutedItems;
            List<Item> appropriateItems = GetAppropriateItems(items);

            while (true)
            {
                if (items.Count < 1 || this.Weight > this.Capacity || appropriateItems.Count == this.items.Count)
                    break;

                SpawnScouts(numberOfFields, numberOfScouts, items, out scoutedItems);
                SpawnForagers(numberOfForagers, scoutedItems, items, out scoutedItems);
                scoutedItems = GetItems(scoutedItems, appropriateItems);

                if (scoutedItems.Count != 0)
                {
                    this.Weight += CalculateWeight(scoutedItems);
                    this.items.AddRange(scoutedItems);

                    foreach (Item item in scoutedItems)
                        Console.WriteLine($"Був доданий новий предмет. Вага: {item.Weight}. Ціна: {item.Price}");

                    items.RemoveAll(item => scoutedItems.Contains(item));
                }
            }

            return CalculatePrice(this.items);

            void SpawnScouts(int numberOfFields, int numberOfScouts, List<Item> items, out List<Item> scoutedItems)
            {
                scoutedItems = new List<Item>();
                scoutedItems.AddRange(GetRandomItems(0, items.Count, numberOfScouts, items));
            }

            void SpawnForagers(int count, List<Item> itemsToVisit, List<Item> items, out List<Item> visitedItems)
            {
                visitedItems = new List<Item>();
                visitedItems.AddRange(itemsToVisit);

                if (count > visitedItems.Count)
                {
                    List<Item> otherItems = items.Except(visitedItems).ToList();
                    visitedItems.AddRange(GetRandomItems(0, otherItems.Count, count - visitedItems.Count, items));
                }
            }

            List<Item> GetRandomItems(int min, int max, int numberOfElements, List<Item> items)
            {
                Random random = new Random();
                List<Item> selectedItems = new List<Item>();

                int[] positions = Enumerable.Range(min, max).OrderBy(i => random.Next(min, max)).Take(numberOfElements).ToArray();

                for (int i = 0; i < positions.Length; ++i)
                    selectedItems.Add(items[positions[i]]);

                return selectedItems;
            }

            List<Item> GetAppropriateItems(List<Item> items)
            {
                int weight = 0;
                List<Item> appropriateItems = new List<Item>();
                items = items.OrderByDescending(item => item.Nectar).ToList();

                GetList(items, ref weight);

                List<Item> newItemList = items.Except(appropriateItems).OrderBy(item => item.Weight).OrderBy(item => item.Price).ToList();

                GetList(newItemList, ref weight);

                return appropriateItems;

                void GetList(List<Item> items, ref int weight)
                {
                    foreach (Item item in items)
                    {
                        if (weight + item.Weight <= this.Capacity)
                        {
                            weight += item.Weight;
                            appropriateItems.Add(item);
                        }
                        else
                            break;
                    }
                }
            }

            List<Item> GetItems(List<Item> scoutedItems, List<Item> appropriateItems) => scoutedItems.Intersect(appropriateItems).ToList();

            int CalculateWeight(List<Item> selectedItems) => selectedItems.Sum(item => item.Weight);

            int CalculatePrice(List<Item> selectedItems) => selectedItems.Sum(item => item.Price);
        }
    }

    internal sealed class Item
    {
        public int Weight { get; }

        public int Price { get; }

        public float Nectar { get => (float)this.Price / this.Weight; }

        public Item(int weight, int price)
        {
            this.Weight = weight;
            this.Price = price;
        }

        public static Item GenerateItemsParameters(int minWeight, int maxWeight, int minPrice, int maxPrice)
        {
            Random random = new Random();
            return new Item(random.Next(minWeight, maxWeight), random.Next(minPrice, maxPrice));
        }
    }
}