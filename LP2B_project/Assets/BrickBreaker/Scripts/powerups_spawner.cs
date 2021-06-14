using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerups_spawner : MonoBehaviour
{
    /* References */
    private GameObject ref_prefab_powerup;
    private brickCreation_script bricks_script;

    /* Variables */
     private bool sticky = false;

    // Start is called before the first frame update
    void Start()
    {
        bricks_script = FindObjectOfType<brickCreation_script>();
    }

    /**********************************************************************************/

    // Update is called once per frame
    void Update()
    {
        
    }

    /**********************************************************************************/

    public void summon(float x, float y)
    {
        getRandomPower();
        GameObject newDogeCoin = Instantiate(ref_prefab_powerup);
        newDogeCoin.name = ref_prefab_powerup.name;
        newDogeCoin.transform.position = new Vector3(x,y,-2);
        if(newDogeCoin.gameObject.name.Contains("doge_coin"))  bricks_script.playDoge();
    }

    /**********************************************************************************/

    public void getRandomPower()
    {
        int index = Random.Range(1,6);
        switch(index)
        {
            case 1:
                ref_prefab_powerup = Resources.Load<GameObject>("Prefabs/doge_coin");
            break;
            case 2:
                ref_prefab_powerup = Resources.Load<GameObject>("Prefabs/Catch");
            break;
            /*case 3:
                ref_prefab_powerup = Resources.Load<GameObject>("Prefabs/Disruption");
            break;*/
            case 3:
                ref_prefab_powerup = Resources.Load<GameObject>("Prefabs/Enlarge");
            break;
            case 4:
                ref_prefab_powerup = Resources.Load<GameObject>("Prefabs/Laser");
            break;
            case 5:
                ref_prefab_powerup = Resources.Load<GameObject>("Prefabs/Slow");
            break;
            
        }

    }

    /**********************************************************************************/
    
    public bool isBallSticky(){return this.sticky;}

    public void setSticky(bool s){this.sticky = s;}

}
