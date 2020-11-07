using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stake: Building_Item
{

  public Stake()
  {
    building_id = 8;


    name = "Wooden Stake";
    description = "Mulitpurpose. But mainly fences. Vampyearls maybe sometimes";
    tag = "Building Item";
    sprites = new Sprite[]
    {SpriteManager.FindSpriteFromSheet(SpriteManager.FenceSprites,"fence_post")};
    initial_sprite = sprites[0];

    Set_Phys_Rep();
  }

}
