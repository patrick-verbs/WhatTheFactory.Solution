using System;
using System.IO;
using System.Text;

namespace Factory.Models
{
  public class AppSettings
  {
    public bool SettingsValidated { get; set; }

    public static void CheckSettings()
    {
      string appsettingsFile = @"appsettings.json";
      if (!File.Exists(appsettingsFile))
      {
        Console.WriteLine("\n\n\n\n\nIn order to run, this app requires an \"appsettings.json\" file containing your MySQL username and password.");
        Console.WriteLine("\nWould you like the program to create the appsettings.json file for you?");
        Console.Write("(");
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.Write("Y");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("es, let's build the file now");
        Console.ResetColor();
        Console.Write(")\n(");
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.Write("N");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Write("o, I'll create the file myself");
        Console.ResetColor();
        Console.Write(")\n ");
        string userChoice = (Console.ReadLine()).ToLower();
        if (userChoice[0] != 'y')
        {
          Console.Write("\nUnderstood! \"appsettings.json\" will not be created automatically.");
          Console.Write("\n(1 of 3)");
          Console.ReadLine();
          Console.Write("\nPlease follow the README instructions to create your appsettings.json file.");
          Console.Write("\n(2 of 3)");
          Console.ReadLine();
          Console.Write("\nThis app will likely now fail with the error \"");
          Console.ForegroundColor = ConsoleColor.DarkRed;
          Console.Write("dotnet quit unexpectedly");
          Console.ResetColor();
          Console.Write("\" due to running without the required file.");
          Console.Write("\n(OK)");
          Console.ReadLine();
        }
        else
        {
          Console.WriteLine("Building the appsettings.json file...");
          Console.WriteLine("Please enter your MySQL password the appsettings.json file: ");
          string mySqlPassword = Console.ReadLine();
          try
          {
            // Create the "appsettings.json" file, or overwrite if the file exists.
            using (FileStream fs = File.Create(appsettingsFile))
            {
              byte[] info = new UTF8Encoding(true).GetBytes("{\n  \"SettingsValidated\": false,\n  \"ConnectionStrings\": {\n    \"DefaultConnection\": \"Server=localhost;Port=3306;database=patrick_lee;uid=root;pwd=" + mySqlPassword + ";\"\n  }\n}");
              // Add some information to the file.
              fs.Write(info, 0, info.Length);
            }
          }
          catch (Exception exception)
          {
            Console.WriteLine(exception.ToString());
          }
        }
      }
    }

//     public static void ValidateSettings()
//     {
//       // Read "appsettings.json" to a string
//       string appSettings = File.ReadAllText("appsettings.json");

//       // Deserialize file syntax into an object
//       var appSettingsObject = new AppSettings()
//     }

  }
}