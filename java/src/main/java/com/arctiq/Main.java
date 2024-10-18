package com.arctiq;

import com.arctiq.chess.ChessBoard;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;

public class Main {
    public static void main(String[] args) {
        ChessBoard chessBoard = new ChessBoard("Player1", "Player2");

        chessBoard.addPiece("black", "knight", "b8");
        chessBoard.addPiece("white", "rook", "d8");
        chessBoard.addPiece("black", "king", "e8");
        chessBoard.addPiece("black", "bishop", "f8");
        chessBoard.addPiece("black", "rook", "h8");
        chessBoard.addPiece("black", "pawn", "a7");
        chessBoard.addPiece("black", "pawn", "f7");
        chessBoard.addPiece("black", "pawn", "g7");
        chessBoard.addPiece("black", "pawn", "h7");
        chessBoard.addPiece("black", "queen", "e6");
        chessBoard.addPiece("black", "pawn", "e5");
        chessBoard.addPiece("white", "bishop", "g5");
        chessBoard.addPiece("white", "pawn", "e4");
        chessBoard.addPiece("white", "pawn", "a2");
        chessBoard.addPiece("white", "pawn", "b2");
        chessBoard.addPiece("white", "pawn", "c2");
        chessBoard.addPiece("white", "pawn", "f2");
        chessBoard.addPiece("white", "pawn", "g2");
        chessBoard.addPiece("white", "pawn", "h2");
        chessBoard.addPiece("white", "king", "c1");
        chessBoard.toggleCanCastle("white", "kingside");
        chessBoard.toggleCanCastle("white", "queenside");
        chessBoard.toggleCanCastle("black", "kingside");
        chessBoard.setToMove("black");
        System.out.println(chessBoard.getFEN());
        System.out.println(chessBoard.getASCIIBoard());

        try {
            String url = "https://fen2image.chessvision.ai/";
            System.out.println(url + urlEncode(chessBoard.getFEN()));
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
    }

    public static String urlEncode(String input) throws UnsupportedEncodingException {
        String encoded = URLEncoder.encode(input, "UTF-8");
        return encoded.replace("+", "%20");
    }
}
