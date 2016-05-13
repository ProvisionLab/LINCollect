USE DBSurveys;

select 'survey' as '';
CREATE TABLE IF NOT EXISTS Survey(
	id					SERIAL,
	enumerator_id		BIGINT UNSIGNED NOT NULL,
	respondent_id		BIGINT UNSIGNED NOT NULL,
	company_id			BIGINT UNSIGNED NOT NULL,

	PRIMARY KEY (id),	
	FOREIGN KEY (respondent_id) REFERENCES User (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (enumerator_id) REFERENCES User (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (company_id) REFERENCES Company (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

select 'survey_detail' as '';
CREATE TABLE IF NOT EXISTS Survey_Detail(
	id 							SERIAL,
	survey_field_id				BIGINT UNSIGNED NOT NULL, 
	fk_parent_survey_id			BIGINT UNSIGNED NOT NULL,
    user_answer 				VARCHAR(1000) NOT NULL,

	PRIMARY KEY (id),	
	FOREIGN KEY (survey_field_id) REFERENCES Survey_Field (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (fk_parent_survey_id) REFERENCES Survey (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);