using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PCController : MonoBehaviour
{
    //public event Action<int> currentOpenSoftwareCountHaveIncrased;
    //public event Action<int> currentOpenSoftwareCountHaveDecrased;

    //private int currentOpenSoftwareCount = 0;

    public Transform taskColumnGridTransform;
    public List<GameObject> taskColumnList;

    public GameObject columnPrefab;
    public Button button;

    //public GameObject edgeColumnPrefab;
    //public GameObject notepadColumnPrefab;
    //public GameObject qqColumnPrefab;


    //public int CurrentOpenSoftwareCount
    //{
    //    get { return _currentOpenSoftwareCount; }
    //    set
    //    {
    //        if (value > _currentOpenSoftwareCount)
    //        {
    //            AddSoftwareInTaskColumn();
    //        }
    //        else if (value < _currentOpenSoftwareCount)
    //        {
    //            RemoveSoftwareInTaskColumn();
    //        }
    //        _currentOpenSoftwareCount = value;
    //    }
    //}
    public static PCController Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        taskColumnList = new List<GameObject>();
    }

    public void AddSoftwareInTaskColumn(int _id)
    {
        if (CheckTaskColumnByID(_id))
        {
            GameObject obj = GameObject.Instantiate(columnPrefab, taskColumnGridTransform);

            taskColumnList.Add(obj);
            if (_id == 0)
            {
                SearchEngine searchEngine = obj.AddComponent<SearchEngine>();
                searchEngine.appButton = obj.GetComponent<Button>();

                searchEngine.appButton.onClick.AddListener(searchEngine.OnClick);
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/SearchEngineLogo/Edge");
            }
            else if (_id == 1)
            {
                //currentOpenSoftwareCount++;
                NotePad notepad = obj.AddComponent<NotePad>();
                notepad.appButton = obj.GetComponent<Button>();

                notepad.appButton.onClick.AddListener(notepad.OnClick);
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/NoteBoard/Notepad");
            }
            else if (_id == 2)
            {
                QQ qq = obj.AddComponent<QQ>();
                qq.appButton = obj.GetComponent<Button>();

                qq.appButton.onClick.AddListener(qq.OnClick);

                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/QQ/qqPixel");
                //currentOpenSoftwareCount++;
            }
            else
            {
                Debug.Log("添加的id无效");
                Destroy(obj.gameObject);
            }
        }
        
    }

    public void RemoveSoftwareInTaskColumn(int _id)
    {
        Debug.Log("开始删除" + _id);

        //foreach(GameObject obj in taskColumnList)
        //{
        //    if(obj.GetComponent<AppItem>() != null)
        //    {
        //        if (obj.GetComponent<AppItem>().appID == _id)
        //        {
        //            Destroy(obj.gameObject);
        //            taskColumnList.Remove(obj);
        //        }
        //    }
        //}

        for (int i = 0; i < taskColumnList.Count; i++)
        {
            GameObject obj = taskColumnList[i];
            if (obj.GetComponent<AppItem>() != null)
            {
                if (obj.GetComponent<AppItem>().appID == _id)
                {
                    Destroy(obj.gameObject);
                    taskColumnList.Remove(obj);
                }
            }
        }
    }

    public bool CheckTaskColumnByID(int _id)
    {
        foreach (var item in taskColumnList)
        {
            if(item.GetComponent<AppItem>().appID == _id)
            {
                return false;
            }
        }

        return true;
    }

}
