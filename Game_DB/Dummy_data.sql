USE Awakening_realm;

-- Effect type, effect description
INSERT INTO Effect (Effect_type, effect_description) VALUES
('Effect 1', 'Doubles the healing of the healers for 1 round (it doesnt double itself) and reduces two energy gage of the enemy player.'),
('Effect 2', 'Ignore the defense of one of the enemy cards placed for 1 round 
(ex. if the defense is 30 this ability would ignore the defense and apply the damage direct to the health of the player)'),
('Effect 3', 'Can dodge one of the enemys card attacks and also after the dodge can see the enemys played cards for 2 rounds'),
('Effect 4', 'Applies to the enemy a dot damage of 10 attack and the healing is 50% less effective for 3 rounds (if the enemy places a card that heals 20, it just heals 10)'),
('Effect 5', 'Creates a barrier for the alies that gives 50 defense for 2 rounds'),
('Effect 6', 'Debuf the enemy making the attacks 20% weaker for 2 rounds and life steal 30 life points of the enemy (The life steal effect passes the amount of life steal from the enemy player to the player, in this case 30 would be decreasing to the enemy players health and 30 would be increasing to the player)'),
('Effect 7', 'Reflect all damage taken for 1 round and also heals 10 life points for 3 rounds'),
('Effect 8', 'Double the damage of the ally cards for 1 round and curse the enemy causing 10 damage over time and 20% healing reduction for 2 rounds'),
('Effect 9', 'Gives the player an Extra energy'),
('Effect 10', 'The attack of the card placed deals x2 for one turn'),
('Effect 11', 'Posion the enemy player for 2 rounds dealing 20 damage'),
('Effect 12', 'Heals the player 30 health'),
('Effect 13', 'Cures all debuf applied to the player'),
('Effect 14', 'Enhance shields played x2'),
('Effect 15', 'Reduces enemy attack to 50%'),
('Effect 16', 'Damage taken will be reflected to the enemy'),
('Effect 17', 'Disables an enemy card played'),
('Effect 18', 'You can see the opponents cards');

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
('Wyrm', 'Description', 12, 7, 5, 'Monster', 0, 100, 'Common', 1, NULL),
('Werewolf', 'Description', 10, 12, 3, 'Monster', 0, 100, 'Common', 1, NULL),
('Goblin', 'Description', 3, 5, 10, 'Monster', 0, 100, 'Common', 1, NULL),
('Harpies', 'Description', 10, 10, 9, 'Monster', 0, 100, 'Common', 1, NULL),
('Troll', 'Description', 6, 15, 4, 'Monster', 0, 100, 'Common', 1, NULL),
('Treants', 'Description', 2, 6, 15, 'Monster', 0, 100, 'Common', 1, NULL),
('Wyvern', 'Description', 15, 10, 0, 'Monster', 0, 100, 'Common', 1, NULL),

-- Magical
('Giant', 'Description', 5, 15, 0, 'Magical', 0, 100, 'Common', 1, NULL),
('Elf', 'Description', 10, 5, 0, 'Magical', 0, 100, 'Common', 1, NULL),
('Necromancer', 'Description', 15, 2, 10, 'Magical', 0, 100, 'Common', 1, NULL),
('Golem', 'Description', 1, 20, 0, 'Magical', 0, 100, 'Common', 1, NULL),
('Elemental Guardian', 'Description', 12, 12, 9, 'Magical', 0, 100, 'Common', 1, NULL),
('Dwarves', 'Description', 8, 12, 6, 'Magical', 0, 100, 'Common', 1, NULL),
('Wizard', 'Description', 14, 4, 10, 'Magical', 0, 100, 'Common', 1, NULL),
('Summoners', 'Description', 7, 7, 13, 'Magical', 0, 100, 'Common', 1, NULL),

-- Celestial
('Hell Hound', 'Description', 10, 8, 2, 'Celestial', 0, 100, 'Common', 1, NULL),
('Archangel', 'Description', 14, 10, 0, 'Celestial', 0, 100, 'Common', 1, NULL),
('Nephalem', 'Description', 10, 5, 10, 'Celestial', 0, 100, 'Common', 1, NULL),
('Succubus', 'Description', 9, 12, 4, 'Celestial', 0, 100, 'Common', 1, NULL),
('Voidshade Phantom', 'Description', 14, 2, 0, 'Celestial', 0, 100, 'Common', 1, NULL),
('Titans', 'Description', 1, 15, 5, 'Celestial', 0, 100, 'Common', 1, NULL),
('Valkyries', 'Description', 8, 4, 9, 'Celestial', 0, 100, 'Common', 1, NULL),
('Celestial Bug', 'Description', 0, 2, 20, 'Celestial', 0, 100, 'Common', 1, NULL),

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
('Archangel Asmir', 'Description', 35, 25, 20, 'Celestial', 0, 300, 'Legendary', 1, 'Effect 7'),
('Demon King', 'Description', 35, 15, 30, 'Celestial', 0, 300, 'Legendary', 1, 'Effect 8'),

-- Special Cards
('Special 1', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 9'),
('Special 2', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 10'),
('Special 3', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 11'),
('Special 4', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 12'),
('Special 5', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 13'),
('Special 6', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 14'),
('Special 7', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 15'),
('Special 8', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 16'),
('Special 9', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 17'),
('Special 10', 'Description', 0, 0, 0, 'Global', 0, 150, 'Special', 1, 'Effect 18');

-- player name, last name, age, email, realm, is npc, player exp, win, lost, coins, exp points
INSERT INTO Player (player_name, player_last_name, player_age, player_email, realm, is_npc, player_exp, win_record, lose_record, coins, exp_points) VALUES
('John', 'Doe', 25, 'john.doe@example.com', 'Human', 'No', 500, 10, 5, 10, 300),
('Jane', 'Smith', 30, 'jane.smith@example.com', 'Monster', 'No', 600, 15, 8, 15, 200),
('John', 'Doe', 25, 'john.doe@example.com', 'Magical', 'No', 500, 10, 5, 5, 400),
('Jane', 'Smith', 30, 'jane.smith@example.com', 'Magical', 'No', 600, 15, 8, 50, 100),
('Alice', 'Johnson', 28, 'alice.johnson@example.com', 'Magical', 'No', 450, 12, 6, 20, 150),
('Bob', 'Williams', 35, 'bob.williams@example.com', 'Celestial', 'No', 520, 14, 9, 24, 230),
('Emily', 'Brown', 22, 'emily.brown@example.com', 'Human', 'No', 480, 11, 7, 10, 200),
('David', 'Miller', 27, 'david.miller@example.com', 'Monster', 'No', 530, 13, 10, 30, 440),
('Sarah', 'Wilson', 32, 'sarah.wilson@example.com', 'Monster', 'No', 570, 16, 12, 26, 130),
('James', 'Taylor', 29, 'james.taylor@example.com', 'Celestial', 'No', 490, 10, 5, 32, 105),
('Laura', 'Anderson', 26, 'laura.anderson@example.com', 'Celestial', 'No', 510, 9, 4, 35, 160),
('Michael', 'Thomas', 34, 'michael.thomas@example.com', 'Celestial', 'No', 560, 17, 11, 100, 700),
('Emma', 'Jackson', 31, 'emma.jackson@example.com', 'Monster', 'No', 540, 15, 9, 20, 500),
('Chris', 'White', 24, 'chris.white@example.com', 'Magical', 'No', 470, 8, 3, 120, 600),
('Olivia', 'Harris', 33, 'olivia.harris@example.com', 'Monster', 'No', 580, 18, 13, 20, 300),
('Daniel', 'Martin', 36, 'daniel.martin@example.com', 'Human', 'No', 500, 12, 6, 10, 130),
('Sophia', 'Lee', 23, 'sophia.lee@example.com', 'Neptune', 'No', 460, 7, 2, 40, 370),
('Kevin', 'Clark', 38, 'kevin.clark@example.com', 'Monster', 'No', 590, 19, 14, 35, 320),
('Isabella', 'Lewis', 21, 'isabella.lewis@example.com', 'Human', 'No', 450, 6, 1, 50, 650),
('Ethan', 'Walker', 37, 'ethan.walker@example.com', 'Monster', 'No', 550, 16, 10, 35, 650),
('Natalie', 'Allen', 40, 'natalie.allen@example.com', 'Magical', 'No', 610, 20, 15, 95, 100),
('Liam', 'Young', 39, 'liam.young@example.com', 'Human', 'No', 530, 14, 8, 10, 100);

-- deck name, description, ammount
INSERT INTO Deck (deck_name, deck_description, card_ammount) VALUES
('Deck 1', 'Description', 10),
('Deck 2', 'Description', 10),
('Deck 3', 'Description', 10),
('Deck 4', 'Description', 10),
('Deck 5', 'Description', 10),
('Deck 6', 'Description', 10),
('Deck 7', 'Description', 10),
('Deck 8', 'Description', 10),
('Deck 9', 'Description', 10),
('Deck 10', 'Description', 10),
('Deck 11', 'Description', 10),
('Deck 12', 'Description', 10),
('Deck 13', 'Description', 10),
('Deck 14', 'Description', 10),
('Deck 15', 'Description', 10),
('Deck 16', 'Description', 10),
('Deck 17', 'Description', 10),
('Deck 18', 'Description', 10),
('Deck 19', 'Description', 10),
('Deck 20', 'Description', 10),
('Deck 21', 'Description', 10),
('Deck 22', 'Description', 10),
('Deck 23', 'Description', 10),
('Deck 24', 'Description', 10),
('Deck 25', 'Description', 10),
('Deck 26', 'Description', 10),
('Deck 27', 'Description', 10),
('Deck 28', 'Description', 10),
('Deck 29', 'Description', 10),
('Deck 30', 'Description', 10);

-- game level, scene, game duration
INSERT INTO Game (game_level, game_scene, game_duration) VALUES
(1, 'Scene 1', '01:00:00'),
(2, 'Scene 2', '01:30:00'),
(3, 'Scene 3', '00:45:00'),
(4, 'Scene 4', '02:00:00'),
(5, 'Scene 1', '01:15:00'),
(6, 'Scene 2', '00:30:00'),
(7, 'Scene 3', '02:30:00'),
(8, 'Scene 4', '01:45:00');

-- turn duration, turn status
INSERT INTO Turn (turn_duration, turn_status) VALUES
('00:01:00', 'Round 1'),
('00:01:30', 'Round 2'),
('00:02:00', 'Round 3'),
('00:02:30', 'Round 4'),
('00:03:00', 'Round 5'),
('00:03:30', 'Round 6'),
('00:04:00', 'Round 7'),
('00:04:30', 'Round 8'),
('00:05:00', 'Round 9'),
('00:05:30', 'Round 10');
