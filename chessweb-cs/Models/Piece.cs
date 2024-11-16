namespace ChessWeb.Models;
public abstract class Piece
{
    public PieceColor Color { get; protected set; }
    public Position Position { get; protected set; }
    public PieceType Type { get; protected set; }
    public string ImagePath { get; protected set; }
    public abstract char Symbol { get; }
    public abstract bool IsValidMove(Position newPosition, Board board);

    public Piece(PieceColor color, PieceType type, Position position)
    {
        Color = color;
        Type = type;
        Position = position;
        ImagePath = $"/images/{(color == PieceColor.White ? 'w' : 'b')}{char.ToUpper(Symbol)}.png";
    }

    public void UpdatePosition(Position newPosition)
    {
        Position = newPosition;
    }
}

public enum PieceColor
{
    White,
    Black
}

public enum PieceType
{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
}

public record Position(int Row, int Column);