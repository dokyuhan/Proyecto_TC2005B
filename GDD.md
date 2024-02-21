# **Awekening of the Realms**

# **Despertar de los Reinos**

## _Game Design Document_

---

##### **Todos los derechos reservados. Alberto Limón Cancino, Do Kyu Han Kim y Gabriel Edid Harari ©2024**

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

Welcome to _Awekening of the Realms_ in this dynamic Deck Building Game you'll embark on a journey across different opponents and kingdoms. Choose one of four kingdoms, embrace the perks and fight. Harness the power of your cards as you strategically build your deck, with each card possessing unique abilities that you must consider for each of your turns.

Bienvenido a _Despertar de los Reinos_ en este dinámico juego de construcción de mazos, te embarcarás en un viaje para derrotar a diferentes oponentes. Elige un reino, aprovecha las ventajas y pelea. Aprovecha el poder de tus cartas mientras construyes estratégicamente tu mazo, considera que cada carta, ademas de tus ventajas por reino, posee habilidades únicas que debe.

### **Gameplay**

When starting the game for the first time the user will be asked to choose between four different realms: Human, Magic, Devil and Monster. This decision will be crucial, keep on reading. Each realm will have between 10-12 cards, each one with three different statystics: attack, defense and healing. Be aware that some cards may have more than one of those stats. The user first starts with three or four basic cards from each realm, these will be used for his first matches.

Every card has a the possibility to upgrade with XP points (more on that later) up to a maximum of three levels. Every player will go to battle with a deck of 10 cards, you can combine cards from all the different realms! Though you might want to have more cards from your own realm because if the card matches your realm it will be boosted.

Once the players go into battle they will have five cards in front of them from which to choose to play, you can play a maximum of two cards per turn. Every turn you will need to strategically think to either attack, defend, heal or maybe a combination of all three of them. Your objective is to deplete your oponents health bar of 100 hitpoints. That means you will also have 100 hitpoints, but remember you can have healing cards that can get you back up again or even break that 100 threshold when played wisely.

How will a match look like? Evey turn for every player runs at the same time, that means you have a timer to put down two of your amazing cards. Supose you are Player 1 and you play two cards that their added stats sum: 17 attack, 8 defense and 2 healing. Once you have finished putting down your cards the turn is complete and all of these stats will come to action, but don't be so sure everything worked! Player X also played his two cards and their added stats are as follows: attack 10, defense 25 and 0 healing. Now that both players have their cards down and the addes stats have been revelead we will see how we did:

Our attack of 18 was completely blocked by Player X 25 defense, the remaing of defense from Player X is lost.

Our defense of 8 was pretty effective, we blocked 8 of the 10 of attack our opponent launched, so we only lost 2 hitpoints

Our healing gave us 5 more health, so we are actually 3 more hitpoints above than what we started.

Players will need to act accordingly and decide if they want to be more agressive or more conservative. Some cards need for a number of cards to be played before they can be used. So suppose the dragon needs 5 cards to be played, so the user must use 5 of his other cards before he can use that one powerfull card.

Once the match is over and the player has won it will recieve XP points to upgrade his cards and will also recieve a random card. That card may be new or not, if the card is not new then it will be turned to more XP points.

### **Mindset**

In our game the goal is to provoke a sense of adventure, empowerment, and strategy in the player. We intend to provoke these emotions by:

Empowerment: We expect pleayers to feel empowered and improved as they progress through the game, upgrading their cards and mastering their abilities. Choosing which cards to include in their deck and how to utilize them in epinc battles. Players will experience the joy of overcoming challenges and outsmarting opponents.

Adventure: The game's realms will provoke a sense of adventure. Each player can choose its own realm and explore the benefits of it provides. Figting against opponents with other realms will make the player question if they chose or not the right realm.

Strategy: By offering a variety of cards, each one with its own statystics, players will be encouraged to think strategically and plan their decisions carefully. The decision-making process of selecting cards, managing resources, and adapting to different situations will keep players engaged and entertained throughout the game.

En nuestro juego el objetivo es provocar en el jugador una sensación de aventura, empoderamiento y estrategia. Pretendemos provocar estas emociones de la siguiente manera:

Empoderamiento: Esperamos que los jugadores se sientan empoderados y progresando a medida que avanzan en el juego, mejorando sus cartas y dominando sus habilidades. Al elegir qué cartas incluir en su mazo y cómo utilizarlas en las batallas, los jugadores experimentarán la emoción de superar desafíos y ganarle a sus oponentes.

Aventura: Los reinos del juego provocarán una sensación de aventura. Cada jugador puede elegir su reino y explorar los beneficios de cada uno. Pelear contra oponentes con otros reinos hará que el jugador se pregunte si eligió o no el reino correcto.

Estrategia: Al ofrecer una variedad de cartas, cada una con sus propias habilidades, animará a los jugadores a pensar estratégicamente y planificar sus decisiones con cuidado. El proceso de toma de decisiones de selección de cartas, gestión de recursos y adaptación a diferentes situaciones mantendrá a los jugadores interesados y entretenidos durante todo el juego.

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

Are there any interesting mechanics? If so, how are you going to accomplish them? Physics, algorithms, etc.

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
