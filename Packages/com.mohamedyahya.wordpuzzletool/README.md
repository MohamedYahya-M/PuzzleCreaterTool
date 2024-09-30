Word Puzzle Generator Tool

Overview

This Unity package provides a Word Puzzle Game Tool designed to create levels for a word puzzle game. It allows you to dynamically generate levels with word choices, scenario images, and good/bad ending images through an easy-to-use editor tool. The package follows the MVC pattern, includes usage of Observer/EventAction patterns, and is structured to be imported and used as a tool.

Features

Easily create multiple levels using the Level Generator Tool.
Customizable word choices and correct answers for each level.
Supports scenario, good ending, and bad ending images.
Automatically loads and displays the correct level content in the game.
Structured following the Model-View-Controller (MVC) pattern for better maintainability.
Includes Factory Pattern for level creation.

Installation

Download or clone the package.
Open your Unity project and go to the Package Manager.
Click the + icon and select Add package from disk.
Select the package.json file from the folder where you downloaded the tool.
Unity will automatically import the package into your project.

How to Use

1. Accessing the Level Generator Tool
Go to Tools > Level Generator Tool from the Unity menu bar.
A new window will open where you can define and generate levels.
2. Creating Levels
Number of Levels: Set the number of levels you want to generate.
For each level, you can specify:
Scenario Image: The main image that represents the level scenario.
Good Ending Image: The image shown when the correct word is selected.
Bad Ending Image: The image shown when the wrong word is selected.
Word Options: Input up to 5 word options.
Correct Answer Index: Select the index of the correct answer from the word options.
Click Generate Level to save the level configuration.
The levels will be saved as ScriptableObjects in the Assets/Levels folder, and you can edit them later if needed.
3. Loading Levels in Game
The LevelManager script handles loading levels in the game.
The first level is automatically loaded when the game starts, and subsequent levels can be loaded dynamically after a correct answer or when the game ends.
4. Editing Levels
Once a level is generated, you can find the level assets in the Assets/Levels folder.
Double-click the level file to edit any part of the level, such as word choices or images.

Design Patterns Used

MVC Pattern: The project is structured using the Model-View-Controller pattern.

Model: LevelData contains the data for each level.
View: PuzzleView handles displaying the level content.
Controller: PuzzleController manages interactions and game logic.
Observer/EventAction Pattern: Used to manage game events like selecting the correct or wrong answer and updating UI elements.

Factory Pattern: The LevelGenerator tool uses the factory pattern to create ScriptableObject levels at runtime.

Requirements

Unity 2021.1 or newer.