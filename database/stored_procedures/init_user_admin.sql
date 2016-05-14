USE DBSurveys;
select 'procedures/init_user_admin started' as '';

DROP PROCEDURE IF EXISTS add_user;
DELIMITER // 
CREATE PROCEDURE `add_user` (
	IN creator_login 				VARCHAR(100),
	IN target_username 				VARCHAR(100),
	IN target_password 				VARCHAR(100),
	IN target_right_name 			VARCHAR(100)
) 
proc_label:BEGIN
	DECLARE creator_id BIGINT UNSIGNED;
	DECLARE right_id BIGINT UNSIGNED;
	
	SELECT 	u.id 
	INTO 	creator_id
	FROM 	User u
	JOIN	User_Right ur 
		ON	u.user_right_id 	= ur.id
	WHERE 	u.login 			= creator_login
	AND 	ur.access_level 	= 0;

	IF (creator_id IS NULL)
	THEN
		BEGIN
			SELECT 'SecurityError: you dont have rights';
			LEAVE proc_label;
		END;
	END IF;

	IF EXISTS(
			SELECT 1 
			FROM User u
			WHERE u.login = target_username) 
	THEN
		SELECT 'User already exists';
		LEAVE proc_label;
	END IF;

	SELECT 	ur.id
	INTO 	right_id
	FROM	User_Right ur
	WHERE 	ur.name = target_right_name;

	IF (right_id IS NULL) THEN
		BEGIN
			SELECT 'Desired right not found';
			LEAVE proc_label;
		END;
	END IF;

	INSERT 	INTO User (login, password, user_right_id) 
			VALUES (target_username, target_password, right_id);

	SELECT 'SUCCESS';

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