USE DBSurveys;
select 'structure\init_vocabulary started' as '';

CREATE TABLE IF NOT EXISTS Language(
	id 					SERIAL,
	name 				VARCHAR (20)
);

CREATE TABLE IF NOT EXISTS Vocabulary(
	id 						SERIAL,
	word 					VARCHAR (50) UNIQUE, # 39 - Longest German word
	language_id 			BIGINT UNSIGNED NOT NULL,
	parent_translation_id	BIGINT UNSIGNED NULL,
	PRIMARY KEY(id),
	FOREIGN KEY (language_id) REFERENCES Language (id),
	FOREIGN KEY (parent_translation_id) REFERENCES Vocabulary (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

select 'structure\init_vocabulary executed' as '';