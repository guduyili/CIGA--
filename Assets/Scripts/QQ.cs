using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QQ : AppItem
{
    [SerializeField]
    private Transform PCTransform;

    public bool _isWinning = false;

    public Dialog1 Dialog1;
    public bool isWinning 
    {  
        get 
        {
            return _isWinning; 
        }

        set
        {
            if (_isWinning != value)
            {
                Dialog1.OnEndedNext();
                _isWinning = value;
            }
        }
    }
    private void Start()
    {
        isWinning = false;

        appID = 2;
        PCTransform = GameObject.Find("Canvas/PC").GetComponent<Transform>();

        targetObj = PCTransform.Find("QQApp").gameObject;
    }
}
