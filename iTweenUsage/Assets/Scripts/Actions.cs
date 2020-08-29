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
            iTween.MoveBy(gameObject, iTween.Hash(ITweenKeys.X, 4, ITweenKeys.EaseType, "easeInOutExpo", ITweenKeys.LoopType, "pingPong", ITweenKeys.Delay, 0.3f, ITweenEventKeys.OnStart, nameof(PlaySound)));
        }

        var isRotateClicked = GUI.Button(new Rect(10, 30, 100, 20), "Rotate");
        if (isRotateClicked)
        {
            iTween.RotateBy(gameObject, iTween.Hash(ITweenKeys.Y, 0.5f, ITweenKeys.EaseType, iTween.EaseType.easeInOutExpo, ITweenKeys.LoopType, iTween.LoopType.pingPong, ITweenKeys.Delay, 0.5f));
        }

        var isScaleClicked = GUI.Button(new Rect(10, 50, 100, 20), "Scale");
        if (isScaleClicked)
        {
            iTween.ScaleBy(gameObject, iTween.Hash(ITweenKeys.X, 3f, ITweenKeys.Y, 3f, ITweenKeys.Z, 3f, ITweenKeys.EaseType, iTween.EaseType.easeInOutExpo, ITweenKeys.LoopType, iTween.LoopType.pingPong, ITweenKeys.Delay, 1f));
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
            iTween.ColorTo(gameObject, iTween.Hash("r", 5, ITweenKeys.EaseType, iTween.EaseType.easeInOutExpo, ITweenKeys.LoopType, iTween.LoopType.pingPong, ITweenKeys.Delay, 0.5f));
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
            iTween.AudioTo(gameObject, iTween.Hash("pitch", 0, ITweenKeys.Delay, 1, ITweenKeys.Time, 3, ITweenKeys.EaseType, iTween.EaseType.easeInOutExpo, "onComplete", nameof(ReturnAudio)));
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
            iTween.FadeTo(gameObject, iTween.Hash("alpha", 0, ITweenKeys.Time, 1, ITweenKeys.Delay, 0.35f, ITweenKeys.EaseType, iTween.EaseType.easeInOutExpo, ITweenKeys.LoopType, iTween.LoopType.pingPong));
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
            iTween.LookTo(gameObject, iTween.Hash("lookTarget", lookAtItem, ITweenKeys.Time, 1, ITweenKeys.Delay, 1, ITweenKeys.EaseType, iTween.EaseType.linear, ITweenKeys.LoopType, iTween.LoopType.pingPong));
        }

        var isShakeClicked = GUI.Button(new Rect(150, 50, 100, 20), "Shake");
        if (isShakeClicked)
        {
            //Randomly shakes a GameObject's position by a diminishing amount over time.
            /*
                amount(Vector3) - for the magnitude of shake
             */
            iTween.ShakePosition(gameObject, iTween.Hash("amount", new Vector3(2, 2, 2), ITweenKeys.Time, 2, ITweenKeys.Delay, 0.5f, ITweenKeys.EaseType, iTween.EaseType.easeInBounce, ITweenKeys.LoopType, iTween.LoopType.loop));
        }

        var isPunchClicked = GUI.Button(new Rect(150, 70, 100, 20), "Punch");
        if (isPunchClicked)
        {
            //Applies a jolt of force to a GameObject's position and wobbles it back to its initial position.
            iTween.PunchPosition(gameObject, iTween.Hash("amount", new Vector3(5, 5, 5), ITweenKeys.Time, 2, ITweenKeys.Delay, 1, ITweenKeys.LoopType, iTween.LoopType.loop));
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
            iTween.AudioTo(gameObject, iTween.Hash("pitch", 1, ITweenKeys.Time, 1.6, ITweenKeys.Delay, 0.5f));
        }
    }

    private void PlaySound()
    {
        /*
            Plays an AudioClip once based on supplied volume and pitch and following any delay. AudioSource is optional as iTween will provide one.
            More info here http://www.pixelplacement.com/itween/documentation.php
         */
        iTween.Stab(gameObject, iTween.Hash("audioClip", stabSound, "audioSource", audioSource, "volume", 1f, ITweenKeys.Delay, 0.35f, ITweenKeys.LoopType, iTween.LoopType.none));
    }

    private void ReturnAudio()
    {
        //Fades volume and pitch of an AudioSource.Default AudioSource attached to GameObject will be used(if one exists) if not supplied.
        iTween.AudioTo(gameObject, iTween.Hash("pitch", 1f, ITweenKeys.Time, 1.6f, ITweenKeys.Delay, 0.5f));
    }
}


