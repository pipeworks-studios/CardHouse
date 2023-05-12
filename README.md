# CardHouse

Card games are more complicated than one might think. If you can do something with physical cards, then it should be easy to program, right? Well, if you've ever tried to make a card game before, you'll probably have realized that there are layers of complexity that are taken for granted in the design phase - rules that are easy to enforce when you're sitting at a table, but are tricky to structure in an application. When I move a card from my deck to my hand, of course it should be face up. Of course my hand should be splayed out in a radial pattern. Of course when I shuffle my deck the discard pile should go into it. When setting up a card game, none of these things come for free.

Card House is a set of components and prefabs that were created to make constructing card games easier. Their focus is on defining valid player actions for different phases (within a turn, or as turns themselves), how different card groups behave, and what general movements cards themselves are capable of.

Here are some things that CardHouse doesn't have:
- Networking capabilities. Instead, you can prototype multiplayer games as pass-and-play locally.
- Card building tools. CardHouse is super general and doesn't give you components that are geared toward any specific game genre. It should be easy to hook up your own components, though.

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

To get started, try one of the following:
- Launch the "Tutorial" scene to familiarize yourself with the basics
- Duplicate a sample game and start modifying it
- Make a blank scene, throw the "System" prefab in, and drag in the card groups you'll need from the Prefabs folder. Most of what you'll need to set up a game's rules can be hooked up with CardTransferOperators and the PhaseManager.