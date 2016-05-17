USE DBSurveys;

select 'sp_add_survey_template' as '';

DROP PROCEDURE IF EXISTS sp_add_survey_template;
DELIMITER // 
CREATE PROCEDURE `sp_add_survey_template` (	
	IN creator_login 				VARCHAR(100),
    IN creator_password				VARCHAR(50),
    IN template_title				VARCHAR(50),
    IN language_id					BIGINT UNSIGNED,
    OUT result_id					BIGINT UNSIGNED,
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
    DECLARE userId BIGINT UNSIGNED;
    SET userId = 0;
    
    IF NOT is_user_exists(creator_login, creator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(creator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
    
    SELECT 		u.Id 
    INTO 		userId    
    FROM 		User u 
    WHERE 		u.login = creator_login;

    INSERT INTO survey_template (template_name, user_created_id, user_modified_id, created, last_modified, language_id)
    	VALUES ( template_title, userId, userId, NOW(), NOW(), language_id);

	SELECT LAST_INSERT_ID() INTO result_id;
    SELECT 0 INTO error_code;
END//
DELIMITER ;

###################################

select 'sp_update_survey_template' as '';
DROP PROCEDURE IF EXISTS sp_update_survey_template;
DELIMITER // 
CREATE PROCEDURE `sp_update_survey_template` (	
	IN updator_login 				VARCHAR(100),
    IN updator_password				VARCHAR(50),
	IN template_id					BIGINT UNSIGNED,
    IN template_title				VARCHAR(50),
    IN language_id					BIGINT UNSIGNED,
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
    DECLARE userId BIGINT UNSIGNED;
    SET userId = 0;
    
    IF NOT is_user_exists(updator_login, updator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(updator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
    
    SELECT 		u.Id 
    INTO 		userId    
    FROM 		User u 
    WHERE 		u.login = creator_login;
	
	UPDATE 	survey_template st
	SET 	st.template_name = 	template_name,
			st.user_modified_id = userId,
			st.last_modified = NOW(),
            st.language_id = language_id
	WHERE	
			st.id = template_id;
            
    SELECT 0 INTO error_code;
END//
DELIMITER ;

###################################

select 'sp_remove_survey_template' as '';
DROP PROCEDURE IF EXISTS sp_remove_survey_template;
DELIMITER // 
CREATE PROCEDURE `sp_remove_survey_template` (	
	IN updator_login 				VARCHAR(100),
    IN updator_password				VARCHAR(50),
	IN template_id					BIGINT UNSIGNED,
	 OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
    IF NOT is_user_exists(updator_login, updator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(updator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
    
    DELETE 
	FROM 	survey_template
	WHERE 	Id = template_id;
	
    SELECT 0 INTO error_code;
END//
DELIMITER ;

###################################