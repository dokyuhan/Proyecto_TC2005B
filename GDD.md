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
   1. Options
2. Level Select
3. Game
   1. Inventory
   2. Assessment / Next Level
4. End Credits

_(example)_

### **Controls**

How will the player interact with the game? Will they be able to choose the controls? What kind of in-game events are they going to be able to trigger, and how? (e.g. pressing buttons, opening doors, etc.)

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
   2. Special 1:
   3. Special 1:
   4. Special 1:
   5. Special 1:
   6. Special 1:
   7. Special 1:
   8. Special 1:
   9. Special 1:
   10. Special 1:

Cards are upgradable using XP points (details to follow) and can achieve up to three levels of enhancement. Also chosing the specific realm would benefit the player through out the battle: Human realm gives 30 more health and 10% more attack for every card to the player(meaning that the health threshold would be 130 hit points and the cards from the deck would benefit form an increase of damage), Devil realm for every 3 turns it disables one of the enemy player card, Magic realm for every 2 turns steals one energy of the enemy and the Monster real for every 3 turns can place an extra card. Players must adapt their strategies, choosing between aggressive or defensive tactics. Certain cards are conditional, requiring an amount of energy.

## _Level Design_

---

_(Note : These sections can safely be skipped if they&#39;re not relevant, or you&#39;d rather go about it another way. For most games, at least one of them should be useful. But I&#39;ll understand if you don&#39;t want to use them. It&#39;ll only hurt my feelings a little bit.)_

### **Themes**

1. Forest
   1. Mood
      1. Dark, calm, foreboding
   2. Objects
      1. _Ambient_
         1. Fireflies
         2. Beams of moonlight
         3. Tall grass
      2. _Interactive_
         1. Wolves
         2. Goblins
         3. Rocks
2. Castle
   1. Mood
      1. Dangerous, tense, active
   2. Objects
      1. _Ambient_
         1. Rodents
         2. Torches
         3. Suits of armor
      2. _Interactive_
         1. Guards
         2. Giant rats
         3. Chests

_(example)_

### **Game Flow**

1. Player starts in forest
2. Pond to the left, must move right
3. To the right is a hill, player jumps to traverse it (&quot;jump&quot; taught)
4. Player encounters castle - door&#39;s shut and locked
5. There&#39;s a window within jump height, and a rock on the ground
6. Player picks up rock and throws at glass (&quot;throw&quot; taught)
7. … etc.

_(example)_

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

What kinds of colors will you be using? Do you have a limited palette to work with? A post-processed HSV map/image? Consistency is key for immersion.

What kind of graphic style are you going for? Cartoony? Pixel-y? Cute? How, specifically? Solid, thick outlines with flat hues? Non-black outlines with limited tints/shades? Emphasize smooth curvatures over sharp angles? Describe a set of general rules depicting your style here.

Well-designed feedback, both good (e.g. leveling up) and bad (e.g. being hit), are great for teaching the player how to play through trial and error, instead of scripting a lengthy tutorial. What kind of visual feedback are you going to use to let the player know they&#39;re interacting with something? That they \*can\* interact with something?

### **Graphics Needed**

1. Characters
   1. Human-like
      1. Goblin (idle, walking, throwing)
      2. Guard (idle, walking, stabbing)
      3. Prisoner (walking, running)
   2. Other
      1. Wolf (idle, walking, running)
      2. Giant Rat (idle, scurrying)
2. Blocks
   1. Dirt
   2. Dirt/Grass
   3. Stone Block
   4. Stone Bricks
   5. Tiled Floor
   6. Weathered Stone Block
   7. Weathered Stone Bricks
3. Ambient
   1. Tall Grass
   2. Rodent (idle, scurrying)
   3. Torch
   4. Armored Suit
   5. Chains (matching Weathered Stone Bricks)
   6. Blood stains (matching Weathered Stone Bricks)
4. Other
   1. Chest
   2. Door (matching Stone Bricks)
   3. Gate
   4. Button (matching Weathered Stone Bricks)

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

1. develop base classes
   1. base entity
      1. base player
      2. base enemy
      3. base block
2. base app state
   1. game world
   2. menu world
3. develop player and basic block classes
   1. physics / collisions
4. find some smooth controls/physics
5. develop other derived classes
   1. blocks
      1. moving
      2. falling
      3. breaking
      4. cloud
   2. enemies
      1. soldier
      2. rat
      3. etc.
6. design levels
   1. introduce motion/jumping
   2. introduce throwing
   3. mind the pacing, let the player play between lessons
7. design sounds
8. design music

_(example)_
