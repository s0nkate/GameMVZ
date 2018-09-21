using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColldowSkill : MonoBehaviour {

    public Image imageColldown;
    public float cooldown = 5;
    bool isCooldown;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isCooldown = true;
        }
        if (isCooldown)
        {
            imageColldown.fillAmount += 1 / cooldown * Time.deltaTime;
        }
        if(imageColldown.fillAmount >= 1)
        {
            imageColldown.fillAmount = 0;
            isCooldown = false;
        }
	}
}
