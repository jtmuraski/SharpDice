using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDice.Farkle
{
    public class FarkleGame
    {
        // ---Fields---
        private int _currentTurn;
        private List<Player> _players;
        private int _scoreToWin;
        private int _currentPlayerTurn; // This is linked to the TurnOrderNbr in the Player class. It is used to determine which player's turn it is.
        private bool _isGameWon;

        // ---Properties---
        public List<Player> Players { get { return _players; } }
        public int CurrentTurn { get { return _currentTurn; } }
        public int ScoreToWin { get { return _scoreToWin; } }
        public int CurrentPlayerTurn { get { return _currentPlayerTurn; } } 
        public bool IsGameWon { get { return _isGameWon; } }


        public FarkleGame()
        {
            _players = new List<Player>();
            _currentTurn = 1;
            _currentPlayerTurn = 1;
        }

        /// <summary>
        /// Add a player to the player list
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="isHuman"></param>
        /// <param name="playerNbr"></param>
        /// <param name="turnOrderNbr"></param>
        public void AddPlayer(string playerName, bool isHuman, int playerNbr, int turnOrderNbr)
        {
            Player player = new Player(playerName, isHuman, playerNbr, turnOrderNbr);
            _players.Add(player);
        }

        /// <summary>
        /// Remove a player from the list. Returns true if the action was successful and false if unsuccessful
        /// </summary>
        /// <param name="playerNbr"></param>
        /// <returns></returns>
        public bool RemovePlayer(int playerNbr) 
        {
            var playerToRemove = _players.SingleOrDefault(p => p.PlayerNumber == playerNbr);
            if(playerToRemove != null)
            {
                _players.Remove(playerToRemove);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Set the score required to win the game
        /// </summary>
        /// <param name="score"></param>
        public void SetScoreToWin(int score)
        {
            _scoreToWin = score;
        }

        /// <summary>
        /// Advance to the next turn
        /// </summary>
        public void NextTurn() => _currentTurn++;

        /// <summary>
        /// Reset the Farkele Game values to their default settings
        /// </summary>
        public void Reset()
        {
            _players.Clear();
            _scoreToWin = 0;
            _currentPlayerTurn = 1;
            _currentTurn = 1;
        }
    }
}
