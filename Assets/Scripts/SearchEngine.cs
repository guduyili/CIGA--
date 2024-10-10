using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SearchEngine : AppItem
{
    public GameObject initalBg;
    public GameObject searchEngine;
    public GameObject taskColumn;
    public GameObject software;
    public TMP_InputField inputField; // TMP 输入框
    public Button searchButton; // 搜索按钮
    public GameObject resultPanel; // 结果面板
    public TMP_Text[] resultTexts; // 显示结果的 TMP 文本

    [SerializeField]
    private Transform PCTransform;

    public GameObject resultPanel1;
    public GameObject QQApp;
    void Start()
    {
        appID = 0;
        PCTransform = GameObject.Find("Canvas/PC").GetComponent<Transform>();
        initalBg = PCTransform.Find("InitalBg").gameObject;
        searchEngine = PCTransform.Find("SearchEngine").gameObject;
        taskColumn = PCTransform.Find("TaskColumn").gameObject;
        software = PCTransform.Find("Software").gameObject;
        inputField = searchEngine.transform.Find("SearchBox/Input/InputField (TMP)").GetComponent<TMP_InputField>();
        searchButton = searchEngine.transform.Find("SearchBox/Input/SearchButton").GetComponent<Button>();
        resultPanel = searchEngine.transform.Find("ResultPanel").gameObject;

        resultPanel1 = resultPanel.transform.Find("Result Panel1").gameObject;
        resultTexts = new TMP_Text[4];
        for (int i = 0; i < resultTexts.Length; i++)
        {
            resultTexts[i] = resultPanel.transform.Find("Result Panel" + (i + 1)).GetComponent<TMP_Text>();
        }

        QQApp = software.transform.Find("QQButton").gameObject;
        searchButton.onClick.AddListener(OnSearchButtonClicked);
        resultPanel.SetActive(false); // 开始时隐藏结果面板
    }

    public void ScenceTieba()
    {
        SceneManager.LoadScene("Tieba");
    }

    public void ScenceDialog()
    {
        SceneManager.LoadScene("Dialog");
    }

    public override void OnClick()
    {
        initalBg.SetActive(false);
        searchEngine.SetActive(true);
        software.SetActive(false);
        PCController.Instance.AddSoftwareInTaskColumn(0);
    }

    public override void OnMin()
    {
        initalBg.SetActive(true);
        searchEngine.SetActive(false);
        software.SetActive(true);
    }

    public override void OnClose()
    {
        initalBg.SetActive(true);
        searchEngine.SetActive(false);
        software.SetActive(true);
        PCController.Instance.RemoveSoftwareInTaskColumn(0);
    }

    // 用于存储搜索词和对应结果的字典
    private Dictionary<string, List<string>> searchDatabase = new Dictionary<string, List<string>>()
    {
        { "西油", new List<string> { "院校直播带你云端鉴校", "西油好不好-大学生必备网", "为什么我不推荐油砖？", "张*峰：西油是一所性价比超高的学校"} },
        { "csharp", new List<string> { "C# Programming Guide", "Microsoft Docs", "Learn C# in Unity" } },
        { "game", new List<string> { "Game Development", "Unity Games", "Top Indie Games" } },
        { "高考查分", new List<string> { "阳光高考_教育部高校招生阳光工程指定平台" } },

        { "strawberry", new List<string> { "Strawberry百科大全", "为什么说Strawberry是神", "为什么说墨茶喜欢吃strawberry" ,"致敬传奇strawberry爱好者" } },
        { "strawberry麦当劳", new List<string> { "麦当劳-美国连锁快餐", "胖猫的外卖，为什么麦当劳真发货，其他商家发空包？", "别骂胖猫了，起码他是一个真君子", "大型纪录片之《我要吃麦当劳》" } },
        { "strawberry麦当劳螺旋桨", new List<string> { "草莓麦旋风" } },
        // 可以添加更多的搜索词和对应结果
    };

    // 用于存储每个结果与对应场景的映射
    private Dictionary<string, string> sceneMappings = new Dictionary<string, string>()
    {
        { "院校直播带你云端鉴校", "Dialog" },
        { "西油好不好-大学生必备网", "Tieba" },
        { "为什么我不推荐油砖？", "Tieba" },
        { "张*峰：西油是一所性价比超高的学校", "Scene4" },
        { "C# Programming Guide", "Scene5" },
        { "Microsoft Docs", "Scene6" },
        { "Learn C# in Unity", "Scene7" },
        { "Game Development", "Scene8" },
        { "Unity Games", "Scene9" },
        { "Top Indie Games", "Scene10" },
        {"阳光高考_教育部高校招生阳光工程指定平台" ,"ChaFen"}
        // 添加更多的结果与对应场景的映射
    };

    public void OnSearchButtonClicked()
    {
        if (inputField == null)
        {
            Debug.LogError("inputField is null");
            return;
        }

        if(inputField.text == "strawberry麦当劳螺旋桨")
        {
            Button victoryButton = resultPanel1.GetComponent<Button>();
            victoryButton.onClick.AddListener(Win);
        }

        string query = inputField.text.ToLower(); // 获取输入框中的内容并转为小写
        if (searchDatabase.ContainsKey(query))
        {
            DisplayResults(searchDatabase[query]);
        }
        else
        {
            DisplayNoResults();
        }
    }

    public void Win()
    {
        QQApp.GetComponent<QQ>().isWinning = true;
    }

    public void DisplayResults(List<string> results)
    {
        if (resultPanel == null)
        {
            Debug.LogError("resultPanel is null");
            return;
        }

        resultPanel.SetActive(true); // 显示结果面板

        for (int i = 0; i < resultTexts.Length; i++)
        {
            if (i < results.Count)
            {
                if (resultTexts[i] == null)
                {
                    Debug.LogError($"resultTexts[{i}] is null");
                    continue;
                }

                resultTexts[i].text = results[i];
                resultTexts[i].gameObject.SetActive(true); // 显示对应的结果文本
                // 添加按钮点击事件
                string result = results[i]; // 需要捕获当前的result
                resultTexts[i].GetComponent<Button>().onClick.AddListener(() => OnResultClicked(result));
            }
            else
            {
                if (resultTexts[i] != null)
                {
                    resultTexts[i].gameObject.SetActive(false); // 隐藏多余的文本
                }
            }
        }
    }

    public void DisplayNoResults()
    {
        if (resultPanel == null || resultTexts[0] == null)
        {
            Debug.LogError("resultPanel or resultTexts[0] is null");
            return;
        }

        resultPanel.SetActive(true); // 显示结果面板
        resultTexts[0].text = "No results found.";
        for (int i = 1; i < resultTexts.Length; i++)
        {
            if (resultTexts[i] != null)
            {
                resultTexts[i].gameObject.SetActive(false); // 隐藏其他文本
            }
        }
    }

    public void OnResultClicked(string result)
    {
        if (sceneMappings.ContainsKey(result))
        {
            SceneManager.LoadScene(sceneMappings[result]);
        }
    }
}
