using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridmanager : MonoBehaviour

{
  public GameObject Tile;
  public Item[,] item_grid;
  public Sprite tile_sprite;
  //please try and make all grid dimensions a multiple of 2
  public int vertical, horizontal;


  public GameObject[,] grid  {get; private set;}

  private List<Vector3> empty_squares;


  //setting the grid's (0,0) so that it's centered on the plane's (0,0)
  //use this in conjunction with grid coordinates to place objects correctly.
  // e.g. Place_Object(new Vector3(grid_pos.x + anchor_x, grid_pos.y + anchor_y))
  int anchor_x, anchor_y;


    void Awake()
    {
      //making sure the grid is ready before we spawn the player
      grid = new GameObject[horizontal,vertical];


      anchor_x = (int)-(horizontal/2);
      anchor_y = (int)-(vertical/2);

      Populate_Grid();
    }
      // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Populate_Grid()
    {
      //Used to initially populate the grid array with empty tiles.
      //Each tile gameobject carries grid_pos and can fit a single item.
      for(int x = 0; x < horizontal; x++)
      {
        for(int y = 0; y < vertical; y++)
        {
          var tile = Instantiate(Tile,new Vector3(x + anchor_x, y + anchor_y),Quaternion.identity);
          tile.name = ("TILE " + x.ToString() + "," + y.ToString());
          tile.GetComponent<tilebehavior>().grid_pos = new Vector3(x,y);
          grid[x,y] = tile;


        }
      }

      Set_Empty_Squares();
    }


    public void Set_Empty_Squares()
    {
      //sets the list of empty squares to do logical processes with
      //called every time tile is updated
      empty_squares = new List<Vector3>();

      for(int x = 0; x < grid.GetLength(0); x++)
      {
        for(int y = 0; y < grid.GetLength(1); y++)
        {
          if (grid[x,y].GetComponent<tilebehavior>().Is_Empty())
          {
            empty_squares.Add(new Vector3(x,y));
          }
        }
      }
    }

    public List<Vector3> Get_Empty_Squares()
    {
      return empty_squares;
    }

    //potentially obsolete method but i'm scared of the ramifications if i remove
    public Vector3 Grid_Pos_to_Transform_Pos(Vector3 grid_pos)
    {
      return grid[(int)grid_pos.x,(int)grid_pos.y].GetComponent<tilebehavior>().transform.position;
    }


    //self describing method, finds a random empty square.
    public GameObject Find_Empty_Square()
    {

      if(empty_squares.Count == 0) return null;

      int rand = Random.Range(0,empty_squares.Count);

      Vector3 square_pos = empty_squares[rand];
      return grid[(int)square_pos.x,(int)square_pos.y];
    }

    public Vector3 Get_Tile_Pos(GameObject tile)
    {
      return tile.GetComponent<tilebehavior>().grid_pos;
    }

    //called to update the contents of a square in the grid.
    // **** UNFINISHED *****
    // potentially obsolete
    public void Update_Square(Vector3 grid_pos, string action, GameObject obj = null)
    {
      //is something being "add"ed or "remove"d?

      switch(action)
      {
        case "add":
          Get_Tile(grid_pos).GetComponent<tilebehavior>().Add_To_Tile(obj);
          break;

        case "remove":
          Get_Tile(grid_pos).GetComponent<tilebehavior>().Remove_From_Tile();
          break;
      }

      Set_Empty_Squares();

    }

    //pulls the tile at whatever grid location we desire :)
    public GameObject Get_Tile(Vector3 pos)
    {
      int x = (int)pos.x;
      int y = (int)pos.y;

      if(x < 0 || x > grid.GetLength(0) - 1 || y < 0 || y > grid.GetLength(1) - 1) return null;
      return grid[(int)pos.x,(int)pos.y];
    }


}
