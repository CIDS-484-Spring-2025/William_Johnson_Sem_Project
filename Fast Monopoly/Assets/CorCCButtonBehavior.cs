using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CorCCButtonBehavior : ButtonBehavior
{
    public Material blueHighlight;
    public Material redHighlight;
    [HideInInspector] public BordSpaceBehavior spaceButtonIsUsedFor;
    

    

    // Update is called once per frame
    protected override void Update()
    {

        if(RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition)){

            image.color = Color.white;

            spaceButtonIsUsedFor.highLight.SetActive(true);
            spaceButtonIsUsedFor.highLight.GetComponent<MaterialChanger>().ChangeMaterial(blueHighlight);

            if(Input.GetButtonDown("Fire1")){
                spaceButtonIsUsedFor.highLight.GetComponent<MaterialChanger>().ChangeMaterial(redHighlight);
                spaceButtonIsUsedFor.highLight.SetActive(false);
                References.canvas.hasJustClickedButton = true;
                References.canvas.currentSpace = spaceButtonIsUsedFor;
                playButtonSound();
                eventToTrigger.Invoke();
            }

        }else{

            image.color = Color.grey;

            spaceButtonIsUsedFor.highLight.GetComponent<MaterialChanger>().ChangeMaterial(redHighlight);

            //if mike is landing on the same space, don't turn off the highlight
            if(References.mike.nextSpaceInt() != spaceButtonIsUsedFor.spaceIndex){
                spaceButtonIsUsedFor.highLight.SetActive(false);
            }

        }
        
    }

    



}
