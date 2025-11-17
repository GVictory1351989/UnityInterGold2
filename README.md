
## Overview

Ye project ek **multiplayer card game** hai jo Unity aur PlayerIO pe based hai. Players ke paas decks hote hain,
cards ko hand me draw karte hain, play karte hain aur energy system ke basis pe turn-by-turn gameplay hota hai.
## Project Structure

PlayerIOManager:- 
  localtest flag enabled disabled when enabled then local machine test here
  Authenticate Player
  Send Message 
  Connection Created 

EventManagerUtils:-
    Enables scripts to communicate without references (decoupled architecture).
    Supports subscribing, unsubscribing, and raising typed events.
    GameEvent<T> lets you send packaged data + optional callback
 LoginPage:-
      UI for login screen
      Connecting to PlayerIO lobby + room
      Showing connection status
      Waiting screen handling
      Listening for server messages
      Switching to GamePlayPage when match is found
GamePlayPage handles:
          UI setup for player + opponent.
          Subscribing to server messages using EventManager.
          Mapping server message types → GameState handlers.
          Displaying opponent info when they connect.
          Routing every PlayerIO message to correct GameState.
          Managing card database for in-game card instances.
          Future gameplay: PlayCard, opponent card, updates.
          
    ServerSide Code:-
     LobbyRoom.cs
      Accept players entering the lobby
      Queue them for matchmaking
      When 2 players are ready → create a 1v1 game room
      Send both players to that room

     GameRoom.cs
        Turn order
        Card draw/play
        Energy
        Timers
        
Messaging between server and Unity clients
