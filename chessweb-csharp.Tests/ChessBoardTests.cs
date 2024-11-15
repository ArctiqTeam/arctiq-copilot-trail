// ChessBoardTests.cs
using Microsoft.Playwright;
using Xunit;

public class ChessBoardTests : IAsyncLifetime
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new()
        {
            Headless = true
        });
        _page = await _browser.NewPageAsync();
        
        // Make sure the app is running and wait for it to be ready
        await _page.GotoAsync("http://localhost:8080");
        // Wait for the chess board to be rendered
        await _page.WaitForSelectorAsync(".chess-piece");
    }

    public async Task DisposeAsync()
    {
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }

    [Fact]
    public async Task ChessPiece_ShouldHaveCorrectDimensions()
    {
        // Wait for the first chess piece to be fully rendered
        var piece = await _page.WaitForSelectorAsync(".chess-piece");
        Assert.NotNull(piece);
        
        var box = await piece!.BoundingBoxAsync();
        Assert.NotNull(box);
        
        // Use approximately equal for floating point comparisons
        Assert.True(Math.Abs(26.01 - box.Width) < 0.1);
        Assert.Equal(50, box.Height);
    }

    [Fact]
    public async Task ChessPiece_ShouldHaveGrabCursor()
    {
        var piece = await _page.WaitForSelectorAsync(".chess-piece");
        Assert.NotNull(piece);
        
        var cursor = await piece.EvaluateAsync<string>("el => window.getComputedStyle(el).cursor");
        Assert.Contains("grab", cursor.ToLower());
    }
}