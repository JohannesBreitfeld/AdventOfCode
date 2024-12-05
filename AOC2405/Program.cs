var rules = GetRules();
var printOrders = GetPrintOrders();
var correctOrders = GetFilteredOrders(rules, printOrders);
var incorrectOrders = GetFilteredOrders(rules, printOrders, false);

var sum = correctOrders.Select(order => order[order.Count / 2]).Sum();

var sortedIncorrectOrders = SortIncorrectOrders(incorrectOrders, rules);
var sum2 = sortedIncorrectOrders.Select(order => order[order.Count / 2]).Sum();

Console.WriteLine($"Problem1 : {sum}");
Console.WriteLine($"Problem2 : {sum2}");

static Dictionary<int, List<int>> GetRules()
{
    var path = Path.Combine("..", "..", "..", "..", "input05rules.txt");

    var rules = File.ReadAllLines(path);
    var rulesIntList = rules
        .Select(r => r.Split("|")
        .Select(s => int.Parse(s))
        .ToList())
        .ToList();
    var rulesDictionary = new Dictionary<int, List<int>>();

    for (int i = 0; i < rulesIntList.Count(); i++)
    {
        if (!rulesDictionary.ContainsKey(rulesIntList[i][0]))
        {
            rulesDictionary[rulesIntList[i][0]] = new List<int>();
        }
        rulesDictionary[rulesIntList[i][0]].Add(rulesIntList[i][1]);
    }
    return rulesDictionary;
}

static List<List<int>> GetPrintOrders()
{
    var path = Path.Combine("..", "..", "..", "..", "input05order.txt");
    var orderEntries = File.ReadAllLines(path);
    var orderIntList = orderEntries.Select(o => o.Split(",").Select(s => int.Parse(s)).ToList()).ToList();

    return orderIntList;
}

static List<List<int>> GetFilteredOrders(Dictionary<int, List<int>> rules, List<List<int>> printOrders, bool problem1 = true)
{
    var filteredOrders = new List<List<int>>();

    foreach (var order in printOrders)
    {
        var isValid = true;
        for (int i = 0; i < order.Count - 1; i++)
        {
            var currentNumber = order[i];
            for (int j = i + 1; j < order.Count; j++)
            {
                var nextNumber = order[j];
                if (!rules.ContainsKey(currentNumber))
                {
                    isValid = false;
                }
                else if (!rules[currentNumber].Contains(nextNumber))
                {
                    isValid = false;
                }
            }
        }
        if (problem1 && isValid)
        {
            filteredOrders.Add(order);
        }
        if (!problem1 && !isValid)
        {
            filteredOrders.Add(order);
        }
    }
    return filteredOrders;
}

static List<List<int>> SortIncorrectOrders(List<List<int>> orders, Dictionary<int, List<int>> rules)
{
    var sortedOrders = new List<List<int>>();

    foreach (var list in orders)
    {
        var newlist = new List<int>();

        for (int i = 0; i < list.Count; i++)
        {
            if (i == 0 || !rules.ContainsKey(list[i]))
            {
                newlist.Add(list[i]);
            }
            else
            {
                for (int j = newlist.Count - 1; j >= 0; j--)
                {
                    if (j == 0 && rules[list[i]].Contains(newlist[j]))
                    {
                        newlist.Insert(0, list[i]);
                        break;
                    }
                    else if (!rules[list[i]].Contains(newlist[j]))
                    {
                        newlist.Insert(j + 1, list[i]);
                        break;
                    }
                }
            }
        }
        sortedOrders.Add(newlist);
    }
    return sortedOrders;
}



