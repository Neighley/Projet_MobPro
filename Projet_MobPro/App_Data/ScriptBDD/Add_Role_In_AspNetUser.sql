ALTER TABLE [AspNetUsers]
ADD CONSTRAINT FK_AspNetUsers_T_role FOREIGN KEY (role_id) REFERENCES T_role(id);
