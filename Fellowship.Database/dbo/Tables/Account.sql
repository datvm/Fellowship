CREATE TABLE [dbo].[Account] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [CreatedByAccountId] INT            NULL,
    [PhoneNumber]        VARCHAR (20)   NULL,
    [FacebookId]         VARCHAR (128)  NULL,
    [FacebookProfile]    NVARCHAR (MAX) NULL,
    [Name]               NVARCHAR (100) NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Account_PhoneNumber]
    ON [dbo].[Account]([PhoneNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Account_FacebookId]
    ON [dbo].[Account]([FacebookId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Account_CreatedByAccountId]
    ON [dbo].[Account]([CreatedByAccountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Account_Name]
    ON [dbo].[Account]([Name] ASC);

