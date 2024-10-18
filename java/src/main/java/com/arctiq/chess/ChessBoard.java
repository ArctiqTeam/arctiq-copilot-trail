package com.arctiq.chess;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.logging.Logger;

public class ChessBoard {
    private ChessSquare[][] board;
    private List<ChessPiece> capturedPieces;
    private List<String> moveList;
    private String player1;
    private String player2;
    private char toMove;
    private List<Character> canCastle;
    private ChessSquare enPassant;
    private int halfMoveClock;
    private int fullMoveNumber;
    private static final Logger LOGGER = Logger.getLogger(ChessBoard.class.getName());

    public ChessBoard(String player1, String player2) {
        this.board = new ChessSquare[8][8];
        this.capturedPieces = new ArrayList<>();
        this.moveList = new ArrayList<>();
        this.player1 = player1;
        this.player2 = player2;
        this.toMove = 'w';
        this.canCastle = Arrays.asList('K', 'Q', 'k', 'q');
        this.enPassant = null;
        this.halfMoveClock = 0;
        this.fullMoveNumber = 0;

        for (int rank = 0; rank < 8; rank++) {
            for (char file = 0; file < 8; file++) {
                String square = (char) (file + 'a') + Integer.toString(rank + 1);
                this.board[rank][file] = new ChessSquare(square);
            }
        }
    }

    // create method to set initial chess board position
    public void setInitialPosition() {
        // Set the initial position of the chess board
        this.addPiece("white", "rook", "a1");
        this.addPiece("white", "knight", "b1");
        this.addPiece("white", "bishop", "c1");
        this.addPiece("white", "queen", "d1");
        this.addPiece("white", "king", "e1");
        this.addPiece("white", "bishop", "f1");
        this.addPiece("white", "knight", "g1");
        this.addPiece("white", "rook", "h1");
        for (char file = 'a'; file <= 'h'; file++) {
            this.addPiece("white", "pawn", file + "2");
            this.addPiece("black", "pawn", file + "7");
        }
        this.addPiece("black", "rook", "a8");
        this.addPiece("black", "knight", "b8");
        this.addPiece("black", "bishop", "c8");
        this.addPiece("black", "queen", "d8");
        this.addPiece("black", "king", "e8");
        this.addPiece("black", "bishop", "f8");
        this.addPiece("black", "knight", "g8");
        this.addPiece("black", "rook", "h8");
    }

    public boolean isValidPiece(String color, String piece) {
        // Attempt to create a new ChessPiece object with the given color and piece
        try {
            ChessPiece chessPiece = new ChessPiece(color, piece);
        } catch (IllegalArgumentException e) {
            // If an IllegalArgumentException is thrown, return false
            LOGGER.severe("Invalid piece: " + piece + " for color: " + color);
            return false;
        }
        // If the object is created successfully, return true, otherwise return false
        return true;
    }

    // validate the input square by coordinates
    public boolean isValidSquare(String square) {
        // Check if the input square coordinate is valid
        try {
            ChessSquare chessSquare = new ChessSquare(square);
        } catch (IllegalArgumentException e) {
            LOGGER.severe("Invalid square: " + square);
            return false;
        }
        return true;
    }

    public void addPiece(String color, String piece, String square) {
        // Validate the input piece and position
        ChessPiece to_add = null;
        ChessSquare target = null;
        try {
            to_add = new ChessPiece(color, piece);
        } catch (IllegalArgumentException e) {
            throw new IllegalArgumentException("Invalid piece: " + piece + " for color: " + color);
        }
        try {
            target = new ChessSquare(square);
        } catch (IllegalArgumentException e) {
            throw new IllegalArgumentException("Invalid square: " + square);
        }

        // Check if the square is occupied
        if(this.getPieceAt(square) != null) {
            LOGGER.severe("Square is occupied: " + square);
            LOGGER.severe("Piece: " + this.getPieceAt(square).toString());
            throw new IllegalArgumentException("Square is occupied: " + square);
        } else {
            // Add the piece to the board
            this.board[target.getRankIdx()][target.getFileIdx()].setPiece(to_add);
        }
    }

    public void removePiece(String square) {
        // Remove piece from the board
        ChessSquare target = new ChessSquare(square);
        if (this.getPieceAt(square) != null) this.board[target.getRankIdx()][target.getFileIdx()].clearPiece();
    }

