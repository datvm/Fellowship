CREATE TABLE [dbo].[GroupMemberActivity] (
    [Id]                    BIGINT   IDENTITY (1, 1) NOT NULL,
    [GroupMemberId]         INT      NOT NULL,
    [LinkedGroupActivityId] INT      NULL,
    [Fund]                  MONEY    NOT NULL,
    [CreatedTime]           DATETIME NOT NULL,
    CONSTRAINT [PK_GroupMemberActivity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupMemberActivity_GroupActivity] FOREIGN KEY ([LinkedGroupActivityId]) REFERENCES [dbo].[GroupActivity] ([Id]),
    CONSTRAINT [FK_GroupMemberActivity_GroupMember] FOREIGN KEY ([GroupMemberId]) REFERENCES [dbo].[GroupMember] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_GroupMemberActivity_MemberActivities]
    ON [dbo].[GroupMemberActivity]([GroupMemberId] ASC, [CreatedTime] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GroupMemberActivity_LinkedGroupActivityId]
    ON [dbo].[GroupMemberActivity]([LinkedGroupActivityId] ASC, [Fund] ASC);

