using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTetromino : MonoBehaviour
{
    public static CurrentTetromino instance;
    public bool mustBeDestroyed = false;
    public PhysicsMaterial2D childPhysicswhenFallen;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float XInput = Input.GetAxisRaw("Horizontal");
        float YInput = Input.GetAxisRaw("Vertical");

        if (XInput != 0)
        {
            foreach(Transform child in transform)
            {
                Rigidbody2D childrb2d = child.GetComponent<Rigidbody2D>();
                childrb2d.AddForce(new Vector2(XInput * 500f * Time.deltaTime, 0));
            }
        }

        if (YInput != 0)
        {
            foreach (Transform child in transform)
            {
                Rigidbody2D childrb2d = child.GetComponent<Rigidbody2D>();
                childrb2d.AddTorque(-1f * 12.5f * YInput);
            }
        }

        if (mustBeDestroyed)
            SelfDestruct();
    }

    public void SelfDestruct()
    {
            
        foreach(Transform child in transform)
        {
            child.tag = "fallen block";
            Destroy(child.gameObject.GetComponent<ComponentScript>());

            Rigidbody2D childRb2d = child.gameObject.GetComponent<Rigidbody2D>();
            //childRb2d.sharedMaterial = childPhysicswhenFallen;
            
        }

        GameController.instance.mustCreateNewTetromino = true;
        instance = null;
        Destroy(this);
    }

}
