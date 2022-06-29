using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesFlip : MonoBehaviour {

    enum Axis { xLocal, yLocal, zLocal };

    [SerializeField] bool flip = false;
    [SerializeField] AudioClip flipSound;
    [SerializeField] int teleportUp = 40;
    [SerializeField] GameObject start, finish, player;

    new AudioSource audio;
    EventManager eventManager;

    private void Awake() {
        audio = GetComponent<AudioSource>();
        eventManager = GetComponent<EventManager>();
        audio.Stop();
    }

    public void FlipCheck() {
        if (!flip)
            Destroy(GetComponent<EventManager>());
    }

    public void Tele() {
        TeleportPlayerRelative(player.gameObject, Axis.yLocal, teleportUp);
    }

    public void DoFlip() {
        if(flip) {
            Vector3 startPosition = new Vector3(start.transform.position.x, start.transform.position.y, 0);
            Vector3 finishPosition = new Vector3(finish.transform.position.x, finish.transform.position.y, 0);
            Destroy(start);
            finish.transform.position = startPosition;
            flip = false;
            PlayAudio();
        }
    }

    public void FlipRename() {
        this.gameObject.tag = "Flip";
    }

    public global::System.Boolean isFlip => flip;

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
