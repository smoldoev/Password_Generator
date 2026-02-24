🔐 PasswordGenerator

Password Generator- is a secure console-based password generator written in C# (.NET) using the powerful Spectre.Console library for a beautiful CLI interface.

It generates strong, cryptographically secure passwords with customizable options and evaluates their strength.

🚀 Features

✅ Custom password length (6–32 characters)

✅ Choose character types:

Numbers (0–9)

Lowercase letters (a–z)

Uppercase letters (A–Z)

Special symbols (!@#$%^&*)

✅ Cryptographically secure random generator (RandomNumberGenerator)

✅ Ensures at least one character from each selected category

✅ Password strength evaluation (Weak / Medium / Strong)

✅ Clean and modern console UI powered by Spectre.Console

✅ Shuffled output to avoid predictable patterns

🖥️ Preview

When you run the app, you’ll see:

PassGen Pro
Welcome to Pro Password Generator!

Length of Password? (from 6 to 32)
Choose what should be in the password:

Your pro-password:
╔══════════════════════╗
║   K9#dL2@xTq!p       ║
╚══════════════════════╝

Difficulty: Strong
Length: 12 symbols
🛠️ Technologies Used

C#

.NET 8

Spectre.Console

TextCopy

🔒 Security

This project uses:

RandomNumberGenerator.Create()
→ Cryptographically secure random generation

Guaranteed inclusion of selected character types

Password shuffling to avoid predictable positioning

⚠ Note:
The internal Shuffle() method currently uses Random.
For even stronger security, it can be upgraded to use RandomNumberGenerator.

📦 Installation
1️⃣ Clone the repository
git clone https://github.com/YOUR_USERNAME/PasswordGeneratorPro.git
cd PasswordGeneratorPro
2️⃣ Install dependencies

If not installed automatically:

dotnet add package Spectre.Console
3️⃣ Run the project
dotnet run
📊 Password Strength Logic

The password strength score is calculated based on:

Length ≥ 8

Length ≥ 12

Contains digits

Contains both lowercase and uppercase

Contains special characters

Score result:

Score	Strength
0–2	Weak
3–4	Medium
5	Strong
📌 Future Improvements

🔐 Replace Random in Shuffle with cryptographic RNG

📋 Add copy-to-clipboard feature

📁 Add export to file option

🎨 Add themes (Dark / Neon / Minimal)

🌍 Multi-language support

👨‍💻 Author

Sabyr Moldoev
Student of Computer Engineering

📄 License

This project is open-source and available under the MIT License.

