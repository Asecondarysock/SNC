// So, in this script I want to either calculate or fetch the speed of my player, which I then can feed to the UI element at the top of the screen.
// Next to the standard "UnityEngine" library, I am going to be needing the "UnityEngine.UI" library of functions, to enable me to communicate with the UI parts of the engine.
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour {
    // I am going to need the get the transform information of my player entity, a variable that can store that information.
    public Rigidbody player;
    // Then I need a variable to output that information in a format that the UI system can then display, it needs to be storing text.
    public Text score_text;


    // Update is called once per frame
    void Update()
    {
        score_text.text = player.velocity.magnitude.ToString();
    }
}
