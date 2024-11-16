namespace ChessWeb.Models;
public class Knight : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'N' : 'n';
    
    public Knight(PieceColor color, Position position): base(color, PieceType.Knight, position)
    {
    }

    public override bool IsValidMove(Position newPosition, Board board)
    {
        int rowDiff = Math.Abs(newPosition.Row - Position.Row);
        int colDiff = Math.Abs(newPosition.Column - Position.Column);
        
        if (!((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2)))
            return false;
            
        var targetPiece = board.Squares[newPosition.Row, newPosition.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}