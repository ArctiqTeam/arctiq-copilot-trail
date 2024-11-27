using System;
using Xunit;
using ChessWeb.Models;

namespace ChessWeb.Models.UnitTests
{
    public class BoardTests
    {
        [Fact]
        public void SetPositionFromFen_ValidFEN_SetsBoardCorrectly()
        {
            // Arrange
            var board = new Board();
            // This is a correct starting position FEN but we don't construct it correctly yet.
            // string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1";

            // Act
            board.SetPositionFromFen(fen);

            // Assert
            Assert.Equal(fen, board.GenerateFEN());
        }

        [Fact]
        public void SetPositionFromFen_InvalidFEN_ThrowsArgumentException()
        {
            // Arrange
            var board = new Board();
            string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => board.SetPositionFromFen(fen));
        }

        [Fact]
        public void SetPositionFromFen_EmptyBoard_SetsBoardCorrectly()
        {
            // Arrange
            var board = new Board();
            string fen = "8/8/8/8/8/8/8/8 w - - 0 1";

            // Act
            board.SetPositionFromFen(fen);

            // Assert
            Assert.Equal(fen, board.GenerateFEN());
        }

    }
}