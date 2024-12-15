namespace ChessWeb.Models;
public class Knight : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'N' : 'n';
    
    public Knight(PieceColor color, Position position): base(color, PieceType.Knight, position)
    {
    }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        // TODO: Implement using TDD
        return true;
    }
}