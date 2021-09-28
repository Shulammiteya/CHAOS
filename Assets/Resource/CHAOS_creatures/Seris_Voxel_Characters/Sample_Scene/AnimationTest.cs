using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour {
	public Animator anim;
	public void OnClick(string ButtonName){
		anim.Play (ButtonName,0,0f);
	}
}
