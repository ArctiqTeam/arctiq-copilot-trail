using ChessWeb.Models; // Adjust this namespace based on where your Board and PieceColor types are defined
public class GameState
{
    public Board Board { get; set; } = new();
    public PieceColor CurrentTurn { get; private set; } = PieceColor.White;
    public bool IsGameOver { get; set; }
    public Position? LastMoveFrom { get; private set; }
    public Position? LastMoveTo { get; private set; }
    public string? FEN { get; set; }
    
    public GameState(string fen)
    {
        Board = new Board();
        Board.SetPositionFromFen(fen);
        this.FEN = Board.GenerateFEN();
        CurrentTurn = PieceColor.White;
        IsGameOver = false;
    }

    public void SwitchTurn()
    {
        CurrentTurn = CurrentTurn == PieceColor.White ? 
            PieceColor.Black : PieceColor.White;
    }

    public bool MovePiece(Position from, Position to, out string message)
    {
        var piece = Board.Squares[from.Row, from.Column];
        if (piece == null)
        {
            message = "No piece at selected position";
            return false;
        }

        // Validate piece exists and belongs to current player
        if (piece.Color != CurrentTurn)
        {
            message = "It's not your turn";
            return false;
        }

        bool isValidMove = piece.IsValidMove(from, to, Board);

        if (isValidMove)
        {
            Board.Squares[to.Row, to.Column] = piece;
            Board.Squares[from.Row, from.Column] = null;

            CurrentTurn = CurrentTurn == PieceColor.White ? 
                PieceColor.Black : PieceColor.White;
            message = "Move successful";
            this.FEN = Board.GenerateFEN();
            return true;
        }
        message = "Invalid move for this piece";
        return false;
    }

    public void UpdateLastMove(Position from, Position to)
    {
        LastMoveFrom = from;
        LastMoveTo = to;
    }

    public void Reset()
    {
        Board.SetPositionFromFen("start");
        this.FEN = Board.GenerateFEN();
        CurrentTurn = PieceColor.White; // Reset to white's turn
        LastMoveFrom = null; // Clear last move
        LastMoveTo = null;
        IsGameOver = false;
    }
}