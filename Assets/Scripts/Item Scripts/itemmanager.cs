using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class itemmanager : MonoBehaviour
{
    public GameObject Player;
    public GameObject playermanager;
    public Itemdatabase item_database;
    private GameObject[,] grid;
    private GameObject gridmanager;
    public Item[,] items_on_grid;


    void Awake()
    {
        item_database = GameObject.FindWithTag("Level Controller").GetComponent<LevelController>().itemDatabase;
        gridmanager = GameObject.FindWithTag("Grid");


    }

    void Start()
    {
      gridmanager = GameObject.FindWithTag("Grid");
      //Setting the reference from here so we don't get a null reference error
      gridmanager.GetComponent<gridmanager>().item_grid = items_on_grid;
      grid = gridmanager.GetComponent<gridmanager>().grid;
      items_on_grid = new Item[grid.GetLength(0),grid.GetLength(1)];



      SaveSystem.Load_Level_Grid(gridmanager);

    }
    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.P))
      {
        Give_Item(3);
      }
      if(Input.GetKeyDown(KeyCode.O))
      {
        Give_Item(2);
      }
      if(Input.GetKeyDown(KeyCode.I))
      {
        Give_Item(5);
      }

      for(int i = 0; i < items_on_grid.GetLength(0); i++)
      for(int j = 0; j < items_on_grid.GetLength(1); j++)
      {
        var temp_item = items_on_grid[i,j];

        if(temp_item != null && temp_item.is_building)
        {
          Building item = (Building)temp_item;
          item.Update();
        }
      }
    }

    //Give_Item() contains an override for if you wanna add many items

    public void Give_Item(int id)
    {
      var item = item_database.Get_Item(id);
      if(Add_To_Inventory(item)) return;
      item.Delete();
    }

    public void Give_Item(List<int> ids)
    {
      foreach(int id in ids)
      {
        var item = item_database.Get_Item(id);
        if(Add_To_Inventory(item)) return;
      }
    }



    public bool Add_To_Inventory(Item item)
    {


      var inventory = GameObject.FindWithTag("Player").GetComponent<player>()
      .inventory;




      //Gotta loop through inventory twice
      //first to see if we can stack any items
      //if not we check for the first empty slot


      for(int i = 0; i < inventory.GetLength(1); i++)
      for(int j = 0; j < inventory.GetLength(0); j++)
      {
        if(inventory[j,i] == null) continue;
        if(inventory[j,i].is_stackable && item.id == inventory[j,i].id)
        {

          //Inventory display will handle the item game objects now.
          inventory[j,i].count += 1;

          GameObject.FindWithTag("Player").GetComponent<player>().Send_Inv_Data();
          return true;
        }
      }

      for(int i = 0; i < inventory.GetLength(1); i++)
      for(int j = 0; j < inventory.GetLength(0); j++)
      {
        if(inventory[j,i] == null)
        {
          inventory[j,i] = item;
          if(item != null) inventory[j,i].count += 1;
          GameObject.FindWithTag("Player").GetComponent<player>().Send_Inv_Data();
          return true;
        }

      }

      return false;
    }


    public bool Spawn_On_Tile(int id, GameObject tile)
    {



     if(tile == null || ! tile.GetComponent<tilebehavior>().is_empty == true) return false;

     Item item = item_database.Get_Item(id);


      item.grid_pos = tile.GetComponent<tilebehavior>().grid_pos;
      item.is_placed = true;

      items_on_grid[(int)item.grid_pos.x,(int)item.grid_pos.y] = item;

      tile.GetComponent<tilebehavior>().Add_To_Tile(item.phys_rep);

      return true;
    }

    public bool Spawn_On_Tile(Item item, GameObject tile)
    {

     if(tile == null || ! tile.GetComponent<tilebehavior>().is_empty == true) return false;



      item.grid_pos = tile.GetComponent<tilebehavior>().grid_pos;
      item.is_placed = true;

      items_on_grid[(int)item.grid_pos.x,(int)item.grid_pos.y] = item;

      tile.GetComponent<tilebehavior>().Add_To_Tile(item.phys_rep);

      return true;
    }

    public bool Load_On_Level(Dictionary<string,dynamic> item_data)
    {


      Item item = item_database.Get_Item(item_data["id"]);

      item.Load_Data(item_data);

      Debug.Log(item.grid_pos);

      Debug.Log(gridmanager.name);
      Debug.Log(items_on_grid);

      GameObject target_tile = gridmanager.GetComponent<gridmanager>().Get_Tile(item.grid_pos);

      Spawn_On_Tile(item,target_tile);

      return true;
    }

    public bool Load_To_Inventory(Dictionary<string,dynamic> item_data)
    {
      if(item_data == null) return false;

      Item item = item_database.Get_Item(item_data["id"]);

      item.Load_Data(item_data);
      Add_To_Inventory(item);

      return true;
    }

    public List<Item> Find_Items_By_Tag(string t)
    {

      //gonna return this list of items that have the tag
      List<Item> item_matches = new List<Item>();

      for(int i = 0; i < items_on_grid.GetLength(0); i++)
        for(int j = 0; j < items_on_grid.GetLength(1); j++)
        {
          if (items_on_grid[i,j] == null) continue;
          if (items_on_grid[i,j].tag == t)
            item_matches.Add(items_on_grid[i,j]);
        }

      return item_matches;
    }

    public bool Delete_Item(Item item)
    {
      if(item == null) return false;

      //Delete the sprite
      Destroy(item.phys_rep);

      //Kill the item :)
      items_on_grid[(int)item.grid_pos.x,(int)item.grid_pos.y] = null;
      grid[(int)item.grid_pos.x,(int)item.grid_pos.y].GetComponent<tilebehavior>().Remove_From_Tile();
      item = null;

      // we did it
      return true;
    }

    public bool Push_Item(Item item, Vector3 direction)
    {
      int x = (int)(item.grid_pos + direction).x;
      int y = (int)(item.grid_pos + direction).y;

      if(x > grid.GetLength(0) - 1 || y > grid.GetLength(1) - 1 || x < 0 || y < 0) return false;

      if(grid[x,y].GetComponent<tilebehavior>().Is_Empty())
      {
        grid[(int)item.grid_pos.x,(int)item.grid_pos.y].GetComponent<tilebehavior>().Remove_From_Tile();
        items_on_grid[(int)item.grid_pos.x,(int)item.grid_pos.y] = null;

        item.grid_pos += direction;

        grid[(int)item.grid_pos.x,(int)item.grid_pos.y].GetComponent<tilebehavior>().Add_To_Tile(item.phys_rep);
        items_on_grid[(int)item.grid_pos.x,(int)item.grid_pos.y] = item;

        gridmanager.GetComponent<gridmanager>().Set_Empty_Squares();

        return true;
      }

      return false;
    }

}
