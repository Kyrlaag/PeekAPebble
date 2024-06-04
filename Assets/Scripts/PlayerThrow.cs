using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject rock;
    public GameObject skillshot;
    public float throwSpeed = 0.01f;
    public float throwDistance = 100f;
    public GameObject player;
    const int throwLimit = 100;
    int currentRockCount;
    bool willUseSkillshot;

    void Start()
    {
        currentRockCount = throwLimit;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Skill"))
        {
            toggleSkillshot();
        }

        if(Input.GetMouseButtonDown(0) && currentRockCount > 0 && willUseSkillshot)
        {
            rock.SetActive(true);
            Vector3 throwPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            throwPos.z = -9f;

            
            float distance = Vector2.Distance(player.transform.position, throwPos);
            if (distance > throwDistance)
            {
                throwPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized * throwDistance;
            }

            StartCoroutine(throwRock(throwPos));
        }

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (this.transform.position - pos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector3 rotation = new Vector3(0f, 0f, angle);
        skillshot.transform.rotation = Quaternion.Euler(rotation);

    }
    void toggleSkillshot()
    {
        skillshot.SetActive(!skillshot.activeSelf);
        willUseSkillshot = !willUseSkillshot;
    }
    IEnumerator throwRock(Vector3 throwPos) 
    {
        toggleSkillshot();
        currentRockCount--;
        GameObject newObject = Instantiate(rock, transform.parent);
        
        float elapsedTime = 0;
        float waitTime = 3f;
        newObject.transform.position = player.transform.position;
        while (elapsedTime < waitTime)
        {
            newObject.transform.position = Vector3.Lerp(newObject.transform.position, throwPos, throwSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
 
        //Emrecan bu kodtan sonra tas yere dusuyor burada ne yapmak istedigini yaparsin.
        yield return new WaitForSeconds(5);
        Destroy(newObject);
        yield return null;
    }
}
