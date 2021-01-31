using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyuncuHareket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f);

        if(hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if(hitInfo.collider.CompareTag("etkiArayüz"))
            {
                Debug.Log("Selam");
            }
        }
    }
}
