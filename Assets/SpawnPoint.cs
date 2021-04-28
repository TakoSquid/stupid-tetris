using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public uint nbPieceInTopSection()
    {
        uint nb = 0;

        foreach (GameObject fallen_compo in GameObject.FindGameObjectsWithTag("fallen block"))
        {
            if (col.bounds.Contains(fallen_compo.transform.position))
                nb += 1;
        }
        return nb;
    }
}
