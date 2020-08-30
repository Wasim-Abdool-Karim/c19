using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D avatar)
    {
      // print parent name
      Debug.Log(transform.parent.name);
      Destroy(gameObject);
    }
}
