using Assignment4;

List<Client> listOfClients = new List<Client>();

LoadFileValuesToMemory(listOfClients);

MainProgram();


void MainProgram()
{
    string mainMenuChoice = string.Empty;


    while (mainMenuChoice.ToUpper() != "Q")
	{
		try
		{
			DisplayMainMenu();
            mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
            if (mainMenuChoice == "N")
				NewClient();
			if (mainMenuChoice == "S")
				ShowClientInfo(listOfClients.Count, listOfClients.LastOrDefault());

			if (mainMenuChoice == "F")
				FindClientInList(listOfClients);
			if (mainMenuChoice == "R")
				RemoveClientFromList(listOfClients);
			if (mainMenuChoice == "D")
				DisplayAllClientsInList(listOfClients);
			if (mainMenuChoice == "Q")
			{
				SaveMemoryValuesToFile(listOfClients);
				Console.WriteLine("\nProgram ended.");
			}
			if (mainMenuChoice == "E")
			{
				string editMenuChoice = string.Empty;

                DisplayAllClientsInList(listOfClients);

                int index = PromptHeightWeight("\nWhat id would you like to edit? ");

                while (editMenuChoice.ToUpper() != "R")
				{		
                    DisplayEditMenu();
                    editMenuChoice = Prompt("\nWhat would you like to edit? ").ToUpper();
                    if (editMenuChoice == "F")
						listOfClients[index - 1].FirstName = GetFirstName();
					if (editMenuChoice == "L")
                        listOfClients[index - 1].LastName = GetLastName();
					if (editMenuChoice == "W")
                        listOfClients[index - 1].Weight = GetWeight();
					if (editMenuChoice == "H")
                        listOfClients[index - 1].Height = GetHeight();					
				}
			}


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Message}");
		}
	}
}

void DisplayMainMenu()
{
	Console.WriteLine("\nMain Menu");
	Console.WriteLine("N) New Client");
	Console.WriteLine("S) Show Client Info");
	Console.WriteLine("E) Edit Client Info");
	Console.WriteLine("F) Find Client In List");
	Console.WriteLine("R) Remove Client From List");
	Console.WriteLine("D) Display all Clients in List");
	Console.WriteLine("Q) Quit");
}

void DisplayEditMenu()
{
	Console.WriteLine("\nEdit Menu");
	Console.WriteLine("F) First Name");
	Console.WriteLine("L) Last Name");
	Console.WriteLine("W) Weight");
	Console.WriteLine("H) Height");
	Console.WriteLine("R) Return to Main Menu");
}

void ShowClientInfo(int id, Client client)
{
	if (client == null)
		Console.WriteLine($"No Client In Memory");
	else
	{
		Console.WriteLine($"[{id}] Client Name:{client.FullName}");
		Console.WriteLine($"    BMI Score:\t{client.BmiScore:n2}");
		Console.WriteLine($"    BMI Status:\t{client.BmiStatus}");
	}
}

string Prompt(string prompt)
{
	string myString = "";
	while (true)
	{
		try
		{
		Console.Write(prompt);
		myString = Console.ReadLine().Trim();
		if(string.IsNullOrEmpty(myString))
			throw new Exception($"Empty Input: Please enter something.");
		break;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	return myString;
}

int PromptHeightWeight(string prompt)
{
	int num = 0;
	while (true)
	{
		try
		{
		Console.Write(prompt);
		num = int.Parse(Console.ReadLine());
		if(num <= 0)
			throw new Exception($"Weight and Height cannot be <= 0.");
		break;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	return num;
}

void NewClient()
{
	Console.WriteLine();

	Client myClient = new Client(
		GetFirstName(),
 		GetLastName(),
		GetWeight(),
		GetHeight()
	);

	listOfClients.Add( myClient );
}

string GetFirstName()
{
	return Prompt($"Enter client's first name: ");
}

string GetLastName()
{
	return Prompt($"Enter client's last name: ");	
}

int GetWeight()
{
	return PromptHeightWeight($"Enter client's weight in pounds: ");
}

int GetHeight()
{
	return PromptHeightWeight($"Enter client's height in inches: ");
}


void FindClientInList(List<Client> listOfClients)
{
	List<Client> searchedClients = new List<Client>();

	string myString = Prompt($"\nEnter Client First or Last name: ");
	foreach (Client client in listOfClients)
	{
		if (client.FullName.ToUpper().Contains(myString.ToUpper()))
		{
            searchedClients.Add(client);
        }
	}

	if (searchedClients.Count == 0)
	{
		Console.WriteLine($"No Clients Match");
	}
	else
	{
		foreach (Client client in searchedClients) 
		{
			Console.WriteLine(client.FullName);
		}
	}
}

void RemoveClientFromList(List<Client> listOfClients)
{
	bool validId = false;
    DisplayAllClientsInList(listOfClients);

	while (!validId)
	{
		int index = PromptHeightWeight("\nWhat id would you like to remove? ");

		if (index > 0 && index <= listOfClients.Count)
		{
			listOfClients.RemoveAt(index - 1);
			validId = true;
		}
	}

	Console.WriteLine("\nClient removed.");
}

void DisplayAllClientsInList(List<Client> listOfClients)
{
	int index = 1;

	foreach (Client client in listOfClients)
	{
		ShowClientInfo(index, client);
		index++;
	}
}

void LoadFileValuesToMemory(List<Client> listOfClients)
{
	try
	{
		string fileName = @"c:\temp\ClientFile.csv";
		if (!File.Exists(fileName))
			throw new Exception($"The file {fileName} does not exist.");
		string[] csvFileInput = File.ReadAllLines(fileName);
		for (int i = 0; i < csvFileInput.Length; i++)
		{
			string[] items = csvFileInput[i].Split(',');
            Client myClient = new(items[0], items[1], int.Parse(items[2]), int.Parse(items[3]));
            listOfClients.Add(myClient);
        }

		Console.WriteLine($"Load complete. {fileName} has {listOfClients.Count} data entries");
	}
	catch (Exception ex)
	{
		Console.WriteLine($"{ex.Message}");
	}
}

void SaveMemoryValuesToFile(List<Client> listOfClients)
{
    string fileName = @"c:\temp\ClientFile.csv";
    string[] csvLines = new string[listOfClients.Count];
	for (int i = 0; i < listOfClients.Count; i++)
	{
		csvLines[i] = $"{listOfClients[i].FirstName},{listOfClients[i].LastName},{listOfClients[i].Weight},{listOfClients[i].Height}";        
    }
    File.WriteAllLines(fileName, csvLines);
    Console.WriteLine($"Save complete. {fileName} has {listOfClients.Count} entries.");
}



