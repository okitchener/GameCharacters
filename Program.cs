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
        ShowSf2Menu();
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
    List<Dk> dks = [];
    // check if file exists
    if (File.Exists(dkFileName))
    {
        dks = JsonSerializer.Deserialize<List<Dk>>
        (File.ReadAllText(dkFileName))!;
        logger.Info($"File deserialized {dkFileName}");
    }

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
            foreach (var c in dks)
            {
                Console.WriteLine(c.Display());
            }
        }
        else if (choice == "2")
        {
            //Add Donkey Kong Character
            Dk dk = new()
            {
                Id = dks.Count == 0 ? 1 : dks.Max(c => c.Id) + 1
            };
            // Input Character details
            Console.WriteLine("Enter Species:");
            dk.Species = Console.ReadLine()!;
            Console.WriteLine("Enter Name:");
            dk.Name = Console.ReadLine();
            Console.WriteLine("Enter Description:");
            dk.Description = Console.ReadLine();
            // Add Character
            dks.Add(dk);
            File.WriteAllText(dkFileName,
            JsonSerializer.Serialize(dks));
            logger.Info($"Character added: {dk.Name}");
        }
        else if (choice == "3")
        {
            //Remove Donkey Kong Character
            Console.WriteLine("Enter Id of the Character to remove:");
            if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
            {
                Dk? character = dks.FirstOrDefault(c => c.Id == Id);
                if (character == null)
                {
                    logger.Error($"Character Id {Id} not found");
                }
                else
                {
                    dks.Remove(character);
                    // serialize list<dkCharacter> to json file
                    File.WriteAllText(dkFileName,
                    JsonSerializer.Serialize(dks));
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

static void ShowSf2Menu()
{
    // Create logger instance for this method
    var logger = LogManager.GetCurrentClassLogger();

    // deserialize sf2 json from file into List<Sf2>
    string sf2FileName = "sf2.json";
    List<Sf2> sf2s = [];
    // check if file exists
    if (File.Exists(sf2FileName))
    {
        sf2s = JsonSerializer.Deserialize<List<Sf2>>
        (File.ReadAllText(sf2FileName))!;
        logger.Info($"File deserialized {sf2FileName}");
    }

    do
    {
        Console.WriteLine("\nStreet Fighter 2 Characters Menu");
        Console.WriteLine("1) Display Street Fighter 2 Characters");
        Console.WriteLine("2) Add Street Fighter 2 Character");
        Console.WriteLine("3) Remove Street Fighter 2 Character");
        Console.WriteLine("Enter to return to main menu");

        //input selection
        string? choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            //Display Street Fighter 2 Characters
            foreach (var c in sf2s)
            {
                Console.WriteLine(c.Display());
            }

        }
        else if (choice == "2")
        {
            //Add Street Fighter 2 Character
            Sf2 sf2 = new()
            {
                Id = sf2s.Count == 0 ? 1 : sf2s.Max(c => c.Id) + 1
            };
            // Input Character details
            InputCharacter(sf2);
            // Add Character
            sf2s.Add(sf2);
            File.WriteAllText(sf2FileName,
            JsonSerializer.Serialize(sf2s));
            logger.Info($"Character added: {sf2.Name}");
        }
        else if (choice == "3")
        {
            //Remove Street Fighter 2 Character
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