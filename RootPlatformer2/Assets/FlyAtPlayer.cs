using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAtPlayer : MonoBehaviour
{
    private static Transform player;
    [SerializeField] private float seekRange;
    [SerializeField] private float speed;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindObjectOfType<GroundedCharacterController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target;
        if(Vector3.Distance(player.position, transform.position) > seekRange)
        {
            target = startPosition;
            
        }
        else
        {
            target = player.position;
        }
        transform.position = Vector3.MoveTowards( transform.position, target, speed * Time.deltaTime);
        
    }
}
