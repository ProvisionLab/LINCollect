USE DBSurveys;

select 'user_right' as '';
CREATE TABLE IF NOT EXISTS User_Right(
	id 					SERIAL,
	last_modified 		DATE NULL,
	name 				VARCHAR(50) NOT NULL UNIQUE,
	access_level		VARCHAR(50) NOT NULL UNIQUE,
	PRIMARY KEY (id)
);