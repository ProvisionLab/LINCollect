USE DBSurveys;

select 'user' as '';
CREATE TABLE IF NOT EXISTS User(
	id 					SERIAL,
	login 				VARCHAR(20) UNIQUE NOT NULL,
	password			varchar(32) NOT NULL,
	salt				varchar(32) NOT NULL,
	user_right_id 		BIGINT UNSIGNED NOT NULL,
	is_deleted			TINYINT not null default 0,
	PRIMARY KEY (id),
	FOREIGN KEY (user_right_id) REFERENCES User_Right (id)
	ON DELETE RESTRICT
);