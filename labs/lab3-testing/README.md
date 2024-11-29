# Lab 3: Testing with Copilot [WIP]

## Prerequisites

## Estimated time

- 60 minutes

## Objectives

Learn how to use Github Copilot to create unit tests and how to use it for test-driven development (TDD).

* The different ways to create tests using Copilot
* Increase the code coverage
* Using the terminal context to fix errors
* Mocking a class
* Using Copilot for TDD

# Reminder: This is a WIP

## Part 1 - Creating unit tests

### 1.1

* Show some bad prompts
* Show better ones
* Show how easy it is to create test cases
* Show how Copilot can mimick tests you've already written
* Show that you can prompt Copilot again when edge-cases are missing
* Show that there are limitations and it might not catch all the edge-cases

### 1.2

* Show how to do mocking and mock the controller

## Part 2 - TDD

Use the second example project.

* Via TDD, create the code required for the rook to move
* Give the class 10 minues to create the code for the Knight's movement
* People who finish early might write tests for the queen's movement and then write the code for it
* Finish the module by showing Copilot's ability to optimize code in Visual Studio (it's limited to this IDE)

-------------------------------------------------------------

# Part 1

## Part 1.1

When presenting the workshop, ensure youThings to test for pawns: 

* Can the pawn move forward one square (is a valid move)
* Can the pawn move forward two squares from its initial position (is a valid move)
* Can the pawn move forward two squares from a different position (is an invalid move)
* Can the pawn move diagonally to capture another piece (is a valid movie)
* Can the pawn move diagonally even though there's no piece to capture (is an invalid move)
* Can the pawn move diagonally to capture a piece that's the same color (is an invalid move)
* Can the pawn move forward one square if blocked by another piece (is an invalid move)
* Can the pawn move forward two squares if blocked by another piece that's one square ahead, jumping over a piece (is an invalid move)
* Can the pawn move forward two squares if blocked by another piece that's two squares ahead (is an invalid move)

You must create the same tests again for the chess piece of the other color. White and Black must be tested. 

Start with very naive prompts. Copilot will generate only 2 or 4 tests. It will not cover every edge cases. You can ask questions like "What edge cases are missed by our unit tests?". You have to review Copilot's output every single time.

### Testing the out of bound exception

The prompt `/tests  pawn moving out of the board and asserting that an exception has occured` didn't work. It made the pawn move two squares, which is impossibe, so the tests will fail. 

The new prompt `/tests  pawn moving out of the board and asserting that an exception has occured. Ensure the pawn moves only one square away else it's an invalid move and the exception won't be thrown because the move won't be executed. ` will still not work. 

`/tests  pawn moving out of the board and asserting that an exception has occured. Ensure the destination square is only one block away from the source.` was blocked by the Responsible AI Service when using the inline chat. Use the side panel chat instead. 

If you use the same prompt `/tests pawn moving out of the board and asserting that an exception has occured. Ensure the destination square is only one block away from the source.`, it will generate a test with likely the wrong exception. It must be `System.IndexOutOfRangeException`. The @terminal to fix. 

@terminal /explain will suggest a change to the caller code so it throws a `ArgumentOutOfRangeException` to match the unit test instead. That's wrong. Ask Copilot to fix the unit test instead.  

`Please fix the unit test. The unit test has to expect the correct exception to be thrown. `

The use of `ArgumentOutOfRangeException` isn't a bad suggestion by itself. This is what was suggested:
```csharp
public bool IsValidMove(Position from, Position to, Board board)
{
    if (to.Row < 0 || to.Row >= board.Squares.GetLength(0) || to.Column < 0 || to.Column >= board.Squares.GetLength(1))
    {
        throw new ArgumentOutOfRangeException("The move is out of the board's bounds.");
    }

    // Existing logic for validating the move
}
```
It makes the reason of the exception cleared. Is Valid Move should not accept out of range arguments.

