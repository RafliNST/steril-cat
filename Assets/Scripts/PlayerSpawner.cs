using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Player & Input Gameobject")]
    [SerializeField]
    GameObject player; //, gameInput;

    [Header("Scene Spawn Properties")]
    [SerializeField]
    List<SceneSpawnPoint> spawnPoints = new List<SceneSpawnPoint>();

    public static PlayerSpawner Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }   else
        {
            Destroy(gameObject);
        }
        Vector3 sceneSpawn = GetSpawnPointForCurrentScene();
        GameObject playerInstance = Instantiate(player, sceneSpawn, Quaternion.identity);

        playerInstance.GetComponent<Player>()._input = GameObject.FindGameObjectWithTag("GameInput").GetComponent<PlayerInput>();
    }

    public Vector3 GetSpawnPointForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        var spawn = spawnPoints
            .FirstOrDefault(sp => sp.sceneName == currentSceneName)?.spawnPoint ?? Vector3.zero;

        return spawn;
    }

    [Serializable]
    class SceneSpawnPoint
    {
        public string sceneName;
        public Vector3 spawnPoint;
    }
}
