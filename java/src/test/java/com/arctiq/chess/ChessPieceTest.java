package com.arctiq.chess;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

public class ChessPieceTest {
    private ChessPiece chessPiece;

    @BeforeEach
    void setUp() {
        chessPiece = new ChessPiece("white", "rook");
    }

    @Test
    void testGetColor() {
        assertEquals("white", chessPiece.getColor());
    }

    @Test
    void testGetType() {
        assertEquals("rook", chessPiece.getType());
    }

    @Test
    void testToString() {
        assertEquals("white rook", chessPiece.toString());
    }

    @Test
    void testToFEN() {
        assertEquals("R", chessPiece.toFEN());
    }

    @Test
    void testInvalidColor() {
        assertThrows(IllegalArgumentException.class, () -> new ChessPiece("red", "rook"));
    }

    @Test
    void testInvalidType() {
        assertThrows(IllegalArgumentException.class, () -> new ChessPiece("white", "pontiff"));
    }
}
