CREATE TABLE [dbo].[T_niveau_experience](
	[id] [int] NOT NULL,
	[nom_niveau_experience] [varchar](255) NULL,
	[domaine] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_profil]
	ADD CONSTRAINT [FK_T_profil_T_niveau_experience] FOREIGN KEY ([niveau_experience_id]) REFERENCES [dbo].[T_niveau_experience] ([Id])
