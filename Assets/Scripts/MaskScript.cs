using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // When avatar makes contact with Mask
    void OnTriggerEnter2D(Collider2D avatar)
    {
      Destroy(gameObject);
    }
}
