using System;
using System.IO;
using System.Text;

namespace Factory.Models
{
  public class AppSettings
  {
    public bool SettingsValidated { get; set; }

    public static void ConsoleGuiYesNo(string yesContextMessage, string noContextMessage)
    {
      Console.ResetColor();
      Console.Write("(");
      Console.BackgroundColor = ConsoleColor.DarkGreen;
      Console.Write("Y");
      Console.ResetColor();
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      Console.Write("es" + yesContextMessage);
      Console.ResetColor();
      Console.Write(")\n(");
      Console.BackgroundColor = ConsoleColor.DarkMagenta;
      Console.Write("N");
      Console.ResetColor();
      Console.ForegroundColor = ConsoleColor.DarkMagenta;
      Console.Write("o" + noContextMessage);
      Console.ResetColor();
      Console.Write(")\n ");
    }

    public static void CheckSettings()
    {
      string userChoice;
      string appsettingsFile = @"appsettings.json";
      if (!File.Exists(appsettingsFile))
      {
        Console.WriteLine("\n\n\n\n\nIn order to run, this app requires an \"appsettings.json\" file containing your MySQL username and password.");
        Console.WriteLine("\nWould you like the program to create the appsettings.json file for you?");
        string yesContext = ", let's build the file now";
        string noContext = ", I'll create the file myself";
        ConsoleGuiYesNo(yesContext, noContext);

        userChoice = (Console.ReadLine()).ToLower();
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
          Console.WriteLine("\nBuilding the appsettings.json file...");

          string appServer = "localhost";
          Console.BackgroundColor = ConsoleColor.DarkBlue;
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("\nSERVER: " + appServer);
          Console.ResetColor();
          Console.Write($"'Server' will be set to '{appServer}' for the web app to run. Would you like to change this setting? (NOT RECOMMENDED)\n");
          ConsoleGuiYesNo("", "");
          userChoice = (Console.ReadLine()).ToLower();
          if (userChoice[0] == 'y')
          {
            Console.WriteLine("Please specify the SERVER you would like to set in appsettings.json: ");
            appServer = Console.ReadLine();
          }

          string appPort = "3306";
          Console.BackgroundColor = ConsoleColor.DarkBlue;
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("\nPORT: " + appPort);
          Console.ResetColor();
          Console.Write($"'Port' will be set to '{appPort}' by default. Would you like to specify a different port?\n");
          ConsoleGuiYesNo("", "");
          userChoice = (Console.ReadLine()).ToLower();
          if (userChoice[0] == 'y')
          {
            Console.WriteLine("Please specify the PORT used by your MySQL server installation: ");
            appPort = Console.ReadLine();
          }

          string appDatabase = "patrick_lee";
          Console.BackgroundColor = ConsoleColor.DarkBlue;
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("\nDATABASE NAME: " + appDatabase);
          Console.ResetColor();
          Console.Write($"'Database' will be set to '{appDatabase}' by default. Would you like to specify a different name for the database?\n");
          ConsoleGuiYesNo("", "");
          userChoice = (Console.ReadLine()).ToLower();
          if (userChoice[0] == 'y')
          {
            Console.WriteLine("Please specify the DATABASE NAME you would like: ");
            appDatabase = Console.ReadLine();
          }

          string appUserId = "root";
          Console.BackgroundColor = ConsoleColor.DarkBlue;
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("\nUSER ID: " + appUserId);
          Console.ResetColor();
          Console.Write($"'User ID' will be set to '{appUserId}' by default. Would you like to specify a different MySQL user?\n");
          ConsoleGuiYesNo("", "");
          userChoice = (Console.ReadLine()).ToLower();
          if (userChoice[0] == 'y')
          {
            Console.WriteLine("Please specify the USER ID used by your MySQL server installation: ");
            appUserId = Console.ReadLine();
          }

          string appPassword = "epicodus";
          Console.BackgroundColor = ConsoleColor.DarkBlue;
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("\nPASSWORD: " + appPassword);
          Console.ResetColor();
          Console.Write($"Password will be set to '{appPassword}' by default. Would you like to specify a different password?\n");
          ConsoleGuiYesNo("", "");
          userChoice = (Console.ReadLine()).ToLower();
          if (userChoice[0] == 'y')
          {
            Console.WriteLine($"Please specify the PASSWORD used by your MySQL server's '{appUserId}' account: ");
            appPassword = Console.ReadLine();
          }

          try
          {
            // Create the "appsettings.json" file, or overwrite if the file exists.
            using (FileStream fs = File.Create(appsettingsFile))
            {
              byte[] info = new UTF8Encoding(true).GetBytes("{\n  \"SettingsValidated\": false,\n  \"ConnectionStrings\": {\n    \"DefaultConnection\": \"Server=" + appServer + ";Port=" + appPort + ";database=" + appDatabase + ";uid=" + appUserId + ";pwd=" + appPassword + ";\"\n  }\n}");
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