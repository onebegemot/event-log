using EventLog.Interfaces;

namespace EventLog.Models;

internal record PropertyInfo<TEntity>(Func<TEntity, object> Getter, string Name) :
    IPropertyInfo;