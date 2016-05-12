USE DBSurveys;

select 'seed started' as '';

select 'seed user rights' as '';
INSERT INTO User_Right (name, access_level) VALUES ('Administrator', 0);
INSERT INTO User_Right (name, access_level) VALUES ('Enumerator', 1);
INSERT INTO User_Right (name, access_level) VALUES ('Respondent', 2);
		
select * from User_Right;

#TODO: hardcode salt and password hashes
# select * from User;

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
