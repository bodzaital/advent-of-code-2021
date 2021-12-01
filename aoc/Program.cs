// Day 1: Sonar Sweep

// Part 1

string[] lines = File.ReadAllLines("Day1.txt");
int[] depths = lines.Select(x => int.Parse(x)).ToArray();
int larger = 0;

for (int i = 1; i < depths.Length; i++)
{
    if (depths[i] > depths[i - 1])
    {
        larger++;
    }
}

Console.WriteLine($"{larger} measurements are larger than the previous measurement.");

// Part 2

List<int> sums = new();
larger = 0;

for (int i = 0; i < depths.Length; i++)
{
    if (i + 2 > depths.Length - 1)
    {
        break;
    }

    sums.Add(depths[i] + depths[i + 1] + depths[i + 2]);
}

for (int i = 1; i < sums.Count; i++)
{
    if (sums[i] > sums[i - 1])
    {
        larger++;
    }
}

Console.WriteLine($"{larger} 3-window sums are larger than the previous 3-window sums.");
