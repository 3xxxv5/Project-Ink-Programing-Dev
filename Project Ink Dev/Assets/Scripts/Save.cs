using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Save
{
    public List<int> hideCollections = new List<int>() { 0, 0, 0 };
    public List<bool> isLevelPass = new List<bool>() { true, false, false , false };
    public List<string> itemMap = new List<string>();
}

#region 具体存档方法
public class SaveManager
{

    private static string path = Application.dataPath + "/Data.pomo";
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
  public static void SaveByJSON(Save save)
  {
    string JsonString = JsonUtility.ToJson(save);
    StreamWriter sw = new StreamWriter(path);
    sw.Write(JsonString);
    sw.Close();
  }
  /// <summary>
  /// 读取存档<para />
  /// </summary>
  public static Save LoadByJSON()
  {
    if (File.Exists(path))
    {
      StreamReader sr = new StreamReader(path);
      string JsonString = sr.ReadToEnd();
      sr.Close();
      Save save = JsonUtility.FromJson<Save>(JsonString);
      // GameMesMananger.firstLevelCurGetHiddenItemNum = save.hideCollections;
      return save;
    }
    else
    {
            Save save = new Save();
            return save;
    }
  }
}
#endregion