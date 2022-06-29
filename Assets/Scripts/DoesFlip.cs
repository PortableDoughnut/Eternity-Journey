using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesFlip : MonoBehaviour {

    enum Axis { xLocal, yLocal, zLocal };

    [SerializeField] bool flip = false;
    [SerializeField] AudioClip flipSound;
    [SerializeField] int teleportUp = 40;

    GameObject start, finish, player;
    Vector3 startPosition, finishPosition;
    new AudioSource audio;

    private void Awake() {


        start = GameObject.FindWithTag("Start");
        finish = GameObject.FindWithTag("Finish");
        player = GameObject.FindWithTag("Player");
        startPosition = start.transform.position;
        finishPosition = finish.transform.position;
        audio = GetComponent<AudioSource>();
        audio.Stop();
    }

    public void Tele() {
        TeleportPlayerRelative(player.gameObject, Axis.yLocal, teleportUp);
    }

    public void DoFlip() {
        if(flip) {
            finish.transform.position = startPosition;
            start.transform.position = finishPosition;
            flip = false;
            PlayAudio();
        }
    }

    public bool isFlip() {
        return flip;
    }

    private void PlayAudio() {
        if(audio.isPlaying) {return;}
        audio.PlayOneShot(flipSound);
    }

    void TeleportPlayerRelative(GameObject player, Axis ax, int diffrence)
    {
        player.transform.position = newPosition(player, ax, diffrence);
    }

    Vector3 newPosition(GameObject itemObject, Axis ax, int diffrence)
    {
        Vector3 toReturn;
        switch (ax)
        {
            case Axis.xLocal:
                {
                    toReturn = new Vector3(itemObject.transform.position.x + diffrence, itemObject.transform.position.y, itemObject.transform.position.z);
                    break;
                }

            case Axis.yLocal:
                {
                    toReturn = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y + diffrence, itemObject.transform.position.z);
                    break;
                }

            case Axis.zLocal:
                {
                    toReturn = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y, itemObject.transform.position.z + diffrence);
                    break;
                }

            default:
                {
                    toReturn = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y, itemObject.transform.position.z);
                    break;
                }
        }
        return toReturn;
    }
}
