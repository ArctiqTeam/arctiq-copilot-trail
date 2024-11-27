namespace ChessWeb.Models;
using System;
using System.Text;
public class Board
{
    private const string INITIAL_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    private const string EMPTY_FEN = "8/8/8/8/8/8/8/8 w - - 0 1";
    public Piece?[,] Squares { get; private set; } = new Piece[8,8];
    
    public void SetPositionFromFen(string fen = INITIAL_FEN)
    {
        if (string.IsNullOrWhiteSpace(fen) || fen == "start")
            fen = INITIAL_FEN;
        else if (fen == "empty")
            fen = EMPTY_FEN;
        Squares = new Piece[8,8]; // Reset board
        string[] fenParts = fen.Split(' ');
        string position = fenParts[0];
        int row = 0;
        int col = 0;
        
        foreach (char c in position)
        {
            if (c == '/')
            {
                row++;
                col = 0;
            }
            else if (char.IsDigit(c))
            {
                col += (c - '0');
            }
            else
            {
                PieceColor color = char.IsUpper(c) ? PieceColor.White : PieceColor.Black;
                var pos = new Position(row, col);
                
                Squares[row, col] = char.ToUpper(c) switch
                {
                    'P' => new Pawn(color, pos),
                    'R' => new Rook(color, pos),
                    'N' => new Knight(color, pos),
                    'B' => new Bishop(color, pos),
                    'Q' => new Queen(color, pos),
                    'K' => new King(color, pos),
                    _ => throw new ArgumentException($"Invalid piece character: {c}")
                };
                col++;
            }
        }
    }

    public string GenerateFEN()
    {
        StringBuilder fen = new StringBuilder();
        for (int row = 0; row < 8; row++)
        {
            int emptyCount = 0;
            for (int col = 0; col < 8; col++)
            {
                var piece = Squares[row, col];
                if (piece == null)
                {
                    emptyCount++;
                }
                else
                {
                    if (emptyCount > 0)
                    {
                        fen.Append(emptyCount);
                        emptyCount = 0;
                    }
                    char pieceChar = piece.Type switch
                    {
                        PieceType.Pawn => 'p',
                        PieceType.Knight => 'n',
                        PieceType.Bishop => 'b',
                        PieceType.Rook => 'r',
                        PieceType.Queen => 'q',
                        PieceType.King => 'k',
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    fen.Append(piece.Color == PieceColor.White ? char.ToUpper(pieceChar) : pieceChar);
                }
            }
            if (emptyCount > 0)
            {
                fen.Append(emptyCount);
            }
            if (row < 7)
            {
                fen.Append('/');
            }
        }
        // Add additional FEN parts (active color, castling availability, etc.) if needed
        fen.Append(" w - - 0 1"); // Simplified for example purposes
        return fen.ToString();
    }

    public void MovePiece(Position from, Position to)
    {
        var piece = Squares[from.Row, from.Column];
        if (piece != null) {
            Squares[to.Row, to.Column] = piece;
            Squares[from.Row, from.Column] = null;
            piece.UpdatePosition(to);
        }
    }
}