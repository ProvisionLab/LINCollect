USE DBSurveys;

select 'sp_add_user' as '';
DROP PROCEDURE IF EXISTS sp_add_user;
DELIMITER // 
CREATE PROCEDURE `sp_add_user` (
	IN creator_login 				VARCHAR(100),
	IN creator_password				VARCHAR(100),
	IN target_username 				VARCHAR(100),
	IN target_password 				VARCHAR(100),
	IN target_salt					VARCHAR(100),
	IN target_right_id 				BIGINT UNSIGNED,
	OUT result_id					BIGINT UNSIGNED,
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
	
	IF EXISTS(
			SELECT 1 
			FROM User u
			WHERE u.login = target_username) 
	THEN
		SELECT 'User already exists';
		LEAVE proc_label;
	END IF;

	INSERT 	INTO User (login, password, salt, user_right_id) 
			VALUES (target_username, target_password, target_salt, target_right_id);
			
	SELECT LAST_INSERT_ID() INTO result_id;

END//

select 'sp_update_user' as '';//
CREATE PROCEDURE `sp_update_user` (
	IN creator_login 				VARCHAR(100),
	IN creator_password				VARCHAR(100),
	IN target_id					BIGINT UNSIGNED,
	IN target_username 				VARCHAR(100),
	IN target_password 				VARCHAR(100),
	IN target_salt					VARCHAR(100),
	IN target_right_id 				BIGINT UNSIGNED,
	IN target_right_name 			VARCHAR(100),
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
	
	IF EXISTS(
			SELECT 1 
			FROM User u
			WHERE u.login = target_username) 
	THEN
		SELECT 'User already exists';
		LEAVE proc_label;
	END IF;
	
	UPDATE 	User u
	SET 	u.login = target_username,
			u.password = target_password,
			u.salt = target_salt,
			u.user_right_id = target_right_id
	WHERE 
		u.id = target_id;
		
	SELECT 0 INTO error_code;

END//

select 'sp_add_language_to_user' as '';//
CREATE PROCEDURE `sp_add_language_to_user` (
	IN creator_login 				VARCHAR(100),
	IN creator_password				VARCHAR(100),
	IN target_username 				VARCHAR(100),
	IN language_id	 				BIGINT UNSIGNED,
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
	
	SELECT 'TODO' as '';
	
	SELECT 0 INTO error_code;

END//

DELIMITER ;


#call add_user('Administrator', 'Respondent', 'password', '12324324234Respondent');
#call add_user('Respondent', 'new_respondent', 'password', 'Respondent');
#call add_user('new_respondent', 'new_respondent', 'password', 3);
#call add_user('Administrator', 'Respondent2', 'password', 'Respondent');
#call add_user('Administrator', 'Enumerator2', 'password', 'Enumerator');
#call add_user('Administrator', 'void_rights', 'password', 'not_existing_right');

#select * from user;

select 'procedures/init_user_admin executed' as '';