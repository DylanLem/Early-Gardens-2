using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class earlbrain : MonoBehaviour
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

  public List<string> Determine_Moves()
  {
    List<string> available_moves = new List<string>();

    foreach(KeyValuePair<string,Vector3> move in direction_to_vector)
    {
      if(Gridmanager.GetComponent<gridmanager>().Get_Empty_Squares().Contains(grid_pos + move.Value)) available_moves.Add(move.Key);
    }

    return available_moves;

  }

  public bool Move(string direction)
  {

    if(! Determine_Moves().Contains(direction)) return false;

    Vector3 new_pos = grid_pos + direction_to_vector[direction];

    Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"remove");

    grid_pos = new_pos;
    transform.position += direction_to_vector[direction];

    Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"add",gameObject);

    return true;

  }

  public bool Move(Vector3 new_pos)
  {

    if(Gridmanager.GetComponent<gridmanager>().Get_Tile(new_pos).GetComponent<tilebehavior>().Is_Empty() != true) return false;

    Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"remove");

    grid_pos = new_pos;
    transform.position = new_pos;

    Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"add",gameObject);

    return true;

  }




}
