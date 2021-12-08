using System.Text.Json;

public class Day7
{
    public delegate int FuelCostCalculator(int [] data, int position);

    public static void Main()
    {
        var data = GetData();
        var testData = GetTestData();

        var mostEfficientPositions = GetMostEfficientPosition(data);

        // Works for test data, not for real data
        // foreach (var position in mostEfficientPositions)
        // {
        //     var fuelCosts = GetPart1TotalFuelCost(testData, position.Key);
        //     Console.WriteLine($"Fuel Cost for position {position.Key} : {fuelCosts}");
        // }
        var part1FuelCostCalculator = new FuelCostCalculator(GetPart1TotalFuelCost);
        var part2FuelCostCalculator = new FuelCostCalculator(GetPart2TotalFuelCost);

        Console.WriteLine($"Part 1 Answer: {CalculateLowestFuelCostForBestPosition(data, part1FuelCostCalculator)}");
        Console.WriteLine($"Part 2 Answer: {CalculateLowestFuelCostForBestPosition(data, part2FuelCostCalculator)}");
    }

    private static (long lowestFuelConsumption, int bestPostion) CalculateLowestFuelCostForBestPosition(int[] data, FuelCostCalculator fuelCostCalculator)
    {
        var lowestFuelConsumption = long.MaxValue;
        var bestPostion = 0;

        for (var i = 0; i < data.Max(); i++)
        {
            var fuelCosts = fuelCostCalculator(data, i);

            if (lowestFuelConsumption > fuelCosts)
            {
                lowestFuelConsumption = fuelCosts;
                bestPostion = i;
            }
        }

        return (lowestFuelConsumption, bestPostion);
    }

    public static int GetPart1TotalFuelCost(int[] data, int position)
    {
        var totalCost = 0;

        foreach (var item in data)
        {
            totalCost += Math.Abs(item - position);
        }

        return totalCost;
    }

    public static int GetPart2TotalFuelCost(int[] data, int position)
    {
        var totalCost = 0;

        foreach (var item in data)
        {
            var cost = Math.Abs(item - position);
            cost = ((cost * cost) + cost) / 2;
            totalCost += cost;
        }

        return totalCost;
    }

    //used in first attempt, doesn't work for real data.
    public static IEnumerable<KeyValuePair<int, int>> GetMostEfficientPosition(int[] data)
    {
        var numberFrequency = new Dictionary<int, int>();

        for (var i = 0; i < data.Length; i++)
        {
            if(numberFrequency.ContainsKey(data[i]))
            {
                numberFrequency[data[i]]++;
            }
            else
            {
                numberFrequency.Add(data[i], 1);
            }
        }

        int maxCount = 0;

        foreach(var key in numberFrequency.Keys)
        {
            numberFrequency.TryGetValue(key, out var value);
            maxCount = maxCount < value ? value : maxCount;
        }

        var vals = numberFrequency.Where(x => x.Value == maxCount);

        return vals;
    }

    public static int[] GetData()
    {
        using var stream = File.OpenRead("../../data.json");
        using var sr = new StreamReader(stream);
        var json = sr.ReadToEnd();

        return JsonSerializer.Deserialize<int[]>(json);
    }

    public static int[] GetTestData()
    {
        return new int[] {16,1,2,0,4,2,7,1,2,14};
    }
}
