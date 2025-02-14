using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public abstract class BaseDescriptiveEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int EnumId { get; set; }
    
    public string Description { get; set; }
}