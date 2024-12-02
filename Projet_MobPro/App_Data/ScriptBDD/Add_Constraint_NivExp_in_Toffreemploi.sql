ALTER TABLE T_niveau_experience
ADD offre_emploi_id INT NULL;

ALTER TABLE T_niveau_experience
ADD CONSTRAINT FK_T_niveau_exp_T_offre_emploi
FOREIGN KEY (offre_emploi_id) 
REFERENCES T_offre_emploi (id);