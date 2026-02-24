using Spectre.Console;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGeneratorPro;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.Write(new FigletText("PassGen Pro").Centered().Color(Color.Cyan1));
        AnsiConsole.MarkupLine("[grey]Welcome to Pro Password Generator![/]\n");

        // Password generation settings
        var length = AnsiConsole.Prompt(
            new TextPrompt<int>("[green]Length of Password?[/]" +
            " (from 6 to 32)")
                .DefaultValue(12)
                .ValidationErrorMessage("[red]Please choose the value![/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        < 6 => ValidationResult.Error("[red]Too short, minimum 6[/]"),
                        > 32 => ValidationResult.Error("[red]Too long, maximum 32[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));

        // Selecting character categories
        AnsiConsole.MarkupLine("\n[grey]Choose what should be in the password:[/]");
        var useDigits = AnsiConsole.Confirm("[yellow]Numbers?[/] (0-9)", true);
        var useLowercase = AnsiConsole.Confirm("[yellow]Lowercase?[/] (a-z)", true);
        var useUppercase = AnsiConsole.Confirm("[yellow]Uppercase?[/] (A-Z)", true);
        var useSpecial = AnsiConsole.Confirm("[yellow]Special symbols?[/] (!@#$%^&*)", true);

        // Ensure at least one character type is selected
        if (!useDigits && !useLowercase && !useUppercase && !useSpecial)
        {
            AnsiConsole.MarkupLine("[red on Black]Error: Must select at least one character type![/]");
            return;
        }

        // Generate the password
        var password = GeneratePassword(length, useDigits, useLowercase, useUppercase, useSpecial);

        // Evaluate password strength
        var strength = CheckStrength(password);

        // Output the result
        AnsiConsole.MarkupLine("\n[underline green]Your pro-password:[/]");

        // Display the password inside a box
        var panel = new Panel($"[bold yellow]{password}[/]")
        {
            Border = BoxBorder.Double,
            Padding = new Padding(2, 1)
        };
        AnsiConsole.Render(panel);

        AnsiConsole.MarkupLine($"Difficulty: {strength}");
        AnsiConsole.MarkupLine($"Length: [cyan]{password.Length}[/] symbols");

        AnsiConsole.MarkupLine("\n[grey]Press any key to exit...[/]");
        Console.ReadKey();
    }

    static string GeneratePassword(int length, bool digits, bool lowercase, bool uppercase, bool special)
    {
        // Build the character pool
        var charPool = new StringBuilder();

        if (digits) charPool.Append("0123456789");
        if (lowercase) charPool.Append("abcdefghijklmnopqrstuvwxyz");
        if (uppercase) charPool.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        if (special) charPool.Append("!@#$%^&*()_-+=<>?");

        var pool = charPool.ToString();
        var password = new char[length];

        // Use a cryptographically secure random number generator
        using var rng = RandomNumberGenerator.Create();

        // Ensure at least one character from each selected category
        // (so the password definitely meets the requirements)
        int index = 0;

        if (digits) password[index++] = pool[GetRandomInt(rng, 0, 10)]; // first 10 characters are digits
        if (lowercase) password[index++] = pool[GetRandomInt(rng, 10, 10 + 26)]; // next 26 are lowercase
        if (uppercase) password[index++] = pool[GetRandomInt(rng, 10 + 26, 10 + 26 + 26)]; // next 26 are uppercase
        if (special) password[index++] = pool[GetRandomInt(rng, 10 + 26 + 26, pool.Length)]; // remaining are special symbols

        // Fill the remaining characters randomly from the entire pool
        for (int i = index; i < length; i++)
        {
            password[i] = pool[GetRandomInt(rng, 0, pool.Length)];
        }

        // Shuffle the password so required characters are not always at the beginning
        return Shuffle(new string(password));
    }

    static int GetRandomInt(RandomNumberGenerator rng, int min, int max)
    {
        // Secure cryptographic random
        byte[] randomNumber = new byte[4];
        rng.GetBytes(randomNumber);
        int value = BitConverter.ToInt32(randomNumber, 0) & int.MaxValue; // make it non-negative
        return min + (value % (max - min));
    }

    static string Shuffle(string input)
    {
        // Simple string shuffling
        var array = input.ToCharArray();
        var rng = new Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (array[n], array[k]) = (array[k], array[n]);
        }
        return new string(array);
    }

    static string CheckStrength(string password)
    {
        int score = 0;

        if (password.Length >= 8) score++;
        if (password.Length >= 12) score++;
        if (password.Any(char.IsDigit)) score++;
        if (password.Any(char.IsLower) && password.Any(char.IsUpper)) score++;
        if (password.Any(ch => !char.IsLetterOrDigit(ch))) score++;

        return score switch
        {
            <= 2 => "[red]Weak[/]",
            3 or 4 => "[yellow]Medium[/]",
            >= 5 => "[green]Strong[/]"
        };
    }
}