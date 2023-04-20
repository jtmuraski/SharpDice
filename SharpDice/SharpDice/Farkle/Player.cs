using SharpDice.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDice.Farkle
{
    public class Player
    {
        // ---Fields---
        private string _name;
        private int _score;
        private int _playerNumber;
        private int _turnOrderNbr;
        private bool _isHumanPlayer;

        // ---Properties---
        public string Name { get { return _name; } }
        public int Score { get { return _score; } set { _score = value; } }
        public int PlayerNumber { get { return _playerNumber; } }
        public int TurnOrderNbr { get { return _turnOrderNbr; } }
        public bool IsHumanPlayer { get { return _isHumanPlayer;   } }

        public Player(string playerName, bool isHuman, int playerNbr, int turnOrderNbr) {
            _name = playerName;
            _score = 0;
            _playerNumber = playerNbr;
            _turnOrderNbr = turnOrderNbr;
            _isHumanPlayer = isHuman;
        }
    }
}
