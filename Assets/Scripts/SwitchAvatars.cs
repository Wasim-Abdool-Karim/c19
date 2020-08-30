using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAvatars : MonoBehaviour
{
    // The two avatars
    public GameObject avatar, avatarMasked;

    // variable which holds active avatar
    int whichAvatarIsOn = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Choose plain avatar
        avatar.gameObject.SetActive(true);
        avatarMasked.gameObject.SetActive(false);
    }

    public void SwitchAvatar()
    {
        switch(whichAvatarIsOn)
        {
          case 1:
            whichAvatarIsOn = 2;

            // disable plain and enable avatar
            avatar.gameObject.SetActive(false);
            avatarMasked.transform.position = new Vector3(avatar.transform.position.x, avatar.transform.position.y, avatar.transform.position.z);
            avatarMasked.SetActive(true);
            break;

          case 2:
            whichAvatarIsOn = 1;

            // disable masked and enable plain
            avatarMasked.gameObject.SetActive(false);
            avatar.gameObject.SetActive(true);
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
