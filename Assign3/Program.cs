
int physicalSize = 31;
int logicalSize = 0;

double minValue = 0.0;
double maxValue = 100.0;

double[] values = new double[physicalSize];
string[] dates = new string[physicalSize];

bool goAgain = true;
while (goAgain)
{
    try
    {
        DisplayMainMenu();
        string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
        if (mainMenuChoice == "L")
            logicalSize = LoadFileValuesToMemory(dates, values);
        if (mainMenuChoice == "S")
            SaveMemoryValuesToFile(dates, values, logicalSize);
        if (mainMenuChoice == "D")
            DisplayMemoryValues(dates, values, logicalSize);
        if (mainMenuChoice == "A")
            logicalSize = AddMemoryValues(dates, values, logicalSize);
        if (mainMenuChoice == "E")
            EditMemoryValues(dates, values, logicalSize);
        if (mainMenuChoice == "Q")
        {
            goAgain = false;
            throw new Exception("Bye, hope to see you again.");
        }
        if (mainMenuChoice == "R")
        {
            while (true)
            {
                if (logicalSize == 0)
                    throw new Exception("No entries loaded. Please load a file into memory");
                DisplayAnalysisMenu();
                string analysisMenuChoice = Prompt("\nEnter an Analysis Menu Choice: ").ToUpper();
                if (analysisMenuChoice == "A")
                    FindAverageOfValuesInMemory(values, logicalSize);
                if (analysisMenuChoice == "H")
                    FindHighestValueInMemory(values, logicalSize);
                if (analysisMenuChoice == "L")
                    FindLowestValueInMemory(values, logicalSize);
                if (analysisMenuChoice == "G")
                    GraphValuesInMemory(dates, values, logicalSize);
                if (analysisMenuChoice == "R")
                    throw new Exception("Returning to Main Menu");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}");
    }
}

void DisplayMainMenu()
{
    Console.WriteLine("\nMain Menu");
    Console.WriteLine("L) Load Values from File to Memory");
    Console.WriteLine("S) Save Values from Memory to File");
    Console.WriteLine("D) Display Values in Memory");
    Console.WriteLine("A) Add Value in Memory");
    Console.WriteLine("E) Edit Value in Memory");
    Console.WriteLine("R) Analysis Menu");
    Console.WriteLine("Q) Quit");
}

void DisplayAnalysisMenu()
{
    Console.WriteLine("\nAnalysis Menu");
    Console.WriteLine("A) Find Average of Values in Memory");
    Console.WriteLine("H) Find Highest Value in Memory");
    Console.WriteLine("L) Find Lowest Value in Memory");
    Console.WriteLine("G) Graph Values in Memory");
    Console.WriteLine("R) Return to Main Menu");
}

String Prompt(string prompt)
{
    bool inValidEntry = true;
    string promptString = "";
    while (inValidEntry)

    {
        try
        {
            Console.Write(prompt);
            promptString = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(promptString))
                throw new Exception($"No input provided. Please enter a value.");
            inValidEntry = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    return promptString;
}

string GetFileName()
{
    string fileName = "";
    do
    {
        fileName = Prompt("Enter file name including .csv or .txt: ");
    } while (string.IsNullOrWhiteSpace(fileName));
    return fileName;
}

int LoadFileValuesToMemory(string[] dates, double[] values)
{
    string fileName = GetFileName();
    int logicalSize = 0;
    string filePath = $"./data/{fileName}";
    if (!File.Exists(filePath))
        throw new Exception($"The file {fileName} does not exist.");
    string[] csvFileInput = File.ReadAllLines(filePath);
    for (int i = 0; i < csvFileInput.Length; i++)
    {
        // Console.WriteLine($"lineIndex: {i}; line: {csvFileInput[i]}");
        string[] items = csvFileInput[i].Split(',');
        for (int j = 0; j < items.Length; j++)
        {
            // Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
        }
        if (i != 0)
        {
            dates[logicalSize] = items[0];
            values[logicalSize] = double.Parse(items[1]);
            logicalSize++;
        }
    }
    Console.WriteLine($"Loading concluded. The {fileName} file has {logicalSize} data entries");
    return logicalSize;
}

void DisplayMemoryValues(string[] dates, double[] values, int logicalSize)
{
    if (logicalSize == 0)
        throw new Exception($"No Entries loaded. Please load a file to memory or add a value in memory");
    Console.WriteLine($"\nCurrent Loaded Entries: {logicalSize}");
    Console.WriteLine($"   Date     Value");
    for (int i = 0; i < logicalSize; i++)
        Console.WriteLine($"{dates[i]}   {values[i]}");
}

double FindHighestValueInMemory(double[] values, int logicalSize)
{
    double highest = values[0];
    for (int i = 0; i < logicalSize; i++)
    {
        if (values[i] > highest)
            highest = values[i];
    }
    Console.WriteLine($"The highest value of the memory is: {highest:c2}.");
    return highest;
}

double FindLowestValueInMemory(double[] values, int logicalSize)
{
    double lowest = values[0];
    for (int i = 0; i < logicalSize; i++)
    {
        if (values[i] < lowest)
            lowest = values[i];
    }
    Console.WriteLine($"The lowest value of the memory is: {lowest:c2}.");
    return lowest;
}

void FindAverageOfValuesInMemory(double[] values, int logicalSize)
{
    double average = 0;
    for (int i = 0; i < logicalSize; i++)
    {
        average = average + values[i];
    }
    average = average / logicalSize;
    Console.WriteLine($"The average value of the memory is: {average:c2}.");
}

void SaveMemoryValuesToFile(string[] dates, double[] values, int logicalSize)
{
    Console.Write("Please enter file name including .csv or .txt: ");

    string fileName = Console.ReadLine();
    string filePath = $"./data/{fileName}";

    string[] fileLines = new string[logicalSize];
    fileLines[0] = "dates,values";
    for (int i = 0; i < logicalSize; i++)
    {
        fileLines[i] = $"{dates[i]},{values[i].ToString()}";
    }
    File.WriteAllLines(filePath, fileLines);

    Console.WriteLine($"Memory values saved to file. The {fileName} file now has {logicalSize} entries. ");
}


double PromptDoubleBetweenMinMax(String prompt, double min, double max)

{
    bool inValidInput = true;
    double num = 0;
    while (inValidInput)
    {
        try
        {
            Console.Write($"{prompt} between {min:n2} and {max:n2}: ");
            num = double.Parse(Console.ReadLine());
            if (num < min || num > max)
                throw new Exception($"Invalid. Must be between {min} and {max}.");
            inValidInput = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    return num;
}

string PromptDate(String prompt)
{
    bool inValidInput = true;
    DateTime date = DateTime.Today;
    Console.WriteLine(date);
    while (inValidInput)
    {
        try
        {
            Console.Write(prompt);
            date = DateTime.Parse(Console.ReadLine());
            Console.WriteLine(date);
            inValidInput = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
    return date.ToString("MM-dd-yyyy");
}


int AddMemoryValues(string[] dates, double[] values, int logicalSize)
{
    string date = "";
    double value = 0.0;

    date = PromptDate("Please enter the date (e.g. 11-23-2023): ");
    bool valid = false;
    for (int i = 0; i < logicalSize; i++)
        if (dates[i].Equals(date))
            valid = true;
    if (valid == true)
        throw new Exception($"{date} already exists. Edit entry or enter a different date instead.");

    value = PromptDoubleBetweenMinMax("Please enter a double value", minValue, maxValue);
    dates[logicalSize] = date;
    values[logicalSize] = value;
    logicalSize += 1;
    return logicalSize;
}

void EditMemoryValues(string[] dates, double[] values, int logicalSize)
{
    string date = "";
    double value = 0.0;
    int index = 0;

    if (logicalSize == 0)
        throw new Exception($"No entries loaded. Please load a file or add a value");
    date = PromptDate("Please enter date (e.g. 11-23-2023): ");
    bool valid = false;
    for (int i = 0; i < logicalSize; i++)
        if (dates[i].Equals(date))
        {
            valid = true;
            index = i;
        }
    if (valid == false)
        throw new Exception($"{date} has not been entered yet. Add entry instead.");
    value = PromptDoubleBetweenMinMax($"Please enter a double value", minValue, maxValue);
    values[index] = value;
}

void GraphValuesInMemory(string[] dates, double[] values, int logicalSize)
{
    //Console.WriteLine($"LogicalSize: {logicalSize}");
    double overallSpread = maxValue - minValue;
    //Console.WriteLine($"overallSpread: {overallSpread}");
    double rowSpread = overallSpread / 10;
    int yAxisMaxValue = (int)(maxValue - rowSpread);
    int yAxisSubtract = (int)rowSpread;
    //Console.WriteLine($"columnSpread: {columnSpread}");
    const int yAxisWidth = 7;
    const int columnWidth = 3;


    Console.Write($"\n{"Dollars",yAxisWidth}");

    for (int row = yAxisMaxValue; row >= minValue; row -= yAxisSubtract)
    {
        Console.Write($"\n{row,yAxisWidth:c0} |");
        for (int col = 1; col <= physicalSize; col++)
        {
            for (int j = 0; j < logicalSize; j++)
            {
                string tempDate = dates[j].Substring(3, 2);
                if (tempDate.Substring(0, 1) == "0")
                    tempDate = tempDate.Substring(1, 1);
                if (col.ToString() == tempDate)
                {
                    if (values[j] >= row && values[j] < row + rowSpread)
                        Console.Write($"{(int)values[j],columnWidth}");
                    else
                        Console.Write($"{" ",columnWidth}");
                    break;
                }
                if (j == logicalSize - 1)
                {
                    if (row == minValue)
                        Console.Write($"{"0",columnWidth}");
                    else
                        Console.Write($"{" ",columnWidth}");
                }
            }
        }
    }
    Console.Write($"\n{"----",yAxisWidth}--");
    for (int col = 1; col <= physicalSize; col++)
        Console.Write($"{"---",columnWidth}");
    Console.Write($"\n{"Days",yAxisWidth} |");
    for (int col = 1; col <= physicalSize; col++)
        Console.Write($"{col,columnWidth}");
    Console.Write($"\n");
}