using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Save
{
  public List<int> hideCollections = new List<int>();
  public List<bool> isLevelPass = new List<bool>();
}

#region 具体存档方法
public class SaveManager
{
  /// <summary>
  /// 创建Save类存放数据<para />
  /// </summary>
  /// <returns></returns>
  public Save CreateSave()
  {
    Save save = new Save();
    // save.hideCollections.Add(GameMesMananger.firstLevelCurGetHiddenItemNum);
    return save;
  }
  /// <summary>
  /// 将创建的Save转换为Json存为文件<para />
  /// </summary>
  public void SaveByJSON()
  {
    Save save = CreateSave();
    string JsonString = JsonUtility.ToJson(save);
    StreamWriter sw = new StreamWriter(Application.dataPath + "/Data.pomo");
    sw.Write(JsonString);
    sw.Close();
  }
  /// <summary>
  /// 读取存档<para />
  /// </summary>
  public void LoadByJSON()
  {
    if (File.Exists(Application.dataPath + "/Data.pomo"))
    {
      StreamReader sr = new StreamReader(Application.dataPath + "/Data.pomp");
      string JsonString = sr.ReadToEnd();
      sr.Close();
      Save save = JsonUtility.FromJson<Save>(JsonString);
      // GameMesMananger.firstLevelCurGetHiddenItemNum = save.hideCollections;
    }
    else
    {
      Debug.LogError("File Not Found.");
    }
  }
}
#endregion