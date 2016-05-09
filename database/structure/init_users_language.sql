USE DBSurveys;
select 'structure/init_users_language.sql started' AS '';

CREATE TABLE IF NOT EXISTS User_Language(
	id 					SERIAL,
	user_id 			BIGINT UNSIGNED NOT NULL,
	language_id 		BIGINT UNSIGNED NOT NULL,

	PRIMARY KEY (id),
	FOREIGN KEY (user_id) REFERENCES User (id),
	FOREIGN KEY (language_id) REFERENCES Language (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

select 'structure/init_users_language.sql executed' AS '';