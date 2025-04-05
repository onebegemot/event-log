-- Show all events

SELECT
    EventLog.Id,
    EventLog.CreatedBy as Initiator,
    EventTypeDescriptions.Description as EventType,
    EventStatusDescriptions.Description as Status,
    CreatedAt,
    (CAST((julianday(EventLog.CompletedAt) - julianday(EventLog.CreatedAt)) * 86400000 AS INTEGER)) AS DurationInMs,
    EventLog.Details,
    EventLog.FailureDetails
FROM EventLog as EventLog
     LEFT OUTER JOIN EventTypeDescriptions as EventTypeDescriptions on EventLog.EventType = EventTypeDescriptions.EnumId
     LEFT OUTER JOIN EventStatusDescriptions as EventStatusDescriptions on EventLog.Status = EventStatusDescriptions.EnumId
where
    (julianday(CreatedAt) - julianday('2025-03-31')) * 86400 > 0
order by EventLog.CreatedAt desc

-- Helper query
-- select * from EventTypeDescriptions

-- Show all events