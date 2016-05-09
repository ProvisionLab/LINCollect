USE DBSurveys;
select 'is_user_exists started' as '';

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
	FROM 	Users u
	WHERE 	BINARY u.login = username
	AND		BINARY  u.password = password;

	RETURN result;

END;
//
DELIMITER ;

SELECT is_user_exists('Administrator', 'Password') as result;
SELECT is_user_exists('Administrator', 'PASSword') as result;
SELECT is_user_exists('ADMINistrator', 'PASSword') as result;
SELECT is_user_exists('Administrator', 'not-password') as result;
SELECT is_user_exists('Administrator', '') as result;
SELECT is_user_exists('not-exist-Administrator', 'password') as result;

select 'is_user_exists executed' as '';
