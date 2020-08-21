using UnityEngine;


/*
    //Adding this attribute, automatically attaches the AudioSource to the object this script is attached to
    For example, as we drag this Script to the Cube object, it automatically attaches the AudioSource component to it in the inspector
 */
[RequireComponent(typeof(AudioSource))] 
public class Actions : MonoBehaviour
{
    public Transform lookAtItem; //sphere to look at
    public AudioSource audioSource; //AudioSource component
    public AudioClip stabSound; //sound to be played on clicking the Stab
    public MeshRenderer meshRenderer; //cube's mesh renderer to change it's color material


    /*
        Although we can create the UI in the Unity's Editor, yet let's do this in the code using OnGUI
        OnGUI - Handles the creation and execution of the UI elements and functionality
     */
    private void OnGUI()
    {
        //Show number of iTweens animations running
        GUI.Label(new Rect(600, 10, 150, 50), "# of iTweens running: " + iTween.Count());

        /*
            iTween Documentation is here
            http://www.pixelplacement.com/itween/documentation.php
         */

        //Create buttons when clicked to begin the iTween animations
        var isMovedClicked = GUI.Button(new Rect(10, 10, 100, 20), "Move");
        if (isMovedClicked)
        {
            /*
            easyType - The easeType in an iTween, as the name implies, is how the object will ease in and out of its animation.
            loopType - Using pingPong will cause the animation to play forward then backward over and over. loopType can also be set to to none and loop.
            delay - it allows you to set how much time you want to pass before the animation begins.
            onStart - onStart is where you can define a method to perform upon the start of an iTween. PlaySound method is created below.
            */
            iTween.MoveBy(gameObject, iTween.Hash("x", 4, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", 0.3f, "onStart", nameof(PlaySound)));
        }

        var isRotateClicked = GUI.Button(new Rect(10, 30, 100, 20), "Rotate");
        if (isRotateClicked)
        {
            iTween.RotateBy(gameObject, iTween.Hash("y", 0.5f, "easeType", iTween.EaseType.easeInOutExpo, "loopType", iTween.LoopType.pingPong, "delay", 0.5f));
        }

        var isScaleClicked = GUI.Button(new Rect(10, 50, 100, 20), "Scale");
        if (isScaleClicked)
        {
            iTween.ScaleBy(gameObject, iTween.Hash("x", 3f, "y", 3f, "z", 3f, "easeType", "easeInOutExpo", "loopType", iTween.LoopType.pingPong, "delay", 1f));
        }

        //Changing an object’s appearance as well as playing with the game’s music
        var isColorClicked = GUI.Button(new Rect(10, 70, 100, 20), "Color It");
        if (isColorClicked)
        {
            /*
             * r(float or double) - for the individual setting of the color red
               delay(float or double) - for the time in seconds the animation will wait before beginning
               easetype(EaseType or string) - for the shape of the easing curve applied to the animation
               looptype(LoopType or string) - for the type of loop to apply once the animation has completed
             */
            iTween.ColorTo(gameObject, iTween.Hash("r", 5, "easeType", iTween.EaseType.easeInOutExpo, "loopType", iTween.LoopType.pingPong, "delay", 0.5f));
        }

        var changePitchClicked = GUI.Button(new Rect(10, 90, 100, 20), "Change Pitch");
        if (changePitchClicked)
        {
            /*
             * Fades volume and pitch of an AudioSource. Default AudioSource attached to GameObject will be used (if one exists) if not supplied.
             * pitch(float or double) - for the target pitch
               time(float or double) - for the time in seconds the animation will take to complete
               delay(float or double) - for the time in seconds the animation will wait before beginning
               easetype(EaseType or string) - for the shape of the easing curve applied to the animation
               looptype(LoopType or string) - for the type of loop to apply once the animation has completed
             */
            iTween.AudioTo(gameObject, iTween.Hash("pitch", 0, "delay", 1, "time", 3, "easeType", iTween.EaseType.easeInOutExpo, "onComplete", nameof(ReturnAudio)));
        }

        var isFadeClicked = GUI.Button(new Rect(150, 10, 100, 20), "Fade It");

        if (isFadeClicked)
        {
            /*
             * alpha(float or double) - for the initial alpha value of the animation.
               time(float or double) - for the time in seconds the animation will take to complete
               delay(float or double) - for the time in seconds the animation will wait before beginning
               easetype(EaseType or string) - for the shape of the easing curve applied to the animation
               looptype(LoopType or string) - for the type of loop to apply once the animation has completed
             */
            iTween.FadeTo(gameObject, iTween.Hash("alpha", 0, "time", 1, "delay", 0.35f, "easeType", iTween.EaseType.easeInOutExpo, "loopType", iTween.LoopType.pingPong));
        }

        ///
        //Create an animation that rapidly shakes the object, “punches” the object, and how an object can be made to look at another point in the world.
        ///
        var lookAtClicked = GUI.Button(new Rect(150, 30, 100, 20), "Look at Target");
        if (lookAtClicked)
        {
            //Rotates a GameObject to look at a supplied Transform or Vector3 over time.
            /*
                looktarget(Transform or Vector3) - for a target the GameObject will look at.
             */
            iTween.LookTo(gameObject, iTween.Hash("lookTarget", lookAtItem, "time", 1, "delay", 1, "easeType", iTween.EaseType.linear, "loopType", iTween.LoopType.pingPong));
        }

        var isShakeClicked = GUI.Button(new Rect(150, 50, 100, 20), "Shake");
        if (isShakeClicked)
        {
            //Randomly shakes a GameObject's position by a diminishing amount over time.
            /*
                amount(Vector3) - for the magnitude of shake
             */
            iTween.ShakePosition(gameObject, iTween.Hash("amount", new Vector3(2, 2, 2), "time", 2, "delay", 0.5f, "easeType", iTween.EaseType.easeInBounce, "loopType", iTween.LoopType.loop));
        }

        var isPunchClicked = GUI.Button(new Rect(150, 70, 100, 20), "Punch");
        if (isPunchClicked)
        {
            //Applies a jolt of force to a GameObject's position and wobbles it back to its initial position.
            iTween.PunchPosition(gameObject, iTween.Hash("amount", new Vector3(5, 5, 5), "time", 2, "delay", 1, "loopType", iTween.LoopType.loop));
        }


        ///
        // Stop all animations
        ///
        var stopEverythingClicked = GUI.Button(new Rect(450, 10, 110, 20), "Stop Everything");
        if (stopEverythingClicked)
        {
            //stop animations
            iTween.Stop();

            //reset object's position, scale, rotation and material color
            transform.position = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(2, 2, 2);
            transform.eulerAngles = new Vector3(-10, 45, -30);
            meshRenderer.material.color = Color.white;

            //adjust audio
            iTween.AudioTo(gameObject, iTween.Hash("pitch", 1, "time", 1.6, "delay", 0.5f));
        }
    }

    private void PlaySound()
    {
        /*
            Plays an AudioClip once based on supplied volume and pitch and following any delay. AudioSource is optional as iTween will provide one.
            More info here http://www.pixelplacement.com/itween/documentation.php
         */
        iTween.Stab(gameObject, iTween.Hash("audioClip", stabSound, "audioSource", audioSource, "volume", 1f, "delay", 0.35f, "loopType", iTween.LoopType.none));
    }

    private void ReturnAudio()
    {
        //Fades volume and pitch of an AudioSource.Default AudioSource attached to GameObject will be used(if one exists) if not supplied.
        iTween.AudioTo(gameObject, iTween.Hash("pitch", 1f, "time", 1.6f, "delay", 0.5f));
    }    
}

