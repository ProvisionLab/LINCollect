USE DBSurveys;


select 'vw_user' as '';


DROP VIEW IF EXISTS vw_user;

CREATE VIEW vw_user 	as 
SELECT 
	u.Id 				as 'Id',
    u.login 			as 'Login',
    u.password 			as 'Password',
    u.salt 				as 'Salt',
    ur.id			 	as 'UserRightId',
    ur.name 			as 'UserRight',
    ur.access_level		as 'AccessLevel'
 from user u
 JOIN user_right ur 
	ON u.user_right_id = ur.id;
 