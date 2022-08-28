using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
  /// <summary>
  /// 对话文本文件，csv格式
  /// </summary>
  public TextAsset dialogDataFile;
  /// <summary>
  /// 左侧人物立绘
  /// </summary>
  public SpriteRenderer spriteLeft;
  /// <summary>
  /// 右侧人物立绘
  /// </summary>
  public SpriteRenderer spriteRight;
  /// <summary>
  /// 角色名字文本
  /// </summary>
  public TextMeshProUGUI nameText;
  /// <summary>
  /// 对话文本，按行分割
  /// </summary>
  private string[] dialogRows;
  private int dialogIndex;
  /// <summary>
  /// 对话内容文本
  /// </summary>
  public TextMeshProUGUI dialogText;
  public List<Sprite> sprites = new List<Sprite>();
  public Button nextButton;
  /// <summary>
  /// 选项按钮预制体
  /// </summary>
  public GameObject optionButton;
  /// <summary>
  /// 选项按钮父节点，用于排列
  /// </summary>
  public Transform buttonGroup;
  Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
  private void Awake()
  {
    ReadText(dialogDataFile);
    // imageDic["仙人"] = sprites[0];
    // imageDic["虾"] = sprites[1];
  }

  private void Start()
  {
    // UpdateText("仙人", "你吃了吗？");
  }

  public void UpdateText(string _name, string _text)
  {
    nameText.text = _name;
    dialogText.text = _text;
  }

  public void UpdateImage(string _name, string _position)
  {
    if (_position == "左")
    {
      spriteLeft.sprite = imageDic[_name];
    }
    else if (_position == "右")
    {
      spriteRight.sprite = imageDic[_name];
    }
  }

  public void ReadText(TextAsset _textAsset)
  {
    dialogRows = _textAsset.text.Split('\n');
  }
  public void ShowDialogRow()
  {
    for (int i = 0; i < dialogRows.Length; ++i)
    {
      string[] cells = dialogRows[i].Split(',');
      if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
      {
        nextButton.gameObject.SetActive(true);
        UpdateText(cells[2], cells[4]);
        // UpdateImage(cells[2], cells[3]);

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
        Debug.Log("剧情结束");
      }
    }
  }
  public void GenerateOption(int _index)
  {
    string[] cells = dialogRows[_index].Split(',');
    if (cells[0] == "&")
    {
      GameObject button = Instantiate<GameObject>(optionButton, buttonGroup);
      button.GetComponentInChildren<TextMeshProUGUI>().text = cells[4];
      button.GetComponent<Button>().onClick.AddListener(
        delegate
        {
          OnOptionClick(int.Parse(cells[5]));
        }
      );
      GenerateOption(_index + 1);
    }
  }
  public void OnClickNext()
  {
    ShowDialogRow();
  }
  public void OnOptionClick(int _id)
  {
    dialogIndex = _id;
    ShowDialogRow();
    for (int i = 0; i < buttonGroup.childCount; ++i)
    {
      Destroy(buttonGroup.GetChild(i).gameObject);
    }
  }
}
