using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    /// <summary>
    /// The reflector is responsible for sending electrical impulses that have reached it from the machine's rotors,
    /// back in reverse order through the rotors i.e. "reflecting" the current back through the rotors.
    /// For more info on reflectors see:
    /// https://ciphermachinesandcryptology.com/en/enigmatech.htm#reflector
    /// https://en.wikipedia.org/wiki/Reflector_(cipher_machine)
    /// </summary>
    public class Reflector
    {
        private readonly int[]? _forwardWiring;

        public Reflector(string encoding)
        {
            _forwardWiring = DecodeWiring(encoding);
        }

        private int[]? DecodeWiring(string encoding)
        {
            char[] charWiring = encoding.ToCharArray();
            int[] wiring = new int[charWiring.Length];
            for (int i = 0; i < charWiring.Length; i++)
            {
                wiring[i] = charWiring[i] - 65;
            }

            return wiring;
        }

        public int Forward(int characterIn)
        {
            return _forwardWiring[characterIn];
        }
    }
}
