using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header ("")]
    [Header ("Timer Settings (in secondi)")]
    public float levelTimer;
    public float partTimer;

    [Header("")]
    [Header("Canvas Settings")]
    public GameObject screenTime;

    void Start()
    {
        
    }
    
    void Update()
    {
        levelTimer -= Time.deltaTime;
        int minutesN = (int)(levelTimer / 60f);
        int secondsN = (int)(levelTimer - (minutesN * 60));
        int centsN = (int)((levelTimer % 1) * 100);
        string minutes;
        string seconds;
        string cents;

        if (minutesN < 10)
            minutes = ("0" + minutesN);
        else
            minutes = minutesN.ToString();

        if (secondsN < 10)
            seconds = ("0" + secondsN);
        else
            seconds = secondsN.ToString();

        if (centsN < 10)
            cents = ("0" + centsN);
        else
            cents = centsN.ToString();

        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = (minutes + ":" + seconds + ":" + cents);
    }
}
