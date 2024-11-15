using ChessWeb.Models; // Adjust this namespace based on where your Board and PieceColor types are defined
public class GameState
{
    public Board Board { get; set; } = new();
    public PieceColor CurrentTurn { get; set; } = PieceColor.White;
    public bool IsGameOver { get; set; }
    
    public GameState(string fen = null)
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
}