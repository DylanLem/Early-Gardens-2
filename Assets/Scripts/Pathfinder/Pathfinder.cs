using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinder
{


  public static List<Vector3> FindPath(Vector3 start_pos, Vector3 end_pos)
  {

    GameObject gridmanager = GameObject.FindWithTag("Grid");

    List<Node> final_path = new List<Node>();


    List<Node> open_nodes = new List<Node>();
    List<Node> closed_nodes = new List<Node>();


    Node start_node = new Node(start_pos, null, null, gridmanager);
    Node end_node = new Node(end_pos,null,null, gridmanager);

    start_node.destination = end_node;

    start_node.Determine_H_Cost();
    start_node.Determine_F_Cost();

    open_nodes.Add(start_node);


    while(open_nodes.Count > 0)
    {


      Node selected_node = Determine_Move(open_nodes,closed_nodes);
      closed_nodes.Add(selected_node);
      open_nodes.Remove(selected_node);


      if(Vector3.Distance(selected_node.grid_pos,end_node.grid_pos) < 2)
      {
        return Convert_Nodes_To_Vector(Create_Path(selected_node));
      }

      selected_node.Set_Neighbours(open_nodes,closed_nodes);
    }


    return null;
  }


  private static Node Determine_Move(List<Node> open_nodes, List<Node> closed_nodes)
  {

    Node selected_node = null;

    foreach(Node n in open_nodes)
      if(selected_node == null || selected_node.f > n.f)
        selected_node = n;


    return selected_node;
  }


  private static List<Node> Create_Path(Node n)
  {

    List<Node> node_path = new List<Node>();

    while(n.parent != null)
    {
      node_path.Add(n);

      n = n.parent;
    }

    return node_path;
  }

  private static List<Vector3> Convert_Nodes_To_Vector(List<Node> node_path)
  {
    if(node_path == null) return null;

    List<Vector3> tile_path = new List<Vector3>();

    for(int i = node_path.Count - 1; i >= 0; i--)
    {
      tile_path.Add(node_path[i].grid_pos);
    }

    return tile_path;
  }


}
