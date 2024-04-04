using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public TMP_InputField InputField;

    private string input;
    public int InitialLifes = 3;
    private int lifes;



    private void OnEnable()
    {
        lifes = InitialLifes;
        InputField.contentType = TMP_InputField.ContentType.IntegerNumber;

        EventManager.Instance.OnDropDespawn += LoseLife;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDropDespawn -= LoseLife;
    }

    public void SaveInput(string s)
    {
        int i;
        if (int.TryParse(s, out i))
        {
            EventManager.Instance.PlayerInput(i);
            InputField.text = "";
            EventSystem.current.SetSelectedGameObject(InputField.gameObject, null);
            InputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
        else
        {
            Debug.Log("Parse Failed");
        }
    }

    private void LoseLife(Waterdrop w)
    {
        lifes--;
        if (lifes == 0) 
        {
            EventManager.Instance.GameOver();
        }
    }
}
