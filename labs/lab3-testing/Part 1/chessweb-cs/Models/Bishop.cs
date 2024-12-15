namespace ChessWeb.Models;
public class Bishop : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'B' : 'b';
    
    public Bishop(PieceColor color, Position position): base(color, PieceType.Bishop, position)
    {
    }        
    public override bool IsValidMove(Position from, Position to, Board board)
    {
        int rowDiff = Math.Abs(to.Row - from.Row);
        int colDiff = Math.Abs(to.Column - from.Column);
        
        if (rowDiff != colDiff)
            return false;
            
        int rowDirection = Math.Sign(to.Row - from.Row);
        int colDirection = Math.Sign(to.Column - from.Column);
        
        int currentRow = from.Row + rowDirection;
        int currentCol = from.Column + colDirection;
        
        while (currentRow != to.Row || currentCol != to.Column)
        {
            if (board.Squares[currentRow, currentCol] != null)
                return false;
                
            currentRow += rowDirection;
            currentCol += colDirection;
        }
        
        var targetPiece = board.Squares[to.Row, to.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}