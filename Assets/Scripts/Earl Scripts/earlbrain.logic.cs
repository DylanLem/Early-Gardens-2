using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public partial class earlbrain : MonoBehaviour
{


  public Item Find_Food()
  {
    //First we have to find the food itself
    List<Item> found_food = Itemmanager.GetComponent<itemmanager>().Find_Items_By_Tag("Food");

    //Give up if theres nothing :( poor hungry earls. perish the thought
    if (found_food.Count() == 0) return null;


    float[] food_distances = new float[found_food.Count()];

    for(int i = 0; i < found_food.Count();i++)
    {

      food_distances[i] = Vector3.Distance(grid_pos,found_food[i].grid_pos);
    }

    Item target_food = found_food[Array.IndexOf(food_distances,food_distances.Min())];

    //if it's within sqrt(2) distance units, we can feast
    if (Vector3.Distance(grid_pos,target_food.grid_pos) <= 1.45f)
    {
      mood = Moods.Eating;
      return target_food;
    }

    return target_food;
  }

  public List<Vector3> Find_Target_Path(Item item)
  {
    Vector3 target_pos = item.grid_pos;

    List<Vector3> path = Pathfinder.FindPath(grid_pos,target_pos);

    return path;
  }


  public Vector3 Get_Target_Direction()
  {
    List<string> moves = Determine_Moves();

    List<Vector3> path = Find_Target_Path(target);


    if(path == null)
        return grid_pos + direction_to_vector[moves[UnityEngine.Random.Range(0,moves.Count)]];

    Debug.Log("HEY");
    return path[0];
   }


  private Moods Determine_Mood()
  {
    if (mood == Moods.Carried) return Moods.Carried;

    if (mood == Moods.Eating) return Moods.Eating;

    if (satiety<50 && Find_Food() != null) return Moods.Hungry;
    if (satiety<=-100) return Moods.Starving;
    return Moods.Idle;
  }


  private void Determine_Action()
  {


    //can earl move? act_timer will tell us.
    act_timer += Time.deltaTime;

    if(act_timer < speed) return;

    //a list of directions earl may move
    var moves = Determine_Moves();

    if(moves.Count() == 0) return;


    act_timer = 0;


    //Earl does an action based on he's mood.
    switch(mood)
    {
      //he's just gonna walk in some random direction. stupid
      case Moods.Idle:
      {
        Move(moves[UnityEngine.Random.Range(0,moves.Count - 1)]);
        break;
      }
      //he's somehow fat AND starving always
      case Moods.Hungry:
      {
        target = Find_Food();

        //we need to do this because earls mood can change in that method.
        if(mood == Moods.Hungry)
        {
          Vector3 move = Get_Target_Direction();

          Move(move);
        }
        break;
      }

      case Moods.Eating:
      {
        Find_Food();

        if(satiety >= 100f || target == null) mood = Moods.Idle;
        else
        {
          Eat();
        }
        break;
      }

      case Moods.Starving:
      {
        Take_Damage(10);
        mood = Moods.Idle;
        attitude = 0;

        break;
      }

      case Moods.Carried:
      {
        Display_Earl_Data();
        break;
      }
    }


  }

}
