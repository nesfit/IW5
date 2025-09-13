using System.ComponentModel.DataAnnotations;

namespace ConfigurationSample;

public class ServerNameConfiguration
{
    [Required]
    public string Name { get; set; }
}