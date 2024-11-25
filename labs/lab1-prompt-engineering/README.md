# Lab 1: Prompt Engineering

## Prerequisites

## Estimated time

- 45 minutes

## Objectives

Learn how to use common prompt engineering techniques using GittHub Copilot

- Zero and Few Shot Prompting
- Chain of Thought Method of Prompting
- Generated Knowledge Prompting 

## Step 1 - Zero and Few Shot Prompting

- Large language models (LLMs) are tuned to follow instructions and are trained on large amounts of data. Large-scale training makes these models capable of performing some tasks in a "zero-shot" manner. 

 - Zero-shot prompting means that the prompt used to interact with the model won't contain examples or demonstrations. The zero-shot prompt directly instructs the model to perform a task without any additional examples to steer it.

- Let's try the following zero-shot prompt to generate a Blazor Server application

    ```
    Create a Blazor Server application that displays ten greatest hits from the 80s on the home page. Provide detailed code and commands
    ```

- Note that the prompt is short and does not contain any examples. The following is part of an example output:

    ```sh
    dotnet new blazorserver -o GreatestHitsApp
    cd GreatestHitsApp
    ```

    ```c#
    namespace GreatestHitsApp.Models
    {
        public class Song
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            public int Year { get; set; }
        }
    }
    ```

- Your output might be slightly different

- While large-language models demonstrate remarkable zero-shot capabilities, they still fall short on more complex tasks when using the zero-shot setting. 

- Few-shot prompting can be used as a technique to enable in-context learning where we provide demonstrations in the prompt to steer the model to better performance. The demonstrations serve as conditioning for subsequent examples where we would like the model to generate a response.

- Let's try the following few-shot prompt to generate a Blazor Server application

     ```
    Create a Blazor Server application that displays ten songs on the home page. Provide detailed code and commands

    Examples:
    “Purple Rain” by Prince (1984)
    “Thriller” by Michael Jackson (1982)
    “Careless Whisper” by Wham! (1984)
    ```

- Note we provide some greatest hits from the 80s as examples

- The output should be similar to the zero-shot prompt, however Copilot is able to figure out the rest of the list based on the examples provided

- Standard few-shot prompting works well for many tasks containing simple repeated patterns but is still not a perfect technique, especially when dealing with more complex tasks, such as complex arithmetic, commonsense, and symbolic reasoning. For these tasks, we need to use more advanced prompting techniques, such as chain-of-thought prompting in the next section.

## Step 2 - Chain of Thought Method of Prompting

TODO

## Step 3 - Generated Knowledge Prompting

TODO

## References

- Prompting Techniques https://www.promptingguide.ai/techniques