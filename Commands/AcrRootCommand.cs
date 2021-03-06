using System;
using System.CommandLine;
using System.CommandLine.Builder;
using AzureContainerRegistry.CLI.Commands;

namespace AzureContainerRegistry.CLI.Commands
{
    class AcrRootCommand : RootCommand
    {
        static readonly string DEFAULT_PASSWORD = new String('*', 5);

        public AcrRootCommand()
        {

            this.Description = "acr operates against an OCI conformant registry";

            this.AddGlobalOption(
                new Option<string>(
                    "--registry",
                    getDefaultValue: () => Environment.GetEnvironmentVariable("REGISTRY_LOGIN") ?? String.Empty,
                    "Registry Login Server")
            );

            this.AddGlobalOption(
                 new Option<string>(
                       "--username",
                       getDefaultValue: () => Environment.GetEnvironmentVariable("REGISTRY_USERNAME") ?? String.Empty,
                       "Registry Username")
            );

            this.AddGlobalOption(
                 new Option<string>(
                       "--password",
                       getDefaultValue: () => !String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("REGISTRY_PASSWORD")) ? DEFAULT_PASSWORD : string.Empty,
                       "Registry Login Server")
            );

            this.AddGlobalOption(
                 new Option<bool>(
                       "--verbose",
                       getDefaultValue: () => false,
                       "Enable verbose logging")
            );

            this.AddCommand(new RepositoryCommand());
            this.AddCommand(new TagCommand());
            this.AddCommand(new PullCommand());
            this.AddCommand(new PushCommand());
            this.AddCommand(new ManifestCommand());
            this.AddCommand(new LayerCommand());

        }
    }
}