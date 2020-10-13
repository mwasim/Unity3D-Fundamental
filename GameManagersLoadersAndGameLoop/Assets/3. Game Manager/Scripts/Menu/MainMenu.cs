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

    private void Start()
    {
        //event should be registered at the time the object is created
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
   
    //Animation events
    public void OnFadeOutComplete()
    {
        Debug.LogWarning("FadeOut Completed");
    }

    public void OnFadeInComplete()
    {
        Debug.LogWarning("FadeIn Completed");

        UIManager.Instance.SetDummyCameraActive(true); //activate / show the dummy camera
    }

    //methods to trigger animations
    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false); //deactivate / hide the dummy camera

        //TODO: Legacy animation is not working and report the log message 'Default clip could not be found in attached animations list.'
        //_mainMenuAnimator.Stop();
        //_mainMenuAnimator.clip = _fadeOutAnimation;        
        //_mainMenuAnimator.Play(); //plays the clip

        //Legacy animation is not working and report the log message 'Default clip could not be found in attached animations list.', so setting the game object as inactive for now
        gameObject.SetActive(false); //hide this game object
    }

    public void FadeIn()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play(); //plays the clip
    }

    private void HandleGameStateChanged(GameManager.GameState currentGameState, GameManager.GameState previousGameState)
    {
        if (previousGameState == GameManager.GameState.PREGAME && currentGameState == GameManager.GameState.RUNNING)
        {
            FadeOut(); //Show the main scene after fade out animation
        }
    }
}

