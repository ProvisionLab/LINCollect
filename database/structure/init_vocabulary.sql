USE DBSurveys;

select 'vocabulary' as '';
CREATE TABLE IF NOT EXISTS Vocabulary(
	id 									SERIAL,
	word 								VARCHAR (50) UNIQUE, # 39 - Longest German word
	language_id 						BIGINT UNSIGNED NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY (language_id) REFERENCES Language (id)
    ON DELETE RESTRICT ON UPDATE CASCADE 
);