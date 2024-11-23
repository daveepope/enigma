# enigma
A dotnet implementation of an Enigma cypher, specifically the Enigma Mark 3, which was the most common Engima implementation in use during WW2. Based on Java implementation created by Dr Mike Pound, with some added features and small optimizations: https://github.com/mikepound/enigma

Enigma is a symmetrical encryption cypher. This means the same Enigma key (settings) can be used to encrypt and then decrypt a message. 

To run, clone the repo. There is a console application (a digital Enigma Machine) that you can use to to enter plaintext to produce cypher text. You can use another Engima machine to then decrypt that message. Please note you will need to install the dotnet sdk in order the run your Enigma machines.

Operator Instructions:

Configure one Enigma Machine let's call it Machine A. Configure Machine A to use a key of your choice (key x).

Configure another Enigma Machine (Machine B) to use the same key x. 

Type your plaintext (p) into Machine A to produce your cypher text (y) which will look something like FHUT HDBK BVKH NIUR.

Type the cypher text (y) into Machine B. Machine B will decrypt the cypher text (x) back to the original plain text (p).
