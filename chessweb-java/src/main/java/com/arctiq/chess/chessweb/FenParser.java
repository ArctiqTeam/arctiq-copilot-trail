package com.arctiq.chess.chessweb;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class FenParser {
    // get the logger object for this class
    private static final Logger logger = LoggerFactory.getLogger(FenParser.class);

    public static ChessBoard parseFen(String fen) throws IllegalArgumentException {

        if (!isValidFEN(fen)) {
            logger.error("Invalid FEN string: {}", fen);
            throw new IllegalArgumentException("Invalid FEN string");
        }
        ChessBoard board = new ChessBoard();
        board.setFen(fen);
        logger.debug("Parsed FEN string: {} OK", fen);
        return board;
    }

    private static boolean isValidFEN(String fen) {
        // Basic validation for FEN string: there are six elements separated by spaces
        String[] parts = fen.split(" ");
        if (parts.length != 6) {
            return false;
        }

        // Validate board layout: 8 rows separated by slashes
        String[] rows = parts[0].split("/");
        if (rows.length != 8) {
            return false;
        }

        for (String row : rows) {
            int count = 0;
            for (char c : row.toCharArray()) {
                if (Character.isDigit(c)) {
                    count += Character.getNumericValue(c);
                } else if ("prnbqkPRNBQK".indexOf(c) != -1) {
                    count++;
                } else {
                    return false;
                }
            }
            if (count != 8) {
                return false;
            }
        }

        // Additional validations can be added here for other parts of the FEN string

        return true;
    }
}