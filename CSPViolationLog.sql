-- Run this once against the database named by the ConstrNC connection string
-- (web.config <connectionStrings>) before deploying the CSP Report-Only header.
-- Collects violation reports POSTed by browsers to CspReport.ashx.

CREATE TABLE CSPViolationLog
(
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    LogDate             VARCHAR(8)      NULL,  -- yyyyMMdd
    LogTime             VARCHAR(6)      NULL,  -- HHmmss
    Disposition         NVARCHAR(20)    NULL,  -- "report" for Report-Only
    ViolatedDirective   NVARCHAR(200)   NULL,
    BlockedUri          NVARCHAR(1000)  NULL,
    DocumentUri         NVARCHAR(1000)  NULL,
    SourceFile          NVARCHAR(1000)  NULL,
    LineNumber          NVARCHAR(20)    NULL,
    ColumnNumber        NVARCHAR(20)    NULL,
    Sample              NVARCHAR(1000)  NULL,  -- truncated blocked content, only present when the
                                                -- CSP directive includes 'report-sample'
    RawReport           NVARCHAR(MAX)   NULL,  -- full JSON body, for anything the parsed columns miss
    CreatedAt           DATETIME        NOT NULL DEFAULT GETDATE()
);

-- If you already created this table before 'report-sample' was added to the Report-Only
-- policy, run this instead of re-creating the table:
-- ALTER TABLE CSPViolationLog ADD Sample NVARCHAR(1000) NULL;
