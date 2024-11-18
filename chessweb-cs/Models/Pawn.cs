namespace ChessWeb.Models;
public class Pawn : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'P' : 'p';
    
    public Pawn(PieceColor color, Position position): base(color, PieceType.Pawn, position)
    {
    }
    
    public override bool IsValidMove(Position from, Position to, Board board)
    {
        int direction = Color == PieceColor.White ? -1 : 1;
        int startRow = Color == PieceColor.White ? 6 : 1;
        
        // Basic forward move
        if (to.Column == from.Column && 
            to.Row == from.Row + direction &&
            board.Squares[to.Row, to.Column] == null)
            return true;
            
        // First move - can move two squares
        if (from.Row == startRow &&
            to.Column == from.Column &&
            to.Row == from.Row + (2 * direction) &&
            board.Squares[from.Row + direction, from.Column] == null &&
            board.Squares[to.Row, to.Column] == null)
            return true;
            
        // Capture diagonally
        if (Math.Abs(to.Column - from.Column) == 1 &&
            to.Row == from.Row + direction)
        {
            var targetPiece = board.Squares[to.Row, to.Column];
            return targetPiece != null && targetPiece.Color != Color;
        }
        
        return false;
    }
}