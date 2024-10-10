using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.Rendering;

public class Dialog1 : MonoBehaviour
{

    //public void ScenceDialog()
    //{
    //    SceneManager.LoadScene("打牌场景");


    //}

    ///// <summary>
    ///// 对话结束开启摄像机
    ///// </summary>
    //public Button nextButton;



    /// <summary>
    /// 对话文本文件 csv格式
    /// </summary>
    public TextAsset dialogDataFile;

    /// <summary>
    /// 左侧角色图像
    /// </summary>
    public SpriteRenderer spriteLeft;

    /// <summary>
    /// 右侧角色图像
    /// </summary>
    public SpriteRenderer spriteRight;

    /// <summary>
    /// 角色名字文本
    /// </summary>
    public TMP_Text nameText;

    /// <summary>
    /// 对话内容文本
    /// </summary>
    public TMP_Text dialogText;



    /// <summary>
    /// 角色图片列表
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();


    /// <summary>
    /// 角色名字对应的图片的字典
    /// </summary>
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();



    /// <summary>
    /// 文字出现速度
    /// </summary>
    public float textspeed = 0.1f;


    /// <summary>
    /// 对话文本按行分割
    /// </summary>
    public string[] dialogRows;

    /// <summary>
    /// 对话继续按钮
    /// </summary>
    public Button nextButton;



    /// <summary>
    /// 对话结束按钮
    /// </summary>
    public Button ENDButton;



    /// <summary>
    /// 选择按钮预制体
    /// </summary>
    public GameObject optionButton;


    /// <summary>
    /// 选项按钮父节点，用于自动排序
    /// </summary>
    public Transform buttonGroup;


    /// <summary>
    /// 文字是否输入完毕
    /// </summary>
    public bool textFinished = true;
    public bool cancelTyping;


    private void Awake()
    {

        imageDic["离"] = sprites[0];
        imageDic["李舒"] = sprites[1];
        
    }

    void Start()
    {
        ReadText(dialogDataFile);
        ShowDialogRow();
        //UpdateText("姜维", "皱皮");
        //UpdateImage("凯尔希", false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyUp(KeyCode.O)) 
        //{
        //    OnClickNext();

        //}

    }

    /// <summary>
    /// 更新文本信息
    /// </summary>
    /// <param name="_name">角色名字</param>
    /// <param name="_text">对话内容</param>
    public void UpdateText(string _name, string _text)
    {

        textFinished = false;
        nameText.text = _name;
        dialogText.text = "";

        StartCoroutine(SetTextUI());

        IEnumerator SetTextUI()
        {
            //for (int i = 0; i <_text.Length; i++)
            //{
            //    dialogText.text += _text[i];
            //    yield return new WaitForSeconds(textspeed);
            //}

            int letter = 0;
            while (!cancelTyping && letter < _text.Length - 1)
            {

                dialogText.text += _text[letter];
                letter++;
                yield return new WaitForSeconds(textspeed);
            }

            dialogText.text = _text;
            cancelTyping = false;
            textFinished = true;


        }


    }

    /// <summary>
    /// 更新图片信息
    /// </summary>
    /// <param name="_name">角色名字</param>
    /// <param name="_atleft">是否出现在左侧</param>
    public void UpdateImage(string _name, string _position)
    {
        if (_position == "左")
        {
            spriteLeft.sprite = imageDic[_name];
        }
        else
        if (_position == "右")
        {
            spriteRight.sprite = imageDic[_name];
        }
    }

    public void ReadText(TextAsset _textasset)
    {
        dialogRows = _textasset.text.Split('\n');
        //foreach(var row in rows)
        //{
        //    string[] cell = row.Split(',');
        //}

    }

    /// <summary>
    /// 当前对话索引值
    /// </summary>
    public int dialogIndex;
    public void ShowDialogRow()
    {

        for (int i = 0; i < dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split(',');


            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);


                nextButton.gameObject.SetActive(true);
                dialogIndex = int.Parse(cells[5]);
                break;
            }

            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                nextButton.gameObject.SetActive(false);
                GenerateOption(i);
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                ENDButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
                //OnClickEnd();
                dialogIndex = int.Parse(cells[5]);
                break;
            }


        }
    }

    //点击继续对话
    public void OnClickNext()
    {
        if (textFinished && !cancelTyping)
        {
            ShowDialogRow();
        }
        else if (!textFinished && !cancelTyping)
        {
            cancelTyping = !cancelTyping;
        }


    }

    public QQ qq;
    //点击最后对话
    public void OnEndedNext()
    {

        if(qq._isWinning) {
            if (textFinished && !cancelTyping)
            {
                ShowDialogRow();
            }
            else if (!textFinished && !cancelTyping)
            {
                cancelTyping = !cancelTyping;
            }
        }
       


    }
    ////当对话结束的时候
    //public void OnClickEnd()
    //{

    //    // 获取PlayerMov脚本的实例
    //    PlayerMov otherScript = FindObjectOfType<PlayerMov>();

    //    otherScript.isInteraction = false;


    //}

    public void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split(",");
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            //绑定按钮事件
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener
                (
                delegate
                {
                    OnOptionClick(int.Parse(cells[5]));
                }
                );
            GenerateOption(_index + 1);


        }
    }

    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDialogRow();

        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }



}
