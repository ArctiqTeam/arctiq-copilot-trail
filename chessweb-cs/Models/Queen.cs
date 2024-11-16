namespace ChessWeb.Models;
public class Queen : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'Q' : 'q';
    
    public Queen(PieceColor color, Position position): base(color, PieceType.Queen, position)
    {
    }

    public override bool IsValidMove(Position newPosition, Board board)
    {
        bool isDiagonal = Math.Abs(newPosition.Row - Position.Row) == 
                         Math.Abs(newPosition.Column - Position.Column);
        bool isStraight = newPosition.Row == Position.Row || 
                         newPosition.Column == Position.Column;
                         
        if (!isDiagonal && !isStraight)
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