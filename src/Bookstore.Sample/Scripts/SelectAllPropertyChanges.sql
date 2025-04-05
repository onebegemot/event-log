-- View all property changes (SQLite adaptation)

with EventAndEntityLog as (
    select
        EventLog.Id as EventLogEntryId,
        EntityLog.Id as EntityLogEntryId,
        CreatedBy as InitiatorId,
        EventLog.CreatedAt as CreatedAt,
        EventTypeDescriptions.Description as EventDescription,
        EntityTypeDescriptions.Description as EntityDescription,
        EntityLog.EntityId as EntityId,
        EntityLog.ActionType as EntityActionType
    from EntityLog
        left join EventLog on EntityLog.EventLogEntryId = EventLog.Id
        left join EventTypeDescriptions on EventType = EventTypeDescriptions.EnumId
        left join EntityTypeDescriptions on EntityLog.EntityType = EntityTypeDescriptions.EnumId
)

select
    EventAndEntityLog.CreatedAt,
    case when EventAndEntityLog.EntityActionType = 1 then 'Create' when EventAndEntityLog.EntityActionType = 2 then 'Update' end as Action,
    EventAndEntityLog.EntityId as EntityId,
    EventAndEntityLog.EntityDescription as Entity,
    PropertyTypeDescriptions.Description as PropertyDescription,
    PropertyLog.Value as Value,
    PropertyLog.Type as ValueType,
    EventAndEntityLog.InitiatorId as InitiatorId,
    EventAndEntityLog.EventDescription,
    EventAndEntityLog.EventLogEntryId
from (
     select PropertyType, cast(Value as varchar(1024)) as Value, EntityLogEntryid, 'String' as Type from StringPropertyLog union all
     select PropertyType, cast(Value as varchar(26)) as Value, EntityLogEntryid, 'DateTime' as Type from DateTimePropertyLog union all
     select PropertyType, cast(Value as varchar(1)) as Value, EntityLogEntryid, 'Bool' as Type from BoolPropertyLog union all
     select PropertyType, cast(Value as varchar(20)) as Value, EntityLogEntryid, 'Int32' as Type from Int32PropertyLog union all
     select PropertyType, cast(Value as varchar(30)) as Value, EntityLogEntryid, 'Decimal' as Type from DecimalPropertyLog union all
     select PropertyType, cast(Value as varchar(30)) as Value, EntityLogEntryid, 'Double' as Type from DoublePropertyLog
) as PropertyLog
     join EventAndEntityLog on EventAndEntityLog.EntityLogEntryId = PropertyLog.EntityLogEntryId
     join PropertyTypeDescriptions ON PropertyLog.PropertyType = PropertyTypeDescriptions.EnumId
where
    (julianday(CreatedAt) - julianday('2025-03-31')) * 86400 > 0
order by EventAndEntityLog.CreatedAt desc

-- Helper queries
-- select * from EventTypeDescriptions
-- select * from EntityTypeDescriptions
-- select * from PropertyTypeDescriptions

-- View all property changes