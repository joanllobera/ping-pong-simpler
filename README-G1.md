# Server
## How to build
To build the server you only need to check if you have the linux module on your Unity version. Then you build your project only including the `ServerScene`, located in the scenes folder. Make sure before to build, you had selected the linux build and checked the `headless mode` option.

## How to put server online 
To run your server, you can connect through ssh, although you need the `pangpongkey.pem`. Open Git Bash, included in Git or Sourcetree, to simulate a linux terminal. Then you need to introduce the following commands in order to acces to the online linux virtual machine _*(This is already running)*_:

    cd <location_of_pangpongkey_file>    
    ssh -i pangpongkey.pem ubuntu@185.52.32.61
    
Then you need to go to the server container folder via command line. To do that you could navigate using this commands till reach the destination folder: **G6.superpowers/Paddle** 

    cd G6.superpowers
    cd Paddle
    
 If you need give permissions to the build, then you should use:

    chmod 777 <name_of_the_executable>
 
When you're sure have the right permissions, you could run your server using: 

    ./Paddle.x86_64 &

Now you have a running server on a linux virtual machine!

# Client
## How to build
First of all, make sure you've typed the following IP on the `Constants.cs` file in order to connect with your headless server.

	public static string IP = "185.52.32.61";

Then open the `ClientScenePowers` scene in Unity and check the TCP connection is setted on the `ClientManager` object _*(in its script)*_.

Now you could build your project making sure you're just only selecting the `ClientScenePowers` scene in the BuildSettings.

## How to launch
To run your client, you only need to execute the build. It should connect automatically to your previos launched server. If not, check the IP or Port variables in the `Constants.cs`
	
	IP: 185.52.32.61
	PORT: 33333

# Link
You could find all this features in this link:
[Link to the RA2_delivery_G1 commit](https://github.com/joanllobera/ping-pong-simpler/commit/e9bf30188bd91bdc51bb28a88e0d835b85184a28)

# Features

We couldn't fully implement any major features. We focused on fixing some issues related to connections, self-representation and other clients packages. 

Working on this issues, take us a lot of time we coudn't spend on developing fresh features for the project, since we couldn't execute or play any game with the given project. 

We have improved the "PaddleUp" superpower, but we couldn't test it and check if it's working correctly because of all commented issues. For this reason, we don't release it. 