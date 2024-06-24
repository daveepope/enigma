using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    /// <summary>
    /// The plugboard (aka Steckerbrett in German) implementation.
    /// The plugboard swaps letters in pairings, and is essentially a monoalphabetic substitution cypher.
    /// The pairings must be provided at time of instantiation otherwise
    /// a character will just map to itself in the absence of a plugboard pairing to another character.
    /// For more info: https://www.cryptomuseum.com/crypto/enigma/i/#steckerbrett
    /// </summary>
    public class Plugboard
    {

        private int[] _wiring;

        public Plugboard(Dictionary<char, char> plugboardConnections)
        {
            _wiring = DecodePlugboard(plugboardConnections);
        }

        public int Forward(int characterIn)
        {
            return _wiring[characterIn];
        }

        private int[] DecodePlugboard(Dictionary<char, char>? plugboard)
        {
            if (plugboard == null || !plugboard.Any())
            {
                return PlugboardWithNoConnections();
            }

            int[] mapping = PlugboardWithNoConnections();

            plugboard.ToList().ForEach(connection =>
            {
                int c1 = connection.Key - 65;
                int c2 = connection.Value - 65;

                mapping[c1] = c2;
                mapping[c2] = c1;
            });

            return mapping;
        }

        private int[] PlugboardWithNoConnections()
        {
            var mapping = new int[26];

            for (var i = 0; i < 26; i++)
            {
                mapping[i] = i;
            }

            return mapping;
        }
    }
}
