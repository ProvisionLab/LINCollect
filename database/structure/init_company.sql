USE DBSurveys;

select 'country' as '';
CREATE TABLE IF NOT EXISTS Country(
	id 				SERIAL,
	name			varchar(50) UNIQUE NOT NULL,
	PRIMARY KEY (id)
	
);

select 'city' as '';
CREATE TABLE IF NOT EXISTS City(
	id 				SERIAL,
	name			varchar(50) NOT NULL,
	country_id		BIGINT UNSIGNED NOT NULL,

	PRIMARY KEY (id),	
	FOREIGN KEY (country_id) REFERENCES Country (id)
	ON DELETE RESTRICT
);

select 'company' as '';
CREATE TABLE IF NOT EXISTS Company(
	id				SERIAL,
	name			VARCHAR(50) UNIQUE,
	city_id			BIGINT UNSIGNED NOT NULL,
	address			VARCHAR(500) NOT NULL,
	phone_number	VARCHAR(30) NOT NULL,
	postal_code		VARCHAR(30) NOT NULL,

	PRIMARY KEY (id),	
	FOREIGN KEY (city_id) REFERENCES City (id)
	ON DELETE RESTRICT
);