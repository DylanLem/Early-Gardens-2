using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionManager : MonoBehaviour
{

    private List<Button> action_buttons;
    public Button selected_button;

    // Start is called before the first frame update
    void Start()
    {
      action_buttons = new List<Button>();

      foreach(Transform child in transform)
        action_buttons.Add(child.GetComponent<Button>());


      foreach(Button b in action_buttons)
        if(b.name == "Grab")
            selected_button = b;


    }

    void Update()
    {

      Update_Selected_Button(selected_button);


    }

    // Update is called once per frame
    public void Update_Selected_Button(Button button)
    {
      foreach(Button b in action_buttons)
      {

        if(b != button)
        {
          b.GetComponent<ActionButton>().Deselect();
        }
        else
          b.GetComponent<ActionButton>().Select();
      }
    }
}