public static class ITweenKeys
{
    //Find more and latest here http://www.pixelplacement.com/itween/documentation.php

    /// <summary>
    /// name	string	an individual name useful for stopping iTweens by name
    /// </summary>
    public const string Name = "name";

    /// <summary>
    /// amount	Vector3	for a point in space the GameObject will animate to.
    /// </summary>
    public const string Amount = "amount";

    /// <summary>
    /// x	float or double	for the individual setting of the x axis
    /// </summary>
    public const string X = "x";
    /// <summary>
    /// y	float or double	for the individual setting of the y axis
    /// </summary>
    public const string Y = "y";
    /// <summary>
    /// z	float or double	for the individual setting of the z axis
    /// </summary>
    public const string Z = "z";

    /// <summary>
    /// orienttopath	boolean	for whether or not the GameObject will orient to its direction of travel. False by default
    /// </summary>
    public const string OrientToPath = "orienttopath";
    /// <summary>
    /// looktarget	Transform or Vector3	for a target the GameObject will look at
    /// </summary>
    public const string LookTarget = "looktarget";
    /// <summary>
    /// looktime float or double	for the time in seconds the object will take to look at either the "looktarget" or "orienttopath"
    /// </summary>
    public const string LookTime = "looktime";


    /// <summary>
    /// axis	string	restricts rotation to the supplied axis only
    /// </summary>
    public const string Axis = "axis";

    /// <summary>
    /// space	Space	for applying the transformation in either the world coordinate or local cordinate system. Defaults to local space
    /// </summary>
    public const string Space = "space";


    /// <summary>
    /// time	float or double	for the time in seconds the animation will take to complete
    /// </summary>
    public const string Time = "time";
    /// <summary>
    /// speed	float or double	can be used instead of time to allow animation based on speed
    /// </summary>
    public const string Speed = "speed";


    /// <summary>
    /// delay	float or double	for the time in seconds the animation will wait before beginning
    /// </summary>
    public const string Delay = "delay";


    /// <summary>
    /// easetype	EaseType or string	for the shape of the easing curve applied to the animation
    /// </summary>
    public const string EaseType = "easetype";


    /// <summary>
    /// looptype	LoopType or string	for the type of loop to apply once the animation has completed
    /// </summary>
    public const string LoopType = "looptype";


    /// <summary>
    /// ignoretimescale	boolean	setting this to true will allow the animation to continue independent of the current time which is helpful for animating menus after a game has been paused by setting Time.timeScale=0
    /// </summary>
    public const string IgnoreTimeScale = "ignoretimescale";
}

public static class ITweenEventKeys
{
    /// <summary>
    /// onstart	string	for the name of a function to launch at the beginning of the animation
    /// </summary>
    public const string OnStart = "onstart";
    /// <summary>
    /// onstarttarget	GameObject	for a reference to the GameObject that holds the "onstart" method
    /// </summary>
    public const string OnStartTarget = "onstarttarget";
    /// <summary>
    /// onstartparams	Object	for arguments to be sent to the "onstart" method
    /// </summary>
    public const string OnStartParams = "onstartparams";



    /// <summary>
    /// onupdate	string	for the name of a function to launch on every step of the animation
    /// </summary>
    public const string OnUpdate = "onupdate";
    /// <summary>
    /// onupdatetarget	GameObject	for a reference to the GameObject that holds the "onupdate" method
    /// </summary>
    public const string OnUpdateTarget = "onupdatetarget";
    /// <summary>
    /// onupdateparams	Object	for arguments to be sent to the "onupdate" method
    /// </summary>
    public const string OnUpdateParams = "onupdateparams";


    /// <summary>
    /// oncomplete	string	for the name of a function to launch at the end of the animation
    /// </summary>
    public const string OnComplete = "oncomplete";
    /// <summary>
    /// oncompletetarget	GameObject	for a reference to the GameObject that holds the "oncomplete" method
    /// </summary>
    public const string OnCompleteTarget = "oncompletetarget";
    /// <summary>
    /// oncompleteparams	Object	for arguments to be sent to the "oncomplete" method
    /// </summary>
    public const string OnCompleteParams = "oncompleteparams";
}

