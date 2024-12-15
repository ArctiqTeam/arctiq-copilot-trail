namespace ChessWeb.Models;
public class Queen : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'Q' : 'q';
    
    public Queen(PieceColor color, Position position): base(color, PieceType.Queen, position)
    {
    }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        // TODO: Implement using TDD
        return true;
    }
}