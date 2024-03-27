using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> PointPos;
    [SerializeField] private float speed = 3f;
    private int value = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(value <= PointPos.Count)
        {
            if (Vector2.Distance(transform.position,PointPos[value].position) < 0.1f)
            {
                value++;
                if(value >= PointPos.Count)
                {
                    
                    value = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, PointPos[value].position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
