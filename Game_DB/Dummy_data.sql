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

-- card name, card description, attack, defense, healing, card realm, power cost, exp cost, rarity, card level, effect
INSERT INTO Cards (card_name, card_description, attack, defense, healing, card_realm, power_cost, exp_cost, rarity, card_level, Effect_type) VALUES

-- Common Cards

-- Human
('Warrior', 'Description', 10, 10, 0, 'Human', 0, 100, 'Common', 1, NULL),
('Archer', 'Description', 15, 5, 0, 'Human', 0, 100, 'Common', 1, NULL),
('Cleric', 'Description', 2, 2, 15, 'Human', 0, 100, 'Common', 1, NULL),
('Knight', 'Description', 5, 15, 0, 'Human', 0, 100, 'Common', 1, NULL),
('Sorceress', 'Description', 10, 1, 0, 'Human', 0, 100, 'Common', 1, NULL),
('Royal Guard', 'Description', 12, 20, 0, 'Human', 0, 100, 'Common', 1, NULL),
('Assasin', 'Description', 25, 1, 0, 'Human', 0, 100, 'Common', 1, NULL),
('Mercenary', 'Description', 10, 4, 5, 'Human', 0, 100, 'Common', 1, NULL),

-- Monster
('Ogre', 'Description', 7, 10, 2, 'Monster', 0, 100, 'Common', 1, NULL),
('Basilisc', 'Description', 12, 7, 5, 'Monster', 0, 100, 'Common', 1, NULL),
('Werewolf', 'Description', 10, 12, 3, 'Monster', 0, 100, 'Common', 1, NULL),
('Goblin', 'Description', 3, 5, 10, 'Monster', 0, 100, 'Common', 1, NULL),
('Harpies', 'Description', 10, 10, 9, 'Monster', 0, 100, 'Common', 1, NULL),
('Troll', 'Description', 6, 15, 4, 'Monster', 0, 100, 'Common', 1, NULL),
('Treant', 'Description', 2, 6, 15, 'Monster', 0, 100, 'Common', 1, NULL),
('Ghoul', 'Description', 15, 10, 0, 'Monster', 0, 100, 'Common', 1, NULL),

-- Magical
('Giant', 'Description', 5, 15, 0, 'Magical', 0, 100, 'Common', 1, NULL),
('Elf', 'Description', 10, 5, 0, 'Magical', 0, 100, 'Common', 1, NULL),
('Necromancer', 'Description', 15, 2, 10, 'Magical', 0, 100, 'Common', 1, NULL),
('Golem', 'Description', 1, 20, 0, 'Magical', 0, 100, 'Common', 1, NULL),
('Elemental Guardian', 'Description', 12, 12, 9, 'Magical', 0, 100, 'Common', 1, NULL),
('Dwarves', 'Description', 8, 12, 6, 'Magical', 0, 100, 'Common', 1, NULL),
('Witch', 'Description', 14, 4, 10, 'Magical', 0, 100, 'Common', 1, NULL),
('Dark Wizard', 'Description', 7, 7, 13, 'Magical', 0, 100, 'Common', 1, NULL),

-- Celestial
('Hell Hound', 'Description', 10, 8, 2, 'Celestial', 0, 100, 'Common', 1, NULL),
('Archangel', 'Description', 14, 10, 0, 'Celestial', 0, 100, 'Common', 1, NULL),
('Nephalem', 'Description', 10, 5, 10, 'Celestial', 0, 100, 'Common', 1, NULL),
('Succubus', 'Description', 9, 12, 4, 'Celestial', 0, 100, 'Common', 1, NULL),
('Nymph', 'Description', 0, 2, 20, 'Celestial', 0, 100, 'Common', 1, NULL),
('Titan', 'Description', 1, 15, 5, 'Celestial', 0, 100, 'Common', 1, NULL),
('Valkyrie', 'Description', 8, 4, 9, 'Celestial', 0, 100, 'Common', 1, NULL),
('Pegasus', 'Description', 14, 0, 2, 'Celestial', 0, 100, 'Common', 1, NULL),

-- Legendary Cards

