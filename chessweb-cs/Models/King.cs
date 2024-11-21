namespace ChessWeb.Models;
public class King : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'K' : 'k';
    
    public King(PieceColor color, Position position): base(color, PieceType.King, position)
    {
    }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        int rowDiff = Math.Abs(to.Row - from.Row);
        int colDiff = Math.Abs(to.Column - from.Column);
        
        if (rowDiff > 1 || colDiff > 1)
            return false;
            
        var targetPiece = board.Squares[to.Row, to.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}