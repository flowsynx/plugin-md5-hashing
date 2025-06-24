using FlowSynx.PluginCore;
using FlowSynx.PluginCore.Extensions;
using FlowSynx.PluginCore.Helpers;
using FlowSynx.Plugins.Md5Hashing.Models;
using System.Security.Cryptography;
using System.Text;

namespace FlowSynx.Plugins.Md5Hashing;

public class Md5HashingPlugin : IPlugin
{
    private IPluginLogger? _logger;
    private bool _isInitialized;

    public PluginMetadata Metadata
    {
        get
        {
            return new PluginMetadata
            {
                Id = Guid.Parse("b5b25ec7-63e9-4f63-8d51-e5a299449321"),
                Name = "Hahing.Md5",
                CompanyName = "FlowSynx",
                Description = Resources.PluginDescription,
                Version = new PluginVersion(1, 0, 0),
                Category = PluginCategory.Security,
                Authors = new List<string> { "FlowSynx" },
                Copyright = "© FlowSynx. All rights reserved.",
                Icon = "flowsynx.png",
                ReadMe = "README.md",
                RepositoryUrl = "https://github.com/flowsynx/plugin-md5-hashing",
                ProjectUrl = "https://flowsynx.io",
                Tags = new List<string>() { "flowSynx", "hashing", "md5", "security" }
            };
        }
    }

    public PluginSpecifications? Specifications { get; set; }

    public Type SpecificationsType => typeof(Md5HashingPluginSpecifications);

    public IReadOnlyCollection<string> SupportedOperations => new List<string>();

    public Task Initialize(IPluginLogger logger)
    {
        if (ReflectionHelper.IsCalledViaReflection())
            throw new InvalidOperationException(Resources.ReflectionBasedAccessIsNotAllowed);

        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
        _isInitialized = true;
        return Task.CompletedTask;
    }

    public Task<object?> ExecuteAsync(PluginParameters parameters, CancellationToken cancellationToken)
    {
        if (ReflectionHelper.IsCalledViaReflection())
            throw new InvalidOperationException(Resources.ReflectionBasedAccessIsNotAllowed);

        if (!_isInitialized)
            throw new InvalidOperationException($"Plugin '{Metadata.Name}' v{Metadata.Version} is not initialized.");

        return Task.FromResult<object?>(ComputeHash(parameters));
    }

    private string ComputeHash(PluginParameters parameters)
    {
        var inputParameter = parameters.ToObject<InputParameter>();

        byte[] inputBytes;

        if (!string.IsNullOrEmpty(inputParameter.InputText))
        {
            inputBytes = Encoding.UTF8.GetBytes(inputParameter.InputText);
        }
        else if (inputParameter.InputBytes is { Length: > 0 })
        {
            inputBytes = inputParameter.InputBytes;
        }
        else
        {
            throw new ArgumentException("No valid input provided for MD5 hashing.");
        }

        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(inputBytes);
        var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

        _logger?.LogInfo($"MD5 hash computed: {hash}");

        return hash;
    }
}