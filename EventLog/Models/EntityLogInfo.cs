using EventLog.Interfaces.Entities;

namespace EventLog.Models;

public record EntityLogInfo<TEntity, TPropertyType>(
    IEnumerable<TEntity> Entities,
    params TPropertyType[] Properties)
        where TEntity : IPkEntity
        where TPropertyType : struct, Enum;
