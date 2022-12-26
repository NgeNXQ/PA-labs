Console.Write("Enter number of elements: ");
int number = Int32.Parse(Console.ReadLine());
Console.Write("Enter lower bound: ");
int lowerBound = Int32.Parse(Console.ReadLine());
Console.Write("Enter upper bound: ");
int upperBound = Int32.Parse(Console.ReadLine()) + 1;

Random r = new Random();

string path = @$"{Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(
    Directory.GetCurrentDirectory()).FullName).FullName).FullName).FullName}\Files\A.txt";

using (StreamWriter sw = new StreamWriter(File.OpenWrite(path)))
{
    for (int i = 0; i < number; ++i)
        sw.Write($"{r.Next(lowerBound, upperBound)} ");
}