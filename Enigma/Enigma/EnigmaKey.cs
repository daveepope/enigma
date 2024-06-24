using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    /// <summary>
    /// An abstraction that describes settings used by an Enigma machine.
    /// The key or settings, include the chosen rotors (aka Walzenlage in German) and their start positions, ring settings (aka Ringstellung),
    /// choice of reflector and plugboard connections (aka Steckerverbindungen).
    /// Enigma settings were circulated during WW2 in a series of "code books" which instructed an Enigma operator on how
    /// to configure their Enigma machine for a given day.
    /// For more information on Enigma settings see: http://www.ellsbury.com/enigma3.htm
    /// </summary>
    public class EnigmaKey
    {
        public EnigmaKey(string[]? rotors, int[]? indicators, int[]? ringSettings,
            Dictionary<char, char>? plugboardConnections, ReflectorType reflector = ReflectorType.B)
        {
            Rotors = rotors ?? new[] { "I", "II", "III" };
            Indicators = indicators ?? new[] { 0, 0, 0 };
            RingSettings = ringSettings ?? new[] { 0, 0, 0 };
            Plugboard = plugboardConnections;
            Reflector = reflector;
        }

        public string[]? Rotors { get; }
        public int[]? Indicators { get; }
        public int[] RingSettings { get; }
        public Dictionary<char, char> Plugboard { get; }
        public ReflectorType Reflector { get; }
    }
}
