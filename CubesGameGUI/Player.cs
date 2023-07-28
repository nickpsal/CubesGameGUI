using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubesGameGUI
{
    public class Player
    {
        public string PlayerName { get; set; }
        public int PlayerScore { get; set; }

        public Player(string playerName)
        {
            PlayerName = playerName;
            PlayerScore = 0;
        }
    }
}