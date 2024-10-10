using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum Sequence
{
    morning,
    noon,
    night
}
public class TimeBoard : MonoBehaviour
{
    public Vector2Int startTime;
    public TextMeshProUGUI timeText;
    public Sequence _sequence;
    public Sequence Sequence
    {
        get
        {
            return _sequence;
        }
        set
        {
            if (value != _sequence)
            {
                //Debug.Log("改变时间");
                _sequence = value;
                UpdateTime();
            }
        }
    }
    private int _pastDay = 0;
    public int PastDay
    {
        get
        {
            return _pastDay;
        }
        set
        {
            if (value > 0)
            {
                pastTime();
            }
            else if (value < 0)
            {
                backtrackingTime();
            }
            _pastDay = value;
        }
    }

    private void Start()
    {
        UpdateTime();

        Sequence = Sequence.noon;
    }

    public void pastTime()
    {
        UpdateTime();
    }

    public void backtrackingTime()
    {
        UpdateTime();
    }

    public void UpdateTime()
    {
        if(_sequence == Sequence.morning)
        {
            timeText.text = "8:38\n";
        }
        else if(_sequence == Sequence.noon) 
        {
            timeText.text = "11:45\n";
        }
        else
        {
            timeText.text = "23:25\n";
        }
        timeText.text += "2024/6/" + (startTime.y + _pastDay);
    }
}
