CREATE TABLE [dbo].[GroupManager] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [GroupId]           INT NOT NULL,
    [GroupMemberId]     INT NOT NULL,
    [SetByAccountId]    INT NOT NULL,
    [CreatedTime]       BIT NOT NULL,
    [CanAddOtherMember] BIT NOT NULL,
    [Deleted]           BIT NOT NULL,
    CONSTRAINT [PK_GroupManager] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_GroupManager_ByGroup]
    ON [dbo].[GroupManager]([GroupId] ASC, [Deleted] ASC);

