CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[T_entreprise]  WITH CHECK ADD FOREIGN KEY([num_tel_id])
REFERENCES [dbo].[T_num_tel] ([id])
GO
ALTER TABLE [dbo].[T_entreprise]  WITH CHECK ADD FOREIGN KEY([site_id])
REFERENCES [dbo].[T_site] ([id])
GO
ALTER TABLE [dbo].[T_offre_emploi]  WITH CHECK ADD FOREIGN KEY([entreprise_id])
REFERENCES [dbo].[T_entreprise] ([id])
GO
ALTER TABLE [dbo].[T_offre_emploi]  WITH CHECK ADD FOREIGN KEY([site_id])
REFERENCES [dbo].[T_site] ([id])
GO
ALTER TABLE [dbo].[T_offre_emploi]  WITH CHECK ADD FOREIGN KEY([statut_id])
REFERENCES [dbo].[T_statut] ([id])
GO
ALTER TABLE [dbo].[T_offre_emploi]  WITH CHECK ADD FOREIGN KEY([type_contrat_id])
REFERENCES [dbo].[T_type_contrat] ([id])
GO
ALTER TABLE [dbo].[T_offre_emploi_competences]  WITH CHECK ADD FOREIGN KEY([competences_id])
REFERENCES [dbo].[T_competences] ([id])
GO
ALTER TABLE [dbo].[T_offre_emploi_competences]  WITH CHECK ADD FOREIGN KEY([offre_emploi_id])
REFERENCES [dbo].[T_offre_emploi] ([id])
GO
ALTER TABLE [dbo].[T_offre_emploi_langues]  WITH CHECK ADD FOREIGN KEY([langues_id])
REFERENCES [dbo].[T_langues] ([id])
GO
ALTER TABLE [dbo].[T_offre_emploi_langues]  WITH CHECK ADD FOREIGN KEY([offre_emploi_id])
REFERENCES [dbo].[T_offre_emploi] ([id])
GO
ALTER TABLE [dbo].[T_profil]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[T_role] ([id])
GO
ALTER TABLE [dbo].[T_profil]  WITH CHECK ADD FOREIGN KEY([type_contrat_id])
REFERENCES [dbo].[T_type_contrat] ([id])
GO
ALTER TABLE [dbo].[T_profil]  WITH CHECK ADD  CONSTRAINT [FK_T_profil_AspNetUsers] FOREIGN KEY([AspNetUser_id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[T_profil] CHECK CONSTRAINT [FK_T_profil_AspNetUsers]
GO
ALTER TABLE [dbo].[T_profil_competences]  WITH CHECK ADD FOREIGN KEY([competences_id])
REFERENCES [dbo].[T_competences] ([id])
GO
ALTER TABLE [dbo].[T_profil_competences]  WITH CHECK ADD FOREIGN KEY([profil_id])
REFERENCES [dbo].[T_profil] ([id])
GO
ALTER TABLE [dbo].[T_profil_langues]  WITH CHECK ADD FOREIGN KEY([langues_id])
REFERENCES [dbo].[T_langues] ([id])
GO
ALTER TABLE [dbo].[T_profil_langues]  WITH CHECK ADD FOREIGN KEY([profil_id])
REFERENCES [dbo].[T_profil] ([id])
GO