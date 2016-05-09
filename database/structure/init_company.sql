USE DBSurveys;
select 'structure/init_company started' as '';

CREATE TABLE IF NOT EXISTS Country(
	id 				SERIAL,
	word_id			BIGINT UNSIGNED NOT NULL UNIQUE,

	PRIMARY KEY (id),	
	FOREIGN KEY (word_id) REFERENCES Vocabulary (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS City(
	id 				SERIAL,
	word_id			BIGINT UNSIGNED NOT NULL,
	country_id		BIGINT UNSIGNED NOT NULL,

	PRIMARY KEY (id),	
	FOREIGN KEY (word_id) REFERENCES Vocabulary (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (country_id) REFERENCES Country (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Company(
	id				SERIAL,
	name			VARCHAR(50),
	country_id		BIGINT UNSIGNED NOT NULL,
	city_id			BIGINT UNSIGNED NOT NULL,
	address			VARCHAR(500),
	phone_number	VARCHAR(30),
	postal_code		VARCHAR(30),

	PRIMARY KEY (id),	
	FOREIGN KEY (city_id) REFERENCES City (id)
	ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (country_id) REFERENCES Country (id)
	ON DELETE RESTRICT ON UPDATE CASCADE
);

select 'structure/init_company executed' as '';