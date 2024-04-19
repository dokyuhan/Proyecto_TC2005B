USE Awakening_realm;

-- Effect type, effect description
INSERT INTO Effect (Effect_type, effect_description) VALUES
('Effect 1', 'Doubles the healing of the healers for 1 round and reduces two energy gage of the enemy player.'),
('Effect 2', 'Ignore the defense of one of the enemy cards placed for 1 round 
(ex. if the defense is 30 this ability would ignore the defense and apply the damage direct to the health of the player)'),
('Effect 3', 'Can dodge one of the enemys card attacks'),
('Effect 4', 'Applies to the enemy a dot damage of 10 attack and the healing is 50% less effective for 3 rounds (if the enemy places a card that heals 20, it just heals 10)'),
('Effect 5', 'Creates a barrier for the alies that gives 50 defense for 2 rounds'),
('Effect 6', 'Debuf the enemy making the attacks 20% weaker for 2 rounds and life steal 30 life points of the enemy (The life steal effect passes the amount of life steal from the enemy player to the player, in this case 30 would be decreasing to the enemy players health and 30 would be increasing to the player)'),
('Effect 7', 'Reflect all damage taken for 1 round and also heals 10 life points for 3 rounds'),
('Effect 8', 'Double the damage of the ally cards for 1 round and curse the enemy causing 10 damage over time and 20% healing reduction for 2 rounds');
-- ('Effect 9', 'Gives the player an Extra energy'),
-- ('Effect 10', 'The attack of the card placed deals x2 for one turn'),
-- ('Effect 11', 'Posion the enemy player for 2 rounds dealing 20 damage'),
-- ('Effect 12', 'Heals the player 30 health'),
-- ('Effect 13', 'Cures all debuf applied to the player'),
-- ('Effect 14', 'Enhance shields played x2'),
-- ('Effect 15', 'Reduces enemy attack to 50%'),
-- ('Effect 16', 'Damage taken will be reflected to the enemy'),
-- ('Effect 17', 'Disables an enemy card played'),
-- ('Effect 18', 'You can see the opponents cards');

-- card name, card description, attack, defense, healing, card realm, power cost, exp cost, rarity, effect
INSERT INTO Cards (card_name, card_description, attack, defense, healing, card_realm, power_cost, rarity, Effect_type) VALUES

-- Common Cards

-- Human
('Warrior', 'Description', 10, 10, 0, 'Human', 0, 'Common', NULL),
('Archer', 'Description', 15, 5, 0, 'Human', 0, 'Common', NULL),
('Cleric', 'Description', 2, 2, 15, 'Human', 0, 'Common', NULL),
('Knight', 'Description', 5, 15, 0, 'Human', 0,  'Common', NULL),
('Sorceress', 'Description', 10, 1, 0, 'Human', 0,  'Common', NULL),
('Royal Guard', 'Description', 12, 20, 0, 'Human', 0, 'Common', NULL),
('Assasin', 'Description', 25, 1, 0, 'Human', 0, 'Common', NULL),
('Mercenary', 'Description', 10, 4, 5, 'Human', 0, 'Common', NULL),

-- Monster
('Ogre', 'Description', 7, 10, 2, 'Monster', 0, 'Common', NULL),
('Basilisc', 'Description', 12, 7, 5, 'Monster', 0, 'Common', NULL),
('Werewolf', 'Description', 10, 12, 3, 'Monster', 0, 'Common', NULL),
('Goblin', 'Description', 3, 5, 10, 'Monster', 0, 'Common', NULL),
('Harpies', 'Description', 10, 10, 9, 'Monster', 0, 'Common', NULL),
('Troll', 'Description', 6, 15, 4, 'Monster', 0, 'Common', NULL),
('Treant', 'Description', 2, 6, 15, 'Monster', 0, 'Common', NULL),
('Ghoul', 'Description', 15, 10, 0, 'Monster', 0, 'Common', NULL),

-- Magical
('Giant', 'Description', 5, 15, 0, 'Magical', 0, 'Common', NULL),
('Elf', 'Description', 10, 5, 0, 'Magical', 0, 'Common', NULL),
('Necromancer', 'Description', 15, 2, 10, 'Magical', 0, 'Common', NULL),
('Golem', 'Description', 1, 20, 0, 'Magical', 0, 'Common', NULL),
('Elemental Guardian', 'Description', 12, 12, 9, 'Magical', 0, 'Common', NULL),
('Dwarves', 'Description', 8, 12, 6, 'Magical', 0, 'Common', NULL),
('Witch', 'Description', 14, 4, 10, 'Magical', 0, 'Common', NULL),
('Dark Wizard', 'Description', 7, 7, 13, 'Magical', 0, 'Common', NULL),

-- Celestial
('Hell Hound', 'Description', 10, 8, 2, 'Celestial', 0, 'Common', NULL),
('Archangel', 'Description', 14, 10, 0, 'Celestial', 0, 'Common', NULL),
('Nephalem', 'Description', 10, 5, 10, 'Celestial', 0, 'Common', NULL),
('Succubus', 'Description', 9, 12, 4, 'Celestial', 0, 'Common', NULL),
('Nymph', 'Description', 0, 2, 20, 'Celestial', 0, 'Common', NULL),
('Titan', 'Description', 1, 15, 5, 'Celestial', 0, 'Common', NULL),
('Valkyrie', 'Description', 8, 4, 9, 'Celestial', 0, 'Common', NULL),
('Pegasus', 'Description', 14, 0, 2, 'Celestial', 0, 'Common', NULL),

-- Legendary Cards

-- Human
('Princess', 'Description', 10, 10, 40, 'Human', 2, 'Legendary', 'Effect 1'),
('King Arthur', 'Description', 35, 25, 15, 'Human', 3, 'Legendary', 'Effect 2'),

-- Monster
('Griffin', 'Description', 30, 30, 10, 'Monster', 2, 'Legendary', 'Effect 3'),
('Dragon', 'Description', 40, 20, 10, 'Monster', 3, 'Legendary', 'Effect 4'),

-- Magical
('Runeforge Dwarf', 'Description', 15, 40, 20, 'Magical', 2, 'Legendary', 'Effect 5'),
('Moonshadow Elf', 'Description', 35, 15, 20, 'Magical', 3, 'Legendary', 'Effect 6'),

-- Celestial
('Hercules', 'Description', 35, 25, 20, 'Celestial', 2, 'Legendary', 'Effect 7'),
('Demon King', 'Description', 35, 15, 30, 'Celestial', 3, 'Legendary', 'Effect 8');

-- Special Cards
-- ('Special 1', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 9'),
-- ('Special 2', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 10'),
-- ('Special 3', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 11'),
-- ('Special 4', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 12'),
-- ('Special 5', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 13'),
-- ('Special 6', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 14'),
-- ('Special 7', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 15'),
-- ('Special 8', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 16'),
-- ('Special 9', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 17'),
-- ('Special 10', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 18');

-- player name, last name, age, email, realm, is npc, player exp, win, lost, coins, exp points
INSERT INTO Players (player_name, player_last_name, player_age, user_name, password, realm, is_npc, level, win_record, lose_record, coins, token) VALUES
('John', 'Doe', 21, 'john.doe', 'password123', 'Human', false, 1, 10, 5, 10000, 2),
('Alberto', 'Limón', 21, 'lemon', 'lemoncito', 'Monster', false, 7, 14, 7, 1000, 2),
('Do Kyu', 'Han Kim', 21, 'DoKyu', 'hanKim', 'Celestial', false, 8, 9, 8, 10000, 2),
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

DELETE FROM Inventory WHERE player_ID = 2;
SELECT * FROM Inventory where player_ID = 21;

UPDATE Players
SET coins = coins + 5000
WHERE player_ID = 21;

SELECT * FROM Game where game_ID = 9;

SELECT 
    c.card_name, 
    COUNT(d.card_ID) AS usage_count
FROM 
    Deck d
JOIN 
    Cards c ON d.card_ID = c.card_ID
GROUP BY 
    c.card_name
ORDER BY 
    usage_count DESC LIMIT 5;
