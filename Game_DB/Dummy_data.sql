USE Awakening_realm;

-- Effect type, effect description
INSERT INTO Effect (Effect_type, effect_description) VALUES
('Effect 1', 'Increases the healing of the healers for 1 round and reduces two energy gage of the enemy player.'),
('Effect 2', 'Ignore the defense of one of the enemy cards placed for 1 round 
(ex. if the defense is 30 this ability would ignore the defense and apply the damage direct to the health of the player)'),
('Effect 3', 'Can dodge one of the enemys card attacks'),
('Effect 4', 'Applies to the enemy a dot damage of 10 attack and the healing is 50% less effective for 3 rounds (if the enemy places a card that heals 20, it just heals 10)'),
('Effect 5', 'Creates a barrier for the alies that gives 50 defense for 2 rounds'),
('Effect 6', 'Debuf the enemy making the attacks 20% weaker for 2 rounds and life steal 30 life points of the enemy (The life steal effect passes the amount of life steal from the enemy player to the player, in this case 30 would be decreasing to the enemy players health and 30 would be increasing to the player)'),
('Effect 7', 'Reflect all damage taken for 1 round and also heals 10 life points for 3 rounds'),
('Effect 8', 'Double the damage of the ally cards for 1 round and curse the enemy causing 10 damage over time and 20% healing reduction for 2 rounds');

-- Insertion of common cards across realms

-- Human Realm
INSERT INTO Cards (card_name, card_description, attack, defense, healing, card_realm, power_cost, rarity, Effect_type) VALUES
('Warrior', 'A sturdy soldier with balanced attack and defense.', 5, 5, 5, 'Human', 0, 'Common', NULL),
('Archer', 'Quick and precise, excellent at dealing damage from a distance.', 10, 5, 0, 'Human', 1, 'Common', NULL),
('Cleric', 'A dedicated healer, vital for sustaining the team.', 2, 3, 10, 'Human', 0, 'Common', NULL),
('Knight', 'Clad in armor, great at absorbing attacks.', 3, 10, 2, 'Human', 0, 'Common', NULL),
('Sorceress', 'Wields magical attacks, but fragile.', 12, 0, 3, 'Human', 0, 'Common', NULL),
('Royal Guard', 'Elite defender with high defense.', 6, 9, 0, 'Human', 0, 'Common', NULL),
('Assasin', 'Deadly in attack with a focus on quick, lethal strikes.', 15, 0, 0, 'Human', 0, 'Common', NULL),
('Mercenary', 'Versatile fighter with moderate attack and healing capabilities.', 6, 4, 5, 'Human', 0, 'Common', NULL),

-- Monster Realm
('Ogre', 'Brutish and strong, good in both offense and defense.', 4, 6, 5, 'Monster', 0, 'Common', NULL),
('Basilisc', 'Mystical creature that can petrify enemies, balanced skills.', 3, 6, 6, 'Monster', 0, 'Common', NULL),
('Werewolf', 'Fierce and resilient, a threat in close combat.', 7, 3, 5, 'Monster', 0, 'Common', NULL),
('Goblin', 'Tricky and elusive, surprisingly effective at healing.', 8, 2, 5, 'Monster', 0, 'Common', NULL),
('Harpies', 'Agile and dangerous, balanced attributes with a touch of healing.', 13, 1, 1, 'Monster', 0, 'Common', NULL),
('Troll', 'Tough and hardy, capable of withstanding considerable damage.', 0, 12, 3, 'Monster', 0, 'Common', NULL),
('Treant', 'Ancient and wise, focuses on healing and protection.', 1, 1, 13, 'Monster', 0, 'Common', NULL),
('Ghoul', 'Menacing and aggressive, excellent at dealing damage.', 11, 4, 0, 'Monster', 0, 'Common', NULL),

-- Magical Realm
('Giant', 'Imposing force, extremely tough.', 0, 12, 3, 'Magical', 0, 'Common', NULL),
('Elf', 'Graceful and quick, adept at both attack and defense.', 14, 1, 0, 'Magical', 0, 'Common', NULL),
('Necromancer', 'Masters of the dark arts, can both harm and heal.', 12, 2, 1, 'Magical', 0, 'Common', NULL),
('Golem', 'Constructed titan, almost impervious to damage.', 2, 12, 1, 'Magical', 0, 'Common', NULL),
('Elemental Guardian', 'Powerful protector, balanced in attack and defense with healing.', 5, 5, 5, 'Magical', 0, 'Common', NULL),
('Dwarves', 'Stout and sturdy, good at defending and healing.', 4, 8, 3, 'Magical', 0, 'Common', NULL),
('Witch', 'Skilled in curses and healing, a formidable foe.', 10, 3, 2, 'Magical', 0, 'Common', NULL),
('Dark Wizard', 'Deals in forbidden spells, good at damage and healing.', 8, 0, 7, 'Magical', 0, 'Common', NULL),

-- Celestial Realm
('Hell Hound', 'Ferocious and relentless in attack.', 12, 2, 1, 'Celestial', 0, 'Common', NULL),
('Archangel', 'Mighty and majestic, strong in combat.', 9, 1, 5, 'Celestial', 0, 'Common', NULL),
('Nephalem', 'Offspring of angels and demons, versatile in battle.', 6, 4, 5, 'Celestial', 0, 'Common', NULL),
('Succubus', 'Seduces and deceives, moderate in defense and healing.', 1, 5, 9, 'Celestial', 0, 'Common', NULL),
('Nymph', 'Gentle and healing, crucial support role.', 0, 1, 14, 'Celestial', 0, 'Common', NULL),
('Titan', 'Giant warriors, formidable in defense and healing.', 1, 14, 0, 'Celestial', 0, 'Common', NULL),
('Valkyrie', 'Warrior maidens from the skies, balanced attack and moderate healing.', 12, 1, 2, 'Celestial', 0, 'Common', NULL),
('Pegasus', 'Swift and agile, great for quick strikes.', 4, 6, 5, 'Celestial', 0, 'Common', NULL);

-- Insertion of legendary cards with specific effects
INSERT INTO Cards (card_name, card_description, attack, defense, healing, card_realm, power_cost, rarity, Effect_type) VALUES
('Princess', 'A royal healer with unparalleled powers of restoration.', 10, 10, 15, 'Human', 4, 'Legendary', 'Effect 1'),
('King Arthur', 'The legendary king with a mighty sword and armor.', 30, 20, 0, 'Human', 3, 'Legendary', 'Effect 2'),
('Griffin', 'Majestic creature that commands the skies, dodges attacks effortlessly.', 10, 30, 10, 'Monster', 3, 'Legendary', 'Effect 3'),
('Dragon', 'Fearsome and fiery, deals devastating damage over time.', 30, 10, 10, 'Monster', 4, 'Legendary', 'Effect 4'),
('Runeforge Dwarf', 'Master craftsman, provides formidable defenses and repairs.', 5, 35, 10, 'Magical', 3, 'Legendary', 'Effect 5'),
('Moonshadow Elf', 'Elusive and deadly, weakens enemies while stealing their life force.', 32, 8, 10, 'Magical', 4, 'Legendary', 'Effect 6'),
('Hercules', 'The hero of myths, reflects damage and heals over time.', 18, 16, 16, 'Celestial', 3, 'Legendary', 'Effect 7'),
('Demon King', 'Ruler of the underworld, enhances damage and curses enemies.', 37, 5, 8, 'Celestial', 4, 'Legendary', 'Effect 8');

-- player name, last name, age, email, realm, is npc, player exp, win, lost, coins, exp points
INSERT INTO Players (player_name, player_last_name, player_age, user_name, password, realm, is_npc, level, win_record, lose_record, coins, token) VALUES
('AI', 'AI', 30, 'AI', 'AI', 'Human', true, 100 , 100, 0, 10000, 1),
('John', 'Doe', 21, 'john.doe', 'password123', 'Human', false, 8, 10, 5, 10000, 2),
('Alberto', 'Limón', 21, 'lemon', 'lemoncito', 'Monster', false, 7, 14, 7, 1000, 2),
('Do Kyu', 'Han Kim', 21, 'dokyu', '1234', 'Celestial', false, 8, 9, 8, 10000, 2),
('Gabriel', 'Edid', 21, 'Atrium20', 'gaboMagic', 'Magical', false, 7, 21, 7, 1000, 2),
('Jane', 'Smith', 21, 'jane.smith', 'password123', 'Monster', false, 2, 5, 5, 15, 1),
('Alice', 'Johnson', 21, 'alice.johnson', 'password123', 'Magical', false, 2, 12, 6, 20, 0),
('Bob', 'Williams', 18, 'bob.williams', 'password123', 'Celestial', false, 3, 14, 9, 24, 2),
('Emily', 'Brown', 22, 'emily.brown', 'password123', 'Human', false, 4, 11, 7, 10, 3),
('David', 'Miller', 22, 'david.miller', 'password123', 'Monster', false, 5, 13, 10, 30, 0),
('Sarah', 'Wilson', 22, 'sarah.wilson', 'password123', 'Monster', false, 3, 16, 12, 26, 0),
('James', 'Taylor', 23, 'james.taylor', 'password123', 'Celestial', false, 1, 10, 5, 32, 1),
('Laura', 'Anderson', 23, 'laura.anderson', 'password123', 'Celestial', false, 1, 9, 4, 35, 2),
('Michael', 'Thomas', 24, 'michael.thomas', 'password123', 'Celestial', false, 1, 17, 11, 100, 1),
('Emma', 'Jackson', 25, 'emma.jackson', 'password123', 'Monster', false, 2, 15, 9, 20, 3),
('Chris', 'White', 26, 'chris.white', 'password123', 'Magical', false, 6, 8, 3, 120, 1),
('Olivia', 'Harris', 26, 'olivia.harris', 'password123', 'Monster', false, 5, 18, 13, 20, 1),
('Daniel', 'Martin', 19, 'daniel.martin', 'password123', 'Human', false, 3, 12, 6, 10, 2),
('Sophia', 'Lee', 19, 'sophia.lee', 'password123', 'Monster', false, 2, 7, 2, 40, 0),
('Kevin', 'Clark', 19, 'kevin.clark', 'password123', 'Monster', false, 2, 19, 14, 35, 1),
('Isabella', 'Lewis', 19, 'isabella.lewis', 'password123', 'Human', false, 1, 6, 1, 50, 0),
('Ethan', 'Walker', 30, 'ethan.walker', 'password123', 'Monster', false, 6, 16, 10, 35, 1),
('Natalie', 'Allen', 28, 'natalie.allen', 'password123', 'Magical', false, 1, 20, 15, 95, 0),
('Liam', 'Young', 28, 'liam.young', 'password123', 'Human', false, 4, 14, 8, 10, 1);


-- deck name, description, ammount
INSERT INTO Deck (card_ID, player_ID) VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(3, 2),
(6, 2),
(7, 2),
(8, 2),
(23, 3);

-- game level, scene, game duration
INSERT INTO Game (game_level, game_scene, game_duration, game_turns) VALUES
(1, 'Scene 1', '01:00:00', 6),
(2, 'Scene 2', '01:30:00', 7),
(3, 'Scene 3', '00:45:00', 8),
(4, 'Scene 4', '02:00:00', 10),
(5, 'Scene 1', '01:15:00', 14),
(6, 'Scene 2', '00:30:00', 4),
(7, 'Scene 3', '02:30:00', 9),
(8, 'Scene 4', '01:45:00', 6);

INSERT INTO Inventory (card_ID, player_ID) VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(6, 1),
(1, 2),
(3, 2),
(10, 2),
(2, 3),
(3, 3),
(7, 3);


INSERT INTO Deck (card_ID, player_ID)
SELECT card_ID, player_ID
FROM (
    SELECT c.card_ID, p.player_ID
    FROM Cards AS c
    CROSS JOIN Players AS p
    WHERE c.card_ID <= 20 AND p.player_ID <= 20
    ORDER BY RAND()
    LIMIT 200  -- Adjust the number for more or fewer entries
) AS sub;

-- Generate entries for Inventory
INSERT INTO Inventory (card_ID, player_ID, deck_ID)
SELECT card_ID, player_ID, deck_ID
FROM (
    SELECT c.card_ID, p.player_ID, d.deck_ID
    FROM Cards AS c
    CROSS JOIN Players AS p
    JOIN Deck AS d ON p.player_ID = d.player_ID
    WHERE c.card_ID <= 40 AND p.player_ID <= 20 AND d.deck_ID <= 20
    ORDER BY RAND()
    LIMIT 200  -- Adjust the number for more or fewer entries
) AS sub;


-- Add dummy data for players
DELIMITER $$

CREATE PROCEDURE AddDummyPlayers()
BEGIN
    DECLARE v_max INT DEFAULT 100;
    DECLARE v_counter INT DEFAULT 0;

    WHILE v_counter < v_max DO
        INSERT INTO Players (
            player_name, 
            player_last_name, 
            player_age, 
            user_name, 
            password, 
            realm, 
            is_npc, 
            level, 
            win_record, 
            lose_record, 
            coins, 
            token
        )
        VALUES (
            CONCAT('Name', v_counter), 
            'Surname', 
            FLOOR(17 + RAND() * (30-17)), 
            CONCAT('user', v_counter), 
            'password123', 
            CASE 
                WHEN v_counter % 4 = 0 THEN 'Human'
                WHEN v_counter % 4 = 1 THEN 'Monster'
                WHEN v_counter % 4 = 2 THEN 'Magical'
                ELSE 'Celestial'
            END, 
            v_counter % 2 = 0, 
            FLOOR(1 + RAND() * 8), 
            FLOOR(RAND() * 100), 
            FLOOR(RAND() * 100), 
            FLOOR(100 + RAND() * 10000), 
            FLOOR(RAND() * 100)
        );
        SET v_counter = v_counter + 1;
    END WHILE;
END$$

DELIMITER ;

-- Call the stored procedure
CALL AddDummyPlayers();


