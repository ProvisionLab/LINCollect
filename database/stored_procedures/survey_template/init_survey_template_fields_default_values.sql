USE DBSurveys;

select 'sp_add_field_default_value' as '';

DROP PROCEDURE IF EXISTS sp_add_field_default_value;
DELIMITER // 
CREATE PROCEDURE `sp_add_field_default_value` (	
	IN creator_login 				VARCHAR(100),
    IN creator_password				VARCHAR(50),
	IN field_id						BIGINT UNSIGNED,
	IN language_id					BIGINT UNSIGNED,
	IN default_value				VARCHAR(50), 
	OUT result_id					BIGINT UNSIGNED,	
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
	DECLARE default_value_id BIGINT UNSIGNED;
    DECLARE cross_table_id BIGINT UNSIGNED;
	SET default_value_id = NULL;
	
    IF NOT is_user_exists(creator_login, creator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(creator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
	
	SELECT 	v.id
	INTO 	default_value_id
	FROM 	Vocabulary v
	WHERE 	v.word = default_value
	AND		v.language_id = language_id;
	
	IF default_value_id IS NULL THEN
		INSERT INTO Vocabulary (word, language_id) 
			VALUES (default_value, language_id);
            
		SELECT LAST_INSERT_ID() INTO default_value_id;		
	END IF;
    
    
    SELECT 	cr.id
    INTO	cross_table_id
    FROM 	survey_field_vocabulary_cross cr
    WHERE	cr.fk_survey_field_id = field_id
    AND		cr.fk_vocabulary_word_id = default_value_id;
    
    IF cross_table_id IS NULL THEN
		INSERT INTO survey_field_vocabulary_cross (fk_survey_field_id, fk_vocabulary_word_id) 
		VALUES (field_id, default_value_id);
        SELECT LAST_INSERT_ID() INTO result_id;
    ELSE
		UPDATE 	survey_field_vocabulary_cross cr
		SET 	cr.fk_vocabulary_word_id = default_value_id				
		WHERE	cr.id = cross_table_id;
        SELECT cross_table_id INTO result_id;
    END IF;
	
END//
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_update_field_default_value;
DELIMITER // 
CREATE PROCEDURE `sp_update_field_default_value` (	
	IN creator_login 				VARCHAR(100),
    IN creator_password				VARCHAR(50),
	IN field_id						BIGINT UNSIGNED,
	IN language_id					BIGINT UNSIGNED,
	IN old_default_value			VARCHAR(50),
	IN new_default_value			VARCHAR(50), 
    OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
	DECLARE old_default_value_id BIGINT UNSIGNED;
	DECLARE current_cross_value_id BIGINT UNSIGNED;
	DECLARE new_default_value_id BIGINT UNSIGNED;
	
    IF NOT is_user_exists(creator_login, creator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(creator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
	
	SELECT 	cr.id
	INTO 	current_cross_value_id
	FROM 	survey_field_vocabulary_cross cr
	JOIN	Vocabulary v ON v.id = cr.fk_vocabulary_word_id
	WHERE 	v.word = old_default_value 
	LIMIT 	1;
	
	SELECT 	v.id
	INTO 	new_default_value_id
	FROM	Vocabulary v
	WHERE 	v.word = new_default_value
	AND		v.language_id = language_id;
	
	IF new_default_value_id IS NULL THEN
		INSERT INTO Vocabulary (word, language_id) 
			VALUES (new_default_value, language_id);
		SET new_default_value_id = LAST_INSERT_ID();		
	END IF;
	
	IF current_cross_value_id IS NULL THEN
		INSERT INTO survey_field_vocabulary_cross (fk_survey_field_id, fk_vocabulary_word_id) 
			VALUES (field_id, new_default_value_id);
	ELSE
		UPDATE 	survey_field_vocabulary_cross cr
		SET		cr.fk_vocabulary_word_id = new_default_value_id
		WHERE	cr.id = current_cross_value_id;
	END IF;
		SELECT 0 INTO error_code;	
	
	
END//
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_remove_field_default_value;
DELIMITER // 
CREATE PROCEDURE `sp_remove_field_default_value` (	
	IN creator_login 				VARCHAR(100),
    IN creator_password				VARCHAR(50),
	IN field_id						BIGINT UNSIGNED,
	IN default_value_to_remove		VARCHAR(50),
	OUT error_code					BIGINT UNSIGNED
) 
proc_label:BEGIN
	
    DECLARE wordId BIGINT UNSIGNED;
    
    IF NOT is_user_exists(creator_login, creator_password) THEN
		SELECT 2 INTO error_code;
        LEAVE proc_label;
    END IF;
    IF NOT is_user_admin(creator_login) THEN
		SELECT 1 INTO error_code;
        LEAVE proc_label;
    END IF;
    
    
    SELECT 	v.id
    INTO 	wordId
    FROM 	Vocabulary v
    WHERE 	v.word = default_value_to_remove; 
	
	DELETE 
	FROM 	survey_field_vocabulary_cross 
	WHERE 	fk_vocabulary_word_id = wordId;
	
END//
DELIMITER ;

###################################
