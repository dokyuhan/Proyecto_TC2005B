DROP SCHEMA IF EXISTS Awakening_realm;
CREATE SCHEMA Awakening_realm;
USE Awakening_realm;

CREATE TABLE Effect (
	Effect_type VARCHAR(255) NOT NULL,
    creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
	last_modified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    effect_description VARCHAR(500),
    PRIMARY KEY (Effect_type)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE Cards (
	card_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
	creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
	last_modified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	card_name VARCHAR(255) NOT NULL,
	card_description VARCHAR(255) NOT NULL,
	attack INT NOT NULL, 
	defense INT NOT NULL, 
	healing INT NOT NULL,
    card_realm VARCHAR(50) NOT NULL,
	power_cost INT NOT NULL,
	exp_cost INT NOT NULL,
	rarity VARCHAR(20) NOT NULL,
	card_level INT NOT NULL,
    Effect_type VARCHAR(255),
	PRIMARY KEY (card_ID),
    
    FOREIGN KEY (Effect_type) REFERENCES Effect (Effect_type)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE Player (
	player_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
	creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
	last_modified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	player_name VARCHAR(255) NOT NULL,
	player_last_name VARCHAR(255) NOT NULL,
	player_age INT NOT NULL,
	player_email VARCHAR(255) NOT NULL,
	realm VARCHAR(50) NOT NULL,
	is_npc VARCHAR(50) NOT NULL,
	player_exp INT NOT NULL,
	win_record INT NOT NULL,
	lose_record INT NOT NULL,
    coins INT NOT NULL,
    exp_points INT NOT NULL,
	PRIMARY KEY (player_ID)
    
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE Deck (
	deck_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
	creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
	last_modified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	deck_name VARCHAR(255) NOT NULL,
	deck_description VARCHAR(255) NOT NULL,
	card_ammount INT NOT NULL,
	PRIMARY KEY (deck_ID)
    
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE Inventory (
	inventory_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
	creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
	last_modified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	card_ID INT UNSIGNED,
	player_ID INT UNSIGNED,
	deck_ID INT UNSIGNED,
	PRIMARY KEY (inventory_ID),

	FOREIGN KEY (card_ID) REFERENCES Cards (card_ID) ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY (player_ID) REFERENCES Player (player_ID) ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY (deck_ID) REFERENCES Deck (deck_ID) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE Game (
	game_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
	creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
	last_modified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	player_ID_1 INT UNSIGNED,
	player_ID_2 INT UNSIGNED,
	winner_ID INT UNSIGNED,
	inventory_ID INT UNSIGNED,
	game_level INT NOT NULL,
	game_scene VARCHAR(255) NOT NULL,
	game_duration TIME,
	PRIMARY KEY (game_ID),

	FOREIGN KEY (player_ID_1) REFERENCES Player (player_ID) ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY (player_ID_2) REFERENCES Player (player_ID) ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY (winner_ID) REFERENCES Player (player_ID) ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY (inventory_ID) REFERENCES Inventory (inventory_ID) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE Turn (
	turn_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
	creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
	last_modified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	game_ID INT UNSIGNED,
	turn_duration TIME,
	turn_status VARCHAR(20) NOT NULL,
	PRIMARY KEY (turn_ID),

	FOREIGN KEY (game_ID) REFERENCES Game (game_ID) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;


CREATE TABLE Hand_composition (
	hand_composition_ID INT UNSIGNED NOT NULL AUTO_INCREMENT,
    creation_date DATETIME DEFAULT CURRENT_TIMESTAMP,
	last_modified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	turn_ID INT UNSIGNED,
	PRIMARY KEY (hand_composition_ID),

	FOREIGN KEY (turn_ID) REFERENCES Turn (turn_ID) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;


-- View de tablas de las cartas legendarias
CREATE VIEW Cartas_legendarias AS
SELECT c.card_name, c.card_description, c.attack, c.defense, c.healing, c.card_realm, c.power_cost, c.exp_cost, c.rarity, c.card_level, e.Effect_type, e.effect_description
FROM Cards AS c
LEFT JOIN Effect AS e USING (Effect_type)
WHERE c.rarity IN ('Legendary');

-- View de tablas de las cartas especiales
CREATE VIEW Cartas_especiales AS
SELECT c.card_name, c.card_description, c.card_realm, c.power_cost, c.exp_cost, c.rarity, c.card_level, e.Effect_type, e.effect_description
FROM Cards AS c
LEFT JOIN Effect AS e USING (Effect_type)
WHERE c.rarity IN ('Special');
