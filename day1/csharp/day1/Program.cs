// See https://aka.ms/new-console-template for more information
using System.Text.Json;

string filename = "../data.json";

using FileStream stream = File.OpenRead(filename);

int[] data = JsonSerializer.Deserialize<int[]>(stream);

int totalIncrease = 0;
int prevValue = 0;

for (int i = 0; i < data.Length; i++)
{
    if(i != 0)
    {
        totalIncrease = prevValue < data[i] ? totalIncrease + 1 : totalIncrease;        
    }

    prevValue = data[i];
}

Console.WriteLine($"Part 1 answer: {totalIncrease}");

totalIncrease = 0;
prevValue = 0;

for (int i = 0; i < data.Length; i++)
{    
    int sum = 0;

    if(i != 0 && i + 2 < data.Length)
    {
        sum = data[i] + data[i + 1] + data[i + 2];
        totalIncrease = prevValue < sum ? totalIncrease + 1 : totalIncrease;        
    }

    prevValue = sum;
}

Console.WriteLine($"Part 2 answer: {totalIncrease}");

