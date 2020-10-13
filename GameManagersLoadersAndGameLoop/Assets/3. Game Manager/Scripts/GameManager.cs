using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    /*
        Before implementing the GameManager, make a list of functions the game manager is responsible, for example,

        //DONE: What level the game is currently in (Initially it was TO DO)

        //DONE: Load and Unload Levels

        //DONE: Make the GameManager globall accessible by making it Singleton  - It's being derrived from the Singleton class

        //DONE: Keep track of game state

        //DONE: Generate other persistent systesm (e.g. Game Manager should have the ability to generate and keep track of other managers)
     */

    //PREGAME, RUNNING, PAUSED - keep track of game state
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    //events
    public Events.EventGameState OnGameStateChanged; //any other systems access to this event can listen/subscribe to this event

    //Keep track of other game managers
    [SerializeField] private GameObject[] _systemPrefabs; //To be populate via the Unity Inspector
    private readonly List<GameObject> _instancedSystemPrefabs = new List<GameObject>(); //game managers initializes or instances of the game managers

    private string _currentLevelName = string.Empty; //we'll be accessing scenes by name as it's a better approach

    /*
        to keep track of async operations
        This can be useful, for example to unload all 3 levels, and the game manager will be able to keep track
     */
    private List<AsyncOperation> _loadOperations;

    public GameState CurrentGameState { get; private set; } = GameState.PREGAME;

    private void Start()
    {
        //To avoid accidentally removing the game manager from the scene or unload the scene containin the game manager
        DontDestroyOnLoad(gameObject); //Unity ensures the GameManager is not destroyed

        _loadOperations = new List<AsyncOperation>(); //initialize.

        //ensure to call to instantate system prefabs on start
        InstantiateSystemPrefabs();

        //start the main scene on the start of the game
        //LoadLevel("Main"); //Main scene should be loaded on specific event based on the specific game state, not at the start

        //after system prefabs are instantiated, we can register for events
        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    private void HandleMainMenuFadeComplete(bool isFadeOut)
    {
        if (!isFadeOut) //only when restart button is clicked and events are bubbled up
            UnLoadLevel(_currentLevelName); //unloads the current level
    }

    private void Update()
    {
        if (CurrentGameState == GameState.PREGAME) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void UpdateState(GameState state)
    {
        var prevGameState = CurrentGameState; //store previous game state before updating to the new state

        CurrentGameState = state;

        //based on the game, perform relevant actions
        switch (CurrentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0f; //Pause all simulations in the game
                break;

            default: //fallback
                break;
        }

        //TODOS:
        //1. Dispatch messages
        //2. Transition between states

        //Call events
        OnGameStateChanged?.Invoke(CurrentGameState, prevGameState); //e.g. MainMenu has subscribed to this event
    }

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;

        foreach (var sysPrefab in _systemPrefabs)
        {
            prefabInstance = Instantiate(sysPrefab);

            _instancedSystemPrefabs.Add(prefabInstance); //add to instanced system prefabs so game manager can keep track of these
        }
    }

    public void LoadLevel(string levelName)
    {
        //LoadSceneMode.Additive - adds the scene in addition to the current scene, and we can manage scenes manually
        var asynOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive); //async approach is better as we know

        //if for some reason the scene couldn't be loaded, then asynOperation is null
        if (asynOperation == null)
        {
            //report error
            Debug.LogError($"[GameManager] Unable to load level {levelName}");

            //and simply return
            return;
        }

        asynOperation.completed += OnLoadAsynOperationCompleted; //event fires when asyn operation is completed

        //add to list
        _loadOperations.Add(asynOperation);

        _currentLevelName = levelName;
    }

    private void OnLoadAsynOperationCompleted(AsyncOperation asynOperation)
    {
        if (_loadOperations.Contains(asynOperation))
        {
            //if async operation was successfuly, we can remove it now from the list
            _loadOperations.Remove(asynOperation);

            //TODO: Other stuff can be done here like
            //1. Dispatch messages
            //2. Transition between scenes
            //etc.

            //ensure the load operation list is fully completed
            if (_loadOperations.Count == 0)
            {
                //Update the system, and the other systems can listen to the state change and act accordingly 
                UpdateState(GameState.RUNNING);
            }
        }

        Debug.Log("Load completed");
    }

    public void UnLoadLevel(string levelName)
    {
        var asynOperation = SceneManager.UnloadSceneAsync(levelName);

        //if for some reason the scene couldn't be loaded, then asynOperation is null
        if (asynOperation == null)
        {
            //report error
            Debug.LogError($"[GameManager] Unable to un-load level {levelName}");

            //and simply return
            return;
        }

        asynOperation.completed += OnUnloadAsynOperationCompleted;
    }

    private void OnUnloadAsynOperationCompleted(AsyncOperation obj)
    {
        Debug.Log("Unload completed");
    }

    //To cleanup to avoid memory leaks etc.
    protected override void OnDestroy()
    {
        base.OnDestroy(); //ensure to call base.OnDestroy() of Single base class

        foreach (var instancePrefab in _instancedSystemPrefabs) //system prefabs should be cleaned up
        {
            Destroy(instancePrefab);
        }

        //also clear the list
        _instancedSystemPrefabs.Clear();
    }


    public void StartGame() //fired on specific event 
    {
        LoadLevel("Main");
    }

    public void RestartGame()
    {
        Debug.Log("RESTART GAME...");
        UpdateState(GameState.PREGAME); //based on the game state, scenes can be unloaded etc. / MainMenu responds to this
    }

    public void QuitGame()
    {
        //TODO:
        //Implement Auto-saving
        //Implement features for quiting

        Application.Quit(); //shut down the game
    }

    public void TogglePause()
    {
        var state = CurrentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING;

        UpdateState(state);
    }
}
