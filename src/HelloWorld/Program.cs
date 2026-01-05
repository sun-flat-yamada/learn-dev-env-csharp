using HelloWorld.Services;

namespace HelloWorld;

/// <summary>
/// Main entry point for the Hello World sample program.
/// </summary>
internal static class Program
{
  private const int PROPERTY_COLUMN_WIDTH = 20;
  private const int VALUE_COLUMN_WIDTH = 50;

  private static void Main(string[] args)
  {
    Console.WriteLine("=== Hello World ===\n");

    // Use the greeting service
    var greeting = GreetingService.GetGreeting("World");
    Console.WriteLine(greeting);

    // Display environment information
    displayEnvironmentInfo();

    Console.WriteLine("\n✓ HelloWorld program executed successfully!");
  }

  /// <summary>
  /// Displays current environment and runtime information.
  /// </summary>
  private static void displayEnvironmentInfo()
  {
    Console.WriteLine("\n--- Environment Information ---");
    printTableRow("Property", "Value");
    printTableSeparator();
    printTableRow(".NET Version:", System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
    printTableRow("OS:", System.Runtime.InteropServices.RuntimeInformation.OSDescription);
    printTableRow("Processor Count:", Environment.ProcessorCount.ToString());
  }

  /// <summary>
  /// Prints a formatted table row with two columns.
  /// </summary>
  private static void printTableRow(string property, string value)
  {
    Console.WriteLine("{0} {1}", property.PadRight(PROPERTY_COLUMN_WIDTH), value);
  }

  /// <summary>
  /// Prints a table separator line.
  /// </summary>
  private static void printTableSeparator()
  {
    Console.WriteLine("{0} {1}", new string('-', PROPERTY_COLUMN_WIDTH), new string('-', VALUE_COLUMN_WIDTH));
  }
}
