using System;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _sceneToLoad;
    [SerializeField] private SceneField[] _sceneToUnload;

    private GameObject _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadScenes();
            UnloadScenes();
        }
    }

    private void UnloadScenes()
    {
        for(int i = 0; i < _sceneToUnload.Length; i++)
        {
            for(int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if(loadedScene.name == _sceneToUnload[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(_sceneToUnload[i]);
                }
            }
        }
    }

    public void LoadScenes()
    {
        for (int i = 0; i < _sceneToLoad.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                Debug.Log(loadedScene.name);
                //Debug.Log(_sceneToLoad[j].SceneName);
                if (loadedScene.name == _sceneToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }
            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_sceneToLoad[i],LoadSceneMode.Additive);
            }
        }
    }
}
