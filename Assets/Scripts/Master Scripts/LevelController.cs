using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    public Itemdatabase itemDatabase;

    private GameObject Earlmanager;
    private GameObject Player;
    private GameObject Gridmanager;

    void Awake()
    {
      itemDatabase = new Itemdatabase();

        if (Instance == null)
        {

            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Alpha1) && SceneManager.GetActiveScene().name != "Gardens")
      {
        SaveSystem.Save_Level_Grid(Gridmanager);
        SaveSystem.Save_Player(Player);
        SceneManager.LoadScene("Gardens");
        SaveSystem.Load_Level_Grid(Gridmanager);
      }

      if(Input.GetKeyDown(KeyCode.Alpha2) && SceneManager.GetActiveScene().name != "Marketplace")
      {
        SaveSystem.Save_Level_Grid(Gridmanager);
        SaveSystem.Save_Player(Player);
        SceneManager.LoadScene("Marketplace");
        SaveSystem.Load_Level_Grid(Gridmanager);

      }
    }

    public void Set_Player(GameObject player)
    {
      Player = player;
    }

    public void Set_Earlmanager(GameObject earlmanager)
    {
      Earlmanager = earlmanager;
    }

    public void Set_Grid(GameObject grid)
    {
      Gridmanager = grid;
    }
}
