import json

jsonFile = open("data.json")

data = json.load(jsonFile)

def GetPart1(input):
    total_increases = 0
    prev_value = 0

    for item in input:
        if prev_value != 0 and prev_value < item:
            total_increases += 1

        prev_value = item

    return total_increases

def GetPart2(input):
    total_increases = 0
    prev_value = 0

    for idx, elem in enumerate(input):
        sliding_sum = 0

        if idx + 2 < len(input):
            sliding_sum = elem + input[idx + 1] + input[idx + 2]
            
        if prev_value != 0 and prev_value < sliding_sum:
            total_increases += 1       

        prev_value = sliding_sum

    return total_increases


print(f"Part 1 answer: {GetPart1(data)}")
print(f"Part 2 answer: {GetPart2(data)}")

