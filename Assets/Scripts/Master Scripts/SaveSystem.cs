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

    string path = Application.persistentDataPath + "/" + earl.GetComponent<earlbrain>().name +
     "_save.earl";

    FileStream stream = new FileStream(path, FileMode.Create);

    var data = earl.GetComponent<earlbrain>().Pack_Earl_Data();

    formatter.Serialize(stream, data);

    stream.Close();
  }


  public static Dictionary<string,dynamic> Load_Earl(string earl_name)
  {

    string path = Application.persistentDataPath + earl_name + "_save.earl";

    if (File.Exists(path))
      {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Open);

        var data = formatter.Deserialize(stream) as Dictionary<string,dynamic>;

        stream.Close();

        return data;
      }

    return null;
  }

  public static List<dynamic> Load_Earls(string earl_grid)
  {
    DirectoryInfo savedir = new DirectoryInfo(Application.persistentDataPath + "/");

    FileInfo[] paths = savedir.GetFiles("*" + "_save.earl");

    List<dynamic> earls_data = new List<dynamic>();




    foreach(FileInfo path in paths)
    {
      string f = path.FullName;

      if (File.Exists(f))
        {
          BinaryFormatter formatter = new BinaryFormatter();

          FileStream stream = new FileStream(f, FileMode.Open);

          var data = formatter.Deserialize(stream) as Dictionary<string,dynamic>;

          stream.Close();

          if(data["home_grid"] == earl_grid)
            earls_data.Add(data);

        }
    }

    return earls_data;
  }

  public static void Save_Level_Grid(GameObject gridmanager)
  {
    BinaryFormatter formatter = new BinaryFormatter();

    string path = Application.persistentDataPath + "/" +
     gridmanager.GetComponent<gridmanager>().grid_name + "_save.level";

    FileStream stream = new FileStream(path, FileMode.Create);

    var data = gridmanager.GetComponent<gridmanager>().Pack_Level_Data();

    formatter.Serialize(stream, data);

    stream.Close();
  }

  public static void Load_Level_Grid(GameObject grid_target)
  {
    string path = Application.persistentDataPath + "/" +
     grid_target.GetComponent<gridmanager>().grid_name + "_save.level";

    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();

      FileStream stream = new FileStream(path, FileMode.Open);

      var data = formatter.Deserialize(stream) as List<dynamic>;

      grid_target.GetComponent<gridmanager>().Load_Level(data);

      stream.Close();
    }

  }

}
