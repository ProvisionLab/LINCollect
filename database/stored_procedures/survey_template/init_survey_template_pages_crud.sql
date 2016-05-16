USE DBSurveys;

select 'sp_add_survey_template_page' as '';

DROP PROCEDURE IF EXISTS sp_add_survey_template_page;
DELIMITER // 
CREATE PROCEDURE `sp_add_survey_template_page` (	
	IN creator_login 				VARCHAR(100),
    IN creator_password				VARCHAR(50),
    IN page_title					VARCHAR(50),
    IN parent_survey_template_id	BIGINT UNSIGNED,
    OUT result_id					BIGINT UNSIGNED,
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
    DECLARE pageIndex BIGINT UNSIGNED;
    SET pageIndex = 0;
    
    IF NOT is_user_exists(creator_login, creator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(creator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
    
    SELECT  MAX(p.page_index) + 1
    INTO	pageIndex
    FROM	survey_page p
    JOIN 	survey_template st 
		ON p.fk_survey_template_parent_id = parent_survey_template_id;
        
	IF pageIndex is NULL 
    THEN 
		SET pageIndex = 1;
    END IF;

    INSERT INTO survey_page (page_index, page_title, fk_survey_template_parent_id)
    	VALUES ( pageIndex, page_title, parent_survey_template_id);

	SELECT LAST_INSERT_ID() INTO result_id;
    SELECT 0 INTO error_code;
END//
DELIMITER ;

###################################

select 'sp_update_survey_template_page' as '';

DROP PROCEDURE IF EXISTS sp_update_survey_template_page;
DELIMITER // 
CREATE PROCEDURE `sp_update_survey_template_page` (	
	IN updator_login 				VARCHAR(100),
    IN updator_password				VARCHAR(50),
    IN page_id						BIGINT UNSIGNED,
    IN page_title					VARCHAR(50),
    IN page_index					BIGINT UNSIGNED,
    OUT result_id					BIGINT UNSIGNED,
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
    DECLARE pageIndex BIGINT UNSIGNED;
    SET pageIndex = 0;
    
    IF NOT is_user_exists(updator_login, updator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(updator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
    
    UPDATE 	survey_page sp
	SET 	sp.page_title = 	page_title,
			sp.user_modified_id = userId,
			sp.last_modified = NOW(),
            sp.page_index = page_index
	WHERE	
			sp.id = page_id;

	SELECT LAST_INSERT_ID() INTO result_id;
    SELECT 0 INTO error_code;
END//
DELIMITER ;

###################################

select 'sp_remove_survey_template_page' as '';
DROP PROCEDURE IF EXISTS sp_remove_survey_template_page;
DELIMITER // 
CREATE PROCEDURE `sp_remove_survey_template_page` (	
	IN updator_login 				VARCHAR(100),
    IN updator_password				VARCHAR(50),
    IN page_id						BIGINT UNSIGNED,
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
    DECLARE pageIndex BIGINT UNSIGNED;
    SET pageIndex = 0;
    
    IF NOT is_user_exists(updator_login, updator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(updator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
    
	DELETE
	FROM	survey_page
	WHERE	id = page_id;
    
    SELECT 0 INTO error_code;
END//
DELIMITER ;

###################################

