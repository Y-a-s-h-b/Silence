using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private SceneField _persistentGameplay;
    [SerializeField] private SceneField _levelScene;

    private List<AsyncOperation> _sceneToLoad = new List<AsyncOperation>();
    bool started = false;
    private void Awake()
    {
        started = false;   
    }
    public void StartGame()
    {
        if (!started)
        {
            _sceneToLoad.Add(SceneManager.LoadSceneAsync(_persistentGameplay));
            _sceneToLoad.Add(SceneManager.LoadSceneAsync(_levelScene, LoadSceneMode.Additive));
            started = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
