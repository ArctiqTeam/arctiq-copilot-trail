using Xunit;
using ChessWeb.Models;

namespace RookModel.UnitTests.Models
{
    public class RookModel_IsValidMove
    {
        [Fact]
        public void IsValidMove_StraightMoveVertical_ReturnsTrue()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(PieceColor.White, new Position(3, 3));
            board.Squares[3, 3] = rook;

            // Act
            var result = rook.IsValidMove(new Position(3, 3), new Position(7, 3), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_StraightMoveHorizontal_ReturnsTrue()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(PieceColor.White, new Position(3, 3));
            board.Squares[3, 3] = rook;

            // Act
            var result = rook.IsValidMove(new Position(3, 3), new Position(3, 7), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_DiagonalMove_ReturnsFalse()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(PieceColor.White, new Position(3, 3));
            board.Squares[3, 3] = rook;

            // Act
            var result = rook.IsValidMove(new Position(3, 3), new Position(5, 5), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_PathBlockedByOwnPiece_ReturnsFalse()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(PieceColor.White, new Position(3, 3));
            var blockingPiece = new Pawn(PieceColor.White, new Position(5, 3));
            board.Squares[3, 3] = rook;
            board.Squares[5, 3] = blockingPiece;

            // Act
            var result = rook.IsValidMove(new Position(3, 3), new Position(7, 3), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_PathBlockedByOpponentPiece_ReturnsFalse()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(PieceColor.White, new Position(3, 3));
            var blockingPiece = new Pawn(PieceColor.Black, new Position(5, 3));
            board.Squares[3, 3] = rook;
            board.Squares[5, 3] = blockingPiece;

            // Act
            var result = rook.IsValidMove(new Position(3, 3), new Position(7, 3), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_CaptureOpponentPiece_ReturnsTrue()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(PieceColor.White, new Position(3, 3));
            var opponentPiece = new Pawn(PieceColor.Black, new Position(5, 3));
            board.Squares[3, 3] = rook;
            board.Squares[5, 3] = opponentPiece;

            // Act
            var result = rook.IsValidMove(new Position(3, 3), new Position(5, 3), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_MoveOutOfBoard_ThenThrowsException()
        {
            // Arrange
            var rook = new Rook(PieceColor.White, new Position(0, 0));
            var board = new Board();
            board.Squares[0, 0] = rook;

            // Act & Assert - Rook tries to move out of the board
            Assert.Throws<IndexOutOfRangeException>(() => rook.IsValidMove(rook.Position, new Position(-1, 0), board));
        }

        [Fact]
        public void IsValidMove_MoveToSamePosition_ReturnsFalse()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(PieceColor.White, new Position(3, 3));
            board.Squares[3, 3] = rook;

            // Act
            var result = rook.IsValidMove(new Position(3, 3), new Position(3, 3), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_CaptureOwnPiece_ReturnsFalse()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(PieceColor.White, new Position(3, 3));
            var ownPiece = new Pawn(PieceColor.White, new Position(5, 3));
            board.Squares[3, 3] = rook;
            board.Squares[5, 3] = ownPiece;

            // Act
            var result = rook.IsValidMove(new Position(3, 3), new Position(5, 3), board);

            // Assert
            Assert.False(result);
        }
    }
}