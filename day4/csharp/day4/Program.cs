using System.Text.Json;

public class Day4
{
    public static Dictionary<int, Board> BoardRowFoundValues = new Dictionary<int, Board>();
    public static void Main()
    {
        var data = GetData();

        for (int i = 0; i < data.DrawnNumbers.Length; i++)
        {
            var currentNumber = data.DrawnNumbers[i];
            for (int j = 0; j < data.Boards.GetLength(0); j++)
            {
                TryAddBoardToDictionary(j);
                
                if(BoardRowFoundValues[j].IsWinner())
                {
                    continue;
                }
                
                for (int k = 0; k < data.Boards.GetLength(1); k++)
                {
                    for (int l = 0; l < data.Boards.GetLength(2); l++)
                    {
                        var currentBoardRowValue = data.Boards[j, k, l];

                        if(currentBoardRowValue == currentNumber)
                        {
                            BoardRowFoundValues[j].Rows.Add(k);
                            BoardRowFoundValues[j].Columns.Add(l);
                            BoardRowFoundValues[j].MarkedNumbers.Add(currentNumber);

                            if(BoardRowFoundValues[j].IsWinner())
                            {
                                GetBoardNumbers(data, j);
                                Console.WriteLine(BoardRowFoundValues[j].UnmarkedNumberSum() * currentNumber);
                            }
                        }
                    }
                }
            }
        }
    }

    private static void GetBoardNumbers(InputObj data, int pos)
    {
        for (int j = pos; j == pos; j++)
        {
            for (int k = 0; k < data.Boards.GetLength(1); k++)
            {
                for (int l = 0; l < data.Boards.GetLength(2); l++)
                {
                    BoardRowFoundValues[pos].Numbers.Add(data.Boards[j, k, l]);
                }
            }
        }
    }

    private static void TryAddBoardToDictionary(int j)
    {
        if (!BoardRowFoundValues.ContainsKey(j))
        {
            BoardRowFoundValues.Add(j, new Board());
        }
    }

    public static InputObj GetData()
    {
        var filename = "../../data.json";
        using var stream = File.OpenRead(filename);

        using var streamReader = new StreamReader(stream);
        var json = streamReader.ReadToEnd();

        return Newtonsoft.Json.JsonConvert.DeserializeObject<InputObj>(json);
    }
}

public class InputObj
{
    public int[] DrawnNumbers { get; set; }
    public int[,,] Boards { get; set; }
}

public class Board
{
    public List<int> Numbers { get; set; }
    public List<int> Rows { get; set; }
    public List<int> Columns { get; set; }
    public List<int> MarkedNumbers { get; set; }

    public Board()
    {
        Numbers = new List<int>();
        Rows = new List<int>();
        Columns = new List<int>();
        MarkedNumbers = new List<int>();
    }

    public int UnmarkedNumberSum()
    {
        return Numbers.Where(x => !MarkedNumbers.Contains(x)).Sum();
    }

    public bool IsWinner()
    {
        return RowWinner() || ColumnWinner();
    }

    private bool RowWinner()
    {
        for (int i = 0; i < 5; i++)
        {
            if(Columns.Where(x => x == i).Count() == 5)
            {
                return true;
            }
        }

        return false;
    }

    private bool ColumnWinner()
    {
        for (int i = 0; i < 5; i++)
        {
            if(Rows.Where(x => x == i).Count() == 5)
            {
                return true;
            }
        }

        return false;
    }
}
