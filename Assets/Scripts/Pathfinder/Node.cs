using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

  public GameObject Gridmanager;


  public Node destination,parent;

  public float f, g, h = 0;
  public Vector3 grid_pos;

  public Dictionary<string,Node> neighbours;

  public Node(Vector3 _grid_pos, Node _destination, Node _parent, GameObject gridmanager)
  {

    grid_pos = _grid_pos;
    destination = _destination;
    parent = _parent;
    Gridmanager = gridmanager;

    Determine_G_Cost(parent);
    Determine_H_Cost();
    Determine_F_Cost();

  }

  public bool Determine_G_Cost(Node parent)
  {
    if(parent == null) return false;

    if(Vector3.Distance(grid_pos,parent.grid_pos) + parent.g < g)
    {
      g = Vector3.Distance(grid_pos,parent.grid_pos) + parent.g;
      this.parent = parent;
      return true;
    }

    return false;
  }

  public void Determine_H_Cost()
  {
    if(destination == null) return;
    h = Vector3.Distance(grid_pos,destination.grid_pos);
  }

  public void Determine_F_Cost()
  {
    f = g + h;
  }



  public void Set_Neighbours(List<Node> open_nodes,List<Node> closed_nodes)
  {



    neighbours = new Dictionary<string,Node>()
    {
      {"Top-Left",null}, {"Top-Mid",null}, {"Top-Right",null},
      {"Mid-Left",null},/*Your building !*/{"Mid-Right",null},
      {"Bot-Left",null}, {"Bot-Mid",null}, {"Bot-Right",null}
    };

    int dict_indexer = -1;

    for(int row = (int)grid_pos.y + 1; row >= grid_pos.y - 1; row--)
      for(int column = (int)grid_pos.x - 1; column <= grid_pos.x + 1;  column++)
      {
        //skips the tile the building is on
        if(new Vector3(column,row) == grid_pos) continue;

        dict_indexer += 1;


        foreach(Node node in open_nodes)
        {
          if(node.grid_pos == new Vector3(column,row))
          {
            neighbours[neighbours.ElementAt(dict_indexer).Key] = node;
            node.Determine_G_Cost(this);
            node.Determine_F_Cost();
          }
        }

        foreach(Node node in closed_nodes)
        {
          if(node.grid_pos == new Vector3(column,row))
            neighbours[neighbours.ElementAt(dict_indexer).Key] = node;
        }

        if(neighbours[neighbours.ElementAt(dict_indexer).Key] == null)
          if(Gridmanager.GetComponent<gridmanager>().Get_Empty_Squares().Contains(new Vector3(column,row)))
          {
            neighbours[neighbours.ElementAt(dict_indexer).Key] = new Node(new Vector3(column,row),destination,this, Gridmanager);
            open_nodes.Add(neighbours[neighbours.ElementAt(dict_indexer).Key]);
          }
      }
  }
}
