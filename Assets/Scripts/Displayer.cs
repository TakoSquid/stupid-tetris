using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displayer : MonoBehaviour
{
    SpriteRenderer sp;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.forward, 1);
    }

    public void updateSprite(GameObject gb)
    {
        if (gb)
            sp.sprite = gb.GetComponent<SpriteRenderer>().sprite;
    }
}
