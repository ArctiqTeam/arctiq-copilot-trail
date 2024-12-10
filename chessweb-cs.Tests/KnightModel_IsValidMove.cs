using Xunit;
using ChessWeb.Models;

namespace KnightModel.UnitTests.Models
{
    public class KnightModel_IsValidMove
    {
        [Fact]
        public void IsValidMove_WhenMoveIsForwardOneSquare_ThenReturnsFalse()
        {
            // Arrange
            var knight = new Knight(PieceColor.White, new Position(1, 1));
            var board = new Board();
            board.Squares[1, 1] = knight;

            // Act - We're moving the white knight one square forward down the board
            var result = knight.IsValidMove(knight.Position, new Position(2, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenMoveIsLShape_ThenReturnsTrue()
        {
            // Arrange
            var knight = new Knight(PieceColor.White, new Position(1, 1));
            var board = new Board();
            board.Squares[1, 1] = knight;

            // Act - We're moving the white knight in an L-shape
            var result = knight.IsValidMove(knight.Position, new Position(3, 2), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_WhenMoveIsAnotherLShape_ThenReturnsTrue()
        {
            // Arrange
            var knight = new Knight(PieceColor.White, new Position(4, 4));
            var board = new Board();
            board.Squares[4, 4] = knight;

            // Act - We're moving the white knight in another L-shape
            var result = knight.IsValidMove(knight.Position, new Position(6, 5), board);

            // Assert
            Assert.True(result);
        }

        // TODO: Test more invalid moves

    }
    
}
