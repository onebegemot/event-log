using AHWS.EventLog.Interfaces;

namespace AHWS.EventLog.Models;

internal record PropertyInfo<TEntity>(Func<TEntity, object> Getter, string Name) :
    IPropertyInfo;