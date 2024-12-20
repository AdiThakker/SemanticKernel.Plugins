using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI.Chat;

var builder = Kernel
                .CreateBuilder()
                .AddOllamaChatCompletion("phi3", new Uri("http://localhost:11434"));

var kernel = builder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Step 3: Load Files
string promptTemplate = await File.ReadAllTextAsync("prompts.txt");
string instructions = await File.ReadAllTextAsync("instructions.txt");
string csvData = await File.ReadAllTextAsync("data.csv");


string formattedPrompt = promptTemplate
    .Replace("{{instructions}}", instructions)
    .Replace("{{csvData}}", csvData);

// Step 5: Add the Prompt to Chat History
ChatHistory history = [];

// Step 6: Get AI-Generated Response
var response = await chatCompletionService.GetChatMessageContentAsync(
    history,
    kernel: kernel
);

// Step 7: Output the Response
Console.WriteLine("Generated SQL:");
Console.WriteLine(response);