# FUNNY BUNNY

**[English](#english) · [Español](#español)**

**[Play it on itch.io](https://ignosuke.itch.io/funny-bunny)** ·

---

## English

A 2D side scroller built in Unity. A blue rabbit with a stolen gun, a field full of things that bite, and a pair of wings waiting at the end for whoever clears the place out.

### How to play

| | |
|---|---|
| **Move** | `A` `D` |
| **Jump** | `W` |
| **Shoot** | `Left mouse button` |
| **Pause** | `P` |

Hold jump for height, tap it for a hop.

### Getting started

**Turn around and grab the gun.** You spawn unarmed, facing the level, with the gun sitting a little way behind you — walk left before you walk right. Until you pick it up the shoot button does nothing and the rabbit keeps its empty-handed animations.

**Then clear the place out.** Kill every enemy in the level and a pair of wings appears at the far end. Touch them and the level is yours. Dying before that sends you to the defeat screen with a retry.

### Three ways to get hurt

**Enemies** deal contact damage and knock you backwards, which is as likely to drop you into a pit as the hit itself is to kill you. You get a window of invulnerability afterwards — the rabbit blinks while it lasts.

**The void** below the level takes a heart and drops you back on the last solid ground you stood on. Hazards deliberately ignore invulnerability frames: sitting inside one keeps hurting, so you have to actually get out.

**Running out of hearts** ends the run. The HUD reads left to right, and the heart about to be spent is drawn larger than the rest — a glance tells you how much slack you have.

### Pickups

One of each hides inside a random enemy, reshuffled every time you start the level, so a route that worked last run won't hand you the same tools this one.

| | |
|---|---|
| **Red heart** | Heals a heart. Won't let itself be wasted at full health |
| **Blue heart** | A shield that eats one hit whole, however big. Takes over the large heart slot while you have it |
| **Bullet** | Fire rate from 4 to 10 shots a second, with a countdown in the HUD |
| **Wings** | Appear once the level is clear. Grabbing them wins it |

### The cast

**Worms** walk a straight line until they meet a wall or a ledge, then stop for a second — long enough to read as deciding — before turning around.

**Flies** are too fat to go anywhere. They bob in place, slowing as they reach each end of their arc.

**Bees** trace a U through the air, picking up speed through the dip and easing off at the top of each side.

Flyers start at a random point along their path rather than at the middle, so a pair of them never moves in lockstep. Worms pick their starting direction the same way.

### Game feel

None of this is in the brief. All of it is why the rabbit is pleasant to move.

**Coyote time.** You can still jump for a tenth of a second after walking off a ledge, which quietly fixes the most common "I pressed jump and nothing happened".

**Jump buffer.** Pressing jump just before you land doesn't get thrown away — it fires the moment you touch down.

**Variable jump height.** Releasing the button mid-rise cuts part of the upward velocity. A tap is a hop, a hold is the full jump.

**Asymmetric gravity.** The rabbit rises light and falls heavy, with a third, heavier setting past a certain fall speed and a cap so nothing tunnels through thin platforms. Hang time without floatiness.

**A camera that stays still.** It holds position while you move inside a rectangle in the middle of the screen and only eases after you once you leave it, so ordinary running doesn't shake the view. It stops at the edges of the level, which it works out from the tilemap.

**Parallax.** Two cloud layers at different depths, wrapping endlessly, reused on the main menu with a slow drift instead of camera tracking.

**Winning in slow motion.** Grabbing the wings eases time to a stop over a second and a half before the victory panel shows up, so you actually see the moment.

### Settings

Master, music and SFX each have their own bus on the mixer and their own stepped control, reachable from the main menu and mid-game from the pause screen. Values persist between sessions.

### Built with

Unity 6 · C# · 2D physics · Input System · Tilemap · URP · TextMeshPro

### How it's put together

**ScriptableObjects for every tunable value.** No magic numbers in the Inspector of individual objects: `PlayerSo`, `EnemySo`, `BulletSo`, `CameraSo`, one movement asset per enemy type, one per pickup. MonoBehaviours serialize references — the data SO, an input action, a sibling component — and nothing else.

**Damage logic belongs to whoever takes it.** Enemies and bullets implement `IDamageSource` and expose a number; they never touch anybody's health. `Health` detects the contact, filters it against its own layer mask, and decides what the hit means. Armour, resistances or a second shield type would all land in one place.

**A `DamageInfo` carrying the source layer.** The same hit is read differently by each system that cares: knockback only reacts to enemies, respawn only to hazards, the flash to everything. No system asks another what happened.

**Events instead of downward references.** Jumping, landing, shooting, taking damage, dying, picking something up — each system raises its own and never learns who listens. The animator, the audio and the HUD all subscribe to the same handful of events, which is why adding sound at the end touched no gameplay code at all.

**Levels that describe themselves.** The camera bounds, the invisible walls at either end and the void below are all derived from the terrain tilemap at startup. A new level needs its tilemap painted and nothing else positioned by hand.

**Animation driven by `Animator.Play`.** Nine states, resolved each frame from velocity and ground contact, dispatched by cached hash. No transition graph to maintain and no bool left stuck.

**Ground detection as its own component.** Three downward raycasts, exposing both "touching ground" for jumping and "squarely on ground" for respawn checkpoints — which is what stops the game from putting you back on the exact ledge you just slid off.

### Running it locally

```
git clone <repo-url>
```

Open the project in Unity, load `MainMenuScene`, press Play.

> `GameScene` runs on its own, but the volume settings are read from disk on the menu, so starting there leaves the mixer at its defaults.

---

## Español

Un side scroller 2D hecho en Unity. Un conejo azul con un arma robada, un campo lleno de cosas que muerden y un par de alitas esperando al final para el que limpie el lugar.

### Cómo se juega

| | |
|---|---|
| **Moverse** | `A` `D` |
| **Saltar** | `W` |
| **Disparar** | `Click izquierdo` |
| **Pausa** | `P` |

Mantené el salto para llegar alto, tocalo para un salto corto.

### Para empezar

**Date vuelta y agarrá el arma.** Aparecés desarmado, mirando hacia el nivel, con el arma un poco más atrás tuyo: caminá para la izquierda antes de caminar para la derecha. Hasta que no la agarres el botón de disparo no hace nada y el conejo mantiene sus animaciones con las manos vacías.

**Después limpiá el lugar.** Matá a todos los enemigos del nivel y aparecen unas alitas al fondo. Tocalas y el nivel es tuyo. Morir antes te manda a la pantalla de derrota con un reintentar.

### Tres formas de recibir daño

**Los enemigos** hacen daño por contacto y te empujan hacia atrás, lo cual te puede tirar a un pozo con la misma facilidad con la que el golpe te mata. Después quedás invulnerable un rato: el conejo parpadea mientras dura.

**El vacío** debajo del nivel te saca un corazón y te devuelve al último suelo firme que pisaste. Los peligros ignoran a propósito los frames de invulnerabilidad: quedarte adentro de uno sigue doliendo, así que tenés que salir de verdad.

**Quedarte sin corazones** termina la partida. El HUD se lee de izquierda a derecha, y el corazón que está por gastarse se dibuja más grande que el resto: de un vistazo sabés cuánto margen te queda.

### Pickups

Hay uno de cada uno escondido en un enemigo al azar, barajado de nuevo cada vez que empezás el nivel, así que una ruta que funcionó la partida pasada no te va a dar las mismas herramientas en esta.

| | |
|---|---|
| **Corazón rojo** | Cura un corazón. No se deja desperdiciar con la vida llena |
| **Corazón azul** | Un escudo que se come un golpe entero, por grande que sea. Ocupa el lugar del corazón grande mientras lo tengas |
| **Bala** | Cadencia de 4 a 10 disparos por segundo, con cuenta regresiva en el HUD |
| **Alitas** | Aparecen cuando el nivel está limpio. Agarrarlas lo gana |

### El elenco

**Los gusanos** caminan en línea recta hasta encontrar una pared o un borde, y ahí frenan un segundo (lo justo para que se lea como que están decidiendo) antes de darse vuelta.

**Las moscas** están demasiado gordas para ir a ningún lado. Flotan en el lugar, frenando al llegar a cada extremo de su recorrido.

**Las abejas** dibujan una U en el aire, agarrando velocidad en la bajada y aflojando arriba de cada lado.

Los voladores arrancan en un punto al azar de su trayectoria en vez de en el medio, así dos nunca se mueven sincronizados. Los gusanos eligen su dirección inicial igual.

### Game feel

Nada de esto estaba en la consigna. Todo esto es por qué el conejo se siente bien de mover.

**Coyote time.** Podés saltar hasta una décima de segundo después de haberte pasado del borde, lo cual arregla en silencio el clásico "apreté saltar y no saltó".

**Jump buffer.** Apretar saltar justo antes de aterrizar no se descarta: se ejecuta apenas tocás el piso.

**Altura de salto variable.** Soltar el botón mientras subís corta parte de la velocidad. Un toque es un saltito, mantenerlo es el salto completo.

**Gravedad asimétrica.** El conejo sube liviano y cae pesado, con un tercer valor más pesado todavía pasada cierta velocidad de caída y un tope para que no atraviese plataformas finas. Tiempo en el aire sin sensación de flotar.

**Una cámara que se queda quieta.** Mantiene su posición mientras te movés dentro de un rectángulo en el centro de la pantalla y recién te sigue cuando salís de ahí, así correr normalmente no sacude la vista. Frena en los bordes del nivel, que calcula sola a partir del tilemap.

**Parallax.** Dos capas de nubes a distinta profundidad, repitiéndose infinitamente, reusadas en el menú principal con un desplazamiento lento en lugar de seguir a la cámara.

**Ganar en cámara lenta.** Agarrar las alitas frena el tiempo progresivamente durante un segundo y medio antes de que aparezca el panel de victoria, así llegás a ver el momento.

### Configuración

Master, música y efectos tienen cada uno su bus en el mixer y su propio control por pasos, accesible desde el menú principal y en partida desde la pausa. Los valores se guardan entre sesiones.

### Hecho con

Unity 6 · C# · físicas 2D · Input System · Tilemap · URP · TextMeshPro

### Cómo está armado

**ScriptableObjects para cada valor configurable.** Nada de números mágicos en el Inspector de cada objeto: `PlayerSo`, `EnemySo`, `BulletSo`, `CameraSo`, un asset de movimiento por tipo de enemigo, uno por pickup. Los MonoBehaviour serializan referencias (el SO de datos, una acción de input, un componente hermano) y nada más.

**La lógica del daño es de quien lo recibe.** Enemigos y balas implementan `IDamageSource` y exponen un número; nunca tocan la vida de nadie. `Health` detecta el contacto, lo filtra contra su propia máscara de layers y decide qué significa el golpe. Armadura, resistencias o un segundo tipo de escudo caerían todos en el mismo lugar.

**Un `DamageInfo` que lleva la layer de origen.** El mismo golpe lo lee distinto cada sistema al que le interesa: el knockback solo reacciona a los enemigos, el respawn solo a los peligros, el parpadeo a todo. Ningún sistema le pregunta a otro qué pasó.

**Eventos en lugar de referencias hacia abajo.** Saltar, aterrizar, disparar, recibir daño, morir, agarrar algo: cada sistema emite el suyo y nunca se entera de quién escucha. El animator, el audio y el HUD se suscriben al mismo puñado de eventos, que es por qué agregar sonido al final no tocó una sola línea de gameplay.

**Niveles que se describen solos.** Los límites de la cámara, las paredes invisibles de los extremos y el vacío de abajo salen todos del tilemap del terreno al arrancar. Un nivel nuevo necesita su tilemap pintado y nada más ubicado a mano.

**Animación manejada con `Animator.Play`.** Nueve estados, resueltos cada frame a partir de la velocidad y el contacto con el suelo, despachados por hash cacheado. Sin grafo de transiciones que mantener y sin ningún bool trabado.

**La detección de suelo como componente aparte.** Tres raycasts hacia abajo, que exponen tanto "tocando suelo" para el salto como "bien apoyado" para los checkpoints de respawn, que es lo que evita que el juego te devuelva al borde exacto del que te acabás de caer.

### Correrlo localmente

```
git clone <repo-url>
```

Abrí el proyecto en Unity, cargá `MainMenuScene` y dale Play.

> `GameScene` corre por su cuenta, pero la configuración de volumen se lee al entrar al menú, así que arrancar desde ahí deja el mixer en sus valores por defecto.

---

## Credits · Créditos

Game assets:

- **Tileset & characters** — *source*
- **UI pack** — *source*
- **Sound effects & music** — *source*

---

Built for a game development course.
