USE DBSurveys;

select 'seed started' as '';

select 'seed user rights' as '';
INSERT INTO User_Right (name, access_level) VALUES ('Administrator', 0);
INSERT INTO User_Right (name, access_level) VALUES ('Enumerator', 1);
INSERT INTO User_Right (name, access_level) VALUES ('Respondent', 2);
		
select * from User_Right;


select 'seed user. Password is "Password"' as '';

INSERT INTO User(login, password, salt, user_right_id, is_deleted) 
	VALUES ('Admin', 'DFD14FF9FA9464A63ADAEF65271E8C32', 20160513102550, 1, 0);
	
INSERT INTO User(login, password, salt, user_right_id, is_deleted) 
	VALUES ('Administrator', '56031FBA0CCBC8D952180EA17B075F09', 20160513102551, 1, 0);
	
INSERT INTO User(login, password, salt, user_right_id, is_deleted) 
	VALUES ('Enumerator', 'BD637497D6B03DB4DF9E69B6DAAC9C98', 20160513102552, 1, 0);
	
INSERT INTO User(login, password, salt, user_right_id, is_deleted) 
	VALUES ('Respondent', 'BD637497D6B03DB4DF9E69B6DAAC9C98', 20160513102552, 1, 0);
select * from User;


select 'field type' as '';
INSERT INTO Survey_Field_Type (field_type) VALUES ('TextBox');
INSERT INTO Survey_Field_Type (field_type) VALUES ('Email');
INSERT INTO Survey_Field_Type (field_type) VALUES ('Checkbox');
INSERT INTO Survey_Field_Type (field_type) VALUES ('List');
INSERT INTO Survey_Field_Type (field_type) VALUES ('Button');
INSERT INTO Survey_Field_Type (field_type) VALUES ('DatePicker');
INSERT INTO Survey_Field_Type (field_type) VALUES ('DropdownList');
INSERT INTO Survey_Field_Type (field_type) VALUES ('RadioButton');
INSERT INTO Survey_Field_Type (field_type) VALUES ('GroupBox');
		
select 'seed finished' as '';
