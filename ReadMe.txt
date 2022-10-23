Hi,
Video : 
1.)added a Demo video in Video folder.

Game Control : 
1.)use <- or -> for snake turn.
2.) Bydefault snake will move in forward direction,
 but to increase speed additional control of up arrow key is given.
 
Scripts :
1.) 1 monobehaviour classes  in MainMenu scene to handle UI and click event.
2.) 2 monobehaviour classes in gamescene , one for snake collision detection 
 and 2nd is GameManager for game configuration.
3.) Helper class for IO function and Struct class.
4.) Tried to follow single member single responsibility for easy code understanding.
5.) future improvement can be done with S.O.L.I.D principle and 
 class can have there separate interface to improve enhancement in code.
 
Saving :
1.) saving in game is not async as not needed now and currently saving as text without .extension

Food : 
1.)Food can be added by creating prefab and adding it to scriptable objects 
Using scriptable object good because level design mostly done by designer which usually not familir with json or xml
