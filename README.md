Avatar
Para ejecutar el proyecto se utiliza el comando dotnet run --project Cards-Game.Visual en consola dentro de la carpeta con el nombre Cards-Game Project 
Especificaciones del juego: 
Existen tres modos de juego
1.	Jugador vs jugador
2.	Jugador vs jugador virtual
3.	Jugador virtual vs jugador virtual
Juego por turnos jugador vs jugador que tiene el objetivo de reducir a 0 los puntos de vida del jugador contrario. 

Si el mazo se queda sin cartas se termina el juego y ganará quien tenga más vida.

La base del juego son los 4 elementos primarios (agua, tierra, fuego, aire). Cada jugador tendrá un orbe o núcleo de ese elemento primario, el cual potenciará todos los ataques y defensas de las cartas de dicho elemento.
El juego contiene 2 tipos de cartas:
•	Los monstruos: serán los encargados de hacer daño al enemigo, ellos contarán con un nombre, un efecto, un elemento (agua, fuego, aire, tierra o una fusión de dichos elementos), un valor de ataque y uno de defensa.
•	Las cartas de magia:  contaran con algún efecto especial que podrá modificar el estado de la partida, estas no podrán atacar ni ser atacadas.
Los monstruos fusión: serán obtenidas como resultado de la fusión de monstruos (siempre que sus elementos puedan fusionarse). Tendrán como elemento el obtenido de la fusión de ambos monstruos anteriores con nuevos valores de ataque y defensa.
Reglas del juego:
•	A cada jugador al iniciar la partida se le asignara un orbe o núcleo random de un elemento.
•	Existirá un mazo único (conformado por todos los tipos de carta) donde cada jugador robará cartas de manera aleatoria.
•	Al iniciar se repartirán 5 cartas para cada oponente de manera aleatoria.
•	El jugador que inicia no podrá atacar en ese turno.
 Descripción de un turno:
•	Un turno comienza con el robo de una carta del mazo por parte del jugador.
•	Invocar: se podrán realizar dos invocaciones por turno
-	Para invocar un monstruo se dará clic sobre el monstruo deseado
•	Usar magia: cada carta mágica se podrá activar una sola vez
-	para usar una magia se hará clic sobre la carta mágica deseada. 
•	Ataque: se podrán realizar los ataques deseados (cada monstruo solo puede atacar una vez por turno)
-	para atacar un monstruo enemigo primero se dará clic sobre el monstruo atacante y luego otro clic sobre el monstruo atacado. De otra manera el juego se rompe, no rompas el juego, el juego te quiere. En el caso que el enemigo no tenga monstruos en el campo se realizará doble clic sobre el monstruo atacante y atacará directo a la vida del oponente. 
•	Activar efecto de un monstruo: los efectos se podrán activar una vez el monstruo ya esté en el campo. (cada monstruo puede activar su efecto una vez por turno)
-	en la parte izquierda del campo aparecerá un asistente con el nombre Usar efecto que consta de varios marcadores que hacen referencia a los monstruos de tu campo según su posición. Para activar el efecto se hará clic en el marcador que hace referencia a la posición del monstruo deseado. De otra manera el juego se rompe, no rompas el juego, el juego te quiere.


Fase de ataque:
•	Si el ataque de la carta atacante es mayor de la defensa de la carta atacada entonces la carta atacada se destruye y el jugador atacado pierde de sus puntos de vida la diferencia entre el ataque de la carta atacante y la defensa de la carta atacada.
•	Si el ataque de la carta atacante es menor que la defensa de la carta atacada entonces la carta atacante se destruye y la carta atacada toma como el valor de defensa la diferencia entre el ataque de la carta atacante y la defensa de la carta atacada.
•	Si el ataque de la carta atacante y la defensa de la carta atacada son iguales ambas cartas se destruyen.
o	Los valores de la defensa y el ataque se verán afectados según el elemento de la carta y el elemento del orbe de ambos jugadores. Antes de atacar piénsalo dos veces antes de atacar o estarás perdido
Crear cartas:
•	Para crear cartas se deben llenar todos los campos existentes. De otra manera se rompe el juego, no rompas el juego, el juego te quiere.

Tabla de monstruos fusiones
ELEMENTOS NIVEL 2
ELEMENTOS	FUEGO	AGUA	  AIRE	       TIERRA
FUEGO	        FLAMA	 ?	LLAMARADA	LAVA
AGUA	         ?	 ?	 HIELO	       PLANTA
AIRE	      LLAMARADA	HIELO	CICLON	         ?
TIERRA	         LAVA	PLANTA	  ?	      TERREMOTO


Tabla de multiplicador de daño
ATAQUE Y DEFENSA DE LOS ELEMENTOS
ELEMENTOS	FLAMA	LLAMARADA	LAVA	HIELO	PLANTA	CICLON	TERREMOTO	FUEGO	AGUA	TIERRA	AIRE
FLAMA	         0,5	  0,5	         0,5	  2	  2	  1	  0,5	          1	  1	   1	  1
LLAMARADA	  1	   1	         0,5	 1,5	 1,5	 0,5	  0,5	          1	 0,5	  0,5	  2
LAVA	         1,5	   1	         0,5	 1,5	 1,5	 0,5	  0,5	         1,5	  1	  0,5	 0,5
HIELO	         0,5	  0,5	         0,5	  1	 1,5	 0,5	  1,5	         0,5	 1,5	   1	  1
PLANTA	         0,5	  0,5	         0,5	 0,5	  1	 0,5	  0,5	         0,5	  2	   2	  1
CICLON	          1	   1	          1	 1,5	  2	  1	  0,5	          1	  2	   1	 1,5
TEREMOTO	 1,5	  1,5	         1,5	 0,5	 1,5	 0,5	  0,5	         1,5	 1,5	  0,5	 0,5
FUEGO	         0,5	  0,5	         0,5	 1,5	 1,5	  1	  0,5	          1	 0,5	  0,5	 1,5
AGUA	         1,5	   1	         0,5	 0,5	 0,5	  1	   1	          2	 0,5	  1,5	 0,5
TIERRA	         1,5	  1,5	         0,5	  1	 0,5	 0,5	  0,5	         1,5	  1	  0,5	 0,5
AIRE	         0,5	  0,5	         0,5	  1 	 1,5	  1	  0,5	         0,5	 1,5	  0,5	  1
