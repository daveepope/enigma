using Enigma;

namespace Mark3Enigma
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var key = new EnigmaKey(
                rotors: new[] { "II", "I", "VII" },
                indicators: new int[] { 13, 2, 20 },
                ringSettings: new int[] { 3, 2, 17 },
                plugboardConnections: new Dictionary<char, char>()
                {
                { 'T', 'H' },
                { 'U', 'N' },
                { 'R', 'G' },
                { 'P', 'Q' },
                { 'J', 'K' },
                { 'E', 'X' },
                { 'D', 'S' },
                { 'B', 'I' },
                { 'Y', 'W' },
                { 'O', 'L' }
                },
                reflector: ReflectorType.B);

            var enigma = new EnigmaMark3(key);

            string connectionsToPrint = string.Empty;

            key.Plugboard.ToList().ForEach(connection =>
            {
                connectionsToPrint += connection.Key.ToString() + connection.Value.ToString() + " ";
            });

            Console.WriteLine($"Enigma configuration: " +
                              $"\nRotors: {key.Rotors[0]}, {key.Rotors[1]}, {key.Rotors[2]}" +
                              $"\nReflector: {key.Reflector}" +
                              $"\nRotor Positions: {key.Indicators[0]}, {key.Indicators[1]}, {key.Indicators[2]}" +
                              $"\nRing Settings: {key.RingSettings[0]}, {key.RingSettings[1]}, {key.RingSettings[2]}" +
                              $"\nPlugboard Connections: {connectionsToPrint}");

            Console.WriteLine("\n\nType your message... Hit enter to exit...");

            do
            {
                var characterGroupingCount = 0;
                while (!Console.KeyAvailable)
                {
                    try
                    {
                        var input = Console.ReadKey(true);
                        var encryptedCharacter = enigma.Encrypt(input.KeyChar);
                        if (characterGroupingCount == 5)
                        {
                            Console.Write(" " + encryptedCharacter);
                            characterGroupingCount = 0;
                        }
                        else
                        {
                            Console.Write(encryptedCharacter);
                        }
                        characterGroupingCount++;
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("\n" + e.Message);
                        return;
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);

            Console.WriteLine("Exiting...");
        }
    }
}
