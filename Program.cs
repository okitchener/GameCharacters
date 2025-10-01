using NLog;
using System.Reflection;
using System.Text.Json;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

do
{
    // Main Menu - display choices to user
    Console.WriteLine("===== Game Characters Menu =====");
    Console.WriteLine("1) Mario Characters");
    Console.WriteLine("2) Donkey Kong Characters");
    Console.WriteLine("3) Street Fighter 2 Characters");
    Console.WriteLine("Enter to quit");
    Console.Write("Select an option: ");

    // input selection
    string? choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);

    if (choice == "1")
    {
        ShowMarioMenu();
    }
    else if (choice == "2")
    {
        ShowDkMenu();
    }
    else if (choice == "3")
    {
        // Street Fighter 2 Menu Placeholder
    }
    else if (string.IsNullOrEmpty(choice))
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid choice. Please try again.");
        logger.Info("Invalid choice");
    }
} while (true);

logger.Info("Program ended");

static void ShowMarioMenu()
{
    // Create logger instance for this method
    var logger = LogManager.GetCurrentClassLogger();

    // deserialize mario json from file into List<Mario>
    string marioFileName = "mario.json";
    List<Mario> marios = [];
    // check if file exists
    if (File.Exists(marioFileName))
    {
        marios = JsonSerializer.Deserialize<List<Mario>>
        (File.ReadAllText(marioFileName))!;
        logger.Info($"File deserialized {marioFileName}");
    }

    do
    {
        Console.WriteLine("\nMario Characters Menu");
        Console.WriteLine("1) Display Mario Characters");
        Console.WriteLine("2) Add Mario Character");
        Console.WriteLine("3) Remove Mario Character");
        Console.WriteLine("Enter to return to main menu");
        Console.Write("Select an option: ");

        // input selection
        string? choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            // Display Mario Characters
            foreach (var c in marios)
            {
                Console.WriteLine(c.Display());
            }
        }
        else if (choice == "2")
        {
            // Add Mario Character
            Mario mario = new()
            {
                Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
            };
            InputCharacter(mario);
            // Add Character
            marios.Add(mario);
            File.WriteAllText(marioFileName,
            JsonSerializer.Serialize(marios));
            logger.Info($"Character added: {mario.Name}");
        }
        else if (choice == "3")
        {
            // Remove Mario Character
            Console.WriteLine("Enter Id of the Character to remove:");
            if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
            {
                Mario? character = marios.FirstOrDefault(c => c.Id == Id);
                if (character == null)
                {
                    logger.Error($"Character Id {Id} not found");
                }
                else
                {
                    marios.Remove(character);
                    // serialize list<marioCharacter> to json file
                    File.WriteAllText(marioFileName,
                    JsonSerializer.Serialize(marios));
                    logger.Info($"Character Id {Id} removed");
                }
            }
            else
            {
                logger.Error("Invalid Id");
            }
        }
        else if (string.IsNullOrEmpty(choice))
        {
            break;
        }
        else
        {
            logger.Info("Invalid choice");
        }
    } while (true);
}

static void ShowDkMenu()
{
    // Create logger instance for this method
    var logger = LogManager.GetCurrentClassLogger();

    // deserialize dk json from file into List<Dk>
    string dkFileName = "dk.json";
    List<Dk> dks = JsonSerializer.Deserialize<List<Dk>>
        (File.ReadAllText(dkFileName))!;

    do
    {
        Console.WriteLine("\nDonkey Kong Characters Menu");
        Console.WriteLine("1) Display Donkey Kong Characters");
        Console.WriteLine("2) Add Donkey Kong Character");
        Console.WriteLine("3) Remove Donkey Kong Character");
        Console.WriteLine("Enter to return to main menu");

        //input selection
        string? choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            //Display Donkey Kong Characters
        }
        else if (choice == "2")
        {
            //Add Donkey Kong Character
        }
        else if (choice == "3")
        {
            //Remove Donkey Kong Character
        }
        else if (string.IsNullOrEmpty(choice))
        {
            break;
        }
        else
        {
            logger.Info("Invalid choice");
        }
    } while (true);
}

logger.Info("Program ended");

static void InputCharacter(Character character)
{
    Type type = character.GetType();
    PropertyInfo[] properties = type.GetProperties();
    var props = properties.Where(p => p.Name != "Id");
    foreach (PropertyInfo prop in props)
    {
        if (prop.PropertyType == typeof(string))
        {
            Console.WriteLine($"Enter {prop.Name}:");
            prop.SetValue(character, Console.ReadLine());
        }
        else if (prop.PropertyType == typeof(List<string>))
        {
            List<string> list = [];
            do
            {
                Console.WriteLine($"Enter {prop.Name} or (Enter to quit):");
                string response = Console.ReadLine()!;
                if (string.IsNullOrEmpty(response))
                {
                    break;
                }
            list.Add(response);
            } while (true);
            prop.SetValue(character, list);
        }
    }
}