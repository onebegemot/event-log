using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AHWS.EventLog.Models.Entities.PropertyLogEntries;

public abstract class BaseDescriptiveEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int EnumId { get; set; }
    
    public string Description { get; set; }
}