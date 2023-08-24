# CardHouse

## Documentation
https://pipeworks-studios.github.io/CardHouse/

## Getting Started
To get started, open the CardHouse Unity project or import the Unity package. The recommended Unity version is 2022.3.0f1. Then try one of the following:
- Launch the interactive tutorial with the "Tools -> CardHouse -> Launch Tutorial" menu option
- Duplicate a sample game and start modifying it under _CardHouse/SampleGames/_
- Duplicate the "Card Table" scene and start modifying it under _CardHouse/CardHouseCore/Scenes/_
- Create a new scene, add the "System" prefab from _CardHouse/CardHouseCore/_, and then add the card groups you'll need from the _CardHouse/CardHouseCore/CardGroupPrefabs/_ folder. Most of what you'll need to do to set up a game's rules can be done with CardTransferOperators and the PhaseManager.

## Intro
Card games are complicated! They have all these rules about what you can play and when, and coding state machines to govern all this behavior can be a headache. CardHouse gives you a starting point for adding card-based mechanics to any genre of game! This toolkit includes systems for common card operations like shuffling and dealing cards, resource management, and local multiplayer (pass-and-play style). Plus, CardHouse is written with extensibility in mind. Components focus on using UnityActions to orchestrate behavior so you can hook in your own custom game logic wherever you need!

## Contents
In this package you will find:
- A an interactive tutorial that lets you "feel" how different components work
- Sample games in common genres like CCG, deck-builder, and solitaire. Refer to these to see how common card groups and phases should be configured
- A "System" prefab to put in your scene that's used for global resources like phase management and drag handling
- Prefabs for common card groups like decks, hands, and card grids
- A turn-passing button prefab
- A canvas for displaying player resources like health and mana
- "Seeker" components for managing card position, rotation, and scale
- Scriptable objects for defining resources, decks, and the cards within them
- Handlers for clicks and drags
- "Gate" components that limit card drags based on things like if it's your turn and if you can afford to play it
- Test scenes used in development to verify card behavior

## Caveats
Here are some things that CardHouse doesn't currently have:
- Networking capabilities. Instead, you can prototype multiplayer games as pass-and-play locally.
- Card building tools. CardHouse is super general and doesn't give you components that are geared toward any specific game genre. It should be easy to hook up your own components, though.
