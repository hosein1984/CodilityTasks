using System;
using Solution;
using Xunit;

namespace Tests
{
    public class TapeTests
    {
        [Fact]
        public void ctor_throws_if_input_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new Tape(null));
        }

        [Theory]
        [InlineData(new[] {3, 1, 2, 4, 3}, 1, 3, 10, 7, false, true)]
        [InlineData(new[] {-10, 10}, 1, -10, 10, 20, false, false)]
        public void ctor_correctly_initializes_the_properties(
            int[] input, int splitIndex, int leftSum, int rightSum, int diff, bool canMoveLeft, bool canMoveRight)
        {
            var sut = new Tape(input);

            Assert.Equal(splitIndex, sut.SplitIndex);
            Assert.Equal(leftSum, sut.LeftSum);
            Assert.Equal(rightSum, sut.RightSum);
            Assert.Equal(diff, sut.Diff);
            Assert.Equal(canMoveRight, sut.CanMoveRight);
            Assert.Equal(canMoveLeft, sut.CanMoveLeft);
        }

        [Fact]
        public void move_split_to_right_correctly_alters_the_sums()
        {
            var input = new[] {3, 1, 2, 4, 3};
            var sut = new Tape(input);

            sut.MoveSplitToRight();

            Assert.Equal(2, sut.SplitIndex);
            Assert.Equal(4, sut.LeftSum);
            Assert.Equal(9, sut.RightSum);
        }

        [Fact]
        public void move_split_to_left_correctly_alters_the_sums()
        {
            var input = new[] {3, 1, 2, 4, 3};
            var sut = new Tape(input);

            sut.MoveSplitToRight();
            sut.MoveSplitToLeft();

            Assert.Equal(1, sut.SplitIndex);
            Assert.Equal(3, sut.LeftSum);
            Assert.Equal(10, sut.RightSum);
        }

        [Fact]
        public void move_split_to_left_when_there_no_moves_left_will_throw()
        {
            var input = new[] {3, 1, 2, 4, 3};
            var sut = new Tape(input);

            Assert.Throws<InvalidOperationException>(() => sut.MoveSplitToLeft());
        }

        [Fact]
        public void move_split_to_right_when_there_no_moves_left_will_throw()
        {
            var input = new[] {3, 1, 2, 4, 3};
            var sut = new Tape(input);

            while (true)
            {
                sut.MoveSplitToRight();
                if (!sut.CanMoveRight)
                {
                    break;
                }
            }

            Assert.Throws<InvalidOperationException>(() => sut.MoveSplitToRight());
        }

        [Theory]
        [InlineData(new[] {3, 1, 2, 4, 3}, 1)]
        [InlineData(new[] {-10, 10}, 20)]
        public void calculate_min_diff_behaves_correctly(int[] input, int expected)
        {
            var sut = new Tape(input);
            var result = sut.CalculateMinDiff();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void
            calculate_min_diff_behaves_correctly_even_if_some_one_mannually_called_move_right_or_left_prior_to_calling_calculate_min_diff()
        {
            var input = new[] { 3, 1, 2, 4, 3 };
            var sut = new Tape(input);
            sut.MoveSplitToRight();
            sut.MoveSplitToRight();
            sut.MoveSplitToRight();
            var result = sut.CalculateMinDiff();

            Assert.Equal(1, result);
        }
    }
}
