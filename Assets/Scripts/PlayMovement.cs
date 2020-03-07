/* Things I learned (chronologically listed)
 * "ctrl+k+c" comments an entire selected block of text,
 * "ctrl+k+u" uncomments it. 
 * "///" adds summary documentation when used in front of functions/methods (what term do I use??)
 * "Shift+Alt" edits multiple lines
 * 
 * 
*/



// Inside this script, all player movements are defined. Whenever a key is pressed, an editable force will be added to the player-model. 
// The value of the forces is stored within the "public float xForce" variables. "Public" allows us to actually edit these values within our editor under the components tab of our game object.
// "Float" just tells the program that this variable has floating point commas, so it allows for values in between 500 and 501 (500,057 for example).

using UnityEngine;

// the first line tells the program to use unityengine, which i understand as a library of physics functions that the unity physics function works with.
// the first of the following lines creates a public class that we aplayer_bodyitrarily name "Playmovement" and i understand it to be a subclass of a base class called "monobehaviour",
// which you apparently have to declare in c#.
// the line "public rigidbody player_body;" is the first of many variables used within this script, which are "public", which enables them to be adjusted in the editor (at runtime!), 
// and be written by other code outside of this script. "Private" variables are exclusively written to within this script.
// "Rigidbody" declares the type of the variable, in this case it's a type of variable that can store information about rigidbodies (properties).
// "Transform" is again a type which is part of Unity and can store information about Position, Rotation and Scale.
// "Vector3" variables are able to store information about a vector along the 3 axes X, Y, Z.
// "float" are able to store floating point numbers.


public class PlayMovement : MonoBehaviour

{
    public Rigidbody player_body;
    // ^ this enables us to specify which object this script is called/used by in the editor.

    // public Transform player_transform;
    // ^ I thought I needed this to specify which transforms-values I want to read/write but I guess not...

    public Vector3 scale_default;
    // ^ this allows me to edit the player bodies scale

    public float scale_rate;
    // ^ this allows me to edit the speed at which the player bodies scale reduces

    public float scale_decay;
    // ^ this allows me to determine the rate at which the scale approaches it's default value

    public Vector3 scale_override;
    // ^ I need this to reduce the player bodies scale according to the scale_rate (see above)

    private Vector3 scale_amount;
    // ^ This variable stores the result of the calculation "current scale" - "scale rate"

    public float force_forw = 200f;

    public float force_sidew = 20f;

    public float force_backw = 20f;

    public float force_upw = 5000f;




    void Start()
    {
        player_body.useGravity = true;

        scale_default = new Vector3(1f, 1f, 1f);
        //^Warum geht es nur so? Wieso kann ich nicht sagen "scale_default = (0f,0f,0f);"?? Das new steht einfach nur für einen in dem moment gespawnten, unbenannten speicherort der 3 floats beinhalten kann?
        scale_override = new Vector3(1f, 1f, 1f);

        scale_rate = 0.5f;
        scale_decay = 0.368f;
        //^ These are all just default values. "Scale_decay" was chosen with intent though, it's ~1/e. I like to use e because it shows up so much in nature.

        
    }

   
    // void says the following function doesn't return a value Start is called at the beginning of the programm, not continually
    // player_body.useGravity tells "player_body" to be affected by gravity, the method is again just part of the Unity Physics Library
    // Update is called once per frame
   
    //The Method 
    void FixedUpdate()
    //is preferable for physics applications, as it is being called at a rate that corresponds with the updaterate of the physics simulation and not at the frame rate, like "void Update".

    {
        // I want to read playerinputs, I know no other way than

        if (Input.GetKey("w"))
        // So I do that for every key I need (this solution sucks because I can't let the player rebind keys).
        // To make the player movable, I can add the amount of force stored "force_forw" along the Z-axis (when "w" is pressed) using .AddForce on the player_body.
        // Because I want to avoid faster machines being able to run more "Fixed-Update-Cycles" in a given amount of time, I multiply the force I add with Time.deltaTime,
        // which gets lower the faster the cycle took to complete. If it takes less time, less force is added, if the cycle takes longer, more force is added. Hopefully this
        // means the forces will be consistent regardless of framerate.
        // "ForceMode" attribute describes in which way the forces are added. I want to base the agility of the player character on it's physical properties (mass, etc.) so I use "Impulse" as it takes mass into account.


        {
            player_body.AddForce(0, 0, force_forw * Time.deltaTime, ForceMode.Impulse); 
        }
          

        if (Input.GetKey("s"))
        {
            player_body.AddForce(0, 0, -force_backw * Time.deltaTime, ForceMode.Force);
        }

        
        if (Input.GetKey("a"))
        {
            player_body.AddForce(-force_sidew * Time.deltaTime, 0, 0, ForceMode.Impulse);
        }
        
        if (Input.GetKey("d"))
        {
            player_body.AddForce(force_sidew * Time.deltaTime, 0, 0, ForceMode.Impulse);
        }

        // For jumping, I initially simply added an upward force, as long as space was pressed. But that allowed for jetpacking and I wanted to experiment with other jumping mechanics, so I commented 
        // that out: 
        // if (Input.GetKey("space"))
        // {
        //     player_body.AddForce(0, upForce * Time.deltaTime, 0);
        // }
        //
        // I want to use "space" to increase the scale of the player model, similar to how the other movement keys continually add force in specific direction. 
        // Maybe I can do it by telling the program to incrememnt the transform.scale variable by X during every update - cycle. This would only allow me to grow the scale, but I want it to automatically contract
        // back to it's original size, similarly to how gravity works against any Y-Forces. So, I need a constant/dynamic "counter-force" that accellerates to shrink the player model back to it's original size.
        // I need to get the transform.scale value of the player object.I added "public transform player" to the variable declarations.
        // I want to override the players scale with an editable value.
        // Now, when the program reads the input "space" it's supposed to increment the "localScale" of the object this script is called by, by the public "scale_override" variable.

        if (Input.GetKey("space"))
        {
            transform.localScale = transform.localScale + scale_override;
        }
             
        // Now, the player fucking explodes as soon as I tap space. I want him to revert back to his default size when I don't press space, instead of the growth being permanent.
        // I want to check if the current player scale is greater than the default scale, so I can create a routine that decreases the size when that condition is met.
        // I have no idea how to do that atm, to continue I need to understand how to find out whether the scale of my player-object is bigger than the default.

       if (transform.localScale.y > scale_default.y)
        // It looks like I can't compare vectors to vectors but only parts of them to each other... I chose "y" here because that's "up", which seems fitting for jumping.
        {
            scale_amount = transform.localScale - scale_default;
            transform.localScale = transform.localScale - scale_amount * scale_rate; 
        }

        // Now, if I set the scale rate above 1, I quickly approach negative scale, which unity then forces towards the positive. Because technically "transform.localScale.y"  is still > "scale_defaul.y",
        // it indefinitely keeps multiplying the scale into inifity.

        // I now understood, that simply increasing the scale doesn't really create the jumping I am looking for. Because there is no upward force being added, the player does not have any upwards-momentum. 
        // All rapidly upscaling does is lift the players origin while keeping contact with the floor. The jumping-illusion comes from then reducing the scale again, which happens while the origin is now further above ground, 
        // so reducing the volume simply "takes the support out from underneath it". 
        // There is no upward momentum which I could tamper with.
        // I am now thinking, I may have to add an upward-force after all.

        

    }    

}