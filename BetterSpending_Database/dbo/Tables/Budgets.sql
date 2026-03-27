CREATE TABLE [dbo].[Budgets] (
    [BudgetsID]   INT           NOT NULL,
    [UserID]      INT           NOT NULL,
    [BudgetName]  NVARCHAR (50) NULL,
    [TotalAmount] FLOAT (53)    NOT NULL,
    [StartDate]   DATETIME      NULL,
    [EndDate]     DATETIME      NULL,
    [CategoryID]  INT           NOT NULL,
    [RecurringID] INT           NOT NULL,
    [LanguageID]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([BudgetsID] ASC),
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]),
    FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([LanguageID]),
    FOREIGN KEY ([RecurringID]) REFERENCES [dbo].[Recurring] ([RecurringID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[SystemUser] ([UserID])
);

