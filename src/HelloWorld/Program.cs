using HelloWorld.Services;

namespace HelloWorld;

/// <summary>
/// Main entry point for the Hello World sample program.
/// </summary>
internal static class Program
{
  private const int PropertyColumnWidth = 20;
  private const int ValueColumnWidth = 50;

  private static void Main(string[] args)
  {
    Console.WriteLine("=== Hello World ===\n");

    // Use the greeting service
    var greeting = GreetingService.GetGreeting("World");
    Console.WriteLine(greeting);

    // Display environment information
    DisplayEnvironmentInfo();

    Console.WriteLine("\n✓ HelloWorld program executed successfully!");
  }

  /// <summary>
  /// Displays current environment and runtime information.
  /// </summary>
  private static void DisplayEnvironmentInfo()
  {
    Console.WriteLine("\n--- Environment Information ---");
    PrintTableRow("Property", "Value");
    PrintTableSeparator();
    PrintTableRow(".NET Version:", System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
    PrintTableRow("OS:", System.Runtime.InteropServices.RuntimeInformation.OSDescription);
    PrintTableRow("Processor Count:", Environment.ProcessorCount.ToString());
  }

  /// <summary>
  /// Prints a formatted table row with two columns.
  /// </summary>
  private static void PrintTableRow(string property, string value)
  {
    Console.WriteLine("{0} {1}", property.PadRight(PropertyColumnWidth), value);
  }

  /// <summary>
  /// Prints a table separator line.
  /// </summary>
  private static void PrintTableSeparator()
  {
    Console.WriteLine("{0} {1}", new string('-', PropertyColumnWidth), new string('-', ValueColumnWidth));
  }
}
