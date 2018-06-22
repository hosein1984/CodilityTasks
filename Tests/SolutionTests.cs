using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Tests
{
    public class SolutionTests
    {
        [Theory]
        [InlineData(new[]{3,1,2,4,3},1)]
        [InlineData(new[]{-1000,1000},2000)]
        public void reverse_of_reverse_is_the_original(int[] inputs, int expected)
        {
            var solution = new Solution.Solution();
            var result = solution.solution(inputs);
            Assert.Equal(expected,result);
        }
    }
}
