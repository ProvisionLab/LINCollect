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
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST1 - P1 - Field3 - RadioButton2', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST1 - P1 - Field3 - RadioButton3', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_button_field_type_id(), NULL, 'ST1 - P1 - Field3 - Buttun', @myLastId, @myErrorCode);

CALL sp_add_survey_template_page(@username, @password, 'ST1 - Page Title_2 for default values!!', @surveyTemplateId, @myLastId, @myErrorCode);
SET @surveyTemplatePageId = @myLastId;

CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_dropdownlist_field_type_id(), NULL, 'ST1 - P2 - Field1 - Dropdown', @myLastId, @myErrorCode);
SET @lastFieldId = @myLastId;
SELECT @lastFieldId;
CALL sp_add_field_default_value(@username, @password, @lastFieldId, @languageId, 'Red', @myLastId, @myErrorCode);
CALL sp_add_field_default_value(@username, @password, @lastFieldId, @languageId, 'Green', @myLastId, @myErrorCode);
CALL sp_add_field_default_value(@username, @password, @lastFieldId, @languageId, 'Blue', @myLastId, @myErrorCode);

CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_list_field_type_id(), NULL, 'ST1 - P2 - Field1 - List', @myLastId, @myErrorCode);
SET @lastFieldId = @myLastId;
SELECT @lastFieldId;
CALL sp_add_field_default_value(@username, @password, @lastFieldId, @languageId, 'Red', @myLastId, @myErrorCode);
CALL sp_add_field_default_value(@username, @password, @lastFieldId, @languageId, 'Green', @myLastId, @myErrorCode);
CALL sp_add_field_default_value(@username, @password, @lastFieldId, @languageId, 'Blue', @myLastId, @myErrorCode);
CALL sp_add_field_default_value(@username, @password, @lastFieldId, @languageId, 'White', @myLastId, @myErrorCode);
CALL sp_add_field_default_value(@username, @password, @lastFieldId, @languageId, 'Black', @myLastId, @myErrorCode);

CALL sp_update_field_default_value(@username, @password, @lastFieldId, @languageId, 'White', 'SnowWhite(Updated)', @myErrorCode);
CALL sp_remove_field_default_value(@username, @password, @lastFieldId, 'Black', @myErrorCode);

select * from vw_survey_template_fields;


SELECT '-----------------Create Survey Template 1' as '';
CALL sp_add_survey_template(@username, @password, 'Survey Template 2', @languageId, @myLastId, @myErrorCode);
SET @surveyTemplateId = @myLastId;

CALL sp_add_survey_template_page(@username, @password, 'ST2 - Page Title_1 (not unique)', @surveyTemplateId, @myLastId, @myErrorCode);
SET @surveyTemplatePageId = @myLastId;

CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_textbox_field_type_id(), NULL, 'ST2 - P1 - Field1 - TextBox', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_groupbox_field_type_id(), NULL, 'ST2 - P1 - Field2 - GroupBox', @myLastId, @myErrorCode);
SET @surveyGroupFieldId = @myLastId;

CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST2 - P1 - Field3 - RadioButton1', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST2 - P1 - Field3 - RadioButton2', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_radiobutton_field_type_id(), @surveyGroupFieldId, 'ST2 - P1 - Field3 - RadioButton3', @myLastId, @myErrorCode);
CALL sp_add_survey_template_field(@username, @password, @surveyTemplatePageId, fn_get_button_field_type_id(), NULL, 'ST2 - P1 - Field3 - Buttun', @myLastId, @myErrorCode);

select * from vw_survey_template_fields;