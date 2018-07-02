CREATE TABLE [dbo].[GroupMember] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [GroupId]   INT            NOT NULL,
    [AccountId] INT            NOT NULL,
    [Nickname]  NVARCHAR (100) NULL,
    [AddedOn]   DATETIME       NOT NULL,
    [Kicked]    BIT            NOT NULL,
    [FundCache] MONEY          NOT NULL,
    CONSTRAINT [PK_GroupMember] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupMember_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_GroupMember_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_GroupMember_GroupMembers]
    ON [dbo].[GroupMember]([GroupId] ASC, [Kicked] ASC, [AddedOn] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GroupMember_MemberOfGroups]
    ON [dbo].[GroupMember]([AccountId] ASC, [Kicked] ASC, [AddedOn] DESC);

