using ChessWeb.Models;

/// <summary>
/// Represents the current state of the game.
/// </summary>
public class GameState
{
    /// <summary>
    /// Gets or sets the board state.
    /// </summary>
    public Board Board { get; set; } = new();

    /// <summary>
    /// Gets or sets the current turn.
    /// </summary>
    public PieceColor CurrentTurn { get; private set; } = PieceColor.White;

    /// <summary>
    /// Gets or sets a value indicating whether the game is over.
    /// </summary>
    public bool IsGameOver { get; set; }

    /// <summary>
    /// Gets or sets the last square a move was made from in the game.
    /// </summary>
    public Position? LastMoveFrom { get; private set; }

    /// <summary>
    /// Gets or sets the last squarae a move was made to in the game.
    /// </summary>
    public Position? LastMoveTo { get; private set; }

    /// <summary>
    /// Gets or sets the FEN representation of the board.
    /// </summary>
    public string? FEN { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="GameState"/> class.
    /// </summary>
    public GameState(string fen)
    {
        Board = new Board();
        Board.SetPositionFromFen(fen);
        CurrentTurn = PieceColor.White;
        IsGameOver = false;
    }

    /// <summary>
    /// Switches the turn to the other player.
    /// </summary>
    public void SwitchTurn()
    {
        CurrentTurn = CurrentTurn == PieceColor.White ? 
            PieceColor.Black : PieceColor.White;
    }

    /// <summary>
    /// Attempts to move a piece from one position to another.
    /// </summary>
    /// <param name="from">The starting position of the piece.</param>
    /// <param name="to">The destination position of the piece.</param>
    /// <param name="message">An output message indicating the result of the move.</param>
    /// <returns>True if the move is successful; otherwise, false.</returns>
    public bool MovePiece(Position from, Position to, out string message)
    {
        var piece = Board.Squares[from.Row, from.Column];
        if (piece == null)
        {
            message = "No piece at selected position";
            return false;
        }

        // Validate piece exists and belongs to current player
        if (piece.Color != CurrentTurn)
        {
            message = "It's not your turn";
            return false;
        }

        bool isValidMove = piece.IsValidMove(from, to, Board);
        //bool isValidMove = true;

        if (isValidMove)
        {
            Board.Squares[to.Row, to.Column] = piece;
            Board.Squares[from.Row, from.Column] = null;

            CurrentTurn = CurrentTurn == PieceColor.White ? 
                PieceColor.Black : PieceColor.White;
            message = "Move successful";
            return true;
        }
        message = "Invalid move for this piece";
        return false;
    }

    /// <summary>
    /// Updates the last move made in the game.
    /// </summary>
    public void UpdateLastMove(Position from, Position to)
    {
        LastMoveFrom = from;
        LastMoveTo = to;
    }

    /// <summary>
    /// Resets the game to its initial state.
    /// </summary>
    public void Reset()
    {
        Board.SetPositionFromFen("start");
        CurrentTurn = PieceColor.White; // Reset to white's turn
        LastMoveFrom = null; // Clear last move
        LastMoveTo = null;
        IsGameOver = false;
    }
}