    public void takePiece(String square, ChessPiece piece) {
        // Remove the piece from the board and add it to the capturedPieces list
        this.capturedPieces.add(this.getPieceAt(square));
        this.removePiece(square);
        this.addPiece(piece.getColor(), piece.getType(), square);
    }

    public ChessPiece getPieceAt(String square) {
        // Get the piece at the given square
        ChessSquare target = new ChessSquare(square);
        return this.board[target.getRankIdx()][target.getFileIdx()].getPiece();
    }

    public void makeMove(String move) {
        // Make a move and update the board, capturedPieces, and moveList
    }

    public void setToMove(String color) {
        // Set the player to move based on the color
        if (color.equals("white")) {
            this.toMove = 'w';
        } else if (color.equals("black")) {
            this.toMove = 'b';
        } else {
            throw new IllegalArgumentException("Invalid color: " + color);
        }
    }

    public void toggleCanCastle(String color, String side) {
        int castleIndex = 0;
        if ("white".equals(color)) {
            castleIndex += 0;
        } else if ("black".equals(color)) {
            castleIndex = 2;
        } else {
            throw new IllegalArgumentException("Invalid color");
        }
        if ("kingside".equals(side)) {
            // Do nothing we're already at the right index.
        } else if ("queenside".equals(side)) {
            castleIndex += 1;
        } else {
            throw new IllegalArgumentException("Invalid side");
        }

        if (this.canCastle.get(castleIndex) != '\u0000') {
            this.canCastle.set(castleIndex, '\u0000');
        } else {
            List<Character> options = Arrays.asList('K', 'Q', 'k', 'q');
            this.canCastle.set(castleIndex, options.get(castleIndex));
        }
    }

    public boolean getCanCastle(String color, String side) {
        // Get the current castling options for the given color and side
        int castleIndex = 0;
        if ("white".equals(color)) {
            castleIndex += 0;
        } else if ("black".equals(color)) {
            castleIndex = 2;
        }  else {
            throw new IllegalArgumentException("Invalid color");
        }
        if ("kingside".equals(side)) {
            // Do nothing we're already at the right index.
        } else if ("queenside".equals(side)) {
            castleIndex += 1;
        } else {
            throw new IllegalArgumentException("Invalid side");
        }
        return this.canCastle.get(castleIndex) != '\u0000';
    }

    public String getFEN() {
        StringBuilder fen = new StringBuilder();

        for (int rank = 7; rank >= 0; rank--) {
            int emptySquares = 0;
            for (int file = 0; file < 8; file++) {
                ChessPiece piece = this.board[rank][file].getPiece();
                if (piece == null) {
                    emptySquares++;
                } else {
                    if (emptySquares != 0) {
                        fen.append(emptySquares);
                        emptySquares = 0;
                    }
                    fen.append(piece.toFEN());
                }
            }
            if (emptySquares != 0) {
                fen.append(emptySquares);
            }
            if (rank != 0) {
                fen.append('/');
            }
        }
        fen.append(' ' + Character.toString(this.toMove) + ' ');
        // Fill in the castling options block:
        for (char castle : this.canCastle) {
            // Will check if the field has been set to null char and print only the existing values.
            if ('\u0000' != castle) {
                fen.append(castle);
            }
        }
        fen.append(' ');
        // Fill in the en passant square block:
        if (this.enPassant != null) {
            fen.append(this.enPassant.getSquare());
        } else {
            fen.append('-');
        }
        fen.append(' ');
        // Fill in the half move clock block:
        fen.append(this.halfMoveClock);
        fen.append(' ');
        // Fill in the full move number block:
        fen.append(this.fullMoveNumber);

        return fen.toString();
    }

    public String getASCIIBoard() {
        // Use StringBuilder to build the ASCII representation of the chess board
        StringBuilder boardRepresentation = new StringBuilder();
        for (int rank = 7; rank >= 0; rank--) {
            for (int file = 0; file < 8; file++) {
                ChessPiece piece = this.board[rank][file].getPiece();
                if (piece == null) {
                    boardRepresentation.append(" . ");
                } else {
                    boardRepresentation.append(" ").append(piece.toFEN()).append(" ");
                }
            }
            boardRepresentation.append("\n");
        }
        return boardRepresentation.toString();
    }
}
