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

Console.WriteLine($"Checksum 2: {checksum2}");
    