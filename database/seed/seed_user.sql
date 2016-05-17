select 'seed user. Password is "Password"' as '';

INSERT INTO User(login, password, salt, user_right_id, is_deleted) 
	VALUES ('Admin', 'DFD14FF9FA9464A63ADAEF65271E8C32', 20160513102550, 1, 0);
	
INSERT INTO User(login, password, salt, user_right_id, is_deleted) 
	VALUES ('Administrator', '56031FBA0CCBC8D952180EA17B075F09', 20160513102551, 1, 0);
	
INSERT INTO User(login, password, salt, user_right_id, is_deleted) 
	VALUES ('Enumerator', 'BD637497D6B03DB4DF9E69B6DAAC9C98', 20160513102552, 2, 0);
	
INSERT INTO User(login, password, salt, user_right_id, is_deleted) 
	VALUES ('Respondent', 'BD637497D6B03DB4DF9E69B6DAAC9C98', 20160513102552, 3, 0);
select * from User;
