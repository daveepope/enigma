using Mark3Enigma;

namespace Enigma.Test
{
    /// <summary>
    /// Expected cypher text obtained from existing online enigma implementations such as:
    /// https://cryptii.com/pipes/enigma-decoder
    /// </summary>
    public class EnigmaEncryptTest
    {
        [Fact]
        public void Encrypt_UsingBasicSettings_EncryptionOutputMatchesExpectedCypherText()
        {
            var key = new EnigmaKey(rotors: new[] { "I", "II", "III" },
            indicators: new int[] { 0, 0, 0 },
            ringSettings: new int[] { 0, 0, 0 },
            null,
            reflector: ReflectorType.B);

            var enigma = new EnigmaMark3(key);

            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZAAAAAAAAAAAAAAAAAAAAAAAAAABBBBBBBBBBBBBBBBBBBBBBBBBBABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string expectedOutput = "BJELRQZVJWARXSNBXORSTNCFMEYHCXTGYJFLINHNXSHIUNTHEORXOPLOVFEKAGADSPNPCMHRVZCYECDAZIHVYGPITMSRZKGGHLSRBLHL";

            char[] cipherText = enigma.Encrypt(input.ToCharArray());

            Assert.Equal(expectedOutput.ToCharArray(), cipherText);
        }

        [Fact]
        public void EncryptThenDecrypt_UsingSameEnigmaKey_EncryptsAndDecryptsSuccessfully()
        {
            var symetricalEnigmaKey = new EnigmaKey(rotors: new[] { "VII", "V", "IV" },
            indicators: new int[] { 10, 5, 12 },
            ringSettings: new int[] { 1, 2, 3 },
            plugboardConnections: null,
            reflector: ReflectorType.B);

            var sender = new EnigmaMark3(symetricalEnigmaKey);

            string input = "THISISATESTOFTHEEMERGENCYBROADCASTSYSTEM";
            string expectedOutput = "EVYMTHNDAIXIUYNKFLNUQWTEEOQINZUHEFOEDCNJ";

            char[] cipherText = sender.Encrypt(input.ToCharArray());

            Assert.Equal(expectedOutput.ToCharArray(), cipherText);

            var receiver = new EnigmaMark3(symetricalEnigmaKey);

            expectedOutput = input;

            char[] plainText = receiver.Encrypt(cipherText);

            Assert.Equal(expectedOutput.ToCharArray(), plainText);
        }

        [Fact]
        public void Encrypt_UsingVariedRotorsAndLongInput_EncryptionOutputMatchesExpectedCypherText()
        {

            var key = new EnigmaKey(rotors: new[] { "III", "VI", "VIII" },
            indicators: new int[] { 3, 5, 9 },
            ringSettings: new int[] { 11, 13, 19 },
            null,
            reflector: ReflectorType.B);

            var enigma = new EnigmaMark3(key);

            char[] longInput = new char[500];
            for (int i = 0; i < 500; i++) longInput[i] = 'A';
            string expectedOutput = "YJKJMFQKPCUOCKTEZQVXYZJWJFROVJMWJVXRCQYFCUVBRELVHRWGPYGCHVLBVJEVTTYVMWKJFOZHLJEXYXRDBEVEHVXKQSBPYZN" +
            "IQDCBGTDDWZQWLHIBQNTYPIEBMNINNGMUPPGLSZCBRJULOLNJSOEDLOBXXGEVTKCOTTLDZPHBUFKLWSFSRKOMXKZELBDJNRUDUCO" +
            "TNCGLIKVKMHHCYDEKFNOECFBWRIEFQQUFXKKGNTSTVHVITVHDFKIJIHOGMDSQUFMZCGGFZMJUKGDNDSNSJKWKENIRQKSUUHJYMIG" +
            "WWNMIESFRCVIBFSOUCLBYEEHMESHSGFDESQZJLTORNFBIFUWIFJTOPVMFQCFCFPYZOJFQRFQZTTTOECTDOOYTGVKEWPSZGHCTQRP" +
            "GZQOVTTOIEGGHEFDOVSUQLLGNOOWGLCLOWSISUGSVIHWCMSIUUSBWQIGWEWRKQFQQRZHMQJNKQTJFDIJYHDFCWTHXUOOCVRCVYOHL";

            char[] cipherText = enigma.Encrypt(longInput);

            Assert.Equal(expectedOutput.ToCharArray(), cipherText);
        }
    }
}