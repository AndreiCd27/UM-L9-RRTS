using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public static float delay = 0.5f;
    private float health;
    private float damage;
    public float Health { get { return health; } set { health = value; } }
    public float Damage { get { return damage; } set { damage = value; } }
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
        Health = 500;
        Damage = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        // foreach (var item in MySelectable.allMySelectables)
        // {
        //     if (mySelectable.selected && Input.GetMouseButtonDown(0))
        //     {
        //         anim.SetFloat("Running", agent.remainingDistance);
        //         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, 100))
        //         {
        //             agent.destination = rayHit.point;
        //         }
        //     }
        //     else{
        //         anim.SetFloat("Running", 0f);
        //     }
        // }
        foreach (var item in MySelectable.allMySelectables)
        {
            // pentru a putea face animatia trebuie ca mai intai sa fie selectat
            if (mySelectable.selected)
            {
                anim.SetFloat("Running", agent.remainingDistance);
                // dupa ce apasam click distanta se modifica si animatia incepe
                if (Input.GetKeyDown("space"))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, 100))
                    {
                        agent.destination = rayHit.point;
                    }
                }
            }
            else
                anim.SetFloat("Running", 0f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            agent.destination = other.transform.position;
            // agent.destination = other.gameObject.transform.position;
            anim.SetBool("Attack", true);
            StartCoroutine(Attack(other));
        }
    }
    IEnumerator Attack(Collider col)
    {
        while (col != null && col.GetComponent<EnemyController>().Health > 0)
        {
            transform.LookAt(col.transform);
            col.GetComponent<EnemyController>().Health -= Damage;
            yield return new WaitForSeconds(delay * 3f);
        }
        anim.SetBool("Attack", false);
        if (col != null)
        {
            Destroy(col.gameObject, delay);
        }


    }
}