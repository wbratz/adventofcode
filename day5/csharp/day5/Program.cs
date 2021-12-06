using System.Text.Json;

public class Day5
{
    public static void Main()
    {
        var data = GetData();
        var Points = new Dictionary<(int, int), int>();

        var coordinateSets = new List<CoordinateSet>();

        foreach (var inputObj in data)
        {
            coordinateSets.Add(ToCoordinateSet(inputObj));
        }

        // part1 answer
        // foreach (var coordinateSet in coordinateSets.Where(x => x.IsHorizonal || x.IsVertical))
        // part2 answer
        foreach (var coordinateSet in coordinateSets)
        {
            foreach (var point in coordinateSet.AllPoints)
            {
                if(Points.ContainsKey(point))
                {
                    Points[point]++;
                }
                else
                {
                    Points.Add(point, 1);
                }
            }
        }

        var pointCount = Points.Where(x => x.Value > 1).ToList();
        Console.WriteLine();
    }

    public static InputObj[] GetTestData()
    {
        var result = new List<InputObj>();

        result.Add(new InputObj { firstCoordinateSet = "0,9", secondCoordinateSet = "5,9" });
        result.Add(new InputObj { firstCoordinateSet = "8,0", secondCoordinateSet = "0,8" });
        result.Add(new InputObj { firstCoordinateSet = "9,4", secondCoordinateSet = "3,4" });
        result.Add(new InputObj { firstCoordinateSet = "2,2", secondCoordinateSet = "2,1" });
        result.Add(new InputObj { firstCoordinateSet = "7,0", secondCoordinateSet = "7,4" });
        result.Add(new InputObj { firstCoordinateSet = "6,4", secondCoordinateSet = "2,0" });
        result.Add(new InputObj { firstCoordinateSet = "0,9", secondCoordinateSet = "2,9" });
        result.Add(new InputObj { firstCoordinateSet = "3,4", secondCoordinateSet = "1,4" });
        result.Add(new InputObj { firstCoordinateSet = "0,0", secondCoordinateSet = "8,8" });
        result.Add(new InputObj { firstCoordinateSet = "5,5", secondCoordinateSet = "8,2" });

        return result.ToArray();
    }

    public static InputObj[] GetData()
    {
        var filename = "../../data.json";
        using var stream = File.OpenRead(filename);
        return JsonSerializer.Deserialize<InputObj[]>(stream);
    }

    private static CoordinateSet ToCoordinateSet(InputObj inputObj)
    {
        var firstCoordinates = inputObj.firstCoordinateSet.Split(",");
        var secondCoodinates = inputObj.secondCoordinateSet.Split(",");
        
        return new CoordinateSet((int.Parse(firstCoordinates[0]), int.Parse(firstCoordinates[1]))
            ,(int.Parse(secondCoodinates[0]), int.Parse(secondCoodinates[1])));
    }
}

public class CoordinateSet
{
    public (int, int) StartPoint { get; set; }
    public (int, int) EndPoint { get; set; }
    public bool IsVertical => StartPoint.Item1 == EndPoint.Item1;
    public bool IsHorizonal => StartPoint.Item2 == EndPoint.Item2;
    public List<(int, int)> AllPoints { get; set; }

    public CoordinateSet((int, int) startPoint, (int, int) endPoint)
    {
        if(startPoint.Item1 > endPoint.Item1)
        {
            StartPoint = endPoint;
            EndPoint = startPoint;
        }
        else
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        AllPoints = GetAllPoints();
    }

    private List<(int, int)> GetAllPoints()
    {
        var result = new List<(int, int)>();

        var diffx = StartPoint.Item1 - EndPoint.Item1;
        var diffy = StartPoint.Item2 - EndPoint.Item2; 

        if(Math.Abs(diffx) > Math.Abs(diffy))
        {
            for (int i = 0; i <= Math.Abs(diffx); i++)
            {
                var x = diffx > 0 ? StartPoint.Item1 - i : StartPoint.Item1 + i;
                var y = StartPoint.Item2 + i;

                if(Math.Abs(diffx) <= i)
                {
                    x = EndPoint.Item1;
                }

                if(Math.Abs(diffy) <= i)
                {
                    y = EndPoint.Item2;
                }

                result.Add((x, y));
            }
        }
        else 
        {
            for (int i = 0; i <= Math.Abs(diffy); i++)
            {
                var x = StartPoint.Item1 + i;
                var y = diffy > 0 ? StartPoint.Item2 - i : StartPoint.Item2 + i;

                if(Math.Abs(diffx) <= i)
                {
                    x = EndPoint.Item1;
                }

                if(Math.Abs(diffy) <= i)
                {
                    y = EndPoint.Item2;
                }

                result.Add((x, y));
            }
        }

        return result;        
    }
}

public class InputObj
{
    public string firstCoordinateSet { get; set; }
    public string secondCoordinateSet { get; set; }
}
