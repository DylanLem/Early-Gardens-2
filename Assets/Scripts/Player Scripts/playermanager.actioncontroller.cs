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
    move_timer = 0;

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

    //This is a horrible function and it needs to be burned
    //or refactored.

    //so many bugs came from this its a little bug hive

    Vector3 move = direction_to_vector[direction];

    if( (grid_pos + move).x < 0 || (grid_pos + move).y < 0
    || (grid_pos + move).x > Gridmanager.GetComponent<gridmanager>().grid.GetLength(0)
    || (grid_pos + move).y > Gridmanager.GetComponent<gridmanager>().grid.GetLength(0) )
    {
      return;
    }

    //if we are carrying an thing
    if(Player.GetComponent<player>().carried_object != null || Player.GetComponent<player>().carried_earl != null)
    {
      Place_Object(direction);
      return;
    }



    //checking for earls on grid

    Debug.Log(Earlmanager.GetComponent<earlmanager>().Get_Earl_Grid());
    if(Earlmanager.GetComponent<earlmanager>().Get_Earl_Grid()[(int)(move + grid_pos).x,(int)(move + grid_pos).y] != null)
    {
      GameObject earl = Earlmanager.GetComponent<earlmanager>().Get_Earl_Grid()[(int)(move + grid_pos).x,(int)(move + grid_pos).y];

      if(earl.GetComponent<earlbrain>().Get_Grid_Pos() == grid_pos + move)
       {
         Player.GetComponent<player>().carried_earl = earl;
         earl.GetComponent<earlbrain>().Become_Carried();

         return;
       }
    }

    //checking for items on grid
    items_on_grid = Itemmanager.GetComponent<itemmanager>().items_on_grid;

    Item item = items_on_grid[(int)(grid_pos + move).x ,(int)(grid_pos + move).y ];

    if(item == null) return;

      //if it's a building try and do a thing with it !
    if(item.is_building == true)
    {
      Interact_With_Object(direction);
      return;
    }



    //If we're holding something, fug it try and pick it up if she stacks.
    if(Player.GetComponent<player>().carried_object != null)
    {

      if(Itemmanager.GetComponent<itemmanager>().Add_To_Inventory(item))
      {
        Gridmanager.GetComponent<gridmanager>().grid[(int)item.grid_pos.x,(int)item.grid_pos.y].
          GetComponent<tilebehavior>().Remove_From_Tile();
        item.is_placed = false;
        items_on_grid[(int)(move + grid_pos).x,(int)(move + grid_pos).y] = null;

        return;
      }

    }

    //If we're not holding something, pick it up
    else
    {

      Itemmanager.GetComponent<itemmanager>().Add_To_Inventory(item);
      Player.GetComponent<player>().carried_object = item;

      Gridmanager.GetComponent<gridmanager>().grid[(int)item.grid_pos.x,(int)item.grid_pos.y].
        GetComponent<tilebehavior>().Remove_From_Tile();
      item.is_placed = false;
      items_on_grid[(int)(move + grid_pos).x,(int)(move + grid_pos).y] = null;

      Player.GetComponent<player>().Display_Carried_Item();


      return;
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



    if(Place_Object(direction))
    {
      i.Build();
      return true;
    }

    return false;
  }

  public bool Interact_With_Object(string direction)
  {
      Vector3 move = direction_to_vector[direction] + grid_pos;

      if(items_on_grid[(int)move.x,(int)move.y]?.is_building == true)
      {
        Building b = (Building)items_on_grid[(int)move.x,(int)move.y];

        b.Interact(Player);

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
