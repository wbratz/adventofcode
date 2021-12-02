using System.Text.Json;

public class Day2
{
    private static int Forward { get; set; }
    private static int Down { get; set; }
    private static int Up { get; set; }
    private static int Aim { get; set; }
    private static int Depth { get; set; }

    public static void Main()
    {
        var filename = "../../data.json";

        using FileStream stream = File.OpenRead(filename);

        var data = JsonSerializer.Deserialize<string[]>(stream);

        foreach (var item in data)
        {
            var tuple = GetDirectionValueTuple(item);
            AssignDirectionalValue(tuple);
        }

        Console.WriteLine($"Part 1 answer: {Forward * Down}");
        Console.WriteLine($"Part 2 Answer: {Forward * Depth}");
    }

    public static (string, int) GetDirectionValueTuple(string str)
    {
        var items = str.Split(" ");

        return (items[0], int.Parse(items[1]));
    }

    public static void AssignDirectionalValue((string, int) tuple)
    {
        switch (tuple.Item1)
        {
            case "forward":
                Forward += tuple.Item2;
                Depth = Aim > 0 ? Depth + (Aim * tuple.Item2) : Depth;
                break;
            case "down":
                Down += tuple.Item2;
                Aim += tuple.Item2;
                break;
            case "up": 
                Down -= tuple.Item2;
                Aim -= tuple.Item2;
                break;
        }
    }
}



