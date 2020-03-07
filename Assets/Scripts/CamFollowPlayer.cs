//Inside this script, the camera is defined. At the top we declare 2 public variables (both are editable within the editor) and name them "player" and "offset". That is done because we want to have the camera follow the 
//player. So we need a place to store the players position. The term "Transform" is actually the data type of the variable we want to declare. So, for the player variable, we tell the game, "this variable is a type of 
//transformation/it needs to be able to fit transformation data in it. Let's disregard the 2nd variable "offset" for now. In the "void Update()" section, which is being executed during every frame, we tell whatever this 
//script is executed by, to change it's "transform.position" (it's coordinates in space) to be equal to the entity we link to the "player" variable. This means, at the beginning of the program, the camera snaps to the 
//same position as the player, meaning it'll move inside him. Now every time the player moves, the camera simply adopts the same position. 
//Here is where the "offset" comes in. If we want to see our player, we can't have the camera always stick to his insides. That's why we declared the public (editable) variable offset with a data-type "Vector3". 
//This data-type tells the program that this variable needs to be able to store 3 floating point values. We need it to be able to do that, because it needs to contain X, Y and Z coordinates, since we want to define the
//cameras distance to our player in all those dimensions. 
//With this addition, in the void Update() section, we tell the camera not to snap to the SAME coordinates as the player, which would be inside him, at his "origin", but at the point in space "X units to the right", 
// "Y units above" and "Z units in front" of the player model. If, for example, we want the camera to be following the player 5 units behind (Z) and 1 unit above (Y) his "origin", the offset variable looks like this: 
// "(0, -5, 1)". To tell the program to add these offset values onto the current players position during every frame, we simply add a "+ offset" to our previous "transform.position = player.position" line within 
// void Update ().

using UnityEngine;

public class CamFollowPlayer : MonoBehaviour 
{
    public Transform player;
    public Vector3 offset;

    // Update is called once per frame

    void Update()
    {
        transform.position = player.position + offset;
    }
}




      
