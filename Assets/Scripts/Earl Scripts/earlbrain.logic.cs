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

    //Setting up our food find algo. Using the distances to sort the food in ascending order.
    // We'll check each food in order of absolute distance.
    Item[] food_array = found_food.ToArray();

    Array.Sort(food_distances, food_array);

    Item target_food = null;


    foreach(Item food in food_array)
    {
      if(Get_Target_Direction(food) != Vector3.positiveInfinity)
      {
        target_food = food;
        break;
      }
    }

    if(target_food == null)
      return null;


    //if it's within sqrt(2) distance units, we can feast
    if(Vector3.Distance(grid_pos,target_food.grid_pos) <= 1.45f)
    {
      mood = Moods.Eating;
      return target_food;
    }

    return target_food;
  }

  public List<Vector3> Find_Target_Path(Item item)
  {
    if(item == null) return null;

    Vector3 target_pos = item.grid_pos;

    List<Vector3> path = Pathfinder.FindPath(grid_pos,target_pos);

    return path;
  }


  public Vector3 Get_Target_Direction(Item item)
  {


    List<string> moves = Determine_Moves();

    List<Vector3> path = Find_Target_Path(item);


    if(path == null || path.Count == 0)
        return Vector3.positiveInfinity;

    return path[0];
   }


  private Moods Determine_Mood()
  {
    if (mood == Moods.Carried) return Moods.Carried;

    if (mood == Moods.Eating) return Moods.Eating;

    if (satiety<50) return Moods.Hungry;
    if (satiety<=-100 && Find_Food() == null) return Moods.Starving;
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

        if(target != null && Vector3.Distance(grid_pos,target.grid_pos) <= 1.45f)
        {
          mood = Moods.Eating;
          break;
        }

        target = Find_Food();

        if(target == null)
        {
          Move(moves[UnityEngine.Random.Range(0,moves.Count - 1)]);
          break;
        }

        //we need to do this because earls mood can change in that method.
        if(mood == Moods.Hungry)
        {
          Vector3 move = Get_Target_Direction(target);

          Move(move);
        }
        break;
      }

      case Moods.Eating:
      {

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
