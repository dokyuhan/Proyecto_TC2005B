# **Awekening of the Realms**

## _Game Design Document_

---

##### **All Rights Reserved. Alberto Limón Cancino, Do Kyu Han Kim and Gabriel Edid Harari ©2024**

##

## _Index_

---

1. [Index](#index)
2. [Game Design](#game-design)
   1. [Summary](#summary)
   2. [Gameplay](#gameplay)
   3. [Mindset](#mindset)
3. [Technical](#technical)
   1. [Screens](#screens)
   2. [Controls](#controls)
   3. [Mechanics](#mechanics)
4. [Level Design](#level-design)
   1. [Themes](#themes)
      1. Ambience
      2. Objects
         1. Ambient
         2. Interactive
      3. Challenges
   2. [Game Flow](#game-flow)
5. [Development](#development)
   1. [Abstract Classes](#abstract-classes--components)
   2. [Derived Classes](#derived-classes--component-compositions)
6. [Graphics](#graphics)
   1. [Style Attributes](#style-attributes)
   2. [Graphics Needed](#graphics-needed)
7. [Sounds/Music](#soundsmusic)
   1. [Style Attributes](#style-attributes-1)
   2. [Sounds Needed](#sounds-needed)
   3. [Music Needed](#music-needed)
8. [Schedule](#schedule)

## _Game Design_

---

### **Summary**

Welcome to _Awakening of the Realms_, a captivating Deck Building Game where you will journey through diverse opponents and enchanting kingdoms. Embark on an adventure by selecting one of four distinct kingdoms, each offering unique benefits and challenges. Embrace the strengths of your chosen kingdom and engage in battle. Skillfully construct your deck, using the individual abilities of each card to strategize and adapt your approach with every turn. Immerse yourself in the game's dark fantasy aesthetic for an experience that promises to be truly unforgettable.

### **Gameplay**

Upon launching the game for the first time, players are asked to select one of four distinct realms: Human, Magic, Spiritual (Angels and demons), and Monster. This initial choice is important for gameplay. The players kick off with three basic cards from all realms, this will lay the foundation for their initial matches. Progressing on the game the player would be able to assemble the deck as the player needs and would be able to get more and better cards. Before starting the matches the player would have to configure his deck up to 10 cards. Each deck can be configured with 10 commons. This deck configuration can be changed later when the player get more cards like the legendary cards and special cards, where the final deck composition would be 7 common cards 2 special cards and 1 legendary card. The player can configure the deck strategically and depending on the players gamestyle.

Battles are structured such that players start with six cards in hand, from which they can play up to two cards each turn. As the match progresses the player will keep on receiving a random cycle of the cards on his deck, this means that the card used in a round can appear again in the hand deck. Tactical thinking is essential, as players must decide whether to attack, defend, heal, or employ a mix of these strategies to deplete their opponent's health bar of 100 hit points. Similarly, players themselves have 100 hit points, with the potential to recover or even exceed this threshold through strategic play of healing cards. Also in order to use the legendary card the player must used 6 common and special cards, each card will give +1 energy. An energy gage would mark the amount of times you used common and special cards in the game, the energy resets after the player used the legendary card.

Matches unfold with each player's turn occurring simultaneously, governed by a timer for selecting two cards to play. For example, imagine you're Player 1 and you deploy two cards combining for 17 attack, 8 defense, and 2 healing. Once cards are played, their stats are activated. However, the outcome is uncertain until both players have played their cards. Suppose Player X plays two cards resulting in 10 attack, 25 defense, and no healing. The resolution is as follows:

Your 17 attack is fully absorbed by Player X's 25 defense, with their residual defense dissipating.

Your 8 defense successfully blocks 8 out of Player X's 10 attack, resulting in a mere 2 hit point loss.

Your healing boosts your health by 5, placing you 3 hit points above your starting total.

In the other turns those player would be able to place different common cards, special cards and legendary cards to get the victory of the game.

Victory in a match awards the player XP points for card upgrades and 10 coins. The coins are able to be used in the game store, where the player can purchase a random card or a token to change the players realm. Further more in the store there can be special packages to buy. Upon purchasing the card, if its duplicate, converts the card into additional XP points, if its a new card then it will be permanently unlocked. Cards can only be upgraded when the full XP cost is met, ensuring upgrades cannot be partially applied.

This gaming mechanism promises to immerse players deeply in the enchanting universe of _Awakening of the Realms_, offering an extraordinary and engaging experience.

### **Mindset**

In our game, the primary aim is to evoke feelings of adventure, empowerment, and strategic thinking within the player. We plan to create these emotions through several key aspects:

**Empowerment:** Players are expected to feel a growing sense of empowerment and progression as they advance through the game. This is achieved by upgrading their cards and mastering their skills. The decision-making process involved in improving their deck and deploying cards in epic battles allows players to enjoy the thrill of overcoming obstacles and outsmarting their adversaries.

**Adventure:** The various realms within the game are designed to provide a spirit of adventure. Players have the autonomy to select their preferred realm and dive into the unique advantages it offers. Encounters with opponents from contrasting realms will prompt players to contemplate their realm choice, adding depth and intrigue to their journey.

**Strategy:** The diversity of cards available, each with its own set of statistics, encourages players to engage in strategic thinking and meticulous planning. Selecting the right cards, efficiently managing resources, and adapting to evolving scenarios are crucial elements that keep players absorbed and entertained from start to finish.

By weaving together these elements, our game aims to provide an immersive experience that challenges players intellectually while simultaneously offering them a sense of growth, exploration, and mastery.

## _Technical_

---

### **Screens**

1. Title Screen
   1. Create a user
      1. Select your realm and name
2. Inventory
   1. Deck Building
   2. Battle Selector
3. Match
   1. Arena
   2. Assessment / Go Back
4. End Credits

### **Controls**

Most of the controls the player will be able to do will be through the trackpad/mouse. The players will be able to click on buttons to access diferent screens, select their deck and go to a match. Inside a match players will be able to press on cards, drag them and place them in front of them to play them.

### **Mechanics**

The game would have 50 cards in total, where for each realm there would be 10 cards. From those cards 8 would be a commom card having different unique stats: Attack, Defense and Healing. Each card has their own unique set of stats:

1. Human Realm:

   1. Warrior: 10 attack, 10 defense, 0 healing
   2. Archer: 15 attack, 5 defense, 0 healing
   3. Cleric: 2 attack, 2 defense, 15 healing
   4. Knight: 5 attack, 15 defense, 0 healing
   5. Sorceress: 10 attack, 1 defense, 9 healing
   6. Royal Guard: 12 attack, 20 defense, 0 healing
   7. Assasin: 25 attack, 1 defense, 0 healing
   8. Mercenary: 10 attack, 4 defense, 5 healing

2. Monster Realm:

   1. Ogre: 7 attack, 10 defense, 2 healing
   2. Wyrm: 12 attack, 7 defense, 5 healing
   3. Werewolf: 10 attack, 12 defense, 3 healing
   4. Goblin: 3 attack, 5 defense, 10 healing
   5. Harpies: 10 attack, 10 defense, 0 healing
   6. Troll: 6 attack, 15 defense, 4 healing
   7. Treants: 2 attack, 6 defense, 15 healing
   8. Whyvern: 15 attack, 10 defense, 0 healing

3. Magical Realm:

   1. Giant: 5 attack, 15 defense, 0 healing
   2. Elfs: 10 attack, 5 defense, 5 healing
   3. Necromancer: 15 attack, 2 defense, 10 healing
   4. Golem: 1 attack, 20 defense, 0 healing
   5. Elemental guardians: 12 attack, 12 defense, 0 healing
   6. Dwarves: 8 attack, 12 defense, 6 healing
   7. Wizard: 14 attack, 4 defense, 10 healing
   8. Summoners: 7 attack, 7 defense, 13 healing

4. Spiritual Realm:
   1. Hell Hound: 10 attack, 8 defense, 2 healing
   2. Archangel: 14 attack, 10 defense, 0 healing
   3. Nephalem: 10 attack, 5 defense, 10 healing
   4. Succubus: 9 attack, 12 defense, 4 healing
   5. Voidshade Phantom: 14 attack, 2 defense, 0 healing
   6. Titans: 1 attack, 15 defense, 5 healing
   7. Valkyries: 8 attack, 4 defense, 9 healing
   8. Celestial bug: 0 attack, 2 defense, 20 healing

From the rest of the 10 cards, the other 2 cards would be a legendary card. The legendary cards are different from the common cards, each of the legendary cards give you a special bonus effect and a more powerful main stats than the common cards.

1. Human Realm:

   1. Princess: 10 attack, 10 deffense, 40 healing - Special ability: Royal Grace - doubles the healing of the healers for 1 round (it doesnt double itself)
   2. King Arthur: 35 attack, 25 defense, 15 healing - Special ability: Excalibur's fury - ignore the defense of one of the enemy cards placed for 1 round (ex. if the defense is 30 this ability would deal the defense stats as damage)

2. Monster Realm:

   1. Griffin: 30 attack, 30 defense, 10 healing - Special ability: Soaring Vigilance - Can dodge one of the enemys card attacks and also after the dodge can see the enemys played cards for 2 rounds
   2. Dragon: 40 attack, 20 defense, 10 healing - Special ability: Inferno Breath - Applies to the enemy a dot damage of 10 attack and the healing is 50% less effective for 3 rounds (if the enemy places a card that heals 20, it just heals 10)

3. Magical Realm:

   1. Runeforge dwarve: 15 attack, 40 defense, 20 healing - Special ability: Runic ward - creates a barrier for the alies that gives 50 defense for 2 rounds
   2. Moonshadow Elf: 35 attack, 15 defense, 20 healing - Special ability: Lunar empowerment - debuf the enemy making the attacks 20% weaker for 2 rounds and life steal 30 life points of the enemy

4. Spiritual Realm:
   1. Archangel Asmir: 35 attack, 25 defense, 20 healing - Special ability: Celestial Resonance - Reflect all damage taken for 1 round and also heals 10 life points for 3 rounds
   2. Demon King: 35 attack, 15 defense, 30 healing - Special ability: Abyssal Dominion - double the damage of the ally cards for 1 round and curse the enemy causing 10 damage over time and 20% healing reduction for 2 rounds

There are also 10 special cards that will benefit the players deck cards, where each of them will also have a unique bonus effect.

1. Special cards:
   1. Special 1:
   2. Special 2:
   3. Special 3:
   4. Special 4:
   5. Special 5:
   6. Special 6:
   7. Special 7:
   8. Special 8:
   9. Special 9:
   10. Special 10:

Cards are upgradable using XP points (details to follow) and can achieve up to three levels of enhancement. Also chosing the specific realm would benefit the player through out the battle: Human realm gives 30 more health and 10% more attack for every card to the player(meaning that the health threshold would be 130 hit points and the cards from the deck would benefit form an increase of damage), Devil realm for every 3 turns it disables one of the enemy player card, Magic realm for every 2 turns steals one energy of the enemy and the Monster real for every 3 turns can place an extra card. Players must adapt their strategies, choosing between aggressive or defensive tactics. Certain cards are conditional, requiring an amount of energy.

## _Level Design_

---

In order for the user to experience a sense of discovery, mystery and progressión we have decided to implement 4 boards. Each board corresponds to one of the four realms and each realm is intended to have 2 levels. An entry level and the boss challenge. The design of the boards will be as follows.

### **Themes**

1. Magical Realm: The Enchanted Grounds

   1. Mood
      1. Mystical, serene, captivating
   2. Objects
      1. Ambient
         1. Floating islands
         2. Auroras
         3. Arcane runes

2. Spiritual Realm: The Celestial Fields

   1. Mood
      1. Divine, peaceful, radiant
   2. Objects
      1. Ambient
         1. Celestial light beams
         2. Soft, glowing clouds
         3. Heavenly things

3. Human Realm: The Kingdom's place

   1. Mood
      1. Heroic, bustling, medieval
   2. Objects
      1. Ambient
         1. Flags of the realm
         2. Castle towers
         3. Shields and swords

4. Monster Realm: The Dark Forest
   1. Mood
      1. Untamed, mysterious, dark
   2. Objects
      1. Ambient
         1. Dark forest
         2. Fog at the edges
         3. Glowing red eyes in the dark

### **Game Flow**

1. Player enters the level selection screen.
2. Player chooses a Realm to fight in.
3. Player chooses the entry fight or the boss fight.
4. Level is selected
5. Player is redirected to the level screen.
6. Player is displayed with a customized board for the Realm in which he chose to fight.
7. Player fights the opponent.
8. Player collects his rewards.
9. Player is redirected to the main screen.

## _Development_

---

### **Abstract Classes / Components**

1. BasePhysics
   1. BasePlayer
   2. BaseEnemy
   3. BaseObject
2. BaseObstacle
3. BaseInteractable

_(example)_

### **Derived Classes / Component Compositions**

1. BasePlayer
   1. PlayerMain
   2. PlayerUnlockable
2. BaseEnemy
   1. EnemyWolf
   2. EnemyGoblin
   3. EnemyGuard (may drop key)
   4. EnemyGiantRat
   5. EnemyPrisoner
3. BaseObject
   1. ObjectRock (pick-up-able, throwable)
   2. ObjectChest (pick-up-able, throwable, spits gold coins with key)
   3. ObjectGoldCoin (cha-ching!)
   4. ObjectKey (pick-up-able, throwable)
4. BaseObstacle
   1. ObstacleWindow (destroyed with rock)
   2. ObstacleWall
   3. ObstacleGate (watches to see if certain buttons are pressed)
5. BaseInteractable
   1. InteractableButton

_(example)_

## _Graphics_

---

### **Style Attributes**

For Awakening of the Realms we will be having a rich and immersive color palette that reflects the dark fantasy aesthetic of the game. Each Realm (levels) will have its own distinct color scheme to have its unique characteristics present. The Human Realm will have warm and earth-like tones. The Magic Realm will have cold and mystical colors. The Mystical Realm will have gold and white details. The Monster Realm will have dark shadow colors.

The artistic style is based on a combination between fantasy and a semi-realistic scenario. Environments and cards will have fantasy characteristics that relate to realistic features. For example; an archer will have realistic features mixed with some fantasy attributes. We intend to give the cards a 3D appearance that can be achieved through the enhanced and photoshop of the cards characters.

Finally, we intend to implement effects to indicate the interactivity in the game. Maybe adding some glow effects or shadows when a card is played or when damage is done or taken.

### **Graphics Needed**

1. Realms

   1. When a player decides to enter an specific realm level, each level will have its unique environment that aligns with its theme.
   2. Realms environment
      1. Human Realm: medevial theme. (castles, swords, shields, flags)
      2. Mystical Realm: mystical theme. (high blue colors, fictional things)
      3. Spiritual Realm: celestial theme. (clouds, heavenly things)
      4. Monster Realm: dark forest theme. (dark colors, red lights, fog)

2. Cards

   1. Different and unique traits for each character. Each card should reflect the unique traits and aesthetic of their respective realm.

3. UI elements

   1. Dark fantasy background
   2. Logo in font
   3. Styled buttons

4. Realm Selection

   1. Representation of the 4 realms to choose. 4 unique images when choosing a Realm.

5. Navigation
   1. The navigation through the game must be in accordance to the dark fantasy theme.
   2. Profile menu should reflect the theme
   3. Loading screens (if needed)

_(example)_

## _Sounds/Music_

---

### **Style Attributes**

Again, consistency is key. Define that consistency here. What kind of instruments do you want to use in your music? Any particular tempo, key? Influences, genre? Mood?

Stylistically, what kind of sound effects are you looking for? Do you want to exaggerate actions with lengthy, cartoony sounds (e.g. mario&#39;s jump), or use just enough to let the player know something happened (e.g. mega man&#39;s landing)? Going for realism? You can use the music style as a bit of a reference too.

Remember, auditory feedback should stand out from the music and other sound effects so the player hears it well. Volume, panning, and frequency/pitch are all important aspects to consider in both music _and_ sounds - so plan accordingly!

### **Sounds Needed**

1. Effects
   1. Soft Footsteps (dirt floor)
   2. Sharper Footsteps (stone floor)
   3. Soft Landing (low vertical velocity)
   4. Hard Landing (high vertical velocity)
   5. Glass Breaking
   6. Chest Opening
   7. Door Opening
2. Feedback
   1. Relieved &quot;Ahhhh!&quot; (health)
   2. Shocked &quot;Ooomph!&quot; (attacked)
   3. Happy chime (extra life)
   4. Sad chime (died)

_(example)_

### **Music Needed**

1. Slow-paced, nerve-racking &quot;forest&quot; track
2. Exciting &quot;castle&quot; track
3. Creepy, slow &quot;dungeon&quot; track
4. Happy ending credits track
5. Rick Astley&#39;s hit #1 single &quot;Never Gonna Give You Up&quot;

_(example)_

## _Schedule_

---

_(define the main activities and the expected dates when they should be finished. This is only a reference, and can change as the project is developed)_

1. Phase one

   1. Database
      1. Modify the existing Entity-Relation to add changes and corrections
      2. Define user stories
   2. Website
      1. Understand how to create an API and link the cards.
   3. General
      1. Finish the GDD and add modifications if necessary.

2. Phase two

   1. Database
      1. Define the database schema for entities.
      2. Implement the database tables with their necessary relations.
   2. Website
      1. Develop the API for operations on the database entities.
      2. Implement all necessary protocols ( authentication and authorization if needed)

3. Phase three

   1. Unity
      1. Set up unity project and integrate basic UI elements.
      2. Implement basic backend logic. (minimal viable setup)
   2. Website
      1. Integrate the API with Unity

4. Phase four

   1. Visual elements
      1. Design the visual elements for the cards, levels, and screens.
      2. Start implementing game logic with cards in unity.
   2. Website
      1. Test if the API is working properly with unity and the database.

5. Phase five
   1. Unity
      1. Develop the advanced game logic and mechanics. (update the minimal viable setup)
      2. Improve the UI/UX elements if needed
      3. Integrate all the visual assets.
6. Phase six
   1. Test the game
   2. Improve the game if necessary.

_(example)_
