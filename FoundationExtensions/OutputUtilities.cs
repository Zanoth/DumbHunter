namespace FoundationExtensions;

public static class OutputUtilities
{
  public static string GetUserInput() => Console.ReadLine();
  public static async Task WriteLine(string output) => Console.WriteLine(output);
  public static async Task Write(string output) => Console.Write(output);

  public static void CreateOutputSpace()
  {
    Console.WriteLine("");
    Console.WriteLine("-------------------");
    Console.WriteLine("");
  }

  public static bool ContinueLoop(bool continueLoop, string LopQuery)
  {
    WriteLine(LopQuery);

    var userInput = "";
    while (userInput != "y")
    {
      userInput = GetUserInput().ToLower();
      if (userInput == "n")
      {
        continueLoop = false;
        break;
      }

      if (userInput != "y")
        WriteLine("Invalid input. Please try again.");
    }
    return continueLoop;
  }

  public static string PadRight(string input, int totalWidth)
  {
    if (input.Length > totalWidth)
    {
      return input.Substring(0, totalWidth - 3) + "...";
    }
    return input.PadRight(totalWidth);
  }
}