using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        int y = 1;
        int z = 3;

        int state = 0;

        if(z == 1 )
        {
            state = 1;
        }
        else if( y < x || y == 1)
        {
            state = 2;
        }
        else
        {
            state = 3;
        }

    }

    
    // Update is called once per frame
    void Update()
    {

    }
}