using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    [Header("")]
    [Header ("World Settings")]
    public GameObject worldCanvas;
    public GameObject enemy;
    public float enemyAdvancement = 96f;

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

    public void LevelFail()
    {
        enemy.transform.Translate(enemyAdvancement, 0f, 0f);
    }

    public void LevelSuccess()
    {
        levels[level].GetComponent<Image>().color = clearedColor; 
        level++;
        levels[level].GetComponent<Button>().interactable = true;
    }
}
