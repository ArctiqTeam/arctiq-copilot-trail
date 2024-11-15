namespace ChessWeb.Models;
public abstract class Piece
{
    public PieceColor Color { get; set; }
    public Position Position { get; set; }
    public abstract bool IsValidMove(Position newPosition, Board board);
    public abstract char Symbol { get; }
}

public enum PieceColor
{
    White,
    Black
}

public record Position(int Row, int Column);