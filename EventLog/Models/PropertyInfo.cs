using EventLog._NugetCode.Interfaces;

namespace EventLog.Models;

internal record PropertyInfo<TEntity>(Func<TEntity, object> Getter, string Name)
    where TEntity : IPkEntity;