CREATE TABLE [dbo].[GroupManager] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [GroupId]           INT NOT NULL,
    [GroupMemberId]     INT NOT NULL,
    [SetByAccountId]    INT NOT NULL,
    [CreatedTime]       BIT NOT NULL,
    [CanAddOtherMember] BIT NOT NULL,
    [Deleted]           BIT NOT NULL,
    CONSTRAINT [PK_GroupManager] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupManager_Account] FOREIGN KEY ([SetByAccountId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_GroupManager_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id]),
    CONSTRAINT [FK_GroupManager_GroupMember] FOREIGN KEY ([GroupMemberId]) REFERENCES [dbo].[GroupMember] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_GroupManager_ByGroup]
    ON [dbo].[GroupManager]([GroupId] ASC, [Deleted] ASC);

