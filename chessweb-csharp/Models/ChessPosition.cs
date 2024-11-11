namespace ChessWeb.Models
{
    public class ChessPosition
    {
        public string Fen { get; set; }

        public ChessPosition(string fen)
        {
            Fen = fen;
        }

        public string GetFEN()
        {
            return Fen;
        }

        // Add methods to parse and manipulate FEN notation
    }
}