In our case, we've chosen to ignore that to avoid this extra check as we didn't care as much about perfect error handling and we wanted to avoid extra code. This shows an interesting bahavior by Copilot where it won't do exactly what you want but will instead suggest changes to improve your code quality.


### Testing the Queen

When asking to test the Queen, the tests will differ depending whether you have the Pawn tests tab open in your editor. Show both examples to the class. You'll get better tests with the Pawn tests tab open. You have to convey to the class that the more and more tests you write, the more and more you get efficient with Copilot as it has examples it's basing itself on.

For the get go, you will be able to have it generate at least 4 test cases, but  you'll see that some might be missing. See if it generates unit tests where it tries to jump over the opponent pieces or it's own color's pieces to capture an opponent piece. 

If you ask copilot `Create a new test case, but we are checking if it moves out of bounds. We have a similar test case for another piece called "IsValidMove_WhenPawnMovesOutOfBoard_ThenThrowsException"`, it will still generate a unit test that uses `ArgumentOutOfRangeException` instead of `IndexOutOfRangeException`. 

Use different models and make sure to include the Pawns' unit tests file. Switch to Claude 3.5 and then to o1-review and use the same prompt. You'll notice that the other models might use `IndexOutOfRangeException`. This helps the class understand that they can laverage the different models to get what they want if some models are inferior. 

With this specific prompt Claude 3.5 generated: 

```csharp
[Fact]
public void IsValidMove_WhenQueenMovesOutOfBoard_ThenThrowsException()
{
    // Arrange
    var queen = new Queen(PieceColor.White, new Position(0, 0));
    var board = new Board();
    board.Squares[0, 0] = queen;

    // Act & Assert - Queen tries to move out of the board
    Assert.Throws<IndexOutOfRangeException>(() => queen.IsValidMove(queen.Position, new Position(-1, -1), board));
}
```

while o1-preview generated:
```csharp
[Fact]
public void IsValidMove_WhenBlackPawnMovesOutOfBoard_ThenThrowsException()
{
    // Arrange
    var blackPawn = new Pawn(PieceColor.Black, new Position(7, 0)); // At the bottom edge
    var board = new Board();
    board.Squares[7, 0] = blackPawn;

    // Act & Assert - Black pawn tries to move forward off the board
    Assert.Throws<IndexOutOfRangeException>(() => blackPawn.IsValidMove(blackPawn.Position, new Position(8, 0), board));
}
```

Claude 3.5 has the best output as it gets the exception right and it tests the right piece. The position it tries to move the piece to is a valid moving pattern, but it out of the chess board.

You might notice that some test cases are missing. You can ask Copilot if some test cases were generated. For example: `Is there a test for checking if I can capture a chess piece of the same color as my chess piece?`.

### Testing the Rook

Create a new test file for the Rook. Set the model to GPT4o and insert the Queen's, Pawn's and Knight's test files into the context. Use the prompt `Generate test cases for the rook. Make sure you cover every possible edge case.` in the chat panel. This time, you'll see that it will generate way more unit tests because Copilot is able to understand the task better. 

It's still only testing the White pieces though, and GTP4o still uses the `ArgumentOutOfRangeException` exception which we previously said was wrong, so you'll get one failing test. It's not that of a big issue, Pawns were the only pieces that are limited to move in a single direction.


----------------------------------------

## Part 1.2 : Mocking

"Write Unit tests for the Game Controller in C#. You must mock the Game Controller class and test the "Move" function. You must add the tests to the UniTest1.cs file. Write multiple test methods that cover a wide range of scenarios, including edge cases, exception handling, and data validation."

After install Moq and pasting the code in the Test file, there will be errors for each unit test. Use the terminal context for Copilot to fix them.

@terminal Help me fix those errors. It seems they were caused by the use of non-overridable members in the setup/verification expressions with Moq. 

We made the function "public virtual bool MovePiece" virtual according to advice from Copilot. This didn't fix the issue. 

