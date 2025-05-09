using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSound : MonoBehaviour
{

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();

        if(EffectsBehavior.duckigeton){

            float randomVolume = Random.Range(0.75f, 1.25f);
            audioSource.volume *= randomVolume;

            float randomPitch = Random.Range(0.75f, 1.25f);
            audioSource.pitch *= randomPitch;

            int i = Random.Range(0, CommunityChestManager.communityChestManager.quacks.Count);
            audioSource.clip =  CommunityChestManager.communityChestManager.quacks[i];
            audioSource.Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying){
            Destroy(gameObject);
        }
    }
}
