using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppItem : MonoBehaviour
{
    public int appID;

    public Button appButton;
    public GameObject targetObj;
    public virtual void OnClick() 
    {
        //Debug.Log("open" + appID);
        PCController.Instance.AddSoftwareInTaskColumn(appID);

        targetObj.SetActive(true);
    }

    public virtual void OnClose() 
    {
        //Debug.Log("remove" + appID);
        PCController.Instance.RemoveSoftwareInTaskColumn(appID);

        targetObj.SetActive(false);
    }

    public virtual void OnMin()
    {
        //PCController.Instance.RemoveSoftwareInTaskColumn(appID);

        targetObj.SetActive(false);
    }
}
