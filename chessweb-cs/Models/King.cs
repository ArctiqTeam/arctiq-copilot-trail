namespace ChessWeb.Models;
public class King : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'K' : 'k';
    
    public King(PieceColor color, Position position): base(color, PieceType.King, position)
    {
    }

    public override bool IsValidMove(Position newPosition, Board board)
    {
        int rowDiff = Math.Abs(newPosition.Row - Position.Row);
        int colDiff = Math.Abs(newPosition.Column - Position.Column);
        
        if (rowDiff > 1 || colDiff > 1)
            return false;
            
        var targetPiece = board.Squares[newPosition.Row, newPosition.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}