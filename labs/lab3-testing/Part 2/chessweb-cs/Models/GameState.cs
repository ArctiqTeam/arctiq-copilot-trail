using ChessWeb.Models; // Adjust this namespace based on where your Board and PieceColor types are defined
public class GameState
{
    public Board Board { get; set; } = new();
    public virtual PieceColor CurrentTurn { get; private set; } = PieceColor.White;
    public virtual bool IsGameOver { get; set; }
    public virtual Position? LastMoveFrom { get; private set; }
    public virtual Position? LastMoveTo { get; private set; }
    
    public GameState(string fen)
    {
        Board = new Board();
        Board.SetPositionFromFen(fen);
        CurrentTurn = PieceColor.White;
        IsGameOver = false;
    }

    public virtual void SwitchTurn()
    {
        CurrentTurn = CurrentTurn == PieceColor.White ? 
            PieceColor.Black : PieceColor.White;
    }

        // In GameState.cs
    public virtual bool MovePiece(Position from, Position to, out string message)
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
        //bool isValidMove = true;

        if (isValidMove)
        {
            Board.Squares[to.Row, to.Column] = piece;
            Board.Squares[from.Row, from.Column] = null;

            CurrentTurn = CurrentTurn == PieceColor.White ? 
                PieceColor.Black : PieceColor.White;
            message = "Move successful";
            return true;
        }
        message = "Invalid move for this piece";
        return false;
    }

    public virtual void UpdateLastMove(Position from, Position to)
    {
        LastMoveFrom = from;
        LastMoveTo = to;
    }

    public virtual void Reset()
    {
        Board.SetPositionFromFen("start");
        CurrentTurn = PieceColor.White; // Reset to white's turn
        LastMoveFrom = null; // Clear last move
        LastMoveTo = null;
        IsGameOver = false;
    }
}