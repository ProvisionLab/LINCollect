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
