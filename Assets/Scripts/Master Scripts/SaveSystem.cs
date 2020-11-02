using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{

  public static void Save_Player(GameObject player)
  {
    BinaryFormatter formatter = new BinaryFormatter();

    string path = Application.persistentDataPath + "/save.player";

    FileStream stream = new FileStream(path, FileMode.Create);

    var data = player.GetComponent<player>().Pack_Data();

    formatter.Serialize(stream, data);

    stream.Close();
  }


  public static Dictionary<string,dynamic> Load_Player(GameObject player_target)
  {
    string path = Application.persistentDataPath + "/save.player";

    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();

      FileStream stream = new FileStream(path, FileMode.Open);

      var data = formatter.Deserialize(stream) as Dictionary<string,dynamic>;

      player_target.GetComponent<player>().Load_Data(data);

      stream.Close();
    }

    return null;
  }


  public static void Save_Earl(GameObject earl)
  {
    BinaryFormatter formatter = new BinaryFormatter();

    string path = Application.persistentDataPath + "/save.earl";

    FileStream stream = new FileStream(path, FileMode.Create);

    var data = earl.GetComponent<earlbrain>().Pack_Earl_Data();

    formatter.Serialize(stream, data);

    stream.Close();
  }

  public static void Save_Level_Grid(GameObject gridmanager)
  {
    BinaryFormatter formatter = new BinaryFormatter();

    string path = Application.persistentDataPath + "/" +
     gridmanager.GetComponent<gridmanager>().grid_name + "save.level";

    FileStream stream = new FileStream(path, FileMode.Create);

    var data = gridmanager.GetComponent<gridmanager>().Pack_Level_Data();

    formatter.Serialize(stream, data);

    stream.Close();
  }

  public static Dictionary<string,dynamic> Load_Level_Grid(GameObject grid_target)
  {
    string path = Application.persistentDataPath + "/" +
     grid_target.GetComponent<gridmanager>().grid_name + "save.level";

    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();

      FileStream stream = new FileStream(path, FileMode.Open);

      var data = formatter.Deserialize(stream) as List<dynamic>;

      grid_target.GetComponent<gridmanager>().Load_Level(data);

      stream.Close();
    }

    return null;
  }

}
