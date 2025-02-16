using EventLog.Interfaces;
using EventLog.Models.Enums;

namespace EventLog.Models;

public record EntityLogInfo<TEntity>(
    IEnumerable<TEntity> Entities,
    params PropertyType[] Properties)
        where TEntity : IPkEntity;
