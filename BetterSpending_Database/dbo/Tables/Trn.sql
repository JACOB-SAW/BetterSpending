CREATE TABLE [dbo].[Trn] (
    [TrnID]      INT      IDENTITY (1, 1) NOT NULL,
    [UserID]     INT      NOT NULL,
    [BudgetsID]  INT      NOT NULL,
    [TrnDate]    DATETIME NULL,
    [TrnAmount]  FLOAT    NOT NULL,
    PRIMARY KEY CLUSTERED ([TrnID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[SystemUser] ([UserID]),
    FOREIGN KEY ([BudgetsID]) REFERENCES [dbo].[Budgets] ([BudgetsID])
);
