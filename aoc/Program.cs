// Day 1: Sonar Sweep

static void Day1()
{
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

    Console.WriteLine($"1.1 {larger} measurements are larger than the previous measurement.");

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
    
    Console.WriteLine($"1.2 {larger} 3-window sums are larger than the previous 3-window sums.");
}

Day1();

// Day 2: Dive!

static void Day2()
{
    string[] lines = File.ReadAllLines("Day2.txt");
    List<string[]> commands = lines.Select((e) => e.Split(' ')).ToList();

    int x = 0;
    int y = 0;

    foreach (string[] command in commands)
    {
        string direction = command[0];
        int distance = int.Parse(command[1]);

        if (direction == "forward")
        {
            x += distance;
        }
        else if (direction == "up")
        {
            y -= distance;
        }
        else if (direction == "down")
        {
            y += distance;
        }
    }

    Console.WriteLine($"2.1 x and y multiplied together is {x * y}");

    x = 0;
    y = 0;
    int aim = 0;

    foreach (string[] command in commands)
    {
        string direction = command[0];
        int distance = int.Parse(command[1]);

        if (direction == "forward")
        {
            x += distance;
            y += aim * distance;
        }
        else if (direction == "up")
        {
            aim -= distance;
        }
        else if (direction == "down")
        {
            aim += distance;
        }
    }

    Console.WriteLine($"2.2 x and y, when y is aim, multiplied together is {x * y}");
}

Day2();

// Day 3: Binary Diagnostic

static void Day3()
{
    // Part 1

    string[] lines = File.ReadAllLines("Day3.txt");

    int num0 = 0;
    int num1 = 0;

    string value1 = string.Empty;
    string value2 = string.Empty;

    for (int i = 0; i < lines[0].Length; i++)
    {
        for (int j = 0; j < lines.Length; j++)
        {
            if (lines[j][i] == '0')
            {
                num0++;
            }
            else
            {
                num1++;
            }
        }

        if (num0 > num1)
        {
            value1 += '0';
        }
        else
        {
            value1 += '1';
        }

        num0 = 0;
        num1 = 0;
    }

    for (int i = 0; i < value1.Length; i++)
    {
        if (value1[i] == '0')
        {
            value2 += '1';
        }
        else
        {
            value2 += '0';
        }
    }

    int power = Convert.ToInt32(value1, 2) * Convert.ToInt32(value2, 2);

    Console.WriteLine($"3.1 The power consumption of the submarine is {power}");

    // Part 2.

    List<ValuesOf<string, bool>> markedList = new();
    num0 = 0;
    num1 = 0;

    foreach (string line in lines)
    {
        markedList.Add(new(line, false));
    }

    for (int i = 0; i < markedList[0].Value1.Length && markedList.Count > 1; i++)
    {
        for (int j = 0; j < markedList.Count; j++)
        {
            char bit = markedList[j].Value1[i];
            if (bit == '0')
            {
                num0++;
            }
            else
            {
                num1++;
            }
        }

        char mostcommonbit = num0 > num1 ? '0' : '1';

        for (int j = 0; j < markedList.Count; j++)
        {
            if (markedList[j].Value1[i] != mostcommonbit)
            {
                markedList[j].Value2 = true;
            }
        }

        markedList.RemoveAll((e) => e.Value2 == true);

        num0 = 0;
        num1 = 0;
    }

    string oxygen = markedList[0].Value1;

    markedList = new();
    num0 = 0;
    num1 = 0;

    foreach (string line in lines)
    {
        markedList.Add(new(line, false));
    }

    for (int i = 0; i < markedList[0].Value1.Length && markedList.Count > 1; i++)
    {
        for (int j = 0; j < markedList.Count; j++)
        {
            char bit = markedList[j].Value1[i];
            if (bit == '0')
            {
                num0++;
            }
            else
            {
                num1++;
            }
        }

        char mostcommonbit = num0 > num1 ? '0' : '1';

        for (int j = 0; j < markedList.Count; j++)
        {
            if (markedList[j].Value1[i] == mostcommonbit)
            {
                markedList[j].Value2 = true;
            }
        }

        markedList.RemoveAll((e) => e.Value2 == true);

        num0 = 0;
        num1 = 0;
    }

    string co2 = markedList[0].Value1;

    int life = Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);

    Console.WriteLine($"3.2 The life support rating is {life}");
}

Day3();

class ValuesOf<T1, T2>
{
    public T1 Value1;
    public T2 Value2;

    public ValuesOf(T1 value1, T2 value2)
    {
        Value1 = value1;
        Value2 = value2;
    }
}