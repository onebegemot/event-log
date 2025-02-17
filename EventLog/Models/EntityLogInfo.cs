using EventLog.Interfaces;
using EventLog.Models.Enums;

namespace EventLog.Models;

public record EntityLogInfo<TEntity, TPropertyType>(
    IEnumerable<TEntity> Entities,
    params TPropertyType[] Properties)
        where TEntity : IPkEntity
        where TPropertyType : struct, Enum;
