using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BansheeController : MonoBehaviour
{
    [SerializeField] private GameObject bansheeObject;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Vector2.Distance(player.transform.position, bansheeObject.transform.position) > 20)
        {
            bansheeObject.SetActive(false);
        }
        else if (bansheeObject.active == false)
        {
            bansheeObject.SetActive(true);
        }
    }
}
