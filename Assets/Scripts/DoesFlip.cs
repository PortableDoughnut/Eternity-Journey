using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesFlip : MonoBehaviour   {

    enum Axis { xLocal, yLocal, zLocal };

    [SerializeField] public bool flip = false;
    [SerializeField] AudioClip flipSound;
    [SerializeField] int teleportUp = 40;
    [SerializeField] GameObject start, finish, player;
    [SerializeField] float waitTime = 5;

    new AudioSource audio;

    private void Awake() {
        audio = GetComponent<AudioSource>();

        audio.Stop();

        if (!flip)
            return;

        FlipRename("Flip");
    }

    void OnCollisionEnter(Collision other)  {
        Debug.Log("Tag is " + this.gameObject.tag);
        Debug.Log("Flip is " + flip);
        if (!flip)
            return;

        Debug.Log("Flipping...");
        StartCoroutine(wait(waitTime));
    }

    public void Tele() {
        Debug.Log("Teleporting Player for flip...");
        TeleportPlayerRelative(player.gameObject, Axis.yLocal, teleportUp);
        Debug.Log("Teleported Player for flip...");

    }

    public void DoFlip()
    {
        if (!flip)
            return;

        Vector3 startPosition = new Vector3(start.transform.position.x, start.transform.position.y, 0);
        Debug.Log("Start: " + startPosition);
        Vector3 finishPosition = new Vector3(finish.transform.position.x, finish.transform.position.y, 0);
        Debug.Log("Finish: " + finishPosition);
        Destroy(start);
        Debug.Log("Destroyed Start");
        finish.transform.position = startPosition;
        Debug.Log("Finish: " + finish.transform.position);

        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        for (int i = 0; i < ghosts.Length; i++) {
            Debug.Log("Ghosts Found: " + ghosts.Length);
            GameObject.Destroy(ghosts[i]);
        }
        Debug.Log("Ghosts Destroyed");

        flip = false;
        Debug.Log("Flip is " + flip);
        PlayAudio();
    }

    public void FlipRename(string toRename) {
        Debug.Log("Tag is " + this.gameObject.tag);
        this.gameObject.tag = toRename;
        Debug.Log("Tag is " + this.gameObject.tag);
    }

    private void PlayAudio() {
        if(audio.isPlaying)
            return;

        audio.PlayOneShot(flipSound);
        Debug.Log("Audio Played");
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

    IEnumerator wait(float waitTim) {
        Tele();
        DoFlip();
        yield return new WaitForSeconds(waitTim);
        FlipRename("Finish");
    }
}
