USE DBSurveys;

# SURVEY TEMPLATE
# user can create survey_template IF admin
# user can add pages to survey_template IF admin
# user can add sections to survey_template IF admin
# user can add fields to survey_template IF admin
# user can remove fields/pages/sections IF admin

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