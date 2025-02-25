select
    EventLog.CreatedBy,
    EventLog.CreatedAt,
    EventTypeDescriptions.Description as Event,
    case when EntityLog.ActionType = 1 then 'Create' when EntityLog.ActionType = 2 then 'Update' end as Action,
    EntityLog.EntityId AS Id,
    EntityTypeDescriptions.Description as Entity,
    PropertyTypeDescriptions.Description as Property,
    cast(Value as varchar(1024)) as Value,
    'String' as Type
from StringPropertyLog
         left join EntityLog on EntityLog.Id = StringPropertyLog.EntityLogEntryId
         left join EventLog on EventLog.Id = EntityLog.EventLogEntryId
         left join EventTypeDescriptions ON EventLog.EventType = EventTypeDescriptions.EnumId
         left join EntityTypeDescriptions ON EntityLog.EntityType = EntityTypeDescriptions.EnumId
         left join PropertyTypeDescriptions ON StringPropertyLog.PropertyType = PropertyTypeDescriptions.EnumId
union all
select
    EventLog.CreatedBy,
    EventLog.CreatedAt,
    EventTypeDescriptions.Description as Event,
    case when EntityLog.ActionType = 1 then 'Create' when EntityLog.ActionType = 2 then 'Update' end as Action,
    EntityLog.EntityId AS Id,
    EntityTypeDescriptions.Description as Entity,
    PropertyTypeDescriptions.Description as Property,
    cast(Value as varchar(1024)) as Value,
    'DateTime' as Type
from DateTimePropertyLog
         left join EntityLog on EntityLog.Id = DateTimePropertyLog.EntityLogEntryId
         left join EventLog on EventLog.Id = EntityLog.EventLogEntryId
         left join EventTypeDescriptions ON EventLog.EventType = EventTypeDescriptions.EnumId
         left join EntityTypeDescriptions ON EntityLog.EntityType = EntityTypeDescriptions.EnumId
         left join PropertyTypeDescriptions ON DateTimePropertyLog.PropertyType = PropertyTypeDescriptions.EnumId
union all
select
    EventLog.CreatedBy,
    EventLog.CreatedAt,
    EventTypeDescriptions.Description as Event,
    case when EntityLog.ActionType = 1 then 'Create' when EntityLog.ActionType = 2 then 'Update' end as Action,
    EntityLog.EntityId AS Id,
    EntityTypeDescriptions.Description as Entity,
    PropertyTypeDescriptions.Description as Property,
    cast(Value as varchar(1024)) as Value,
    'Bool' as Type
from BoolPropertyLog
         left join EntityLog on EntityLog.Id = BoolPropertyLog.EntityLogEntryId
         left join EventLog on EventLog.Id = EntityLog.EventLogEntryId
         left join EventTypeDescriptions ON EventLog.EventType = EventTypeDescriptions.EnumId
         left join EntityTypeDescriptions ON EntityLog.EntityType = EntityTypeDescriptions.EnumId
         left join PropertyTypeDescriptions ON BoolPropertyLog.PropertyType = PropertyTypeDescriptions.EnumId
union all
select
    EventLog.CreatedBy,
    EventLog.CreatedAt,
    EventTypeDescriptions.Description as Event,
    case when EntityLog.ActionType = 1 then 'Create' when EntityLog.ActionType = 2 then 'Update' end as Action,
    EntityLog.EntityId AS Id,
    EntityTypeDescriptions.Description as Entity,
    PropertyTypeDescriptions.Description as Property,
    cast(Value as varchar(1024)) as Value,
    'Int32' as Type
from Int32PropertyLog
         left join EntityLog on EntityLog.Id = Int32PropertyLog.EntityLogEntryId
         left join EventLog on EventLog.Id = EntityLog.EventLogEntryId
         left join EventTypeDescriptions ON EventLog.EventType = EventTypeDescriptions.EnumId
         left join EntityTypeDescriptions ON EntityLog.EntityType = EntityTypeDescriptions.EnumId
         left join PropertyTypeDescriptions ON Int32PropertyLog.PropertyType = PropertyTypeDescriptions.EnumId
union all
select
    EventLog.CreatedBy,
    EventLog.CreatedAt,
    EventTypeDescriptions.Description as Event,
    case when EntityLog.ActionType = 1 then 'Create' when EntityLog.ActionType = 2 then 'Update' end as Action,
    EntityLog.EntityId AS Id,
    EntityTypeDescriptions.Description as Entity,
    PropertyTypeDescriptions.Description as Property,
    cast(Value as varchar(1024)) as Value,
    'Double' as Type
from DoublePropertyLog
         left join EntityLog on EntityLog.Id = DoublePropertyLog.EntityLogEntryId
         left join EventLog on EventLog.Id = EntityLog.EventLogEntryId
         left join EventTypeDescriptions ON EventLog.EventType = EventTypeDescriptions.EnumId
         left join EntityTypeDescriptions ON EntityLog.EntityType = EntityTypeDescriptions.EnumId
         left join PropertyTypeDescriptions ON DoublePropertyLog.PropertyType = PropertyTypeDescriptions.EnumId
union all
select
    EventLog.CreatedBy,
    EventLog.CreatedAt,
    EventTypeDescriptions.Description as Event,
    case when EntityLog.ActionType = 1 then 'Create' when EntityLog.ActionType = 2 then 'Update' end as Action,
    EntityLog.EntityId AS Id,
    EntityTypeDescriptions.Description as Entity,
    PropertyTypeDescriptions.Description as Property,
    cast(Value as varchar(1024)) as Value,
    'Decimal' as Type
from DecimalPropertyLog
         left join EntityLog on EntityLog.Id = DecimalPropertyLog.EntityLogEntryId
         left join EventLog on EventLog.Id = EntityLog.EventLogEntryId
         left join EventTypeDescriptions ON EventLog.EventType = EventTypeDescriptions.EnumId
         left join EntityTypeDescriptions ON EntityLog.EntityType = EntityTypeDescriptions.EnumId
         left join PropertyTypeDescriptions ON DecimalPropertyLog.PropertyType = PropertyTypeDescriptions.EnumId
order by CreatedAt