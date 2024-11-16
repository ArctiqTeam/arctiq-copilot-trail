using Microsoft.AspNetCore.Mvc;
using ChessWeb.Models;
public class GameController : Controller
{
    private readonly GameState _gameState;

    public GameController(GameState gameState)
    {
        _gameState = gameState;
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
        
        bool moveSuccessful = _gameState.MovePiece(from, to);
        
        if (!moveSuccessful)
        {
            return Json(new { success = false, message = "Invalid move" });
        }
        
        return Json(new { success = true });
    }
    
    [HttpPost]
    public IActionResult Reset()
    {
        _gameState.Reset();  // Assuming you have a Reset method in GameState
        return Json(new { success = true });
    }
}