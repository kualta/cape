using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    [Space(10)]
    public GameObject rocket;
    public GameObject homingRocket;
    public GameObject tower;
    public GameObject hammer;

    [Space(10)]
    public GameObject gameOverScreen;

    internal PlayerController playerController;

    public void SpawnHomingRocket(Vector3 position) {
        Instantiate(homingRocket, position, Quaternion.identity);
    }

    public void SpawnRocket(Vector3 position) {
        Instantiate(rocket, position, Quaternion.identity);
    }

    public void Spawn(Vector3 position) {
        player.transform.position = position;
        playerController.Spawn(initialHitPoints: 100f);
        gameOverScreen.SetActive(false);
    }

    public void Spawn() {
        Vector3 defaultSpawnPosition = new Vector3(10f, .5f, 10f);
        transform.position = defaultSpawnPosition;
        playerController.Spawn(initialHitPoints: 100f);
        gameOverScreen.SetActive(false);
    }

    public void Quit() {
        gameOverScreen.SetActive(false);
        Application.Quit();
    }

    public void OnDeath() {
        gameOverScreen.SetActive(true);
    }

    void Start() {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        gameOverScreen = GameObject.Find("GameOver Screen");
        gameOverScreen.SetActive(false);
        StartCoroutine(SpawnRandomRockets());
        StartCoroutine(SpawnRandomHomingRockets());
    }

    internal Vector3 GetRandomVector3(float maxValue, bool onlyPositiveY) {
        Vector3 vector;

        if ( onlyPositiveY ) {
            vector = new Vector3(Random.Range(-maxValue, maxValue),
                                        Random.Range(1f, maxValue),
                                        Random.Range(-maxValue, maxValue));
        } else {
            vector = new Vector3(Random.Range(-maxValue, maxValue),
                                 Random.Range(-maxValue, maxValue),
                                 Random.Range(-maxValue, maxValue));
        }

        return vector;
    }


    IEnumerator SpawnRandomRockets() {
        GameObject player = GameObject.Find("Player");
        Vector3 position = player.transform.position
            + Vector3.up * 10f
            + GetRandomVector3(9f, onlyPositiveY: false);
        SpawnRocket(position);
        Debug.Log("Spawned Rocket at " + position);

        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnRandomRockets());
    }
    
    IEnumerator SpawnRandomHomingRockets() {
        Vector3 position = GetRandomVector3(30f, onlyPositiveY: true);
        SpawnHomingRocket(position);
        Debug.Log("Spawned Homing Rocket at " + position);

        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnRandomHomingRockets());
    }
}
