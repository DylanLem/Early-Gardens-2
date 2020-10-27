using System;
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

  //the neighbouring tiles
  public List<GameObject> neighbours;

  public Plant()
  {
    id = 4;
    age = 0f;
    fertility_cycle = 0f;
    life_stage = 1;
    growth_stages = 3;
    fertility = 5f;
    grow_time = 2f;

    fruit_id = 0;

    name = "Clant";
    description = "If they ain't eatin' this, they're eatin' you!";
    tag = "Plant";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("items/cfruit_3"),
     Resources.Load<Sprite>("items/cfruit_2"),
     Resources.Load<Sprite>("items/cfruit_1"),
     Resources.Load<Sprite>("items/cfruit_0")};

    Set_Phys_Rep();


  }

  protected void Set_Neighbours()
  {
    neighbours = new List<GameObject>();
    var gridmanager = GameObject.FindWithTag("Grid");

    for(int row = (int)grid_pos.y - 1; row <= grid_pos.y + 1; row++)
      for(int column = (int)grid_pos.x - 1; column <= grid_pos.x + 1;  column++)
      {

        if(gridmanager.GetComponent<gridmanager>().Get_Tile(new Vector3(column,row)) == null) continue;

        neighbours.Add(gridmanager.GetComponent<gridmanager>().Get_Tile(new Vector3(column,row)));
      }
  }

  public override void Update()
  {

    if(neighbours == null) Set_Neighbours();

    if (life_stage <= growth_stages)
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
    }
  }

  public void Bear_Fruit()
  {

    if(fertility_cycle >= fertility)
    {
      GameObject selected_tile = neighbours[UnityEngine.Random.Range(0,neighbours.Count)];
      
      if(GameObject.FindWithTag("Item Manager").GetComponent<itemmanager>().Spawn_On_Tile(fruit_id,selected_tile))
      {
        Debug.Log("AY");
        fertility_cycle = 0;
        return;
      }
    }
  }

  public void Die()
  {

  }


}
