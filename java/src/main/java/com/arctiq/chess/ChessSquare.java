package com.arctiq.chess;

/**
 * Represents a square on a chess board.
 */
public class ChessSquare {
    private final boolean isDark;
    private final int rank;
    private final int rank_idx;
    private final char file;
    private final int file_idx;
    private boolean occupied;
    private ChessPiece piece;

    /**
     * Constructs a ChessSquare with the specified square.
     * @param square the square on the chess board, in the format of file and rank (e.g. a1).
     * @throws IllegalArgumentException if the square is invalid.
     */
    public ChessSquare(String square) {
        if (square.length() != 2) {
            throw new IllegalArgumentException("Invalid square. Square must be in the format of file and rank (e.g. a1)");
        }
        char file = square.charAt(0);
        int rank = Integer.parseInt(square.substring(1));
        if (rank < 1 || rank > 8) {
            throw new IllegalArgumentException("Invalid rank. Rank must be within the range of 1-8");
        }
        if (file < 'a' || file > 'h') {
            throw new IllegalArgumentException("Invalid file. File must be within the range of a-h");
        }
        this.isDark = ((rank + (file - 'a')) % 2) != 0;
        this.rank = rank;
        this.rank_idx = rank - 1;
        this.file = file;
        this.file_idx = file - 'a';
        this.occupied = false;
        this.piece = null;
    }

    /**
     * Returns the color of this ChessSquare.
     * @return the color of this ChessSquare, either "dark" or "light".
     */
    public String getColor() {
        if (isDark) {
            return "dark";
        } else {
            return "light";
        }
    }

    /**
     * Returns the rank of this ChessSquare.
     * @return the rank of this ChessSquare.
     */
    public int getRank() {
        return rank;
    }

    /**
     * Returns the rank index of this ChessSquare.
     * @return the rank index of this ChessSquare.
     */
    public int getRankIdx() {
        return rank_idx;
    }

    /**
     * Returns the file of this ChessSquare.
     * @return the file of this ChessSquare.
     */
    public char getFile() {
        return file;
    }

    /**
     * Returns the file index of this ChessSquare.
     * @return the file index of this ChessSquare.
     */
    public int getFileIdx() {
        return file_idx;
    }

    /**
     * Returns the square of this ChessSquare.
     * @return the square of this ChessSquare, in the format of file and rank (e.g. a1).
     */
    public String getSquare() {
        return file + Integer.toString(rank);
    }

    /**
     * Checks if this ChessSquare is occupied.
     * @return true if this ChessSquare is occupied, false otherwise.
     */
    public boolean isOccupied() {
        return occupied;
    }

    /**
     * Returns the ChessPiece on this ChessSquare.
     * @return the ChessPiece on this ChessSquare, or null if the square is not occupied.
     */
    public ChessPiece getPiece() {
        return piece;
    }

    /**
     * Sets the ChessPiece on this ChessSquare.
     * @param piece the ChessPiece to place on this ChessSquare.
     */
    public void setPiece(ChessPiece piece) {
        this.piece = piece;
        this.occupied = piece != null;
    }

    /**
     * Clears the ChessPiece from this ChessSquare.
     */
    public void clearPiece() {
        this.piece = null;
        this.occupied = false;
    }
}