using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SoundController.Instance.PlaySound("Lvl1MusicUnderwaterCastle");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SoundController.Instance.FadeOutSound("Lvl1MusicUnderwaterCastle", 5f);
        }
    }
}
