using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    [Header("")]
    [Header ("World Settings")]
    public GameObject worldCanvas;
    public GameObject enemy;
    public float enemyAdvancement = 96f;

    [Header("")]
    [Header("Other Canvases & Panels")]
    [Header("")]
    public GameObject fullCanvas;
    public GameObject levelStart;
    public GameObject levelFinish;
    public GameObject levelFail;


    [Header("")]
    [Header("Level Settings")]
    [Header("")]
    public GameObject[] levels;
    private int level = 0;
    public Color clearedColor;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        levels[0].GetComponent<Button>().interactable = true;
    }

    void Update()
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "WorldMap")
        {
            worldCanvas.SetActive(false);
            levelStart.SetActive(true);
            PauseGame();
        }
        else
            worldCanvas.SetActive(true);

    }

    public void LevelFail()
    {
        Vector2 newPos = new Vector2(enemy.GetComponent<RectTransform>().anchoredPosition.x + enemyAdvancement, 0f);
        enemy.GetComponent<RectTransform>().anchoredPosition = newPos;
    }

    public void LevelSuccess()
    {
        levels[level].GetComponent<Image>().color = clearedColor; 
        level++;
        levels[level].GetComponent<Button>().interactable = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void FinishMenu()
    {
        levelFinish.SetActive(true);
        PauseGame();
    }

    public void FailMenu()
    {
        levelFail.SetActive(true);
        PauseGame();
    }
}
