using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
   
    protected RectTransform rectangle;
    protected Image image;
    public UnityEvent eventToTrigger;
    public GameObject buttonSound;
    

    protected virtual void Start()
    {
        rectangle = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if(RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition)){

            image.color = Color.black;

            if(Input.GetButtonDown("Fire1")){
                playButtonSound();
                eventToTrigger.Invoke();
            }

        }else{

            image.color = Color.grey;

        }
        
    }

    public void playButtonSound(){

        if(buttonSound != null){
            Instantiate(buttonSound);
        }

    }

    public void ok(){

        if(!References.canvas.currentUsersTurn.jailed){
                References.canvas.clickDicePrompt.SetActive(true);
            }else{//if jailed
                References.canvas.jailedMenu.SetActive(true);
            }

        References.canvas.newPlayersTurnMenu.SetActive(false);
    }

    public void take(){
        foreach (PerkItemSpawnerBehavior spawn in References.canvas.twoMoreEffectsMenu.spawns)
        {
            EffectsBehavior.endTurnAfterAction = false;
            spawn.spawnedPerkOrItem.eventToTrigger.Invoke();
        }
        
        EffectsBehavior.endTurnAfterAction = true;

        References.canvas.twoMoreEffectsMenu.gameObject.SetActive(false);
        References.canvas.nextTurn();
    }

}
