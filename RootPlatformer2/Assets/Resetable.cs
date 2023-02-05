using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetable : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Reset()
    {

        
        if (transform.childCount > 0)
            transform.GetChild(0).position = transform.position;
        else
            Instantiate(enemy, transform, false);
    }
}
