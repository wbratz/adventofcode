
using System.Text.Json;

public class Day6
{

    public static void Main()
    {

        var data = GetData();

       // Console.WriteLine($"Part 1 Answer: {GetPart1(data)}");
        Console.WriteLine($"Part 1 Answer:{CountFish(data, 256)}");

        long CountFish(IEnumerable<int> input, int generations)
        {
            var fish = new long[9];
            foreach (var age in input)
              fish[age]++;

            for (var i = 0; i < generations; ++i)
            {
                //fish[i] == 3
                //fish[i+7] == 10
                //fish[i + 7 % 9] == fish[1] == 130
                // 130
                // 
                // fish[i] count of fish with i days to 0
                // fish[i + 7] ?
                // fish[(i + 7) % 9] returns the index of the fish array that will reach 0 in 7 days
                // fish[(i + 7) % 9] += fish[i % 9] add new fish to the index with the correct maturity index

                // yeah I still don't understand this solution
                fish[(i + 7) % 9] += fish[i % 9];
            }

            return fish.Sum();
        }
    }
    
    public static List<LanternFish> CreateInitialLanternFish(int[] data)
    {
        var result = new List<LanternFish>();

        foreach (var item in data)
        {
            result.Add(new LanternFish(item));
        }

        return result;
    }

    public static int GetPart1(int[] data)
    {
        var lanternFishes = CreateInitialLanternFish(data);
        var days = 80;
        var currentDay = 1;
        //var totalFish = 0;
        // Console.WriteLine($"Initial state: {GetFishString(lanternFishes)}");
        // done via recursion
        // foreach (var item in GetData())
        // {
        //     totalFish += GetFishGenerated(item, days) + 1;
        // }
        while(currentDay <= days)
        {
            for (int i = 0; i < lanternFishes.Count; i++)
            {
                lanternFishes[i].DecrementTimer();
                if(lanternFishes[i].NewFishCreated)
                {
                    lanternFishes.Add(new LanternFish());
                }
            }
            
            currentDay++;
        }
        return lanternFishes.Count;
    }

    public static int GetFishGenerated(int intialValue, int days, int generatedCount = 0)
    {
        days  -= intialValue + 1;
        
        if (days >= 0)
        {
            generatedCount++;
            generatedCount = GetFishGenerated(6, days, generatedCount);
            generatedCount = GetFishGenerated(8, days, generatedCount);
        }
        return generatedCount;
    }

    public static string GetFishString(List<LanternFish> lanternFishes)
    {
        var fishdays = string.Empty;

        foreach (var fish in lanternFishes)
        {
            fishdays += $"{fish.Timer.ToString()}, "; 
        }

        return fishdays;
    }
    public static int[] GetData()
    {
        var filename = "../../data.json";
        using var stream = File.OpenRead(filename);
        return JsonSerializer.Deserialize<int[]>(stream);
    }

    public static int[] GetTestData()
    {
        return new[] { 3, 4, 3, 1, 2 };
    }

    public static bool RunTestHarness(int[] values, int day)
    {
        if(day == 1)
        {
            return Enumerable.SequenceEqual(values, new int[] { 2, 3, 2, 0, 1 });
        }
        if(day == 2)
        {
            return Enumerable.SequenceEqual(values, new int[] { 1, 2, 1, 6, 0, 8 });
        }
        if(day == 3)
        {
            return Enumerable.SequenceEqual(values, new int[] { 0, 1, 0, 5, 6, 7, 8 });
        }
        if(day == 4)
        {
            return Enumerable.SequenceEqual(values, new int[] { 6, 0, 6, 4, 5, 6, 7, 8, 8 });
        }

        return false;
    }

    public class LanternFish
    {
        public int Timer { get; set; }
        public bool NewFishCreated { get; set; }

        public LanternFish()
        {
            Timer = 9;
        }

        public LanternFish(int timer)
        {
            Timer = timer;
        }

        public void DecrementTimer()
        {
            if(Timer == 0)
            {
                Timer = 6;
                NewFishCreated = true;
            }
            else 
            {
                NewFishCreated = false;
                Timer -= 1;
            }
        }
    }
}