using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AHSW.EventLog.Models.Entities.Abstract;

public abstract class BaseDescriptiveEntity<T>
    where T : struct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public T EnumId { get; set; }
    
    public string Description { get; set; }
}