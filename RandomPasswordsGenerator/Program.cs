using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPasswordsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> generatedPasswords= new List<string>();
            Console.WriteLine("Here are 5 strongly secured passwords for you to choose from:");
            Console.WriteLine();
            for (int i = 1; i <= 5; i++)
            {
                string password = GeneratePassword();
                generatedPasswords.Add(password);
                Console.WriteLine($"Password {i}: {password}");
                Task.Delay(20).Wait();
            }
            Console.WriteLine();
            Console.Write("Select a password from the list above by index: ");
            int choice = int.Parse( Console.ReadLine() );

            string filePath = "passwords.txt";
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(generatedPasswords[choice-1]);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(generatedPasswords[choice-1]);
                }
            }

            Console.WriteLine("Password is saved to file");
            generatedPasswords.Clear();

            Console.ReadKey();
        }

        static string GeneratePassword()
        {
            const string capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string smallLetters = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_+-={}[]|\\:;\"'<>,.?/";

            StringBuilder passwordBuilder = new StringBuilder();

            Random random = new Random();

            // Generate a random uppercase letter
            passwordBuilder.Append(capitalLetters[random.Next(capitalLetters.Length)]);

            // Generate a random lowercase letter
            passwordBuilder.Append(smallLetters[random.Next(smallLetters.Length)]);

            // Generate a random digit
            passwordBuilder.Append(digits[random.Next(digits.Length)]);

            // Generate a random special character
            passwordBuilder.Append(specialChars[random.Next(specialChars.Length)]);

            // Generate the remaining characters of the password
            while (passwordBuilder.Length < 10)
            {
                string chars = capitalLetters + smallLetters + digits + specialChars;
                passwordBuilder.Append(chars[random.Next(chars.Length)]);
            }

            // Shuffle the characters of the password randomly
            for (int i = 0; i < passwordBuilder.Length; i++)
            {
                int j = random.Next(passwordBuilder.Length);
                char temp = passwordBuilder[i];
                passwordBuilder[i] = passwordBuilder[j];
                passwordBuilder[j] = temp;
            }
            return passwordBuilder.ToString();
        }
    }
}
