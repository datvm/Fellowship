CREATE TABLE [dbo].[Group] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (100) NOT NULL,
    [Description]          NVARCHAR (MAX) NULL,
    [AdminAccountId]       INT            NOT NULL,
    [MemberCanAddPeople]   BIT            NOT NULL,
    [MemberCanSetActivity] BIT            NOT NULL,
    [Deleted]              BIT            NOT NULL,
    [CreatedTime]          DATETIME       NOT NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Group_Account] FOREIGN KEY ([AdminAccountId]) REFERENCES [dbo].[Account] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Group_Account]
    ON [dbo].[Group]([AdminAccountId] ASC, [Deleted] ASC, [Name] ASC);

