using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CinemachineVirtualCamera mainCam, rocketManCam;
    public List<GameObject> obstaclesPrefab = new List<GameObject>();
    public List<GameObject> obstacles = new List<GameObject>();

    public GameObject GameOverPanel;
    public TextMeshProUGUI fpsCounter;

    public static bool gameOver = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        InstantiateObstacles();
    }

    /// <summary>
    /// Obstacles are instantiated for the beginning, to prevent game design manually.
    /// </summary>
    public void InstantiateObstacles()
    {
        for (int i = 0; i < 1500; i++)
        {
            var randX = Random.Range(-1000f, 1000);
            var randY = Random.Range(-18f, -20f);
            var randZ = Random.Range(100f, 5000f);
            if (obstacles.Count == 0)
            {

                GameObject go = Instantiate(obstaclesPrefab[Random.Range(0, 2)], new Vector3(randX, randY, randZ), Quaternion.identity);
                obstacles.Add(go);
            }
            else
            {
                foreach (var item in obstacles)
                {
                    if (Vector3.Distance(item.transform.position, new Vector3(randX, randY, randZ)) > 150)
                    {

                        GameObject go = Instantiate(obstaclesPrefab[Random.Range(0, 2)], new Vector3(randX, randY, randZ), Quaternion.identity);
                        obstacles.Add(go);
                        break;
                    }

                }
            }


        }
    }

    /// <summary>
    /// When stick are released Camera changes and follow the rocketman.
    /// </summary>
    public void ChangeCam()
    {
        mainCam.enabled = false;
        rocketManCam.enabled = true;
    }
    /// <summary>
    /// Game starts with the default settings.
    /// </summary>
    public void RestartGame()
    {
        GameOverPanel.SetActive(false);
        mainCam.enabled = true;
        rocketManCam.enabled = false;
        RocketManController.instance.SetGameDefaultSettings();
        StickController.instance.SetGameDefaultSettings();
        StartCoroutine(SetGameStatus());



    }
    /// <summary>
    /// touch control with the boolean for the restart.
    /// </summary>
    /// <returns></returns>
    IEnumerator SetGameStatus()
    {
        yield return new WaitForSeconds(1f);

        gameOver = false;
    }

    private void Update()
    {

        FPSCounter();
    }

    /// <summary>
    /// Fps counter added to the game scene 
    /// </summary>
    public void FPSCounter()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        var avgFrameRate = (int)current;
        fpsCounter.text = "FPS:" + avgFrameRate;
    }

}
