SELECT 'Field type id getters' as '';

DROP FUNCTION IF EXISTS fn_get_button_field_type_id;
DELIMITER // 
CREATE FUNCTION fn_get_button_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'Button';

	RETURN result;

END; //

DROP FUNCTION IF EXISTS fn_get_textbox_field_type_id //
CREATE FUNCTION fn_get_textbox_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'TextBox';

	RETURN result;

END; //

DROP FUNCTION IF EXISTS fn_get_email_field_type_id //
CREATE FUNCTION fn_get_email_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'Email';

	RETURN result;

END; //

DROP FUNCTION IF EXISTS fn_get_checkbox_field_type_id //
CREATE FUNCTION fn_get_checkbox_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'Checkbox';

	RETURN result;

END; //

DROP FUNCTION IF EXISTS fn_get_list_field_type_id //
CREATE FUNCTION fn_get_list_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'List';
    
	RETURN result;

END; //

DROP FUNCTION IF EXISTS fn_get_datepicker_field_type_id //
CREATE FUNCTION fn_get_datepicker_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'DatePicker';

	RETURN result;

END; //

DROP FUNCTION IF EXISTS fn_get_dropdownlist_field_type_id //
CREATE FUNCTION fn_get_dropdownlist_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'DropdownList';

	RETURN result;

END; //

DROP FUNCTION IF EXISTS fn_get_radiobutton_field_type_id //
CREATE FUNCTION fn_get_radiobutton_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'RadioButton';

	RETURN result;

END; //

DROP FUNCTION IF EXISTS fn_get_groupbox_field_type_id //
CREATE FUNCTION fn_get_groupbox_field_type_id ()  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	ft.id 
	INTO	result
	FROM 	survey_field_type ft
	WHERE 	ft.field_type = 'GroupBox';

	RETURN result;

END; //

DELIMITER ;