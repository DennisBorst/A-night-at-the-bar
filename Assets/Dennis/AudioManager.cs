using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //The higher the number the angrier your mate is, the lower the number the happier your mate is
    [Range(-100, 100)]
    [SerializeField] private int currentAngryMeter;

    [System.Serializable]
    public class Choises
    {
        public AudioClip conversationStarter;

        public AudioClip[] answers;
        public int[] increaseAngryMeter;

        public AudioClip[] reaction1;
        public AudioClip[] reaction2;
        public AudioClip[] reaction3;
        public AudioClip[] reaction4;
    }

    [SerializeField] private Choises[] choises;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        
    }

    private void NextSentences()
    {

    }
}
