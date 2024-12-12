
var input = "64599 31 674832 2659361 1 0 8867 321";
var stones = InitializeStones(input);
Dictionary<long, List<long>> stoneCache = new();

for (var i = 0; i < 75; i++)
{
    stones = ProcessStonesInBatch(stones);
}

long answer = stones.Values.Sum();
Console.WriteLine(answer);

static Dictionary<long, long> InitializeStones(string input)
{
    var inputList = input.Split(" ").Select(long.Parse).ToList();
    var stones = new Dictionary<long, long>();

    foreach (var number in inputList)
    {
        stones[number] = 1;
    }

    return stones;
}

Dictionary<long, long> ProcessStonesInBatch(Dictionary<long, long> stones)
{
    var blink = new Dictionary<long, long>();

    foreach (var stone in stones.Keys)
    {
        var multiplier = stones[stone];

        foreach (var newStone in TransformStone(stone))
        {
            if (!blink.TryAdd(newStone, multiplier))
            {
                blink[newStone] += multiplier;
            }
        }
    }

    return blink;
}

List<long> TransformStone(long engraving)
{
    if (stoneCache.TryGetValue(engraving, out List<long>? cachedValue))
    {
        return cachedValue;
    }

    var newStones = new List<long>();

    if (engraving == 0)
    {
        newStones.Add(1);
    }
    else
    {
        string str = engraving.ToString();
        if (str.Length % 2 == 0)
        {
            newStones.Add(long.Parse(str.Substring(0, str.Length / 2)));
            newStones.Add(long.Parse(str.Substring(str.Length / 2)));
        }
        else
        {
            newStones.Add(engraving * 2024);
        }
    }

    stoneCache[engraving] = newStones;
    return newStones;
}

