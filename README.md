# LSW Test Interview

## Project info

- Unity Editor 2021.3.2f1
- Project start : 26 August 2022,  5:15 AM GMT

- Additional Unity package :
    - Input System : Version 1.3.0 - January 05, 2022  
    - Cinemachine : Version 2.8.6 - May 25, 2022

## Core Game Loop

Estimated full scope :
- Player traverse within the world
- Player interact with buildings
- Player have inventory system  
- Player enter shop UI mode  
- Player execute transaction 
- Player acquire/equip item  
- Player exit shop UI mode  
- Player traverse within the world  

Finished actual scope :
- Player movement and animation with Blend Tree 
- Tilemap for world building  
- Static collider for non interactable environment in Tilemap (trees, cars and landscape)  
- Interaction system with buildings + trigger collider
- Simple UI for game info + player monologue  
- Inventory panel with scroll rect  
- Prefab for inventory slot and its item  
- Implementing and adopting drap and drop feature to inventory UI (GameDev.Tv online course)  
- Drag and drop feature :
    - If item sprite is dragged to empyt slot will move into destination  
    - If item sprite is dragged to a slot with filled slot than it will swap sprite with destionation sprite  
    - If item sprite is dragged into outside area of inventory panel than it will snap back to source slot position  
- Inventory System feature :
    - Inventory.cs as a main source for inventory data attached to Player GO  
    - Item.cs scriptable object with auto generated UUID as pre defined item in the game   
    - Connecting this new system with existing UI system  
    - InventorySlot.cs and InventoryItem.cs are controlled by new script InventoryUI.cs  
    - InventoryUI.cs communicating with Inventory.cs to populate data to UI mock up  
    - Limiting the slot size from Inventory.cs  (based player current state)
- Item Pickup System  
    - Spawning pickup on the world on game start  
    - Pickup the item to inventory by run over it  
    - Sync the UI with player inventory  
- Item drop system  
    - Create ItemDropper.cs this script should located at Player GO  
    - Its purpose is to drop item into the world based on player location  
    - Design this script for future extension (ex : with saving system)  
## External Assets 

All assets is free item in Unity Asset Store, please download from package manager.  
Some assets are acquired from my personal game dev online course.

Blink´s RPG Adventure  
Path in project : Assets/Pinneaple Dev  
Download url : https://assetstore.unity.com/packages/2d/characters/blink-s-rpg-adventure-155178#content  

City Pack - Top Down - Pixel Art  
Path in Project : Assets/City Pack  
Download URL : https://assetstore.unity.com/packages/2d/textures-materials/city-pack-top-down-pixel-art-195403 

8-Directional Character Template
Path in project : Assets/8-Directional Character Template
Download URl : https://opengameart.org/content/8-directional-character-template

RPG Inventory icons  
Path in project : Assets/RPG_inventory_icons
Download URL : https://assetstore.unity.com/packages/2d/gui/icons/rpg-inventory-icons-56687

GameDev.TV costum script for dragging  
Path in porject : Assets/_Project/GameDev.tv/Dragging Script  
Download URL : https://www.gamedev.tv/p/inventory  
