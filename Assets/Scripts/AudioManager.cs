using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource adSource;
    public AudioClip[] adClips;

    IEnumerator playAudioSequentially()
    {
        yield return null;

        //1.Loop through each AudioClip
        for (int i = 0; i < adClips.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            adSource.clip = adClips[i];

            if (i == adClips.Length - 1)
            {
                adSource.loop = true;
            }

            //3.Play Audio
            adSource.Play();



            //4.Wait for it to finish playing
            while (adSource.isPlaying)
            {
                yield return null;
            }

            //5. Go back to #2 and play the next audio in the adClips array
        }
    }
    void Start()
    {
        StartCoroutine(playAudioSequentially());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
