using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
   private  int counterNum = 0;
    private Text counter;
    BoxCollider col;
    public Manager manager;
    public int number;
    public int numberCell;

    private void CounterAdd()
    {
        counterNum = int.Parse(counter.text.Remove(0, 6));
        counterNum += 1;
        counter.text = "Moves: " + counterNum;
    }

    void Start()
    {
        counter = GameObject.Find("Canvas/Text").GetComponent<Text>();
        counter.text = "Moves: 0";

        col = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
       // if (!manager.isWin)
      //  {
         //   col.enabled = false;
            

            if (!Physics.Linecast(transform.position, transform.position + transform.right))
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            CounterAdd();
        }
            else if (!Physics.Linecast(transform.position, transform.position + -transform.right))
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            CounterAdd();
        }
            else if (!Physics.Linecast(transform.position, transform.position + transform.up))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            CounterAdd();
        }
            else if (!Physics.Linecast(transform.position, transform.position + -transform.up))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            CounterAdd();
            }
            col.enabled = true;
        }
    }
    //private void OnTriggerEnter(Collider other)
   // {
     //   if (other.tag == "trigger")
      //  {
      //      numberCell = other.transform.GetComponent<NumberCell>().numberCell;
       //     manager.win();
      //  }
    //}

//}
