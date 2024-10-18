package com.arctiq.chess;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class ChessBoardTest {
    private ChessBoard chessBoard;

    @BeforeEach
    void setUp() {
        chessBoard = new ChessBoard("Alice", "Bob");
    }

    @Test
    void testAddPiece() {
        chessBoard.addPiece("white", "rook", "a1");
        assertEquals("R", chessBoard.getPieceAt("a1").toFEN());
        chessBoard.addPiece("black", "rook", "h8");
        assertEquals("r", chessBoard.getPieceAt("h8").toFEN());
    }

    @Test
    void testRemovePiece() {
        chessBoard.addPiece( "white","rook", "a8");
        chessBoard.removePiece("a8");
        assertNull(chessBoard.getPieceAt("a8"));
    }

    @Test
    void testAddPieceFailure() {
        assertThrows(IllegalArgumentException.class, () -> chessBoard.addPiece("white", "rook", "a9"));
        assertThrows(IllegalArgumentException.class, () -> chessBoard.addPiece("white", "rook", "i1"));
        assertThrows(IllegalArgumentException.class, () -> chessBoard.addPiece("white", "rook", "a0"));
    }

    @Test
    void testGetFEN() {
        chessBoard.setInitialPosition();
        assertEquals("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 0", chessBoard.getFEN());
    }

    @Test
    void testToggleCanCastle() {
        ChessBoard board = new ChessBoard("player1", "player2");

        // Test for valid inputs
        board.toggleCanCastle("white", "kingside");
        assertFalse(board.getCanCastle("white", "kingside"));
        board.toggleCanCastle("white", "kingside");
        assertTrue(board.getCanCastle("white", "kingside"));

        board.toggleCanCastle("black", "queenside");
        assertFalse(board.getCanCastle("black", "queenside"));
        board.toggleCanCastle("black", "queenside");
        assertTrue(board.getCanCastle("black", "queenside"));

        // Test for invalid inputs
        assertThrows(IllegalArgumentException.class, () -> {
            board.toggleCanCastle("red", "kingside");
        });

        assertThrows(IllegalArgumentException.class, () -> {
            board.toggleCanCastle("white", "northside");
        });
    }

}