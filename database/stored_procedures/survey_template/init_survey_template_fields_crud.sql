USE DBSurveys;

select 'sp_add_survey_template_field' as '';

DROP PROCEDURE IF EXISTS sp_add_survey_template_field;
DELIMITER // 
CREATE PROCEDURE `sp_add_survey_template_field` (	
	IN creator_login 				VARCHAR(100),
    IN creator_password				VARCHAR(50),
    IN parent_page_id				BIGINT UNSIGNED,
    IN survey_field_type_id			BIGINT UNSIGNED,
    IN group_id						BIGINT UNSIGNED,
    IN label						VARCHAR(50),
    OUT result_id					BIGINT UNSIGNED,
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
	DECLARE fieldIndex BIGINT UNSIGNED;
    SET fieldIndex = 0;
    IF NOT is_user_exists(creator_login, creator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(creator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
	
	 SELECT  MAX(sf.field_index) + 1
    INTO	fieldIndex
    FROM	survey_field sf
    JOIN 	survey_page sp
		ON sf.fk_parent_page_id = parent_page_id;
        
	IF fieldIndex is NULL 
    THEN 
		SET fieldIndex = 1;
    END IF;
    
    IF group_id IS NOT NULL 
    THEN
		IF NOT EXISTS (	SELECT 1 
						FROM survey_field sf
						JOIN survey_field_type sftype 
							ON sf.fk_survey_field_type_id = sftype.id
						WHERE sf.id = group_id
                        AND   sf.fk_parent_page_id = parent_page_id
						AND   sftype.field_type = 'Groupbox')
		THEN 
			SELECT 5 INTO error_code;
			LEAVE proc_label;
		END IF;
	END IF;
                   
    INSERT INTO survey_field (fk_parent_page_id, fk_survey_field_type_id, fk_group_id, label, field_index)
       	VALUES ( parent_page_id, survey_field_type_id, group_id, label, fieldIndex);

	SELECT LAST_INSERT_ID() INTO result_id;
    SELECT 0 INTO error_code;
END//
DELIMITER ;


###################################
select 'sp_update_survey_template_field' as '';
DROP PROCEDURE IF EXISTS sp_update_survey_template_field;
DELIMITER // 
CREATE PROCEDURE `sp_update_survey_template_field` (	
	IN creator_login 				VARCHAR(100),
    IN creator_password				VARCHAR(50),
	IN field_id						BIGINT UNSIGNED,
    IN new_label					VARCHAR(50),
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	DECLARE fieldIndex BIGINT UNSIGNED;
    SET fieldIndex = 0;
    IF NOT is_user_exists(creator_login, creator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(creator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
	
	UPDATE 	survey_field sf
	SET 	sf.label = new_label
	WHERE	sf.id = fieldId;
	
    SELECT 0 INTO error_code;
END//
DELIMITER ;

###################################
select 'sp_remove_survey_template_field' as '';
DROP PROCEDURE IF EXISTS sp_remove_survey_template_field;
DELIMITER // 
CREATE PROCEDURE `sp_remove_survey_template_field` (	
	IN creator_login 				VARCHAR(100),
    IN creator_password				VARCHAR(50),
	IN field_id						BIGINT UNSIGNED,
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	IF NOT is_user_exists(creator_login, creator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(creator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
	
	DELETE 
	FROM 	survey_field 
	WHERE 	id = field_id;
	
    SELECT 0 INTO error_code;
END//
DELIMITER ;
