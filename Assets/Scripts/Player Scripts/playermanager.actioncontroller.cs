using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class playermanager : MonoBehaviour
{

  //IF YOU WANNA ADD A NEW ACTION, DO IT HERE BABE


  // make sure you add a reference to your new action here :)
  public enum ContextActions
  {
    None,
    Grab,
    Throw,
    Build,
    Destroy
  }

  //pairing keys to actions
  Dictionary<ContextActions,KeyCode> actions = new Dictionary<ContextActions,KeyCode>()
  {
      {ContextActions.Grab, KeyCode.G},
      {ContextActions.Throw, KeyCode.T},
      {ContextActions.Build, KeyCode.B},
      {ContextActions.Destroy, KeyCode.D}
  };

  // defining the variable to check our currently toggled action
  private ContextActions current_ContextAction = ContextActions.None;



  private void Check_Action(List<KeyCode> keys_pressed)
  {


    foreach(KeyValuePair<ContextActions,KeyCode> action in actions)
    {
      if(keys_pressed.Contains(action.Value))
      {
        Set_Context_Action(action.Key);
      }
    }



    foreach(KeyValuePair<string,List<KeyCode>> direction in directions)
    {
      foreach(KeyCode control_variant in direction.Value)
      if(keys_pressed.Contains(control_variant))
      {
        if (move_timer < 0.18) return;
        if (current_ContextAction != ContextActions.None && Input.GetKey(KeyCode.Space)) Do_Context_Action(current_ContextAction,direction.Key);
        if (move_timer < 0.18) return;
        Move_Player(direction.Key);

      }
    }
  }

  public void Set_Context_Action(ContextActions action)
  {

    switch(action)
    {
      case(ContextActions.Grab):
      {
        grab.GetComponent<ActionButton>().Send_Select_To_Manager();
        break;
      }
      case(ContextActions.Build):
      {

        build.GetComponent<ActionButton>().Send_Select_To_Manager();
        break;
      }
      case(ContextActions.Destroy):
      {

        destroy.GetComponent<ActionButton>().Send_Select_To_Manager();
        break;
      }
    }
    current_ContextAction = action;
  }

  private void Do_Context_Action(ContextActions action, string direction="none")
  {
    //If we are holding space, this will call the desired action function.


    switch(action)
    {
      case ContextActions.Grab:
        Grab_Object(direction);
        break;
      case ContextActions.Throw:
        Throw_Object(direction);
        break;
      case ContextActions.Build:
        Build_Object(direction);
        break;
      case ContextActions.Destroy:
        Destroy_Object(direction);
        break;
    }
  }

  public void Grab_Object(string direction)
  {
    //if we are carrying an earl
    if(Player.GetComponent<player>().carried_object != null || Player.GetComponent<player>().carried_earl != null)
    {
      Place_Object(direction);
      return;
    }

    Vector3 move = direction_to_vector[direction];

    //checking for earls on grid
    foreach(GameObject earl in Earlmanager.GetComponent<earlmanager>().Get_Earl_List())
    {

      if(earl.GetComponent<earlbrain>().Get_Grid_Pos() == grid_pos + move)
       {
         Player.GetComponent<player>().carried_earl = earl;
         earl.GetComponent<earlbrain>().Become_Carried();
       }
    }

    //checking for items on grid
    items_on_grid = Itemmanager.GetComponent<itemmanager>().items_on_grid;

    for(int i = 0; i < items_on_grid.GetLength(0); i++)
    for(int j = 0; j < items_on_grid.GetLength(1); j++)
    {
      if(items_on_grid[i,j] != null && ! items_on_grid[i,j].is_building)
      {

        if(items_on_grid[i,j].grid_pos == grid_pos + move)
        {

          if(Player.GetComponent<player>().carried_object != null)
          {

            if(Itemmanager.GetComponent<itemmanager>().Add_To_Inventory(items_on_grid[i,j]))
            {
              Gridmanager.GetComponent<gridmanager>().grid[(int)items_on_grid[i,j].grid_pos.x,(int)items_on_grid[i,j].grid_pos.y].
                GetComponent<tilebehavior>().Remove_From_Tile();
              items_on_grid[i,j].is_placed = false;
              items_on_grid[i,j] = null;
            }
          }

          else
          {
            Debug.Log("should be remove");
            Itemmanager.GetComponent<itemmanager>().Add_To_Inventory(items_on_grid[i,j]);
            Player.GetComponent<player>().carried_object = items_on_grid[i,j];

            Gridmanager.GetComponent<gridmanager>().grid[(int)items_on_grid[i,j].grid_pos.x,(int)items_on_grid[i,j].grid_pos.y].
              GetComponent<tilebehavior>().Remove_From_Tile();
            items_on_grid[i,j].is_placed = false;
            items_on_grid[i,j] = null;

            Player.GetComponent<player>().Display_Carried_Item();
          }
        }
      }
    }
  }

  public bool Place_Object(string direction)
  {

    Vector3 move = direction_to_vector[direction];

    if(Gridmanager.GetComponent<gridmanager>().Get_Empty_Squares().Contains(grid_pos + move))
     {

       if(Player.GetComponent<player>().carried_earl != null)
       {

        Player.GetComponent<player>().carried_earl.GetComponent<earlbrain>().Become_Placed(grid_pos + move);
         Player.GetComponent<player>().carried_earl = null;
       }

       if(Player.GetComponent<player>().carried_object != null)
       {
         Itemmanager.GetComponent<itemmanager>().Spawn_On_Tile(Player.GetComponent<player>().carried_object, Gridmanager.GetComponent<gridmanager>().grid[(int)(grid_pos + move).x, (int)(grid_pos + move).y]);
         Player.GetComponent<player>().Remove_From_Inventory((int)Player.GetComponent<player>().carried_object.inv_pos.x, (int)Player.GetComponent<player>().carried_object.inv_pos.y);
         Player.GetComponent<player>().carried_object.is_placed = true;
         Player.GetComponent<player>().carried_object = null;
         Destroy(Player.GetComponent<player>().carried_physrep);
       }
       move_timer = 0;
       return true;
     }
    return false;
  }

  public bool Throw_Object(string direction)
  {
    Vector3 move = direction_to_vector[direction];
    var e = Player.GetComponent<player>().carried_earl;
    var o = Player.GetComponent<player>().carried_object;

    if(e == null && o == null) return false;

    //time to toss earl
    if(Place_Object(direction) == false) return false;


    //he's gonna go however many squares.
    for(int i = 0; i < 3; i++)
    {
      //gives it that throwfeel
      //technically its a push
      if(e != null)
        Push_Earl(e,direction);
      if(o != null)
        Itemmanager.GetComponent<itemmanager>().Push_Item(o,move);
    }
    return true;
  }

  public bool Build_Object(string direction)
  {
    Vector3 move = direction_to_vector[direction];

    var carried_obj = Player.GetComponent<player>().carried_object;

    if(carried_obj == null || ! carried_obj.GetType().IsSubclassOf(typeof(Building_Item))) return false;

    var i = (Building_Item)Player.GetComponent<player>().carried_object;

    move_timer = 0;

    if(Place_Object(direction))
    {
      i.Build();
      return true;
    }

    return false;
  }


  public bool Destroy_Object(string direction)
  {
    Vector3 move = direction_to_vector[direction] + grid_pos;
    var item_grid = Itemmanager.GetComponent<itemmanager>().items_on_grid;



    if(move.x < item_grid.GetLength(0) - 1 && move.x >= 0 && move.y < item_grid.GetLength(1) && move.y >= 0)
      return Itemmanager.GetComponent<itemmanager>().Delete_Item(Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)move.x,(int)move.y]);

    return false;
  }




}
