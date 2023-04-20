namespace SharpDice.Core
{
    public class Dice
    {
        // ---Class Fields---
        private int _sides;
        private int _previousRoll;
        private int _currentRoll;
        private bool _weightedDie;
        private int _weightFloor;
        private int _weightCeiling;

        // ---Public Properties---
        public int PreviousRoll { get { return _previousRoll; } }
        public int CurrentRoll { get { return _currentRoll; } }
        public bool IsWeighted { get { return _weightedDie; } }

        // ---Constructors---
        /// <summary>
        /// Initilize a new dice object
        /// </summary>
        /// <param name="sides"></param>
        public Dice(int sides)
        {
            _sides = sides;
            _previousRoll = 0;
            _currentRoll = 0;
            _weightedDie = false;
            _weightFloor = 0;
            _weightCeiling = 0;
        }

        /// <summary>
        /// Initlize a new dice object that happens to be weighted. You can give a dice 'weight' by setting the weighted floor and ceiling. This will make the dice more likely to roll a number in the weighted range.
        /// and create a new min and max value for the dice.
        /// </summary>
        /// <param name="sides"></param>
        /// <param name="weighted"></param>
        /// <param name="weightedFloor"></param>
        /// <param name="weightedCeiling"></param>
        public Dice(int sides, bool weighted, int weightedFloor, int weightedCeiling)
        {
            _sides = sides;
            _previousRoll = 0;
            _weightedDie = weighted;
            _weightFloor = weightedFloor;
            _weightCeiling = weightedCeiling;
        }


        // ---Methods---
        /// <summary>
        /// Roll a dice and get a random number between 1 and the number of sides on the dice.
        /// </summary>
        /// <returns></returns>
        public int RollDice()
        {
            _previousRoll = _currentRoll;
            var roll = new Random();
            _currentRoll = roll.Next(1, _sides);
            return _currentRoll;
        }

        /// <summary>
        /// Roll a weighted dice and get a random number between the weighted floor and the weighted ceiling.
        /// </summary>
        /// <returns></returns>
        public int RollWeightedDice()
        {
            _previousRoll = _currentRoll;
            var roll = new Random();
            _currentRoll = roll.Next(_weightFloor, _weightCeiling);
            return _currentRoll;
        }

    }
}