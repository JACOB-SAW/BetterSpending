CREATE TABLE [dbo].[SystemUser] (
    [UserID]                 INT            IDENTITY (1, 1) NOT NULL,
    [SystemUserFirstName]    NVARCHAR (50)  NOT NULL,
    [SystemUserLastName]     NVARCHAR (50)  NOT NULL,
    [SystemUserEmail]        NVARCHAR (100) NOT NULL,
    [SystemUserPassword]     NVARCHAR (200) NOT NULL,
    [SystemUserProfileImage] NVARCHAR (200) NULL,
    [AccountTypeID]          INT            NOT NULL,
    [LastLogin]              DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    FOREIGN KEY ([AccountTypeID]) REFERENCES [dbo].[AccountType] ([AccountTypeID])
);
