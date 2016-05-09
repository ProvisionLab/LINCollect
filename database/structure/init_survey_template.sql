USE DBSurveys;
select 'structure/init_survey_template started' as '';

# NB! all child elements must have same language_id!



CREATE TABLE IF NOT EXISTS Survey_Template(
	id 					SERIAL,
	word_id 			BIGINT UNSIGNED NOT NULL, #survey_name
	user_modified_id 	BIGINT UNSIGNED NOT NULL, 
	last_modified 		DATE NULL,
	language_id 		BIGINT UNSIGNED NOT NULL, #survey_langiage

	PRIMARY KEY (id),	
	FOREIGN KEY (word_id) REFERENCES Vocabulary (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (user_modified_id) REFERENCES User (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (language_id) REFERENCES Language (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);


CREATE TABLE IF NOT EXISTS Survey_Field_Type(
	id 					SERIAL,
	field_type 			VARCHAR(15),

	PRIMARY KEY (id)
);


# values stored in vocabulary because survey can be in different languages, 
# and so default values in lists, combobox and maybe simple text boxes
# 1 combobox first_value
# 1 combobox second_value
# 2 email example@example.com
# 3 email company@example.com
CREATE TABLE IF NOT EXISTS Survey_Field(
	id 					SERIAL,
	field_type_id 		BIGINT UNSIGNED NOT NULL,
	default_value_id 	BIGINT UNSIGNED NOT NULL,

	PRIMARY KEY (id),	
	FOREIGN KEY (field_type_id) REFERENCES Survey_Field_Type (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (default_value_id) REFERENCES Vocabulary (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

# Map to resolve N..N between SurveyTemplate and TemplateField
# this table will contain many same survey_template_id
# with different survey_field_id
CREATE TABLE IF NOT EXISTS Survey_Field_Template(
	id 					SERIAL,
	survey_template_id 	BIGINT UNSIGNED NOT NULL, 	# parent where this field hosted
	survey_field_id 	BIGINT UNSIGNED NOT NULL,	# field
	word_id				BIGINT UNSIGNED NULL,		# label

	PRIMARY KEY (id),	
	FOREIGN KEY (survey_template_id) REFERENCES Survey_Template (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (survey_field_id) REFERENCES Survey_Field (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (word_id) REFERENCES Vocabulary (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Survey_Detail(
	id 							SERIAL,
	survey_field_template_id	BIGINT UNSIGNED NOT NULL, # fields
	field_value 				VARCHAR(1000) NOT NULL,

	PRIMARY KEY (id),	
	FOREIGN KEY (survey_field_template_id) REFERENCES Survey_Field_Template (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Survey(
	id					SERIAL,
	respondent_id		BIGINT UNSIGNED NOT NULL,
	company_id			BIGINT UNSIGNED NOT NULL,
	survey_detail_id	BIGINT UNSIGNED NOT NULL,

	PRIMARY KEY (id),	
	FOREIGN KEY (respondent_id) REFERENCES User (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (company_id) REFERENCES Company (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (survey_detail_id) REFERENCES Survey_Detail (id)
	ON DELETE RESTRICT ON UPDATE CASCADE

);

select 'structure/init_survey_template executed' as '';