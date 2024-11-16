namespace ChessWeb.Models;
public class Bishop : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'B' : 'b';
    
    public Bishop(PieceColor color, Position position): base(color, PieceType.Bishop, position)
    {
    }        
    public override bool IsValidMove(Position newPosition, Board board)
    {
        int rowDiff = Math.Abs(newPosition.Row - Position.Row);
        int colDiff = Math.Abs(newPosition.Column - Position.Column);
        
        if (rowDiff != colDiff)
            return false;
            
        int rowDirection = Math.Sign(newPosition.Row - Position.Row);
        int colDirection = Math.Sign(newPosition.Column - Position.Column);
        
        int currentRow = Position.Row + rowDirection;
        int currentCol = Position.Column + colDirection;
        
        while (currentRow != newPosition.Row || currentCol != newPosition.Column)
        {
            if (board.Squares[currentRow, currentCol] != null)
                return false;
                
            currentRow += rowDirection;
            currentCol += colDirection;
        }
        
        var targetPiece = board.Squares[newPosition.Row, newPosition.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}