# Proyecto_TC2005B

En este repositorio se muestran todos los avances, trabajos y entregas para la clase TC2005B por el Equipo 3 conformado por:

- Alberto Limón Cancino
- Do Kyu Han Kim
- Gabriel Edid Harari

Nuestro juego comienza en la escena de menu en donde podemos ver dos botones para configurar los datos con los cuales se iniciará el juego. El primero botón es de LogIn en donde se podrá utilizar un usuario registrado en la base de datos. El segundo botón es de creación de usuario que permite crear una cuenta con la cuál ya se podrá hacer LogIn posteriormente. En caso de querer crear una cuenta se debe de esperar unos minutos antes de entrar a la creación de mazo ya que el mazo no se guardará hasta que se actualice la base de datos que puede llegar a tardar unos minutos. En caso de querer entrar directamente por LogIn se puede usar la cuenta (usuario: beto.limon , contraseña: hola). 

Una vez iniciada la sesión se encuentra con otros dos botones; el primero lleva al usuario a la creación de mazo y el segundo a la elección de nivel. Si se desea continuar con la creación de mazo nos vamos a encontrar con una escena en donde vamos a tener el inventario de cartas en la parte baja y 10 espacios para poder arrastrar las cartas y guardarlas para crear un mazo. Para poder crear este mazo es importante arrastrar las 10 cartas que queremos en nuestro mazo y darle al botón Save.

En caso de querer jugar vamos a ir a la selección de nivel en donde nos encontraremos con 8 niveles a los cuales le podremos picar y que nos llevaran directamente al nivel. De momento no se ha creado la lógica en la cual se llena una escena dependiendo el nivel seleccionado; por lo que, cada nivel tiene una escena diferente. Actualmente se están implementando 2 niveles. El primero es en el que se está implementando y probando la IA y el segundo es en donde se está implementando la lógica del juego. 

Para poder ver las mecánicas básicas del juego se debe meter a la escena “prueba nivel”. En esta escena vamos a ver como carga un mazo con base en el jugador que inicio sesión y del otro lado un mazo estándar para el AI. Las mecánicas de momento permiten al usuario arrastrar 2 cartas al centro y arrastrar 2 cartas del oponente al centro (en esta escena no se está probando el AI por lo que manualmente hay que arrastrar las cartas del oponente), después de un timer las 4 cartas hacen sus sumas y restas y bajan vida y escudo al oponente o al jugador. Las cartas pasan a una sección a parte del mazo para que el usuario no las pueda usar hasta después que se acabe sus cartas del mazo.

## Para correr este proyecto

Ir a Game_DB > Awakening_realm_DB.sql y Dummy_data.sql y correr ambos scripts.

Ir a API_Web > Server > app.js y correr "npm i" y luego "npm start" para correr el servidor y el sitio web. IMPORTANTE: Cambiar las credenciales de SQL para que el API se conecte con SQL.

Ir al sitio http://127.0.0.1:3200/play para empezar a jugar! El juego debería de estar ahí mismo listo para ser disfrutado. 

## Para ver este proyecto sin correrlo

Ir a VideoJuego > Liga_Video_Funcionamiento.txt y abrir la liga de google drive donde esta un video del funcionamiento del juego en el sitio web.
