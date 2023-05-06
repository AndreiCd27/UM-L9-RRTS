using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private RaycastHit rayHit;
    private MySelectable mySelectable;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        mySelectable = GetComponent<MySelectable>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() {
        foreach (var item in MySelectable.allMySelectables)
        {
            if (mySelectable.selected && Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out rayHit,100))
                {
                    agent.destination = rayHit.point;
                }
            }
        }
    }
}
