using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPage : MonoBehaviour
{
    public int tabPageID;

    public void SwitchToThisWebsite()
    {

    }

    public void OnCloseTabPage()
    {
        Destroy(this.gameObject);
    }
}
