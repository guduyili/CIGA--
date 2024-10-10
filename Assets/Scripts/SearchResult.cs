using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchResult : MonoBehaviour
{
    public int resultID;

    public void OnClickWebsite()
    {
        SearchEngineManager.instance.EnterWebsiteByID(resultID);
    }
}
