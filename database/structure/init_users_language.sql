USE DBSurveys;


select 'user_language_cross' as '';
CREATE TABLE IF NOT EXISTS User_Language_Cross(
	id 					SERIAL,
	user_id 			BIGINT UNSIGNED NOT NULL,
	language_id 		BIGINT UNSIGNED NOT NULL,

	PRIMARY KEY (id),
	FOREIGN KEY (user_id) REFERENCES User (id)
    ON DELETE CASCADE,
	FOREIGN KEY (language_id) REFERENCES User_Language (id)
	ON DELETE CASCADE
);
