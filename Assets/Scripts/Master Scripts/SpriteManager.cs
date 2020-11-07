using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteManager
{

  public static Sprite[] FenceSprites = Resources.LoadAll<Sprite>("Buildings/Fences/fence");


  public static Sprite FindSpriteFromSheet(Sprite[] sheet, string sprite)
  {
    foreach(Sprite sp in sheet)
    {
      if (sp.name == sprite)
        return sp;
    }

    return null;
  }
}
