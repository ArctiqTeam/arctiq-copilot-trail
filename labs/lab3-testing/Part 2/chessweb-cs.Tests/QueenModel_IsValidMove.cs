using Xunit;
using ChessWeb.Models;

namespace QueenModel.UnitTests.Models
{
    public class QueenModel_IsValidMove
    {
        [Fact]
        public void IsValidMove_DiagonalMove_ReturnsTrue()
        {
            var board = new Board();
            var queen = new Queen(PieceColor.White, new Position(3, 3));
            board.Squares[3, 3] = queen;

            var result = queen.IsValidMove(new Position(3, 3), new Position(5, 5), board);

            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_StraightMove_ReturnsTrue()
        {
            var board = new Board();
            var queen = new Queen(PieceColor.White, new Position(3, 3));
            board.Squares[3, 3] = queen;

            var result = queen.IsValidMove(new Position(3, 3), new Position(3, 7), board);

            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_InvalidMove_ReturnsFalse()
        {
            var board = new Board();
            var queen = new Queen(PieceColor.White, new Position(3, 3));
            board.Squares[3, 3] = queen;

            var result = queen.IsValidMove(new Position(3, 3), new Position(4, 6), board);

            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_PathBlocked_ReturnsFalse()
        {
            var board = new Board();
            var queen = new Queen(PieceColor.White, new Position(3, 3));
            var blockingPiece = new Pawn(PieceColor.White, new Position(4, 4));
            board.Squares[3, 3] = queen;
            board.Squares[4, 4] = blockingPiece;

            var result = queen.IsValidMove(new Position(3, 3), new Position(5, 5), board);

            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_CaptureOpponentPiece_ReturnsTrue()
        {
            var board = new Board();
            var queen = new Queen(PieceColor.White, new Position(3, 3));
            var opponentPiece = new Pawn(PieceColor.Black, new Position(5, 5));
            board.Squares[3, 3] = queen;
            board.Squares[5, 5] = opponentPiece;

            var result = queen.IsValidMove(new Position(3, 3), new Position(5, 5), board);

            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_CaptureOpponentPieceJumpingOverOwnPiece_ReturnsFalse()
        {
            var board = new Board();
            var queen = new Queen(PieceColor.White, new Position(3, 3));
            var ownPiece = new Pawn(PieceColor.White, new Position(4, 4));
            var opponentPiece = new Pawn(PieceColor.Black, new Position(5, 5));
            board.Squares[3, 3] = queen;
            board.Squares[4, 4] = ownPiece;
            board.Squares[5, 5] = opponentPiece;

            var result = queen.IsValidMove(new Position(3, 3), new Position(5, 5), board);

            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_CaptureOpponentPieceJumpingOverOpponentPiece_ReturnsFalse()
        {
            // Arrange
            var queen = new Queen(PieceColor.White, new Position(4, 4));
            var opponentPiece1 = new Pawn(PieceColor.Black, new Position(4, 5));
            var opponentPiece2 = new Pawn(PieceColor.Black, new Position(4, 6));
            var board = new Board();
            board.Squares[4, 4] = queen;
            board.Squares[4, 5] = opponentPiece1;
            board.Squares[4, 6] = opponentPiece2;

            // Act - Queen tries to capture an opponent's piece by jumping over another opponent's piece
            var result = queen.IsValidMove(queen.Position, new Position(4, 6), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenQueenMovesOutOfBoard_ThenThrowsException()
        {
            // Arrange
            var queen = new Queen(PieceColor.White, new Position(0, 0));
            var board = new Board();
            board.Squares[0, 0] = queen;

            // Act & Assert - Queen tries to move out of the board
            Assert.Throws<IndexOutOfRangeException>(() => queen.IsValidMove(queen.Position, new Position(-1, -1), board));
        }

        [Fact]
        public void IsValidMove_CaptureOwnPiece_ReturnsFalse()
        {
            // Arrange
            var queen = new Queen(PieceColor.White, new Position(4, 4));
            var ownPiece = new Pawn(PieceColor.White, new Position(4, 6));
            var board = new Board();
            board.Squares[4, 4] = queen;
            board.Squares[4, 6] = ownPiece;

            // Act - Queen tries to capture its own piece
            var result = queen.IsValidMove(queen.Position, new Position(4, 6), board);

            // Assert
            Assert.False(result);
        }
    }
}