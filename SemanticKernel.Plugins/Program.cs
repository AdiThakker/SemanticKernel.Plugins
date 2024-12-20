using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI.Chat;

var builder = Kernel
                .CreateBuilder()
                .AddOllamaChatCompletion("phi3", new Uri("http://localhost:11434"));

var kernel = builder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

string promptTemplate = await File.ReadAllTextAsync("Plugins/prompts/prompts.txt");
string instructions = await File.ReadAllTextAsync("Plugins/prompts/instructions.txt");
string csvData = await File.ReadAllTextAsync("Plugins/prompts/data.csv");


string formattedPrompt = promptTemplate
    .Replace("{{instructions}}", instructions)
    .Replace("{{csvData}}", csvData);


ChatHistory history = new()
{
    new Microsoft.SemanticKernel.ChatMessageContent(new AuthorRole("User"), formattedPrompt)
};


var response = await chatCompletionService.GetChatMessageContentAsync(
    history,
    kernel: kernel
);

Console.WriteLine("Generated SQL:");
Console.WriteLine(response);