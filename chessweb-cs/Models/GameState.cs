using ChessWeb.Models; // Adjust this namespace based on where your Board and PieceColor types are defined
public class GameState
{
    public Board Board { get; set; } = new();
    public PieceColor CurrentTurn { get; set; } = PieceColor.White;
    public bool IsGameOver { get; set; }
    
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

        // In GameState.cs
    public bool MovePiece(Position from, Position to)
    {
        var piece = Board.Squares[from.Row, from.Column];
        if (piece == null) return false;
        
        Board.Squares[to.Row, to.Column] = piece;
        Board.Squares[from.Row, from.Column] = null;
        return true;
    }

        public void Reset()
    {
        // Reinitialize the board with starting position
        Board.SetPositionFromFen("start");
    }
}