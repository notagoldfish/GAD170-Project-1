using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script: PlayerController
    Author: Gareth Lockett
    Version: 1.0
    Description: Simple player controller script. Use arrow keys to move and spacebar to jump.
*/

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;        // Distance moved (units per second) when user holds down up or down arrow.
    public float turnSpeed = 180f;       // Rotating speed (degrees per second) when user holds down left or right arrow.
    public float jumpHeight = 5f;       // Upward velocity when user presses spacebar.

    public float xp = 0;
    public float xpForNextLevel = 5;
    public int level = 1;

    public float currentMoveSpeed;
    public float currentTurnSpeed;
    public float currentJumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        SetXpForNextLevel();
        SetCurrentMoveSpeed();
        SetCurrentTurnSpeed();
        SetCurrentJumpHeight();
    }


    void SetXpForNextLevel()
    {
        xpForNextLevel = (5f + (level * level * 5f));
        Debug.Log("xpForNextLevel" + xpForNextLevel);
    }

    // For each level, the player adds 10% to the move speed 
    void SetCurrentMoveSpeed()
    {
        currentMoveSpeed = this.moveSpeed + (this.moveSpeed * 0.1f * level);
        Debug.Log("currentMoveSpeed = " + currentMoveSpeed);
    }

    // For each level, the player adds 10% to the turn speed 
    void SetCurrentTurnSpeed()
    {
        currentTurnSpeed = this.turnSpeed + (this.turnSpeed * (level * 0.1f));
        Debug.Log("currentTurnSpeed = " + currentTurnSpeed);
    }

    void SetCurrentJumpHeight()
    {
        currentJumpHeight = this.jumpHeight + (this.jumpHeight * (level * 0.75f));
        Debug.Log("currentJumpHeight = " + currentJumpHeight);
    }


    void LevelUp()
    {
        xp = 0f;
        level++;
        Debug.Log("level" + level);
        SetXpForNextLevel();
        SetCurrentMoveSpeed();
        SetCurrentTurnSpeed();
        SetCurrentJumpHeight();

    }




    //a function to make the player gain the ammount of Xp the you tell it. 
    void GainXP(int xpToGain)
    {
        xp += xpToGain;
        Debug.Log("Gained " + xpToGain + " XP, Current Xp = " + xp + ", XP needed to reach next Level = " + xpForNextLevel);
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Coin")
        {
            GainXP(5);
        }
        if (target.tag == "Goal")
        {
            GainXP(20);
        }
    }
    


    // Update is called once per frame
    void Update()
    {

        if (xp >= xpForNextLevel)
        {
            LevelUp();
        }
        // Check up and down keys to move forwards or backwards.
        if ( Input.GetKey( KeyCode.DownArrow ) == true ) { this.transform.position += this.transform.forward * Time.deltaTime * this.currentMoveSpeed; }
        if( Input.GetKey( KeyCode.UpArrow ) == true ) { this.transform.position -= this.transform.forward * Time.deltaTime * this.currentMoveSpeed; }

        // Check left and right keys to rotate left and right.
        if( Input.GetKey( KeyCode.LeftArrow ) == true ) { this.transform.Rotate( this.transform.up, Time.deltaTime * -this.currentTurnSpeed ); }
        if( Input.GetKey( KeyCode.RightArrow ) == true ) { this.transform.Rotate( this.transform.up, Time.deltaTime * this.currentTurnSpeed ); }

        // Check spacebar to trigger jumping. Checks if vertical velocity (eg velocity.y) is near to zero.
        if( Input.GetKey( KeyCode.Space ) == true && Mathf.Abs( this.GetComponent<Rigidbody>().velocity.y ) < 0.01f )
        {
            this.GetComponent<Rigidbody>().velocity += Vector3.up * this.currentJumpHeight;
        }

        //Win
        if (level > 3)
        {
            Debug.Log("Win");
        }



    }
}

/* Psuedocode
 * Begin
 * Input moveSpeed, turnSpeed, jumpHeight, xp, xpForNextLevel, level, currentMoveSpeed, currentTurnSpeed, currentJumpHeight
 * Using SetXpForNextLevel set xpForNextLevel
 * Output message of xpForNextLevel
 * Using SetCurrentMoveSpeed set currentMoveSpeed
 * Output message of currentMoveSpeed
 * Using SetCurrentTurnSpeed set currentTurnSpeed
 * Output message of currentTurnSpeed
 * Using SetCurrentJumpHeight set currentJumpHeight
 * Output message of currentJumpHeight
 * Set xp to 0
 * Increase level by 1
 * Output message of level
 * Activate SetXpForNextLevel, SetCurrentMoveSpeed, SetCurrentTurnSpeed, SetCurrentJumpHeight
 * Using GainXP, set xp to increase by xpToGain
 * Output message of xpToGain, xp and xpForNextLevel
 * On collide activate GainXP for amount specific to collided
 * If xp is greater than or equal to xpForNextLevel activate LevelUp
 * If Down Arrow key is pressed move object forward by the currentMoveSpeed
 * If Up Arrow key is pressed move object backward by the currentMoveSpeed
 * If Left Arrow key is pressed rotate object by the currentTurnSpeed
 * If Right Arrow key is pressed rotate object by the currentTurnSpeed
 * If Space key is pressed move object up on the y axis by the currentJumpHeight
 * If level 3 or higher is reached 
 * Output message "Win"
*/
