using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachShip : MonoBehaviour
{
    public string playerTag = "Player";       
    public float approachSpeed = 10.0f;        
    public float detectionRange = 10.0f;      

    private Transform player;                

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Hráè s tagom '" + playerTag + "' nebol nájdený.");
        }
    }

    void Update()
    {
        if (player != null && IsPlayerInRange())
        {
            float step = approachSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    bool IsPlayerInRange()
    {
        if (player == null)
            return false;

        float distance = Vector3.Distance(transform.position, player.position);

        return distance <= detectionRange;
    }
}