-- Human
('Princess', 'Description', 10, 10, 40, 'Human', 0, 300, 'Legendary', 1, 'Effect 1'),
('King Arthur', 'Description', 35, 25, 15, 'Human', 0, 300, 'Legendary', 1, 'Effect 2'),

-- Monster
('Griffin', 'Description', 30, 30, 10, 'Monster', 0, 300, 'Legendary', 1, 'Effect 3'),
('Dragon', 'Description', 40, 20, 10, 'Monster', 0, 300, 'Legendary', 1, 'Effect 4'),

-- Magical
('Runeforge Dwarf', 'Description', 15, 40, 20, 'Magical', 0, 300, 'Legendary', 1, 'Effect 5'),
('Moonshadow Elf', 'Description', 35, 15, 20, 'Magical', 0, 300, 'Legendary', 1, 'Effect 6'),

-- Celestial
('Hercules', 'Description', 35, 25, 20, 'Celestial', 0, 300, 'Legendary', 1, 'Effect 7'),
('Demon King', 'Description', 35, 15, 30, 'Celestial', 0, 300, 'Legendary', 1, 'Effect 8');

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
INSERT INTO Players (player_name, player_last_name, player_age, user_name, password, realm, is_npc, level, player_exp, win_record, lose_record, coins, token) VALUES
('John', 'Doe', 25, 'john.doe', 'password123', 'Human', false, 1, 500, 10, 5, 10, 2),
('Jane', 'Smith', 30, 'jane.smith', 'password123', 'Monster', false, 2, 600, 15, 8, 15, 1),
('Alice', 'Johnson', 28, 'alice.johnson', 'password123', 'Magical', false, 2, 450, 12, 6, 20, 0),
('Bob', 'Williams', 35, 'bob.williams', 'password123', 'Celestial', false, 3, 520, 14, 9, 24, 2),
('Emily', 'Brown', 22, 'emily.brown', 'password123', 'Human', false, 4, 480, 11, 7, 10, 3),
('David', 'Miller', 27, 'david.miller', 'password123', 'Monster', false, 5, 530, 13, 10, 30, 0),
('Sarah', 'Wilson', 32, 'sarah.wilson', 'password123', 'Monster', false, 3, 570, 16, 12, 26, 0),
('James', 'Taylor', 29, 'james.taylor', 'password123', 'Celestial', false, 1, 490, 10, 5, 32, 1),
('Laura', 'Anderson', 26, 'laura.anderson', 'password123', 'Celestial', false, 1, 510, 9, 4, 35, 2),
('Michael', 'Thomas', 34, 'michael.thomas', 'password123', 'Celestial', false, 1, 560, 17, 11, 100, 1),
('Emma', 'Jackson', 31, 'emma.jackson', 'password123', 'Monster', false, 2, 540, 15, 9, 20, 3),
('Chris', 'White', 24, 'chris.white', 'password123', 'Magical', false, 6, 470, 8, 3, 120, 1),
('Olivia', 'Harris', 33, 'olivia.harris', 'password123', 'Monster', false, 5, 580, 18, 13, 20, 1),
('Daniel', 'Martin', 36, 'daniel.martin', 'password123', 'Human', false, 3, 500, 12, 6, 10, 2),
('Sophia', 'Lee', 23, 'sophia.lee', 'password123', 'Monster', false, 2, 460, 7, 2, 40, 0),
('Kevin', 'Clark', 38, 'kevin.clark', 'password123', 'Monster', false, 2, 590, 19, 14, 35, 1),
('Isabella', 'Lewis', 21, 'isabella.lewis', 'password123', 'Human', false, 1, 450, 6, 1, 50, 0),
('Ethan', 'Walker', 37, 'ethan.walker', 'password123', 'Monster', false, 6, 550, 16, 10, 35, 1),
('Natalie', 'Allen', 40, 'natalie.allen', 'password123', 'Magical', false, 1, 610, 20, 15, 95, 0),
('Liam', 'Young', 39, 'liam.young', 'password123', 'Human', false, 4, 530, 14, 8, 10, 1);

-- deck name, description, ammount
INSERT INTO Deck (cardID, playerID) VALUES
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

