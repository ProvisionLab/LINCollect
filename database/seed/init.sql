USE DBSurveys;

select 'seed started' as '';

select 'seed user rights' as '';
INSERT INTO User_Right (name, access_level) VALUES ('Administrator', 0);
INSERT INTO User_Right (name, access_level) VALUES ('Enumerator', 1);
INSERT INTO User_Right (name, access_level) VALUES ('Respondent', 2);


		
select * from User_Right;
select * from User;
		
select 'seed finished' as '';
