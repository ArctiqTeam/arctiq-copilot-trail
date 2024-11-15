namespace ChessWeb.Models;
public class Board
{
    private const string INITIAL_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    public Piece?[,] Squares { get; private set; } = new Piece[8,8];
    
    public void SetPositionFromFen(string fen = INITIAL_FEN)
    {
        if (string.IsNullOrWhiteSpace(fen))
            fen = INITIAL_FEN;
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

    public bool MovePiece(Position from, Position to)
    {
        var piece = Squares[from.Row, from.Column];
        if (piece == null || !piece.IsValidMove(to, this))
            return false;
            
        Squares[to.Row, to.Column] = piece;
        Squares[from.Row, from.Column] = null;
        piece.Position = to;
        return true;
    }
}