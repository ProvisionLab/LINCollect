USE DBSurveys;

##########################################################
select 'is_user_exists ' as '';
DROP FUNCTION IF EXISTS is_user_exists;
DELIMITER // 
CREATE FUNCTION is_user_exists (
	username 				VARCHAR(100),
	password 				VARCHAR(100)
)  RETURNS INT
BEGIN
	DECLARE result INT;
	SET result = 0;
	
	SELECT 	1 
	INTO	result
	FROM 	User u
	WHERE 	BINARY u.login = username
	AND		BINARY  u.password = password;

	RETURN result;

END;
//
DELIMITER ;

##########################################################
select 'is_user_admin' as '';
DROP FUNCTION IF EXISTS is_user_admin;
DELIMITER // 
CREATE FUNCTION is_user_admin (
	username 				VARCHAR(100)
)  RETURNS INT
BEGIN
	DECLARE result varchar(50);
	SET result = "";
	
	SELECT 	ur.name
	INTO	result
	FROM 	User u
    JOIN	user_right ur ON u.user_right_id = ur.id
	WHERE 	BINARY u.login = username;

	IF result = 'Administrator' THEN
		return 1;
    ELSE
		return 0;
    END IF;

END;
//
DELIMITER ;
