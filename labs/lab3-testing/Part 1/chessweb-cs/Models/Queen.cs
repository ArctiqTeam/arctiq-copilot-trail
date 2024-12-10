namespace ChessWeb.Models;
public class Queen : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'Q' : 'q';
    
    public Queen(PieceColor color, Position position): base(color, PieceType.Queen, position)
    {
    }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        bool isDiagonal = Math.Abs(to.Row - from.Row) == 
                         Math.Abs(to.Column - from.Column);
        bool isStraight = to.Row == from.Row || 
                         to.Column == from.Column;
                         
        if (!isDiagonal && !isStraight)
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