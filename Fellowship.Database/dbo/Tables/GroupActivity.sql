CREATE TABLE [dbo].[GroupActivity] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [GroupId]     INT            NOT NULL,
    [Code]        VARCHAR (20)   NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Note]        NVARCHAR (MAX) NULL,
    [CreatedTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_GroupActivity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupActivity_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([Id])
);

