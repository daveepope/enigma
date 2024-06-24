using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    /// <summary>
    /// The rotor or "scrambler" implementation. Each rotor had a unique wire encoding.
    /// Each rotator will rotate at different speeds.
    /// A rotor contains a notch (to trigger a turnover event) and a ring setting. The ring setting
    /// will change the position of the alphabet ring and its notch (turnover-point), relative to the internal wiring core.
    /// For more details see: https://en.wikipedia.org/wiki/Enigma_rotor_details
    /// </summary>
    public class Rotor
    {
        protected int _notchPosition;
        protected int _rotorPosition;
        protected int _ringSetting;

        private string _name;
        private int[] _forwardWiring;
        private int[] _backwardWiring;

        public Rotor(string name, string encoding, int rotorPosition, int notchPosition, int ringSetting)
        {
            _name = name;
            _forwardWiring = DecodeWiring(encoding);
            _backwardWiring = InverseWiring(_forwardWiring);
            _rotorPosition = rotorPosition;
            _notchPosition = notchPosition;
            _ringSetting = ringSetting;
        }

        private int[]? InverseWiring(int[] wiring)
        {
            int[] inverse = new int[wiring.Length];
            for (int i = 0; i < wiring.Length; i++)
            {
                int forward = wiring[i];
                inverse[forward] = i;
            }

            return inverse;
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

        public virtual bool IsAtNotch()
        {
            return _notchPosition == _rotorPosition;
        }

        public void Turnover()
        {
            _rotorPosition = (_rotorPosition + 1) % 26;
        }

        public int Forward(int characterIn)
        {
            return Encipher(characterIn, _rotorPosition, _ringSetting, _forwardWiring);
        }

        private int Encipher(int characterIn, int rotorPosition, int ringSetting, int[] mapping)
        {
            int shift = rotorPosition - ringSetting;
            return (mapping[(characterIn + shift + 26) % 26] - shift + 26) % 26;
        }

        public int Backward(int characterIn)
        {
            return Encipher(characterIn, _rotorPosition, _ringSetting, _backwardWiring);
        }
    }

    public class NavalRotor : Rotor
    {
        public NavalRotor(string vi, string encoding, int rotorPosition, int notchPosition, int ringSetting)
            : base(vi, encoding, rotorPosition, notchPosition, ringSetting)
        {

        }

        public override bool IsAtNotch()
        {
            return _rotorPosition == 12 || _rotorPosition == 25;
        }
    }
}