@terminal This didn't fix the issue. According to context from the terminal, describe the problem and fix the issue. 

@terminal I implemented your suggestions. Why is it still failing?

We're stuck in a loop. Copilot is trying to get us to fix the same thing over and over again. Switch to Claude for better results.

```
((f, t, out string m)  shows the following error: 

Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit

"string" is underlined by intellicode
```

Can you rewrite the unit test and provide a FEN string as the parameter?

o1-preview should be better at creating the mocks.

Insert the line `Assert.NotNull(result.Value); ` to shut up warnings.

**Calculate the code coverage:**

"How can I know the code coverage of tests?". o1-preview: 

run `dotnet add package coverlet.collector`. 

Then run `dotnet test --collect:"XPlat Code Coverage"`.

`dotnet tool install -g dotnet-reportgenerator-globaltool`

`reportgenerator -reports:"./**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html` (it's in ~/.dotnet/tools/)

# Part 2

## Part 2.1 - TDD for the knight

Students will write the code for the Knight's movement. Testing code already exist, developers will use promptint to create the code from knowledge they got from the previous Copilot workshop sessions. This is a great time for students to ask questions.

We will use the second project folder which has the Knight code's removed. It also lacks the movement code for the Queen. 

Students have 15 minutes to implement the Knight's code. If they are finished before the 15 mintues went by, they can implement the Queen's code using the TDD method again. 

The solution will be presented for the Knight's code after 15 minutes. We'll also show the solution for the Queen's code if there's enough time left. The Queen's code is much more complicated, as you have to check for chess pieces that are in the way. Fortunately, there's already code for other chess pieces that looks alike and can be used to give Copilot some more context.

Knights move in an L shape and don't capture pieces on their way. They only capture the chess pieces on their landing square. Queens can move in every direction.

For a graphical representation of their movement, see: 
* https://kidschessworld.com/knight/
* https://kidschessworld.com/queen/





------------------------------------------

### Copilot tip 1


Prompt: "Write integration tests for the deposit function in the BankAccount class. Use mocks to simulate the NotificationSystem and verify that it is called correctly after a deposit."

This prompt does a few important things:

* Scope: It specifies integration tests, focusing on the interaction between the deposit function and the NotificationSystem, rather than just unit tests.
* Mocks: It explicitly asks for the use of mocks to simulate the NotificationSystem, ensuring that the interaction with external systems is tested without relying on their actual implementation.
* Verification: The prompt emphasizes verifying that the NotificationSystem is called correctly after a deposit, ensuring that the integration between the components works as expected.
* Specificity: The prompt clearly states the method (deposit) and the class (BankAccount) to be tested.

**Tip:** If Copilot is producing invalid tests, provide examples of inputs and outputs for the function you want to test. This will help Copilot evaluate the expected behavior of the function.

Prompt: "What additional tests should be included to ensure full coverage for the integration between the BankAccount class and the NotificationSystem?"

Prompting Copilot with this question can help you identify missing test cases that may have been overlooked. In this situation, while we tested valid and invalid deposits, we haven't yet covered the withdrawal functionality.




### Copilot tip 2

Prompt: Develop a comprehensive suite of unit tests for the BankAccount() function in Python. Write multiple test methods that cover a wide range of scenarios, including edge cases, exception handling, and data validation.

This prompt does a few important things:

* Scope: It explicitly asks for a comprehensive suite of unit tests, so that Copilot generates more than just basic tests, covering a variety of potential use cases.
* Scenarios: The prompt specifies that the tests should include:
* Edge cases: Tests for boundary conditions (e.g., negative values for deposits or withdrawals, withdrawing more than the available balance).
* Exception handling: Tests for errors that should be raised when certain invalid actions occur (e.g., withdrawing more than the available balance, depositing negative amounts).
* Data validation: Ensures that inputs are correctly validated and handled by the methods.

