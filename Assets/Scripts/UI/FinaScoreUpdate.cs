using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaScoreUpdate : MonoBehaviour
{
    [SerializeField] private TextUI FinalScore;
    // Start is called before the first frame update
    void Start()
    {
        FinalScore.UpdateUI(PlayerPrefs.GetInt("HighScore"));
    }

   
}
