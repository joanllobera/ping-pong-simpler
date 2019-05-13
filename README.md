




# Ping Pong in Immersive Virtual Reality

## Introduction
This repository contains the source code to build a ping pong server and two clients.
Two users with two HMDs will be able to play ping-pong in VR.

## Architecture

A very specific aspect of this project is that it allows for TCP and UDP communication, 
and most of the physics and inverse kinematics is done on a central server, instead of in the clients.

## Compilation

The simplest way to get started is to open the project and:
0. Open the project and import the SteamVR plugin (we tested up to version 1.2.10)
1. Open the scene `ServerScene.unity` in folder `Assets\Scenes`
2. Build the project only with this scene. It can be a build for Linux (where you can use headless mode), or for windows.
3. In the main folder of the server build, add the file `ServerConfig.cfg` found in the folder `ping-pong-simpler\Server`, change the port if necessary
4. Open the scene `ClientSceneVRUpperBody.unity` in folder `Assets\Scenes`
5. Do a different build of the project only with this scene (in this case, for windows).
6. In the main folder of the client build, add the file ClientConfig.cfg, as found in the folder `ping-pong-simpler\Client`. Make sure you change the IP to the one of the server. Change also the port if necessary
7. Execute first the server, and then the client. If you are on a network managed by a University or a large institution, it is possible that the server and the client may need to be on different computers

## Instructions to configure the HTC Vive Pro for the PC

First of all you need to download Steam and login with your steam account.
From the steam client you need to download SteamVR.
When you have connected all the VIVE Hardware you need to the the Room Setup to configure the RoomScale.



## Gamification

The game will start in the Select you username Scene, where the player needs to put a username of 3 letters
To change the current letter, needs to touch with his right hand the floating balls with arrows.
When the player have the desired letter, he/she should touch the Ok button.

Now the player is in the main menu. 
There are differents buttons:
	- The vs Player goes to the Client Scene to play againts other player
	- The vs AI goes to an scene where the player plays versus an AI enemy, but in this moment, that scene isn't into the project
	- The Controls goes in a scene where there is an image that shows the controls of the gameplay 
	- The Features menu goes into a scene where there are other buttons with the features.
		- In this scene, the player can go into all the features, but there are some features that aren't implemented,
		  so in these scenes, there is a big image with Sample. The implemented features have an image explaing that
		  feature. All the scenes in this sub menu have and exit button that put the player into the previous scene.
	- The Exit button closes the game.

In the vs Player Scene (The ClientSceneVRUpperBody) is where the the matches 1 vs 1 can happen. First, the server needs to be open.

If the player connect succefful, he/she can see in his/her PC screen ONLINE (in green). Else if there is some problem, the message in the PC screen will be OFFLINE.

If the player has successfully connected to the server, is going to be waiting until the second player reaches the game.

When the second player arrives, player one must press the front button of his left hand to serve the ball.

The game loop of this scene now is the following: 

	Ofensive player: Player A 
	Defensive player: Player B

	- 

Player A throws ball that does not bounce in any field and falls out -> +1 point for Player B


	- Player A throws ball that bounces on his own field -> +1 point for Player B


	- Player A throws a ball that bounces in the Player B field and returns to bounce in the same field ->
+1 point for Player A


	- Player A throws ball that bounces in Player B field and falls out -> +1 point for Player A
	- 

Player A throws ball that bounces in Player B field and B hits the ball -> 
Ofensive player is now the Player B and Defensive player is Player A


	- The first player to get 20 points wins the match 

The current points of each player will be in the field. The punctuation of the right is from Player 1 and the one on the left is from Player 2.

Each time one of the players scores a point, the ball will teleport automatically to the top of the racket of the Player 1, because he/she needs to serve the ball.


When one of the players reach 20 points, that player wins the match. 
In the wall that have the ENTI logo, it will appear some text. The player that wins will have You win!
And the player that lost will have You lost!
At the bottom of the image, there is the result of the match, with the Player 1 puntuation on the left and the Player 2 puntuation 
on the right.

5 seconds after the match finish, both players will go to the Main Menu Scene. 

If they want to play again, the players need to go to vs Player, and when the match starts, the server will have the Puntuations at 0



## Fingertracking

