using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesFlip : MonoBehaviour   {

    //This enum has values depicting the axis' in the 3D plane
    enum Axis { xLocal, yLocal, zLocal };

    //This variable decides wether the finish landing pad will switch places with the start launching pad after being touched for the first time.
    [SerializeField] public bool flip = false;
    //This AudioClip is what will play when the pads flip.
    [SerializeField] AudioClip flipSound;
    //This is the amount of space that the player will teleport after touching the landing pad.
    [SerializeField] int teleportUp = 40;
    //These are the game objects for the launch pad, the landing pad, and the player.
    [SerializeField] GameObject start, finish, player;
    //This is the amount of time to wait before renaming the tag of the landing pad.
    [SerializeField] float waitTime = 5;

    //This is the AudioSource used to play the AudioClip.
    new AudioSource audio;

    //This starts before the game only once
    private void Awake() {
        //This sets the AudioSource variable to the component attached to the landing pad.
        audio = GetComponent<AudioSource>();
        
        //This stops all audio.
        audio.Stop();

        //If flip is set to false the rest of the method is not ran.
        if (!flip)
            return;

        //This renames the tag of the landing pad to "Flip" so that it wont trigger the success sequence.
        FlipRename("Flip");
    }

    //This is ran when the landing pad collides with something.
    void OnCollisionEnter(Collision other)  {
        //This logs the tag of the landing pad to the console.
        Debug.Log("Tag is " + this.gameObject.tag);
        //This logs the value of the flip boolean to the console.
        Debug.Log("Flip is " + flip);

        //If flip is set to false the rest of the method is not ran.
        if (!flip)
            return;

        //This logs that the flip is starting.
        Debug.Log("Flipping...");
        /*
         * This starts the Coroutine that does the flip.
         * waitTime is the time that it will wait before renamaming the tag back to Finish.
         */
        StartCoroutine(wait(waitTime));
    }

    //This teleports the player away from the landing pad.
    public void Tele() {
        //This logs that it is going to teleport the player to the console.
        Debug.Log("Teleporting Player for flip...");
        /*
         * This uses the TeleportPlayerRelative() method to teleport the player relitive to where they are in the level already.
         * 
         * player.gameObject is passing through the player so that it can teleport it.
         * 
         * Axis.yLocal is saying that it will teleport on the Y axis.
         * 
         * teleportUp is using the varible from earlier to say how much it wants to teleport the player by.
         */
        TeleportPlayerRelative(player.gameObject, Axis.yLocal, teleportUp);
        //This logs that the player teleported to the console.
        Debug.Log("Teleported Player for flip.");

    }

    //This is the method that performs the flip itself.
    public void DoFlip()
    {
        //If flip is set to false this method does not run.
        if (!flip)
            return;

        //This gets the position of the launch pad and stores it in a Veector3 variable.
        Vector3 startPosition = new Vector3(start.transform.position.x, start.transform.position.y, 0);
        //This logs the positon of the launch pad to the console
        Debug.Log("Start: " + startPosition);
        //This gets the position of the landing pad and stores it in a Veector3 variable.
        Vector3 finishPosition = new Vector3(finish.transform.position.x, finish.transform.position.y, 0);
        //This logs the positon of the landing pad to the console
        Debug.Log("Finish: " + finishPosition);
        //This destroys the launch pad.
        Destroy(start);
        //This logs the launch pad was destroyed to the console.
        Debug.Log("Destroyed Start");
        //This sets the position of the Landing Pad to the location in the varible startPosition.
        finish.transform.position = startPosition;
        //This logs the positon of the landing pad to the console
        Debug.Log("Finish: " + finish.transform.position);

        //This gets all the remaning GameObjects with the tag "Ghost" and puts them in an array.
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        //This for loop logs the amount of remaing GameObjects with the tag "Ghost" and then  deletes them.
        for (int i = 0; i < ghosts.Length; i++) {
            //This logs the current amount of GameObjects with the tag "Ghost" to the console.
            Debug.Log("Ghosts Found: " + ghosts.Length);
            //This destroys the current selected ghost in the for loop.
            GameObject.Destroy(ghosts[i]);
        }
        //This logs that all GameObjects with the tag "Ghost" have been destroyed to the console.
        Debug.Log("Ghosts Destroyed");

        //This sets the variable flip to false so that when the player touches it next it will trigger the success sequence.
        flip = false;
        //This logs the value of the flip boolean to the console.
        Debug.Log("Flip is " + flip);
        //This runs the PlayAudio() method.
        PlayAudio();
    }

    //This renames the tag of the Landing Pad.
    public void FlipRename(string toRename) {
        //This logs the current tag of the Landing Pad.
        Debug.Log("Tag is " + this.gameObject.tag);
        //This renames the tag to the string set in the params.
        this.gameObject.tag = toRename;
        //This logs the current tag of the Landing Pad.
        Debug.Log("Tag is " + this.gameObject.tag);
    }

    //This plays the AudioClip of the flipSound.
    private void PlayAudio() {
        //If audio is playing already the method does not run.
        if(audio.isPlaying)
            return;

        //This plays the flipSound AudioClip once.
        audio.PlayOneShot(flipSound);
        //This logs that the audio has been played to the console.
        Debug.Log("Audio Played");
    }

        /*
         * This teleports the player relitive to where they are in the level already.
         * 
         * player is the GameObject that will be teleported.
         * 
         * ax is the Axis that the player will be teleported on.
         * 
         * diffrence is the varible to say how much it wants to teleport the player by.
         */
    void TeleportPlayerRelative(GameObject player, Axis ax, int diffrence)
    {
        //This teleports the player using the newPosition() method.
        player.transform.position = newPosition(player, ax, diffrence);
    }

        /*
         * This calculates a new Vector3 for the player to teleport to.
         * 
         * itemObject is the GameObject that will be teleported.
         * 
         * ax is the Axis that the player will be teleported on.
         * 
         * diffrence is the varible to say how much it wants to teleport the player by.
         */
    Vector3 newPosition(GameObject itemObject, Axis ax, int diffrence)
    {
        //This is the Vector3 variable that will be returned.
        Vector3 toReturn;
        /*
         * This is a switch statement that says what will happen depending on which axis is chosen.
         * It adds the diffrence int to the axis chosen.
         * It retuns the same position as the defalut case.
         * This is asigned to the toReturn int.
         */
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
        //This returns the toReturn variable.
        return toReturn;
    }

    /*
     * This will run the Tele() and DoFlip() methods then wait for the time specified in the waitTime param.
     * I used an IEnumerator so that it would wait for the other methods to finish and so that I could add the WaitForSeconds() function.
     */
    IEnumerator wait(float waitTim) {
        Tele();
        DoFlip();
        //Waits for the time specified in the waitTim param.
        yield return new WaitForSeconds(waitTim);
        FlipRename("Finish");
    }
}
