using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneChange : MonoBehaviour
{
    public int scena;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(scena);
    }
}
