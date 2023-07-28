using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubesGameGUI
{
    public class MiniMax
    {
        private const int MaxScore = int.MaxValue; // Maximum score value

        public int FindBestMove(int numberOfCubesOnTable, int maxDepth, int maxCubesToTake, bool isComputerTurn)
        {
            int bestMove = 0;
            int bestScore = int.MinValue;
            int actualMaxCubesToTake = Math.Min(maxCubesToTake, numberOfCubesOnTable >= maxCubesToTake ? maxCubesToTake : 2);
            List<int> possibleMoves = GeneratePossibleMoves(numberOfCubesOnTable, actualMaxCubesToTake);
            foreach (int move in possibleMoves)
            {
                int score = FindMiniMax(numberOfCubesOnTable - move, maxDepth - 1, actualMaxCubesToTake, !isComputerTurn);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }
            return bestMove;
        }

        private int FindMiniMax(int numberOfCubesOnTable, int depth, int maxCubesToTake, bool isMaximizingPlayer)
        {
            if (CheckIfTableIsEmpty(numberOfCubesOnTable) || depth == 0)
            {
                return EvaluateGameState(numberOfCubesOnTable, isMaximizingPlayer);
            }

            if (isMaximizingPlayer)
            {
                int bestScore = int.MinValue;
                int actualMaxCubesToTake = Math.Min(maxCubesToTake, numberOfCubesOnTable >= maxCubesToTake ? maxCubesToTake : 2);
                List<int> possibleMoves = GeneratePossibleMoves(numberOfCubesOnTable, actualMaxCubesToTake);

                foreach (int move in possibleMoves)
                {
                    int score = FindMiniMax(numberOfCubesOnTable - move, depth - 1, actualMaxCubesToTake, false);
                    bestScore = Math.Max(bestScore, score);
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                int actualMaxCubesToTake = Math.Min(maxCubesToTake, numberOfCubesOnTable >= maxCubesToTake ? maxCubesToTake : 2);
                List<int> possibleMoves = GeneratePossibleMoves(numberOfCubesOnTable, actualMaxCubesToTake);

                foreach (int move in possibleMoves)
                {
                    int score = FindMiniMax(numberOfCubesOnTable - move, depth - 1, actualMaxCubesToTake, true);
                    bestScore = Math.Min(bestScore, score);
                }

                return bestScore;
            }
        }

        private List<int> GeneratePossibleMoves(int numberOfCubes, int maxCubesToTake)
        {
            // Generate all possible moves based on the current number of cubes on the table and the maximum cubes to take
            List<int> possibleMoves = new List<int>();

            for (int move = 1; move <= maxCubesToTake && move <= numberOfCubes; move++)
            {
                possibleMoves.Add(move);
            }

            return possibleMoves;
        }

        private bool CheckIfTableIsEmpty(int numberOfCubesOnTable)
        {
            return numberOfCubesOnTable == 0;
        }

        private int EvaluateGameState(int numberOfCubesOnTable, bool isMaximizingPlayer)
        {
            if (CheckIfTableIsEmpty(numberOfCubesOnTable))
            {
                return isMaximizingPlayer ? -MaxScore : MaxScore;
            }

            // You can customize the evaluation based on your specific game's rules and strategy
            // For example, you can consider factors like the number of cubes remaining, player scores, etc.

            return 0; // Default evaluation score
        }
    }
}
