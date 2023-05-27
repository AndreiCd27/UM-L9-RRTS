using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int gold;
    public int Gold { get { return gold; } set { gold = value; } }
    public Text goldText;

    private AudioSource myAudio;
    public bool audioIsPlaying = false;
    public bool audioStateChanged = false;

    private void Awake() {
        Gold = 500;
        myAudio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetCoins());
    }

    // Update is called once per frame
    void Update()
    {
        if (audioIsPlaying && audioStateChanged)
        {
            myAudio.Play();
            audioStateChanged = false;
        }
        if (!audioIsPlaying && audioStateChanged)
        {
            myAudio.Stop();
            audioStateChanged = false;
        }
        goldText.text = $"🟡: {Gold}";
    }

    IEnumerator GetCoins() {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1,6));
            Gold += 100;
            audioIsPlaying = true;
            audioStateChanged = true;
        }
    }
}
