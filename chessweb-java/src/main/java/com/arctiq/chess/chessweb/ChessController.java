package com.arctiq.chess.chessweb;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Controller
public class ChessController {
    private static final Logger logger = LoggerFactory.getLogger(FenParser.class);
    private ChessBoard chessBoard = new ChessBoard();

    @GetMapping("/")
    public String index(Model model) {
        model.addAttribute("fen", chessBoard.getFen());
        logger.info("GET Root with FEN: {}", chessBoard.getFen());
        return "index";
    }

    @PostMapping("/loadFen")
    public String loadFen(@RequestParam String fen, Model model) {
        try {
            chessBoard = FenParser.parseFen(fen);
            model.addAttribute("fen", chessBoard.getFen());
            logger.info("IN: {}", model.getAttribute("fen"));
        } catch (IllegalArgumentException e) {
            model.addAttribute("error", e.getMessage());
            model.addAttribute("fen", "empty");
        }
        logger.info("POST loadFen with FEN: {}", model.getAttribute("fen"));
        return "index";
    }
}