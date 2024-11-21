namespace ChessWeb.Models;
public class Knight : Piece
{
    public override char Symbol => Color == PieceColor.White ? 'N' : 'n';
    
    public Knight(PieceColor color, Position position): base(color, PieceType.Knight, position)
    {
    }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        int rowDiff = Math.Abs(to.Row - from.Row);
        int colDiff = Math.Abs(to.Column - from.Column);
        
        if (!((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2)))
            return false;
            
        var targetPiece = board.Squares[to.Row, to.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}