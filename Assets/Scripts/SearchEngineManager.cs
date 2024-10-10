using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEngineManager : MonoBehaviour
{
    public static SearchEngineManager instance;
    public Transform tabPageGridTransform;

    private void Awake()
    {
        instance = this; 
    }

    public void EnterWebsiteByID(int _id) 
    {
        CreateTabPageByID(_id);
    }

    public void CreateTabPageByID(int _id)
    {

    }
}
