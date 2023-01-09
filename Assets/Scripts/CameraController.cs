using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float aheadUpDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    


    void Start()
    {
        
    }

    
    void Update()
    { 
        //Follow player
        transform.position = new Vector3(player.position.x +lookAhead, transform.position.y, transform.position.z);
        lookAhead= Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        
    }
}
