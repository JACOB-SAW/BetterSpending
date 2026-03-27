CREATE TABLE [dbo].[Expenses] (
    [ExpensesID]  INT NOT NULL,
    [UserID]      INT NOT NULL,
    [CategoryID]  INT NOT NULL,
    [Amount]      INT NOT NULL,
    [Frequency]   INT NOT NULL,
    [RecurringID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ExpensesID] ASC),
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[SystemUser] ([UserID])
);

