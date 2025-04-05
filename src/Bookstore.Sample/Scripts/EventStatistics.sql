-- Show event statistics

WITH
    EventData AS (
        SELECT
            etd.Description AS EventType,
            (julianday(EventLog.CompletedAt) - julianday(EventLog.CreatedAt)) * 86400000 AS DurationInMs,
            EventLog.Status,
            EventLog.Id
        FROM EventLog
            LEFT JOIN EventTypeDescriptions etd ON EventLog.EventType = etd.EnumId
        WHERE julianday(EventLog.CreatedAt) - julianday('2025-01-01') >= 0
),
     Aggregated AS (
        SELECT
            EventType,
            COUNT(*) AS TotalCount,
            COUNT(CASE WHEN Status != 1 THEN Id END) AS ErrorCount,
            ROUND(AVG(DurationInMs), 0) AS MeanInMs
        FROM EventData
        GROUP BY EventType
     ),
     WithMedian AS (
        SELECT
            ed.EventType,
            ed.DurationInMs,
            RANK() OVER (PARTITION BY ed.EventType ORDER BY ed.DurationInMs) AS rk,
            COUNT(*) OVER (PARTITION BY ed.EventType) AS cnt
        FROM EventData ed
     ),
     MedianCalc AS (
        SELECT
            EventType,
            ROUND(AVG(DurationInMs), 0) AS MedianInMs
        FROM WithMedian
        WHERE rk = (cnt + 1) / 2 OR rk = (cnt + 2) / 2  -- median logic
        GROUP BY EventType
     )
SELECT
    a.EventType,
    a.TotalCount,
    a.ErrorCount,
    m.MedianInMs,
    a.MeanInMs
FROM Aggregated a
    JOIN MedianCalc m ON a.EventType = m.EventType
    JOIN EventData ed ON ed.EventType = a.EventType
GROUP BY a.EventType
ORDER BY a.EventType;

-- Helper query
-- select * from EventTypeDescriptions

-- Show event statistics
