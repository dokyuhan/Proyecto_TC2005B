CREATE TABLE `Card` (
  `Card_Id` int PRIMARY KEY AUTO_INCREMENT,
  `Name` varchar(255),
  `Description` varchar(255),
  `Atack` int,
  `Defense` int,
  `Healing` int,
  `Power_Cost` int,
  `XP_Cost` int,
  `Realm` varchar(255),
  `Rarity` varchar(255),
  `Level` varchar(255),
  `Effect` varchar(255)
);

CREATE TABLE `Player` (
  `Player_Id` int PRIMARY KEY AUTO_INCREMENT,
  `Creation_Date` datetime,
  `Last_Modified` datetime,
  `Name` varchar(255),
  `Realm` varchar(255),
  `Is_NPC` varchar(255),
  `XP` int,
  `Win_Record` int,
  `Loss_Record` int
);

CREATE TABLE `Deck` (
  `Deck_Id` int PRIMARY KEY AUTO_INCREMENT,
  `Creation_Date` datetime,
  `Last_Modified` varchar(255),
  `Name` varchar(255),
  `Description` varchar(255),
  `Card_Ammount` int
);

CREATE TABLE `Deck_Composition` (
  `Deck_Id` int AUTO_INCREMENT,
  `Card_Id` int AUTO_INCREMENT,
  `Player_Id` int AUTO_INCREMENT,
  `Card_Disabled` bool,
  `Last_Modified` datetime,
  PRIMARY KEY (`Deck_Id`, `Card_Id`, `Player_Id`)
);

CREATE TABLE `Turn` (
  `Turn_Id` int PRIMARY KEY AUTO_INCREMENT,
  `Game_Id` int,
  `Date_Time` datetime,
  `Duration` datetime,
  `Turn_Status` bool,
  `Player_Id` int,
  `Card1_Id` int,
  `Card2_Id` int,
  `Damage` int,
  `Defense` int,
  `Healing` int,
  `Effect` varchar(255)
);

CREATE TABLE `Hand_Composition` (
  `Card_Id` int AUTO_INCREMENT,
  `Player_Id` int AUTO_INCREMENT,
  `Deck_Id` int AUTO_INCREMENT,
  `Turn_Id` int,
  `Card_Disabled` bool,
  `Creation_Date` datetime,
  `Last_Modified` varchar(255),
  PRIMARY KEY (`Card_Id`, `Player_Id`, `Deck_Id`)
);

CREATE TABLE `Game` (
  `Game_Id` int PRIMARY KEY AUTO_INCREMENT,
  `Player1_Id` int,
  `Player2_Id` int,
  `Level` int,
  `Scene` varchar(255),
  `Creation_date` datetime,
  `Duration` datetime,
  `Winner_Id` int
);

ALTER TABLE `Deck_Composition` ADD FOREIGN KEY (`Deck_Id`) REFERENCES `Deck` (`Deck_Id`);

ALTER TABLE `Deck_Composition` ADD FOREIGN KEY (`Card_Id`) REFERENCES `Card` (`Card_Id`);

ALTER TABLE `Deck_Composition` ADD FOREIGN KEY (`Player_Id`) REFERENCES `Player` (`Player_Id`);

ALTER TABLE `Turn` ADD FOREIGN KEY (`Game_Id`) REFERENCES `Game` (`Game_Id`);

ALTER TABLE `Turn` ADD FOREIGN KEY (`Player_Id`) REFERENCES `Player` (`Player_Id`);

ALTER TABLE `Turn` ADD FOREIGN KEY (`Card1_Id`) REFERENCES `Card` (`Card_Id`);

ALTER TABLE `Turn` ADD FOREIGN KEY (`Card2_Id`) REFERENCES `Card` (`Card_Id`);

ALTER TABLE `Hand_Composition` ADD FOREIGN KEY (`Card_Id`) REFERENCES `Card` (`Card_Id`);

ALTER TABLE `Hand_Composition` ADD FOREIGN KEY (`Player_Id`) REFERENCES `Player` (`Player_Id`);

ALTER TABLE `Hand_Composition` ADD FOREIGN KEY (`Deck_Id`) REFERENCES `Deck` (`Deck_Id`);

ALTER TABLE `Hand_Composition` ADD FOREIGN KEY (`Turn_Id`) REFERENCES `Turn` (`Turn_Id`);

ALTER TABLE `Game` ADD FOREIGN KEY (`Player1_Id`) REFERENCES `Player` (`Player_Id`);

ALTER TABLE `Game` ADD FOREIGN KEY (`Player2_Id`) REFERENCES `Player` (`Player_Id`);

ALTER TABLE `Game` ADD FOREIGN KEY (`Winner_Id`) REFERENCES `Player` (`Player_Id`);
