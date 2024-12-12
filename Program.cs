using Models;

ConnectionReader connectionReader = new ConnectionReader();
string connectionString = connectionReader.GetConnectionString();
SqlCommands sqlCommand = new SqlCommands(connectionString);


//runs till the user wants to exit
while (true)
{
    Console.Clear();
    // Display the menu options to the user
    Console.WriteLine("What do you want to do?");
    Console.WriteLine("1. Execute an SQL file");
    Console.WriteLine("2. Insert data into the table");
    Console.WriteLine("3. Read data from the table");
    Console.WriteLine("4. Update data in the table");
    Console.WriteLine("5. Delete data from the table");
    Console.WriteLine("6. Delete an existing table");
    Console.WriteLine("7. Exit");
    int choice;
    do
    {
        Console.Write("Enter the number of your choice: ");
    } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7);

    //switch case for the user's choice
    switch (choice)
    {
        case 1:
            // Choose and execute an SQL file
            sqlCommand.ExecuteSqlFile();
            break;
        case 2:
            // Insert data into the table
            sqlCommand.InsertData();
            break;
        case 3:
            // Choose and read data from the table
            sqlCommand.ReadData();
            break;
        case 4:
            // Call UpdateData
            Console.WriteLine("Update data in the table");
            break;
        case 5:
            // Call DeleteData
            Console.WriteLine("Delete data from the table");
            break;
        case 6:
            //Choose and delete table
            sqlCommand.DeleteTable();
            break;
        case 7:
            Console.WriteLine("Exiting the program...");
            return;
        default:
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
            break;
    }
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}




