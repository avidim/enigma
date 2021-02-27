using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    class Rotors
    {
        private string keys;

        public Rotors(int num)
        {
            switch (num)
            {
                case 1: keys = "EKMFLGDQVZNTOWYHXUSPAIBRCJ"; break;
                case 2: keys = "AJDKSIRUXBLHWTMCQGZNPYFVOE"; break;
                case 3: keys = "BDFHJLCPRTXVZNYEIWGAKMUSQO"; break;
            }
        }

        public char getCharacter(int id)
        {
            return keys[id];
        }

        public int getCharacterID(char key)
        {
            return keys.IndexOf(key);
        }
    }
}