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

    string path = Application.persistentDataPath + "/save.donotkillme";

    FileStream stream = new FileStream(path, FileMode.Create);

    var data = player.GetComponent<player>().Pack_Data();

    formatter.Serialize(stream, data);

    stream.Close();
  }

}
