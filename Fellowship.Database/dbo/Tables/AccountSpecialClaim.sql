CREATE TABLE [dbo].[AccountSpecialClaim] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [AccountId]  INT            NOT NULL,
    [ClaimType]  NVARCHAR (100) NOT NULL,
    [ClaimValue] NVARCHAR (100) NULL,
    CONSTRAINT [PK_AccountSpecialClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountSpecialClaim_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AccountSpecialClaim_ClaimType]
    ON [dbo].[AccountSpecialClaim]([ClaimType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AccountSpecialClaim_AccountId]
    ON [dbo].[AccountSpecialClaim]([AccountId] ASC);

