#define ENSURE
using System;
using System.Linq;
using Genesis.Ensure;

namespace Solution
{
    public class Tape
    {
        public Tape(int[] input)
        {
            Ensure.ArgumentNotNull(input, nameof(input));
            Numbers = input;

            SplitIndex = 1;
            LeftSum = Numbers[0];
            RightSum = Numbers.Sum() - LeftSum;
        }

        public int LeftSum { get; private set; }
        public int RightSum { get; private set; }
        public int Diff => Math.Abs(LeftSum - RightSum);
        public int SplitIndex { get; private set; }
        public bool CanMoveLeft => SplitIndex > 1;
        public bool CanMoveRight => SplitIndex < Numbers.Length -1;
        public int[] Numbers { get; }

        public void MoveSplitToRight()
        {
            if (!CanMoveRight) throw new InvalidOperationException();
            LeftSum += Numbers[SplitIndex];
            RightSum -= Numbers[SplitIndex];
            SplitIndex++;
        }

        public void MoveSplitToLeft()
        {
            if (!CanMoveLeft) throw new InvalidOperationException();
            SplitIndex--;
            LeftSum -= Numbers[SplitIndex];
            RightSum += Numbers[SplitIndex];
        }

        public int CalculateMinDiff()
        {
            while (CanMoveLeft)
            {
                MoveSplitToLeft();
            }

            var minDiff = Diff;

            while (CanMoveRight)
            {
                MoveSplitToRight();

                minDiff = Diff < minDiff ? Diff : minDiff;
            }

            return minDiff;
        }

    }
}
