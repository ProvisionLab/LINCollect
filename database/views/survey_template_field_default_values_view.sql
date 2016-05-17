USE DBSurveys;


select 'vw_survey_template_field_default_values' as '';


DROP VIEW IF EXISTS vw_survey_template_field_default_values;

CREATE VIEW vw_survey_template_field_default_values 	as 
SELECT 
	sf.id							as 'FieldId'
    ,sf.label						as 'FieldLabel'
    ,v.word							as 'DefaultValue'
    ,v.language_id					as 'DefaultValueLanguageId'
    ,l.name							as 'DefaultValueLanguage'
    
FROM survey_field sf 
LEFT JOIN Survey_Field_Vocabulary_Cross cr 
	ON cr.fk_survey_field_id = sf.id
LEFT JOIN Vocabulary v 
	ON cr.fk_vocabulary_word_id = v.id
LEFT JOIN User_Language l
	ON l.id = v.language_id;