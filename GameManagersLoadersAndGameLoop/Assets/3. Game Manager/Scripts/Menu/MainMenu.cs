using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /*
        Make a list of TO DO

        1. Track the Animation component
        2. Track the Animation clips for fade in/out
        3. Functions that can receive animation events
        4. Functions to play fade in/out animations
     */

    //should be accessible in the Unity Inspector
    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeOutAnimation;
    [SerializeField] private AnimationClip _fadeInAnimation;

    public Events.EventFadeComplete OnMainMenuFadeComplete; //UIManager listens to this event - bubble up the event

    private void Start()
    {
        //event should be registered at the time the object is created
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
   
    //Animation events
    public void OnFadeOutComplete()
    {
        Debug.Log("FadeOut complete");

        //Legacy animation is not working and report the log message 'Default clip could not be found in attached animations list.', so setting the game object as inactive for now
        gameObject.SetActive(false); //hide this game object

        OnMainMenuFadeComplete?.Invoke(true); //fadout = true
    }

    public void OnFadeInComplete()
    {
        OnMainMenuFadeComplete?.Invoke(false); //fadout = false

        Debug.Log("FadeIn complete");
        UIManager.Instance.SetDummyCameraActive(true); //activate / show the dummy camera

        //Legacy animation is not working and report the log message 'Default clip could not be found in attached animations list.', so setting the game object as inactive for now
        gameObject.SetActive(true); //show this game object
    }

    //methods to trigger animations
    public void FadeOut()
    {        
        UIManager.Instance.SetDummyCameraActive(false); //deactivate / hide the dummy camera

        //TODO: Legacy animation is not working and report the log message 'Default clip could not be found in attached animations list.'
        //_mainMenuAnimator.Stop();
        //_mainMenuAnimator.clip = _fadeOutAnimation;        
        //_mainMenuAnimator.Play(); //plays the clip        

        //TODO: As this animation is legacy and not working so we're manually calling the complete events
        OnFadeOutComplete();
    }

    public void FadeIn()
    {
        //_mainMenuAnimator.Stop();
        //_mainMenuAnimator.clip = _fadeInAnimation;
        //_mainMenuAnimator.Play(); //plays the clip

        //TODO: As this animation is legacy and not working so we're manually calling the complete events
        OnFadeInComplete();
    }

    private void HandleGameStateChanged(GameManager.GameState currentGameState, GameManager.GameState previousGameState)
    {
        if (previousGameState == GameManager.GameState.PREGAME && currentGameState == GameManager.GameState.RUNNING)
        {
            FadeOut(); //Show the main scene after fade out animation
        }

        if (previousGameState != GameManager.GameState.PREGAME && currentGameState == GameManager.GameState.PREGAME)
        {
            FadeIn(); //Show the main scene after fade out animation
            Debug.Log("FadIn..");
        }
    }
}

