using System.ComponentModel.DataAnnotations;

namespace garbageDetetionApi.Models;

public class ApiKey
{
    [Key]
    public Guid Id { get; set; }
    public string Type { get; set; }
}