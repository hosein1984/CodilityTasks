using System;
using System.Linq;
using MoreLinq;

namespace Solution
{
    // https://app.codility.com/programmers/lessons/3-time_complexity/tape_equilibrium/


    public class Solution
    {
        public int solution(int[] inputs)
        {
            return solution1(inputs);
            //return solution2(inputs);
        }

        /// <summary>
        /// left - right = 2 * left - sum
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static int solution1(int[] inputs)
        {
            // calculates the moving sum
            var leftSums = inputs
                .Scan((acc, curr) => acc + curr).ToList();

            // the last element is the total sum
            var totalSum = leftSums.Last();

            // leftSum - rightSum = 2 * leftSum - totalSum
            return leftSums.SkipLast(1)
                .Select((x, i) =>
                    new {Diff = Math.Abs(2 * x - totalSum), Index = i})
                .Min(x => x.Diff);
        }

        public static int solution2(int[] inputs)
        {
            var tape = new Tape(inputs);

            return tape.CalculateMinDiff();
        }
    }
}
