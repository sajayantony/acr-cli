using System.CommandLine;
using System.CommandLine.Invocation;
using AzureContainerRegistry.CLI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace AzureContainerRegistry.CLI.Commands
{
    class PullCommand : Command
    {
        public PullCommand() : base("pull", "Pull an artifact")
        {
            this.AddArgument(new Argument<string>("reference"));
            this.Add(new Option<string>(
                    aliases: new string[] { "--output", "-o" },
                    getDefaultValue: () => System.IO.Directory.GetCurrentDirectory(),
                    description: "Output Directory to download contents"));

            this.Handler = CommandHandler.Create<string, string, IHost>(async (reference, output, host) =>
           {
               var contentStore = host.Services.GetRequiredService<ContentStore>();
               await contentStore.PullAsync(reference.ToArtifactReference(), output);
           });
        }
    }
}
