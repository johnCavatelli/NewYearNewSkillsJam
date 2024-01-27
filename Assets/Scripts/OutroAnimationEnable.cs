using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroAnimationEnable : MonoBehaviour
{
    //This class is attatched to a dummy object. On its enable, it instantly plays an animation
    //this allows the functionality of setting down a note enabling an object to be used
    //so when the player is done reading the final note this object enables and the matrix animation is played
    
    public Animator outroAnim;
    private void Awake() {
        outroAnim.SetTrigger("start");
    }
}
