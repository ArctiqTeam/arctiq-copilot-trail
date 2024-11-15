using Microsoft.AspNetCore.Mvc;
using ChessWeb.Models;
public class GameController : Controller
{
    private readonly GameState _gameState;
    
    public GameController()
    {
        _gameState = new GameState(); // Uses default FEN
    }
    
    //Alternative constructor for custom positions
    public GameController(string fen)
    {
        _gameState = new GameState(fen);
    }

    public IActionResult Index()
    {
        return View(_gameState);
    }
    
    [HttpPost]
    public IActionResult Move(int fromRow, int fromCol, int toRow, int toCol)
    {
        var from = new Position(fromRow, fromCol);
        var to = new Position(toRow, toCol);
        
        if (_gameState.Board.MovePiece(from, to))
        {
            _gameState.SwitchTurn();
        }
        
        return RedirectToAction(nameof(Index));
    }
}