using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform pla;
    [SerializeField] private float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, pla.position, Time.deltaTime*speed);   
    }
}
