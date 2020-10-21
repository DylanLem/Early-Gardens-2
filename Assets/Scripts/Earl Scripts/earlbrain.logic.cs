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


  public string Get_Target_Direction()
  {
    //It ain't pretty but it works. Targets are always type Item
    Vector3 t_pos = target.grid_pos;
    Vector3 e_pos = grid_pos;

    List<string> moves = Determine_Moves();
    float best_move = 999f;
    string selected_move = null;

    foreach(string move in moves)
    {
      float d = Vector3.Distance(grid_pos + direction_to_vector[move], target.grid_pos);

      if(d < best_move)
       {
        selected_move = move;
        best_move = d;
        }
    }

    if(selected_move != null) return selected_move;
    //if nothing is good, just move random, k?
    return moves[UnityEngine.Random.Range(0,moves. Count() -1)];

   }


  private Moods Determine_Mood()
  {
    if (mood == Moods.Carried) return Moods.Carried;

    if (mood == Moods.Eating) return Moods.Eating;

    if (satiety<50 && Find_Food() != null) return Moods.Hungry;
    if (satiety>100) return Moods.Starving;
    return Moods.Idle;
  }


  private void Determine_Action()
  {


    //can earl move? act_timer will tell us.
    act_timer += Time.deltaTime;

    if(act_timer < speed) return;

    //a list of directions earl may move
    var moves = Determine_Moves();
    act_timer = 0;


    //Earl does an action based on he's mood.
    switch(mood)
    {
      //he's just gonna walk in some random direction. stupid
      case Moods.Idle:
        Move(moves[UnityEngine.Random.Range(0,moves.Count - 1)]);
        break;

      //he's somehow fat AND starving always
      case Moods.Hungry:
          target = Find_Food();

          //we need to do this because earls mood can change in that method.
          if(mood == Moods.Hungry)
            Move(Get_Target_Direction());
          break;

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
          Determine_Action();
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
