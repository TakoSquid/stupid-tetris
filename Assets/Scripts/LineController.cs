using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public static LineController instance;
    public uint nbLineDestroyed = 0;

    [Header("Debug")]
    public float xmin = -10f;
    public float xmax = 10f;

    [Header("Custom values")]
    [Range(0, 19)]
    public int lineAmount = 5;
    public float lineHeight = 2.0f;
    public int minimumSquareInLine = 5;

    Vector3 start, stop;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void DestroyLine()
    {
        nbLineDestroyed = 0;

        for (int i = 0; i < lineAmount; ++i)
        {
            int numberInLine = 0;
            start = transform.position + new Vector3(xmin, i * lineHeight);
            stop = transform.position + new Vector3(xmax, i * lineHeight);

            foreach (GameObject fallen_compo in GameObject.FindGameObjectsWithTag("fallen block"))
            {
                if (fallen_compo.transform.position.y > transform.position.y + i * lineHeight && fallen_compo.transform.position.y < transform.position.y + (i + 1) * lineHeight)
                {
                    numberInLine++;
                }
            }

            if(numberInLine >= minimumSquareInLine)
            {
                foreach (GameObject fallen_compo in GameObject.FindGameObjectsWithTag("fallen block"))
                {
                    if (fallen_compo.transform.position.y > transform.position.y + i * lineHeight && fallen_compo.transform.position.y < transform.position.y + (i + 1) * lineHeight)
                    {
                        Destroy(fallen_compo);
                    }
                }

                nbLineDestroyed += 1;
            }
        }

    }  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < lineAmount; ++i)
        {
            start = transform.position + new Vector3(xmin, i * lineHeight);
            stop = transform.position + new Vector3(xmax, i * lineHeight);

            foreach (GameObject fallen_compo in GameObject.FindGameObjectsWithTag("fallen block"))
            {
                if(fallen_compo.transform.position.y > transform.position.y + i * lineHeight && fallen_compo.transform.position.y < transform.position.y + (i+1) * lineHeight)
                {
                    Gizmos.DrawSphere(fallen_compo.transform.position, 0.1f);
                }
            }

            Gizmos.DrawLine(start, stop);
        }
    }
}
