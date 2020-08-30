using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskScript : MonoBehaviour
{
    // for accessing script that switches between avatars
    SwitchAvatars a;

    // Start is called before the first frame update
    void Start()
    {
      a = GameObject.Find("Idle (8)").GetComponent<SwitchAvatars>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Switch to mask-wearing avatar
    void OnTriggerEnter2D(Collider2D avatar)
    {
      Destroy(gameObject);
      a.SwitchAvatar();
    }
}
