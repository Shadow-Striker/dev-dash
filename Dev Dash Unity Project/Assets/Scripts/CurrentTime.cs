using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CurrentTime : MonoBehaviour
{
    [SerializeField] private Text currentDateText;

    private void Update()
    {
        GetCurrentTime();
    }

    private void GetCurrentTime()
    {
        DateTime theDate = DateTime.Now;
        string date = theDate.ToString("yyyy-MM-dd");

        currentDateText.text = date;
    }
}
