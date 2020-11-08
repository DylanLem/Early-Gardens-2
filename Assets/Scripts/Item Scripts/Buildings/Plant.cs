using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plant : Building
{


  //What does this plant create (use item id)?
  public int fruit_id;

  //the amount of time the plant has been alive
  public float age;

  //the plant's maturity level
  public int life_stage;

  //how many stages of growth does this plant have?
  public int growth_stages;

  //how often it produces fruit
  public float fertility;

  //internal clock for fruit bearing
  public float fertility_cycle;

  //how long it takes for each stage
  public float grow_time;



  public Plant() : base()
  {
    age = 0f;
    fertility_cycle = 0f;
    life_stage = 1;
    growth_stages = 3;
    fertility = 5f;
    grow_time = 2f;

    fruit_id = 0;
    neighbours = null;

    name = "Clant";
    description = "If they ain't eatin' this, they're eatin' you!";
    tag = "Plant";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("buildings/tree_0"),
     Resources.Load<Sprite>("buildings/tree_1"),
     Resources.Load<Sprite>("buildings/tree_2")};

    Set_Phys_Rep();
  }



  public override void Update()
  {

    if(neighbours == null)
    {
      Set_Neighbours();
      Set_Dirt_Tile();
    }

    if (life_stage < growth_stages)
    {

      age += Time.deltaTime;
      Attempt_Growth();
    }
    else
    {
      fertility_cycle += Time.deltaTime;
      Bear_Fruit();
    }
  }

  public void Attempt_Growth()
  {
    if(age > grow_time * life_stage)
    {
      life_stage += 1;
      Update_Sprite(sprites[current_sprite + 1]);
          current_sprite += 1;
    }
  }

  public void Bear_Fruit()
  {

    if(fertility_cycle >= fertility)
    {
      GameObject selected_tile = neighbours.ElementAt(UnityEngine.Random.Range(0,neighbours.Count)).Value;

      if(GameObject.FindWithTag("Item Manager").GetComponent<itemmanager>().Spawn_On_Tile(fruit_id,selected_tile))
      {
        fertility_cycle = 0;
        return;
      }
    }
  }

  public void Set_Dirt_Tile()
  {
    var gridmanager = GameObject.FindWithTag("Grid");
    gridmanager.GetComponent<gridmanager>().Get_Tile(grid_pos).GetComponent<tilebehavior>().Set_Sprite(SpriteManager.FindSpriteFromSheet(SpriteManager.EnvironmentSprites, "environment_farmland"));
  }

  public override Dictionary<string,dynamic> Pack_Data()
  {
    Dictionary<string,dynamic> item_data = new Dictionary<string,dynamic>()
    {
      {"id", id},
      {"count", count},
      {"current_sprite", current_sprite},
      {"grid_pos", new int[3]{(int)grid_pos.x,(int)grid_pos.y,(int)grid_pos.z}},
      {"inv_pos", new int[3]{(int)inv_pos.x,(int)inv_pos.y,(int)inv_pos.z}},
      {"life_stage", life_stage}
    };

    return item_data;
  }

  public override void Load_Data(Dictionary<string,dynamic> item_data)
  {
    count = item_data["count"];
    current_sprite = item_data["current_sprite"];
    grid_pos = new Vector3(item_data["grid_pos"][0],item_data["grid_pos"][1],item_data["grid_pos"][2]);
    inv_pos = new Vector3(item_data["inv_pos"][0],item_data["inv_pos"][1],item_data["inv_pos"][2]);

    life_stage = item_data["life_stage"];

    Update_Sprite(sprites[current_sprite]);
  }


  public void Die()
  {

  }


}
