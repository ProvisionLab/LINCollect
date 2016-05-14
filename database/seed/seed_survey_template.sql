SELECT 'creating survey templates' as '';

SET @myErrorCode = 0;
SET @myLastId = 0;
SET @username = 'Administrator';
SET @password = '56031FBA0CCBC8D952180EA17B075F09';
SET @languageId = 0;
SET @surveyTemplateId = 0;
SET @surveyTemplatePageId = 0;
SET @surveyGroupFieldId = 0;

SELECT 	l.id 
INTO 	@languageId 
FROM 	user_language l 
LIMIT 1;

SELECT '-------------------Create Survey Template 1' as '';
CALL sp_add_survey_template(@username, @password, 'Survey Template 1', @languageId, @myLastId, @myErrorCode);
SET @surveyTemplateId = @myLastId;

CALL sp_add_survey_template_page(@username, @password, 'ST1 - Page Title_1 (not unique)', @surveyTemplateId, @myLastId, @myErrorCode);
SET @surveyTemplatePageId = @myLastId;

CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_textbox_field_type_id(), NULL, 'ST1 - P1 - Field1 - TextBox', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_groupbox_field_type_id(), NULL, 'ST1 - P1 - Field2 - GroupBox', @myLastId, @myErrorCode);
SET @surveyGroupFieldId = @myLastId;

CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST1 - P1 - Field3 - RadioButton1', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @fieldTypeRadioButtonId, 'ST1 - P1 - Field3 - RadioButton2', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST1 - P1 - Field3 - RadioButton3', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_button_field_type_id(), NULL, 'ST1 - P1 - Field3 - Buttun', @myLastId, @myErrorCode);

select * from vw_survey_template_details;


SELECT '-----------------Create Survey Template 1' as '';
CALL sp_add_survey_template(@username, @password, 'Survey Template 2', @languageId, @myLastId, @myErrorCode);
SET @surveyTemplateId = @myLastId;

CALL sp_add_survey_template_page(@username, @password, 'ST2 - Page Title_1 (not unique)', @surveyTemplateId, @myLastId, @myErrorCode);
SET @surveyTemplatePageId = @myLastId;

CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_textbox_field_type_id(), NULL, 'ST2 - P1 - Field1 - TextBox', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_groupbox_field_type_id(), NULL, 'ST2 - P1 - Field2 - GroupBox', @myLastId, @myErrorCode);
SET @surveyGroupFieldId = @myLastId;

CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST2 - P1 - Field3 - RadioButton1', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @fieldTypeRadioButtonId, 'ST2 - P1 - Field3 - RadioButton2', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST2 - P1 - Field3 - RadioButton3', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_button_field_type_id(), NULL, 'ST2 - P1 - Field3 - Buttun', @myLastId, @myErrorCode);

select * from vw_survey_template_details;