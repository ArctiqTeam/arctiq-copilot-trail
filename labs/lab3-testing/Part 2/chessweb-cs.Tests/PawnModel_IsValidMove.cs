using Xunit;
using ChessWeb.Models;

namespace PawnModel.UnitTests.Models
{
    public class PawnModel_IsValidMove
    {
        [Fact]
        public void IsValidMove_WhenMoveBlackIsForwardOneSquare_ThenReturnsTrue()
        {
            // Arrange
            var pawn = new Pawn(PieceColor.Black, new Position(1, 1));
            var board = new Board();
            board.Squares[1, 1] = pawn;

            // Act - We're moving the white pawn one square forward down the board
            var result = pawn.IsValidMove(pawn.Position, new Position(2, 1), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_WhenMoveBlackIsForwardTwoSquares_ThenReturnsTrue()
        {
            // Arrange
            var pawn = new Pawn(PieceColor.Black, new Position(1, 1));
            var board = new Board();
            board.Squares[1, 1] = pawn;

            // Act - We're moving the white pawn two squares forward down the board
            var result = pawn.IsValidMove(pawn.Position, new Position(3, 1), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_WhenMoveWhiteIsForwardOneSquare_ThenReturnsTrue()
        {
            // Arrange
            var pawn = new Pawn(PieceColor.White, new Position(6, 1));
            var board = new Board();
            board.Squares[6, 1] = pawn;

            // Act - We're moving the white pawn one square forward up the board
            var result = pawn.IsValidMove(pawn.Position, new Position(5, 1), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_WhenMoveWhiteIsForwardTwoSquares_ThenReturnsTrue()
        {
            // Arrange
            var pawn = new Pawn(PieceColor.White, new Position(6, 1));
            var board = new Board();
            board.Squares[6, 1] = pawn;

            // Act - We're moving the white pawn two squares forward up the board
            var result = pawn.IsValidMove(pawn.Position, new Position(4, 1), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_WhenMoveBlackIsForwardTwoSquaresNotFromStartingPosition_ThenReturnsFalse()
        {
            // Arrange
            var pawn = new Pawn(PieceColor.Black, new Position(2, 1));
            var board = new Board();
            board.Squares[2, 1] = pawn;

            // Act - We're trying to move the black pawn two squares forward from a non-starting position
            var result = pawn.IsValidMove(pawn.Position, new Position(4, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenMoveWhiteIsForwardTwoSquaresNotFromStartingPosition_ThenReturnsFalse()
        {
            // Arrange
            var pawn = new Pawn(PieceColor.White, new Position(5, 1));
            var board = new Board();
            board.Squares[5, 1] = pawn;

            // Act - We're trying to move the white pawn two squares forward from a non-starting position
            var result = pawn.IsValidMove(pawn.Position, new Position(3, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenBlackPawnCapturesWhitePawnDiagonally_ThenReturnsTrue()
        {
            // Arrange
            var blackPawn = new Pawn(PieceColor.Black, new Position(4, 4));
            var whitePawn = new Pawn(PieceColor.White, new Position(5, 5));
            var board = new Board();
            board.Squares[4, 4] = blackPawn;
            board.Squares[5, 5] = whitePawn;

            // Act - Black pawn captures white pawn diagonally
            var result = blackPawn.IsValidMove(blackPawn.Position, new Position(5, 5), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_WhenWhitePawnCapturesBlackPawnDiagonally_ThenReturnsTrue()
        {
            // Arrange
            var whitePawn = new Pawn(PieceColor.White, new Position(4, 4));
            var blackPawn = new Pawn(PieceColor.Black, new Position(3, 3));
            var board = new Board();
            board.Squares[4, 4] = whitePawn;
            board.Squares[3, 3] = blackPawn;

            // Act - White pawn captures black pawn diagonally
            var result = whitePawn.IsValidMove(whitePawn.Position, new Position(3, 3), board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidMove_WhenBlackPawnIsBlockedByAnotherPiece_ThenReturnsFalse()
        {
            // Arrange
            var blackPawn = new Pawn(PieceColor.Black, new Position(1, 1));
            var blockingPiece = new Pawn(PieceColor.Black, new Position(2, 1));
            var board = new Board();
            board.Squares[1, 1] = blackPawn;
            board.Squares[2, 1] = blockingPiece;

            // Act - Black pawn tries to move forward but is blocked
            var result = blackPawn.IsValidMove(blackPawn.Position, new Position(2, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenWhitePawnIsBlockedByAnotherPiece_ThenReturnsFalse()
        {
            // Arrange
            var whitePawn = new Pawn(PieceColor.White, new Position(6, 1));
            var blockingPiece = new Pawn(PieceColor.White, new Position(5, 1));
            var board = new Board();
            board.Squares[6, 1] = whitePawn;
            board.Squares[5, 1] = blockingPiece;

            // Act - White pawn tries to move forward but is blocked
            var result = whitePawn.IsValidMove(whitePawn.Position, new Position(5, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenBlackPawnIsBlockedByAnotherPieceTwoSquaresAhead_ThenReturnsFalse()
        {
            // Arrange
            var blackPawn = new Pawn(PieceColor.Black, new Position(1, 1));
            var blockingPiece = new Pawn(PieceColor.Black, new Position(3, 1));
            var board = new Board();
            board.Squares[1, 1] = blackPawn;
            board.Squares[3, 1] = blockingPiece;

            // Act - Black pawn tries to move two squares forward but is blocked
            var result = blackPawn.IsValidMove(blackPawn.Position, new Position(3, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenWhitePawnIsBlockedByAnotherPieceTwoSquaresAhead_ThenReturnsFalse()
        {
            // Arrange
            var whitePawn = new Pawn(PieceColor.White, new Position(6, 1));
            var blockingPiece = new Pawn(PieceColor.White, new Position(4, 1));
            var board = new Board();
            board.Squares[6, 1] = whitePawn;
            board.Squares[4, 1] = blockingPiece;

            // Act - White pawn tries to move two squares forward but is blocked
            var result = whitePawn.IsValidMove(whitePawn.Position, new Position(4, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenBlackPawnTriesToJumpOverWhitePawnTwoSquaresAhead_ThenReturnsFalse()
        {
            // Arrange
            var blackPawn = new Pawn(PieceColor.Black, new Position(1, 1));
            var whitePawn = new Pawn(PieceColor.White, new Position(2, 1));
            var board = new Board();
            board.Squares[1, 1] = blackPawn;
            board.Squares[2, 1] = whitePawn;

            // Act - Black pawn tries to move two squares forward but is blocked by a white pawn
            var result = blackPawn.IsValidMove(blackPawn.Position, new Position(3, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenWhitePawnTriesToJumpOverBlackPawnTwoSquaresAhead_ThenReturnsFalse()
        {
            // Arrange
            var whitePawn = new Pawn(PieceColor.White, new Position(6, 1));
            var blackPawn = new Pawn(PieceColor.Black, new Position(5, 1));
            var board = new Board();
            board.Squares[6, 1] = whitePawn;
            board.Squares[5, 1] = blackPawn;

            // Act - White pawn tries to move two squares forward but is blocked by a black pawn
            var result = whitePawn.IsValidMove(whitePawn.Position, new Position(4, 1), board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidMove_WhenPawnMovesOutOfBoard_ThenThrowsException()
        {
            // Arrange
            var whitePawn = new Pawn(PieceColor.White, new Position(0, 0)); // Position at the edge of the board
            var board = new Board();
            board.Squares[0, 0] = whitePawn;

            // Act & Assert - Pawn tries to move out of the board
            Assert.Throws<IndexOutOfRangeException>(() => whitePawn.IsValidMove(whitePawn.Position, new Position(-1, 0), board));
        }
    }
}