In order to set up fingertracking with HTC Vive Pro, you might want to follow these steps to make it work:
1- Due to a unity bug, if your unity version is between 2018.2.16 and 2018.3.4, you have to delete the file OPENCL.dll in the unity root folder.
2- Enable Camera in SteamVR settings, which is set to disabled by default. To enable it back you need to click SteamVR properties -> Camera -> Enable Camera.

## Physics ball/walls & private server

The physics of the ball has been updated with lower mass (0.35) and higher bouncing parameters (friction and bounciness 0.9). This values has been adjusted testing hitting the ball in the server scene. The formula for calculating the ball velocity when collides with the paddle has been modified too:  

rb.AddForce((paddlePos + paddle.transform.forward * 0.2f - transform.position).normalized * forceMagnitude);

The floating walls are working on server and collision with the ball was correct, but we couldn't achieve client synchronize with server the transforms of the walls, so we've disabled it on client and server. We have a GIF in the Test Scene where walls move floating in Y axis.

We could connect with private server, ping it and tried to open 8888 port, but we couldn't upload a build and execute it.

The Magnus effect doesn't work perfectly, we couldn't test it with detail because the collision of the ball with the paddle have some lag and if you try to hit the ball so fast, the collision does not produce exactly. We have a GIF in the Test Scene where Magnus effect looks well.

Magnus effect formula:

	Vector3 magnusForce = magunsConstant * this.GetComponent<Rigidbody>().mass * Vector3.Cross(velocity, new Vector3(magnitude, 		magnitude, magnitude));
        rb.AddRelativeForce(magnusForce);
	
Where magnusConstant is equal to 2.


## Machine Learning Player

The Machine Learning for Unity found [here] (https://github.com/Unity-Technologies/ml-agents) has been used to train an AI player. This artificial intelligence is able to play against itself in a level that has a wall in it.
The agent is a ping pong paddle that is able to rotate and move in a restricted area.

The brain that the agent uses performs the following observations:
	- Position of the agent relative to the center of the table.
	- Ball position relative to the agent.
	- Ball bounce on the table.
We've kept things simple and avoided to track the velocity of the ball since it is calculated in the server and not in the client part.

The agent performs the following actions
	- Move itself in the X and Y axis.
	- Rotate iself on its local X axis.
With those actions, the agent is able to hit the ball and return it.

### Agent training
To train this agent, just go to the ServerSceneAI and enable the "Control" toggle in the Academy object. Be sure to run the Anaconda environment to train the agent properly [set_up_training] (https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Learning-Environment-Executable.md) .

The agent has been trained using the default batch file used in the ML-Agents repo. A modifyed batch file has been used (the same that trains the tennis example in the ML-Agents repo) but the brain ended up learning nothing in 500k iterations.
The .bytes file that the brain uses has been generated with a training that lasted 2 million iterations in an environment in which the AI learned to play against itself by bouncing the ball into a wall.

### Playing against the AI
The scene ClientSceneAI replaces the avatar and the VR system with the agent. A modifyed client manager translates the agent position and other positional information and sends it to the server. This scene acts like a self driven player.

## Credits

This project is a simplification of two original projects developed in Autumn 2017 and Winter 2018 by Enric Moreu and Alexandre Via 
as their final degree projects.


Alex Via's project developed a server/client architecture to manage the physics, the inverse kinematics and the communication between the two participants
Enric Moreu's project included a custom hardware component, where he developed a custom controller in the form of a ping pong paddle.
The outcome of this work was in a repository that was significantly bigger, it can be found [here](https://github.com/joanllobera/ping-pong/tree/9cc332536c2dd94f4f0cca1db427304f96764126): 
9cc332536c2dd94f4f0cca1db427304f96764126


The following images link to two videos of the original projects:

[![](http://img.youtube.com/vi/judXWQkDd5E/0.jpg)](http://www.youtube.com/watch?v=judXWQkDd5E "ping pong with IK")
[![](http://img.youtube.com/vi/QxPiP0HnYJk/0.jpg)](http://www.youtube.com/watch?v=QxPiP0HnYJk "ping pong HDK")

Further information:
For general information about the project, contact the manager of this repository.

The original developers can be found at:

Enric Moreu:	enric {dot} moreu {dot} filella {at} gmail {dot} com

Alex Via:  alexviacoll {at} gmail {dot} com
