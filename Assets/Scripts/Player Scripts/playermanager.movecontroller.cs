using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class playermanager : MonoBehaviour
{
  Dictionary<string, Vector3> direction_to_vector = new Dictionary<string, Vector3>()
  {
    {"Up_Left", Vector3.up + Vector3.left},
    {"Up", Vector3.up},
    {"Up_Right", Vector3.up + Vector3.right},
    {"Right", Vector3.right},
    {"Down_Right", Vector3.right + Vector3. down},
    {"Down", Vector3.down},
    {"Down_Left", Vector3.down + Vector3.left},
    {"Left", Vector3.left}
  };



  public bool Move_Player(string direction)
  {
    // The logic of whether or not the move timer is ready is handled outside of this function
    move_timer = 0;


    // this method of indexing a dictionary is more complicated, but saves many lines of code.

    //setting the move vector and finding our empty squares
    Vector3 move = direction_to_vector[direction];
    List<Vector3> empty_squares = Gridmanager.GetComponent<gridmanager>().Get_Empty_Squares();


    if(Player.GetComponent<player>().is_sleeping)
      Player.GetComponent<player>().Wake_Up();

    if(! empty_squares.Contains(grid_pos + move))
    {
      //if the square isnt empty, might there be a earl we can push?
      foreach(GameObject earl in Earlmanager.GetComponent<earlmanager>().Get_Earl_List())
      {

        if(earl.GetComponent<earlbrain>().Get_Grid_Pos() == grid_pos + move)
         {
           //if we can push the earl, move!
           if(Push_Earl(earl,direction)) goto ContinueMove;
           return false;
         }
      }

      //If it wasn't an earl, is there a pushable item?
      Item[,] item_grid = Itemmanager.GetComponent<itemmanager>().items_on_grid;
      for(int i = 0; i < item_grid.GetLength(0); i++)
        for(int j = 0; j < item_grid.GetLength(1); j++)
          {
            var item = item_grid[i,j];
            if(item == null) continue;

            if(item.grid_pos == grid_pos + move && item.is_pushable)
            {
              if(Itemmanager.GetComponent<itemmanager>().Push_Item(item,move)) goto ContinueMove;
            }
          }
      //if no push, then we clearly cant move!
      return false;
    }

    ContinueMove:

      Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"remove");

      //updating player positional data
      Player.transform.position += move;
      grid_pos += move;

      Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"add",Player);

      //updating camera
      if(camera_offset.x + move.x > -2 && camera_offset.x + move.x < 4) camera_offset.x += move.x;
      else Align_Camera(new Vector3(move.x,0));
      if(camera_offset.y + move.y > -2 && camera_offset.y + move.y < 4) camera_offset.y += move.y;
      else Align_Camera(new Vector3(0,move.y));


      Player.GetComponent<player>().grid_pos = grid_pos;
      return true;
  }

  private bool Push_Earl(GameObject earl, string direction)
  {
    return earl.GetComponent<earlbrain>().Move(direction);
  }
}
