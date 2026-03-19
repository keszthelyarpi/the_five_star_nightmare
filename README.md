# The five star nightmare

### A projektről
A The five star nightmare egy 2D-s platformer és feladatkezelő játék. A játékos a hotel területén mozogva a morcos vendégek kiszolgálását, valamint szeszélyes, váratlan csavarokat tartogató kéréseik teljesítését végzi. A feladatok megoldásához hétköznapi képességeket (futás, ugrás, csúszás) kell használni, miközben akadályokat és ellenfeleket kell leküzdenie.

### Fejlesztők
Zsíros Martin (XO6M7X), 
 Keszthelyi Árpád (I4KEBG),
 Molnár Bálint Ottó (VP35W1)

### Technológiák
**Játékmotor:** Unity
**Nyelv:** C#
**Verziókezelés:** Git / GitHub

```mermaid
classDiagram
    class GameManager {
        +Level currentLevel
        +Player player
        +Mission activeMission
        +List~Mission~ missions
        +startGame()
        +loadLevel(levelID: int)
        +updateObjectiveUI()
        +checkMissionStatus()
    }

    class Level {
        +int levelID
        +String levelName
        +List~Entity~ entities
        +List~Interactable~ interactables
        +loadLevelData()
        +addEntity()
        +addInteractable()
    }

    class Entity {
        <<Abstract>>
        +String entityName
        +Vector2 position
        +int health
        +move()
        +takeDamage()
    }

    class Interactable {
        <<Abstract>>
        +int objectID
        +boolean isInteractable
        +triggerInteraction()
    }

    class Player {
        +float stamina
        +jump()
        +climb()
        +interact()
    }

    class NPC {
        +String dialogText
        +Mission assignedMission
        +talk()
        +giveMission()
    }

    class Enemy {
        +int damageAmount
        +Vector2[] patrolRoute
        +patrol()
        +attack()
    }

    class Mission {
        +int missionID
        +String description
        +QuestItem targetItem
        +boolean isCompleted
        +completeMission()
        +getStatus()
    }

    class QuestItem {
        +String itemName
        +int requiredForMission
        +activateItem()
    }

    GameManager "1" --> "many" Level : kezeli
    GameManager "1" --> "1" Player : nyomon követi
    GameManager "1" --> "many" Mission : irányítja
    Level "1" --> "many" Entity : tartalmazza
    Level "1" --> "many" Interactable : tartalmazza
    Entity <|-- Player : leszármazottja
    Entity <|-- NPC : leszármazottja
    Entity <|-- Enemy : leszármazottja
    Interactable <|-- Enemy : leszármazottja
    Interactable <|-- QuestItem : leszármazottja
    NPC "1" --> "1" Mission : kiadja a feladatot
    Mission "1" --> "1" QuestItem : megköveteli
    ```
