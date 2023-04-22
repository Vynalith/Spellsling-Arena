using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWallDeletion : MonoBehaviour
{

    public int columns;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 7f);
        columns = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(columns <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ColumnDestroyed()
    {
        columns -= 1;
    }
}
