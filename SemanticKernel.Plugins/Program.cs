using System;
using Microsoft.SemanticKernel;

namespace SemanticKernel.Plugins
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new kernel and register the Ollama connector
            var kernel = Kernel
                            .CreateBuilder()
                            .AddOpenAIChatCompletion()
                                
                .Build();  
            

            // Import the FileIO plugin
            var fileIoSkill = new FileIOSkill();
            kernel.ImportSkill(fileIoSkill, "FileIO");

            // Start the kernel
            kernel.Run();
        }
    }
}


