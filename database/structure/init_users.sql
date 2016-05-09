USE DBSurveys;
select 'structure/init_users started' as '';

CREATE TABLE IF NOT EXISTS User_Right(
	id 					SERIAL,
	last_modified 		DATE NULL,
	name 				VARCHAR(50) NOT NULL UNIQUE,
	access_level		INT NOT NULL UNIQUE,
	PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS User(
	id 					SERIAL,
	login 				VARCHAR(20) UNIQUE NOT NULL,
	password			varchar(32) NOT NULL,
	salt				varchar(32) NOT NULL,
	user_right_id 		BIGINT UNSIGNED NOT NULL,
	is_deleted			TINYINT not null default 0,
	PRIMARY KEY (id),
	FOREIGN KEY (user_right_id) REFERENCES User_Right (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

select 'structure/init_users executed' as '';