# Lab 3: Testing with Copilot [WIP]

## Prerequisites

To participate in this lab you will need to have one of the following IDEs setup and ready for use:

- [Microsoft VSCode](https://code.visualstudio.com/)
- [JetBrains Rider](https://www.jetbrains.com/rider/)
- [Microsoft Visual Studio](https://visualstudio.microsoft.com/)

Additionally, your chosen IDE should have the necessary Copilot extension installed and authenticated to work with your GitHub account.

- [GitHub: How to set up Copilot in various IDEs](https://docs.github.com/en/copilot/managing-copilot/configure-personal-settings/configuring-github-copilot-in-your-environment)

### Runtime Environment

The primary example in this repository for this lab is written in C#. As such, you will need to install and be able to run the ASP.NET 9.0 Framework.

- [ASP.NET 9.0 Download](https://dotnet.microsoft.com/en-us/download)


### Lab Source Code

There are two different projects that are going to be used for this lab. Each will be given to you in a zip file.

## Estimated time

- 45 minutes

## Objectives

We will learn how to use Github Copilot to create unit tests and how to use it for test-driven development (TDD). In this lab, we will be using `xUnit.net` which is currently the most popular unit test framework for C#.

We will explore:

* The different ways to create tests using Copilot
* How to increase the code coverage
* Using the terminal context to fix errors
* Mocking a class
* Using Copilot for TDD

## Reminder

This module uses two different projects. Use the one that's appropriate for each part of the workshop.

-------------------------------------------------------------

## Part 1 - Creating unit tests

### 1.1

Use the example project named "Part 1" for this first part. Ensure only this part is visible to your editor. We don't want Copilot to accidentally use "Part 2" as context.

This part shows: 

* The results we would get with bad prompts
* The results we would get with better ones
* How easy it is to create test cases after you already have a few of them
* How Copilot can mimick tests you've already written
* How you can prompt Copilot again when edge-cases are missing
* What are Copilot's limitations and how it might not catch all edge-cases

### 1.2

This part shows:

* How to do the mocking. We will mock the controller which can be a bit tricky.

## Part 2 - TDD

### 2.1

In this part, use the second example project. Make sure "Part 1" is not in view of your editor and that you've closed all previous tabs.

* Via TDD, create the code required for the rook to move.
* Give the class 10 to 15 minues to create the code for the Knight's movement.
* People who finish early may write code for the Queen's movement. Tests are already provided.
* Finish the module by showing Copilot's ability to optimize code in Visual Studio (it's limited to this IDE -- without Visual Studio, we might use slides)

-------------------------------------------------------------

# Part 1

## Part 1.1

In this section, we will be testing the Pawn, Queen and Rook.

- You can ask Copilot to create a test project for you, or you can use "Part 1" to get you started. Copilot will explain how to strcture your project and how to create a reference in your **csproj** file.

- Start by writing tests for the **pawn**. Ensure you generate tests for every cases. Even if you have a 100% code converage, there might still be things that aren't being tested. Look at the code being tested and try to think about how you could break it.

- If you start with very naive prompt, Copilot will only generate 2 or 4 tests. It will not cover every edge cases. You can ask questions like "What edge cases are missed by our unit tests?" to get more of them. You should review Copilot's output every single time.

- Remember, the more precise your questions and directives, the better the results.

- If you're not familiar with chess, your can read about the possible chess moves [here](https://www.chess.com/learn-how-to-play-chess).

- Once you're done with this section, you'll have about 16 tests. When you run out of ideas, reveal the spoiler below to see more possible tests.

<details>
  <summary>Spoiler warning</summary>
  
    Things to test for pawns: 

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
</details>

- Some test cases are harder to generate with Copilot. We'll explore this in the next section.

### Testing the out of bound exception

- Due to the way the application is coded, it's impossible to move a chess piece out of bounds. What will happen if a chess piece is moved out of bounds is not explicitly handled in the code, so Copilot likely find this test case in your previous tesitng.

- This is the hardest test case. Getting the other tests done is trivial as long as you've explained with exactitude what you wanted Copilot to do.

- Try getting Copilot to generate this test case by yourself, here are examples of the prompts you might try and results you'll get: 

  - The prompt `/tests  pawn moving out of the board and asserting that an exception has occured` likely won't work. Copilot might not understand. During out testing, it made the pawn move two squares, which is impossible, so the tests will fail even if the final destination was out of bounds as we wanted.

  - Try a more precise prompt such as `/tests  pawn moving out of the board and asserting that an exception has occured. Ensure the pawn moves only one square away else it's an invalid move and the exception won't be thrown because the move won't be executed.`. You'll likely still get something that doens't work. 

  - Try alternative prompts like `/tests  pawn moving out of the board and asserting that an exception has occured. Ensure the destination square is only one block away from the source.`. If it gets blocked by the Responsible AI Service when using the inline chat, use the side panel chat instead. 

- If you used the above prompt, it should generate a test with likely the wrong exception. The test should expect a `System.IndexOutOfRangeException` exception. Use `@terminal` to provide context for Copilot to fix this. If you use `@terminal /explain`, it might suggest a change to the caller code so it throws a `ArgumentOutOfRangeException` to match the unit test instead. While it might not be a bad suggestion, our goal here is to get it to generate the correct unit test.

Ask Copilot to fix the unit test like this instead: 

`Please fix the unit test. The unit test has to expect the correct exception to be thrown.`

The use of `ArgumentOutOfRangeException` isn't a bad suggestion by itself. This is a suggestion you might get:

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

- Copilot didn't bring us in the direction we wanted, but it tried to have us write better code. The code it suggests makes the exception throwing explicit. This is better error handling. `IsValidMove` should not accept out of range arguments.

- In our case, we've chosen to ignore this to avoid this extra check as we didn't care as much about perfect error handling and we wanted to avoid extra code. This shows an interesting bahavior by Copilot where it won't do exactly what you want but will instead suggest changes to improve your code quality.

### Testing the Queen

- When testing the Queen, the tests will differ depending whether or not you have the Pawn tests tab open in your editor. You should try both scenarios to see the difference. You'll get better tests with the Pawn tests tab open as Copilot will use them as an example, but ensure you test without the context first so Copilot will not find this context in your chat history. One thing that you'll notice is that the more tests you write, the more Copilot knows how to write new tests so you'll get more efficient. It will also match the style of existing tests.

- From the get go, you will be able to have it generate at least 4 test cases, but you'll see that some might be missing. See if it generates unit tests where it tries to jump over the opponent pieces or it's own color's pieces to capture an opponent piece.

- If you ask copilot `Create a new test case, but we are checking if it moves out of bounds. We have a similar test case for another piece called "IsValidMove_WhenPawnMovesOutOfBoard_ThenThrowsException"`, it will still generate a unit test that uses `ArgumentOutOfRangeException` instead of `IndexOutOfRangeException`.

- (VS Code only) Use different models and make sure to include the Pawns' unit tests file. Switch to **Claude 3.5** and then to **o1-preview** and use the same prompt. You'll notice that the other models might use `IndexOutOfRangeException`. As you can see, you can laverage the different models to get what you want as some models have a better performance at accomplishing certain tasks.

**NOTE:** If you are using Ryder or Visual Studio, it's currently impossible to change the model. They haven't been updated yet.

With this specific prompt, Claude 3.5 generates: 

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

While o1-preview generates the following
:
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

- Claude 3.5 has the best output as it gets the exception right and it tests the right piece (the Queen, not the Pawn). The position it tries to move the piece to is a valid moving pattern, but it's out of the chess board as we wanted.

- You might notice that some test cases are missing. You can ask Copilot if some test cases were generated. For example: `Is there a test for checking if I can capture a chess piece of the same color as my chess piece?`

### Testing the Rook

- Create a new test file for the Rook. Set the model to GPT4o and insert the Queen's, Pawn's and Knight's test files into the context. Use the prompt `Generate test cases for the rook. Make sure you cover every possible edge case.` in the chat panel. This time, you'll see that it will generate way more unit tests because Copilot is able to understand the task better.

It may still only be testing the White pieces though, and GTP4o still uses the `ArgumentOutOfRangeException` exception which we previously said was wrong, so you'll get one failing test. It's not that of a big issue that we're not testing Paws on the other side of the chess board, Pawns were the only pieces that are limited to move in a single direction.

----------------------------------------

## Part 1.2 : Mocking

- Use the following prompt: 

  `Write Unit tests for the Game Controller in C#. You must mock the Game Controller class and test the "Move" function. You must add the tests to the UniTest1.cs file. Write multiple test methods that cover a wide range of scenarios, including edge cases, exception handling, and data validation.`

- After installing `Moq` and pasting the code in the Test file, there will be errors for each unit test. Use the terminal context for Copilot to fix them.

- `@terminal` will help you fix those errors. It seems they were caused by the use of non-overridable members in the setup/verification expressions with `Moq`. 

- If you make the function `public virtual bool MovePiece` **virtual** according to advice from Copilot, it won't fix the issue. 

- Now use the prompt `@terminal This didn't fix the issue. According to context from the terminal, describe the problem and fix the issue. `

- Run `dotnet test` again after implementing its suggestion.

- It won't work. Use the prompt `@terminal I implemented your suggestions. Why is it still failing?`

- You'll notice that you're stuck in a loop after doing this a few times. Copilot is trying to get you to fix the same thing over and over again. Try switching to Claude to see if you get better results.

  You might see the following error, or part of it:

    ```bash
    ((f, t, out string m)  shows the following error: 

    Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit

    "string" is underlined by intellisense
    ```

  Use the following prompt to fix the above error: `Can you rewrite the unit test and provide a FEN string as the parameter?`

- o1-preview should be better at creating the mocks.

- There might be a lot of warnings. Insert the line `Assert.NotNull(result.Value);` to the unit tests to remove warnings. There should be just 3 warnings in the end.

**Calculate the code coverage:**

- Ask `How can I know the code coverage of tests?` to o1-preview. You should get the following instructions:

  Run `dotnet add package coverlet.collector`. 

  Then run `dotnet test --collect:"XPlat Code Coverage"`.

  `dotnet tool install -g dotnet-reportgenerator-globaltool`

  `reportgenerator -reports:"./**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html` (it's in ~/.dotnet/tools/)

- You then have a test coverage report.

# Part 2

## Part 2.1 - TDD for the knight

- Write the code for the Knight's movement. Testing code already exist, you'll use prompting to create the code from knowledge you've got from the previous Copilot workshop sessions. This is a great time to test your abilities.

- Use the second project folder which has the Knight code's removed. It also lacks the movement code for the Queen. 

- Take 15 minutes of your time to implement the Knight's code using TDD. If you're finished before the 15 mintues went by, you can implement the Queen's code still using the TDD method. 

- The solution will be presented for the Knight's code after 15 minutes. We'll also show the solution for the Queen's code if there's enough time left. The Queen's code is much more complicated, as you have to check for chess pieces that are in the way. Fortunately, there's already code for other chess pieces that looks alike and can be used to give Copilot some more context.

- Knights move in an L shape and don't capture pieces on their way. They only capture the chess pieces on their landing square. Queens can move in every direction.

  For a graphical representation of their movement, see: 
  * https://kidschessworld.com/knight/
  * https://kidschessworld.com/queen/

- If you're unfamiliar with chess, make sure to have a look at those so that you understand the task.
