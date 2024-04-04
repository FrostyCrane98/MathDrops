using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waterdrop : MonoBehaviour
{

    public TextMesh AText, BText, SignText;
    public float FallingSpeed = 3f;
    public int Result;


    private int a, b ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - FallingSpeed * Time.deltaTime);


        if(transform.position.y < -9f)
        {
            DestroyWaterdrop();
        }
    }

    public void DefineOperation(int min, int max, bool useAllOperations)
    {
        a = Random.Range(min, max + 1);
        b = Random.Range(min, max + 1);

        AText.text = a.ToString();
        BText.text = b.ToString();

        if (!useAllOperations)
        {
            int rolledSign = Random.Range(0,2); 
            if ( rolledSign == 0)
            {
                Result = a + b;
                SignText.text = "+";                
            }
            else
            {
                Result = a - b;
                SignText.text = "-";
            }
        }
        else
        {
            int rolledSign = Random.Range(0, 4);
            if (rolledSign == 0)
            {
                Result = a + b;
                SignText.text = "+";
            }
            if (rolledSign == 1)         
            {
                Result = a - b;
                SignText.text = "-";
            }
            if (rolledSign == 2)
            {
                Result = a * b;
                SignText.text = "x";
            }
            if (rolledSign == 3)
            {
                if (b == 0 || a % b != 0)
                {
                    for (int i = 2; i <= a; i++)
                    {
                        b = i;
                        if (a % b == 0)
                        {
                            break;
                        }
                    }
                    if (a % b != 0)
                    {
                        b = a;
                    }
                }
                Result = a / b;
                SignText.text = "÷";
                BText.text = b.ToString();
            }
        }
    }

    public virtual void PopWaterdrop()
    {
        EventManager.Instance.PopDrop();
        Destroy(gameObject);
    }

    private void DestroyWaterdrop()
    {
        EventManager.Instance.DropDespawn(this);
        Destroy(gameObject);
    }
}
