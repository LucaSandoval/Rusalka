using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    public PlayableAsset cutscene;
    public bool hasPlayed = false;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!hasPlayed && c.gameObject.tag == "Player")
        {
            StartCoroutine(StartCutscene());
        }
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(.2f);

        hasPlayed = true;
        timeline.playableAsset = cutscene;
        timeline.Play();
    }
}
