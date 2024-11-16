namespace ChessWeb.Models;
public class Pawn : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'P' : 'p';
    
    public Pawn(PieceColor color, Position position): base(color, PieceType.Pawn, position)
    {
    }
    
    public override bool IsValidMove(Position newPosition, Board board)
    {
        int direction = Color == PieceColor.White ? -1 : 1;
        int startRow = Color == PieceColor.White ? 6 : 1;
        
        // Basic forward move
        if (newPosition.Column == Position.Column && 
            newPosition.Row == Position.Row + direction &&
            board.Squares[newPosition.Row, newPosition.Column] == null)
            return true;
            
        // First move - can move two squares
        if (Position.Row == startRow &&
            newPosition.Column == Position.Column &&
            newPosition.Row == Position.Row + (2 * direction) &&
            board.Squares[Position.Row + direction, Position.Column] == null &&
            board.Squares[newPosition.Row, newPosition.Column] == null)
            return true;
            
        // Capture diagonally
        if (Math.Abs(newPosition.Column - Position.Column) == 1 &&
            newPosition.Row == Position.Row + direction)
        {
            var targetPiece = board.Squares[newPosition.Row, newPosition.Column];
            return targetPiece != null && targetPiece.Color != Color;
        }
        
        return false;
    }
}