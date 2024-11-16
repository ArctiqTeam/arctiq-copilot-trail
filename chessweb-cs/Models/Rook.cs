namespace ChessWeb.Models;
public class Rook : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'R' : 'r';
    
    public Rook(PieceColor color, Position position): base(color, PieceType.Rook, position)
    {
    }
    
    public override bool IsValidMove(Position newPosition, Board board)
    {
        if (newPosition.Row != Position.Row && newPosition.Column != Position.Column)
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