namespace HelloWorld.Services;

/// <summary>
/// Provides greeting functionality.
/// </summary>
public static class GreetingService
{
  /// <summary>
  /// Gets a greeting message for the specified name.
  /// </summary>
  /// <param name="name">The name to greet.</param>
  /// <returns>A personalized greeting message.</returns>
  /// <exception cref="ArgumentNullException">Thrown when name is null.</exception>
  /// <exception cref="ArgumentException">Thrown when name is empty or whitespace.</exception>
  public static string GetGreeting(string name)
  {
    if (name == null)
    {
      throw new ArgumentNullException(nameof(name), "Name cannot be null.");
    }

    if (string.IsNullOrWhiteSpace(name))
    {
      throw new ArgumentException("Name cannot be empty or whitespace.", nameof(name));
    }

    return $"Hello, {name}! Welcome to C# development.";
  }
}
