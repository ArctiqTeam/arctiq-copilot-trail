using Moq; // Mocking library
using Xunit; // Testing framework
using Microsoft.AspNetCore.Mvc; // For Controller and JsonResult
using ChessWeb.Models; // Chess game models
using Newtonsoft.Json.Linq; // For handling JSON objects

public class MockTestingMoveFunction
{
    [Fact]
    public void Move_WhenMoveIsSuccessful_ReturnsSuccessJson()
    {
        // Arrange: Set up the necessary objects and data
        var mockGameState = new Mock<GameState>("empty"); // Mock GameState with an "empty" parameter
        string moveMessage = "Move successful"; // Expected message
        mockGameState
            // Set up the mock to return true and output the moveMessage when MovePiece is called with any positions
            .Setup(gs => gs.MovePiece(It.IsAny<Position>(), It.IsAny<Position>(), out moveMessage))
            .Returns(true);
        var controller = new GameController(mockGameState.Object); // Create controller with mocked GameState

        // Act: Invoke the method under test
        var result = controller.Move(1, 1, 2, 2) as JsonResult; // Call Move method with sample positions

        // Assert: Verify the expected outcome
        Assert.NotNull(result); // Check that result is not null
        Assert.NotNull(result.Value); // Check that result.Value is not null
        var json = JObject.FromObject(result.Value); // Convert result value to JSON object
        Assert.True((bool)json["success"]); // Assert that "success" property is true
    }

    [Fact]
    public void Move_WhenMoveFails_ReturnsFailureJson()
    {
        // Arrange: Set up the necessary objects and data
        var mockGameState = new Mock<GameState>("empty"); // Mock GameState with an "empty" parameter
        string moveMessage = "Invalid move"; // Expected failure message
        mockGameState
            // Set up the mock to return false and output the moveMessage when MovePiece is called with any positions
            .Setup(gs => gs.MovePiece(It.IsAny<Position>(), It.IsAny<Position>(), out moveMessage))
            .Returns(false);
        var controller = new GameController(mockGameState.Object); // Create controller with mocked GameState

        // Act: Invoke the method under test
        var result = controller.Move(1, 1, 2, 2) as JsonResult; // Call Move method with sample positions

        // Assert: Verify the expected outcome
        Assert.NotNull(result); // Check that result is not null
        Assert.NotNull(result.Value); // Check that result.Value is not null
        var json = JObject.FromObject(result.Value); // Convert result value to JSON object
        Assert.False((bool)json["success"]); // Assert that "success" property is false
        Assert.Equal("Invalid move", (string)json["message"]); // Assert that "message" matches expected text
    }
}