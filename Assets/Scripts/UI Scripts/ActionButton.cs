using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public bool is_active;
    public GameObject UIActionManager;

    void Start()
    {
      UIActionManager = GameObject.Find("Action Buttons");

      gameObject.GetComponent<Button>().onClick.AddListener(Send_Select_To_Manager);
    }

    public void Send_Select_To_Manager()
    {
      Debug.Log("eheheh");
      UIActionManager.GetComponent<UIActionManager>().selected_button = gameObject.GetComponent<Button>();
    }

    public void Select()
    {
      GetComponent<Button>().Select();
      is_active = true;
    }

    public void Deselect()
    {
      is_active = false;
    }
}
