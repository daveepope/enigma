using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    public class ReflectorFactory
    {
        public static Reflector Create(ReflectorType reflectorType)
        {
            return reflectorType switch
            {
                // The character wiring encodings for reflectors UKW B & C can be found here: 
                // https://www.cryptomuseum.com/crypto/enigma/wiring.htm#:~:text=UKW%2DB%20was%20the%20standard%20reflector%20throughout%20the%20war%20and,communication%20between%20the%20armed%20forces.
                ReflectorType.B => new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT"),
                ReflectorType.C => new Reflector("FVPJIAOYEDRZXWGCTKUQSBNMHL"),
                _ => new Reflector("ZYXWVUTSRQPONMLKJIHGFEDCBA") // this default reflector doesn't actually exist, but added a simple inverse Reflector for debugging purposes
            };
        }
    }
}
