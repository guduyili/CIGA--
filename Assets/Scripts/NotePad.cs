using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePad : AppItem
{
    [SerializeField]
    private Transform PCTransform;
    void Start()
    {
        appID = 1;
        PCTransform = GameObject.Find("Canvas/PC").GetComponent<Transform>();
        targetObj = PCTransform.Find("NotePadApp").gameObject;

        //targetObj.SetActive(false);
    }

}
