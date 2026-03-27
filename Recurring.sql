CREATE TABLE [dbo].[Categories] (
    [CategoryID]   INT           NOT NULL,
    [UserID]       INT           NOT NULL,
    [CategoryName] NVARCHAR (50) NOT NULL,
    [CategoryType] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CategoryID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[SystemUser] ([UserID])
);

