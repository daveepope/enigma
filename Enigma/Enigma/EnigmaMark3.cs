using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma;

namespace Mark3Enigma
{
    /// <summary>
    /// An implementation of an Enigma Mark 3 symmetrical encryption device with a 3 rotor configuration.
    /// The Enigma Mark 3 was the most common Enigma model in circulation during WW2.
    /// For more info see: https://www.cryptomuseum.com/crypto/enigma/working.htm
    /// and https://www.cryptomuseum.com/crypto/enigma/m3/
    /// </summary>
    public class EnigmaMark3
    {
        private readonly Rotor _leftRotor;
        private readonly Rotor _middleRotor;
        private readonly Rotor _rightRotor;

        private readonly Reflector _reflector;

        private readonly Plugboard _plugboard;

        private EnigmaMark3(string[] rotors, ReflectorType reflector, int[] rotorPositions, int[] ringSettings,
            Dictionary<char, char> plugboardConnections)
        {
            _leftRotor = RotorFactory.Create(rotors[0], rotorPositions[0], ringSettings[0]);
            _middleRotor = RotorFactory.Create(rotors[1], rotorPositions[1], ringSettings[1]);
            _rightRotor = RotorFactory.Create(rotors[2], rotorPositions[2], ringSettings[2]);
            _reflector = ReflectorFactory.Create(reflector);
            _plugboard = new Plugboard(plugboardConnections);
        }

        public EnigmaMark3(EnigmaKey key) : this(key.Rotors, key.Reflector, key.Indicators, key.RingSettings, key.Plugboard) { }

        public char Encrypt(char characterIn)
        {
            if (!char.IsLetter(characterIn))
            {
                throw new InvalidOperationException("Must specify an alphabetic character from A-Z");
            }

            // this is intentional
            characterIn = char.ToUpper(characterIn);

            return (char)(Encrypt(characterIn - 65) + 65); // this is how we get the position of the character in the alphabet
        }

        /// <summary>
        /// Encrypt a plaintext message, or decrypt enigma text if using the same enigma key
        /// </summary>
        /// <param name="input"> the input to encrypt </param>
        /// <returns> the encrypted character </returns>
        public char[] Encrypt(char[] input)
        {
            char[] output = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = Encrypt(input[i]);
            }

            return output;
        }

        private void RotateRotors()
        {
            if (_middleRotor.IsAtNotch()) // if middle rotor notch - double-stepping
            {
                _middleRotor.Turnover();
                _leftRotor.Turnover();
            }
            else if (_rightRotor.IsAtNotch())
            {
                _middleRotor.Turnover();
            }

            _rightRotor.Turnover(); // increment right-most rotor
        }

        /// <summary>
        /// This is the main flow through the enigma machine.
        /// The character will change 7 times as it moves through the machine.
        /// </summary>
        /// <param name="c"> the character to encrypt </param>
        /// <returns> the encrypted character </returns>
        private int Encrypt(int c)
        {
            // start by rotating the rotors
            RotateRotors();

            /*
             * Each function call below represents the flow of electrical current from
             * one component to the next in a "real-life" Enigma machine,
             * i.e. simulating the E2E flow after a key press.
            */

            // next pass character IN through the plugboard
            c = _plugboard.Forward(c);

            // next move forward through rotors, right to left
            var c1 = _rightRotor.Forward(c);
            var c2 = _middleRotor.Forward(c1);
            var c3 = _leftRotor.Forward(c2);

            // now let's move through the reflector
            var c4 = _reflector.Forward(c3);

            // next move backwards through rotors, left to to right
            var c5 = _leftRotor.Backward(c4);
            var c6 = _middleRotor.Backward(c5);
            var c7 = _rightRotor.Backward(c6);

            // finally let's move back OUT through the plugboard
            var plugboardOut = _plugboard.Forward(c7);

            return plugboardOut; // character is now fully encrypted
        }
    }
}
