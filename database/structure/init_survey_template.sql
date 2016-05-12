USE DBSurveys;

select 'survey_template' as '';
# NB! all child elements must have same language_id!
CREATE TABLE IF NOT EXISTS Survey_Template(
	id 					SERIAL,
    template_name		varchar(100) UNIQUE,
    user_created_id		BIGINT UNSIGNED NOT NULL,
	user_modified_id 	BIGINT UNSIGNED NOT NULL, 
    created				DATE NOT NULL,
	last_modified 		DATE NULL,
	language_id 		BIGINT UNSIGNED NOT NULL, #survey_langiage

	PRIMARY KEY (id),	
	FOREIGN KEY (user_created_id) REFERENCES User (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (user_modified_id) REFERENCES User (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,    
	FOREIGN KEY (language_id) REFERENCES Language (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);


select 'survey_field_type' as '';
CREATE TABLE IF NOT EXISTS Survey_Field_Type(
	id 					SERIAL,
	field_type 			VARCHAR(15) UNIQUE,

	PRIMARY KEY (id)
);

select 'survey_field' as '';
# values stored in vocabulary because survey can be in different languages, 
# and so default values in lists, combobox and maybe simple text boxes
# 1 combobox first_value
# 1 combobox second_value
# 2 email example@example.com
# 3 email company@example.com
# default value is stored in vocabulary
CREATE TABLE IF NOT EXISTS Survey_Field(
	id 							SERIAL,
	fk_parent_survey_id			BIGINT UNSIGNED NOT NULL,
    fk_survey_field_type		BIGINT UNSIGNED NOT NULL,
    page_index					BIGINT UNSIGNED NOT NULL,
	group_index					BIGINT UNSIGNED NOT NULL,
	PRIMARY KEY (id),
	FOREIGN KEY (fk_parent_survey_id) REFERENCES Survey_Template (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (fk_survey_field_type) REFERENCES Survey_Field_Type (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);