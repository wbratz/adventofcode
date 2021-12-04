using System.Text.Json;

public class Day3
{
    public static string MostCommon { get; set; }
    public static string LeastCommon {get; set;}
    public static List<string> OxygenGenList { get; set; } 
    public static List<string> Co2ScrubberList { get; set; }

    public static string[] Data = GetData();
    public static void Main()
    {
        OxygenGenList = GetData().ToList();
        Co2ScrubberList = GetData().ToList();

        var zeros = 0;
        var ones = 0;

        for (int i = 0; i < Data[i].Length; i++)
        {
            Reset(ref zeros, ref ones);

            CountValuesAtPosition(Data.ToList(), i, ref zeros, ref ones);

            MostCommon += GetMostCommon(ones, zeros);
            LeastCommon += GetLeastCommon(ones, zeros);

            Reset(ref zeros, ref ones);

            CountValuesAtPosition(OxygenGenList.ToList(), i, ref zeros, ref ones);     

            if(ones >= zeros)
            {
                RemoveFromList(OxygenGenList, '1', i);
            }
            else
            {
                RemoveFromList(OxygenGenList, '0', i);
            }                

            Reset(ref zeros, ref ones);

            CountValuesAtPosition(Co2ScrubberList.ToList(), i, ref zeros, ref ones);
            
            if(zeros <= ones && Co2ScrubberList.Count > 1)
            {
                RemoveFromList(Co2ScrubberList, '0', i);
            }
            else if(Co2ScrubberList.Count > 1)
            {
                RemoveFromList(Co2ScrubberList, '1', i);
            }
        }

        var oxygenRating = Convert.ToInt32(OxygenGenList[0], 2);
        var c02ScrubberRating = Convert.ToInt32(Co2ScrubberList[0], 2);
        
        Console.WriteLine($"Part 1 answer: {Convert.ToInt32(MostCommon, 2) * Convert.ToInt32(LeastCommon, 2)}");
        Console.WriteLine($"Part 2 Answer: {oxygenRating * c02ScrubberRating}");
    }

    public static string GetMostCommon(int ones, int zeros)
    {
        return ones > zeros ? "1": "0";
    }

    public static string GetLeastCommon(int ones, int zeros)
    {
        return ones > zeros ? "0": "1";
    }

    public static void RemoveFromList(List<string> list, char value, int pos)
    {
        Data.Where(x => x[pos] != value).Select(y => list.Remove(y)).ToList();
    }

    public static string[] GetData()
    {
        var filename = "../../data.json";
        using var stream = File.OpenRead(filename);

        return JsonSerializer.Deserialize<string[]>(stream);
    }

    public static void IncrementZerosOrOnes(char val, ref int zeros, ref int ones)
    {
        if(val == '0')
        {
            zeros += 1;
        }
        else
        {
            ones += 1;
        }
    }

    public static void Reset(ref int zeros, ref int ones)
    {
        zeros = 0;
        ones = 0;
    }

    public static void CountValuesAtPosition(List<string> list, int pos, ref int zeros, ref int ones)
    {
        foreach (var item in list)
        {
            IncrementZerosOrOnes(item[pos], ref zeros, ref ones);
        } 
    }
}


