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
            string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

            // Act
            board.SetPositionFromFen(fen);

            // Assert
            Assert.Equal(fen, board.GenerateFEN().Split(' ')[0]);
        }

        [Fact]
        public void SetPositionFromFen_InvalidFEN_ThrowsArgumentException()
        {
            // Arrange
            var board = new Board();
            string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => board.SetPositionFromFen(fen));
        }

        [Fact]
        public void SetPositionFromFen_EmptyBoard_SetsBoardCorrectly()
        {
            // Arrange
            var board = new Board();
            string fen = "8/8/8/8/8/8/8/8";

            // Act
            board.SetPositionFromFen(fen);

            // Assert
            Assert.Equal(fen, board.GenerateFEN().Split(' ')[0]);
        }

    }
}