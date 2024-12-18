using ChessWeb.Models;

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
        CurrentTurn = PieceColor.White;
        LastMoveFrom = null;
        LastMoveTo = null;
        IsGameOver = false;
    }
}
