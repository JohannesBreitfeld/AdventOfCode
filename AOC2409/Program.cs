var path = Path.Combine("..", "..", "..", "..", "input09.txt");
var input = File.ReadAllText(path);

List<int> diskMap = new();

for (int i = 0; i < input.Length; i++)
{
    int parsed = (int)(input[i] - '0');

    if (i % 2 == 0)
    {
        for (int j = 0; j < parsed; j++)
        {
            diskMap.Add(i / 2);
        }
    }
    else
    {
        for (int k = 0; k < parsed; k++)
        {
            diskMap.Add(-1);
        }
    }
}

var problem1 = Problem1(diskMap);
var problem2 = Problem2(diskMap);

Console.WriteLine($"Answer 1: {problem1}");
Console.WriteLine($"Answer 2: {problem2}");

static long Problem1(List<int> diskMap)
{
    int spaceIndex = diskMap.IndexOf(diskMap.First(x => x == -1));
    int fileIndex = diskMap.LastIndexOf(diskMap.Last(x => x != -1));

    while (spaceIndex < fileIndex)
    {
        diskMap[spaceIndex] = diskMap[fileIndex];
        diskMap[fileIndex] = -1;
       
        spaceIndex = diskMap.IndexOf(diskMap.First(x => x == -1));
        fileIndex = diskMap.LastIndexOf(diskMap.Last(x => x != -1));
    }

    long checksum = 0;
    for (int i = 0; i < diskMap.Count && diskMap[i] != -1; i++)
    {
        checksum += i * diskMap[i];
    }
    return checksum;
}

static long Problem2(List<int> diskMap)
{
    var files = new HashSet<(int fileId, int length, int startIndex)>();
    int currentFileId = 0;

    for (int i = 0; i < diskMap.Count; i++)
    {
        if (diskMap[i] != -1)
        {
            int length = 1;
            while (i + 1 < diskMap.Count && diskMap[i + 1] == diskMap[i])
            {
                i++;
                length++;
            }
            files.Add((currentFileId, length, i - length + 1));
            currentFileId++;
        }
    }

    foreach (var file in files.OrderByDescending(f => f.fileId))
    {
        int fileId = file.fileId;
        int fileLength = file.length;
        int originalStartIndex = file.startIndex;
        int freeSpaceStart = -1;

        for (int i = 0; i < diskMap.Count - fileLength + 1; i++)
        {
            if (diskMap.Skip(i).Take(fileLength).All(x => x == -1) && i <= originalStartIndex)
            {
                freeSpaceStart = i;
                break;
            }
        }

        if (freeSpaceStart != -1)
        {
            for (int i = 0; i < diskMap.Count; i++)
            {
                if (diskMap[i] == fileId)
                {
                    diskMap[i] = -1;
                }
            }

            for (int i = freeSpaceStart; i < freeSpaceStart + fileLength; i++)
            {
                diskMap[i] = fileId;
            }
        }
    }

    long checksum2 = 0;
    for (int i = 0; i < diskMap.Count; i++)
    {
        if (diskMap[i] != -1)
        {
            checksum2 += i * diskMap[i];
        }
    }
    return checksum2;
}