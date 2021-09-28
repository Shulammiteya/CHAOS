using UnityEngine;
using System.Collections;

public class Action_Manage : MonoBehaviour {
	public GameObject Target;
	Animator myAnimator;
	// Use this for initialization
	void Start () {
		myAnimator = Target.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ClearAllBool(){
		myAnimator.SetBool ("inspire", false);
		myAnimator.SetBool ("idle",  false);
		myAnimator.SetBool ("dizzy", false);
		myAnimator.SetBool ("walk", false);
		myAnimator.SetBool ("run", false);
		myAnimator.SetBool ("jump", false);
		myAnimator.SetBool ("die", false);
		myAnimator.SetBool ("jump_left", false);
		myAnimator.SetBool ("jump_right", false);
		myAnimator.SetBool ("attack_01", false);
		myAnimator.SetBool ("attack_03", false);
		myAnimator.SetBool ("attack_02", false);
		myAnimator.SetBool ("attack_04", false);
		myAnimator.SetBool ("attack_05", false);
		myAnimator.SetBool ("damage", false);
		myAnimator.SetBool ("down", false);
		myAnimator.SetBool ("up", false);
	}
	public void Pressed_damage(){
		ClearAllBool();
		myAnimator.SetBool ("damage", true);
	}
	public void Pressed_idle(){
		ClearAllBool();
		myAnimator.SetBool ("idle", true);
	}
	public void Pressed_inspire(){
		ClearAllBool();
		myAnimator.SetBool ("inspire", true);
	}
	public void Pressed_dizzy(){
		ClearAllBool();
		myAnimator.SetBool ("dizzy", true);
	}
	public void Pressed_run(){
		ClearAllBool();
		myAnimator.SetBool ("run", true);
	}
	public void Pressed_walk(){
		ClearAllBool();
		myAnimator.SetBool ("walk", true);
	}
	public void Pressed_die(){
		ClearAllBool();
		myAnimator.SetBool ("die", true);
	}
	public void Pressed_jump(){
		ClearAllBool();
		myAnimator.SetBool ("jump", true);
	}
	public void Pressed_jump_left(){
		ClearAllBool();
		myAnimator.SetBool ("jump_left", true);
	}
	public void Pressed_jump_right(){
		ClearAllBool();
		myAnimator.SetBool ("jump_right", true);
	}
	public void Pressed_attack_01(){
		ClearAllBool();
		myAnimator.SetBool ("attack_01", true);
	}
	public void Pressed_attack_02(){
		ClearAllBool();
		myAnimator.SetBool ("attack_02", true);
	}
	public void Pressed_attack_03(){
		ClearAllBool();
		myAnimator.SetBool ("attack_03", true);
	}
	public void Pressed_attack_04(){
		ClearAllBool();
		myAnimator.SetBool ("attack_04", true);
	}
	public void Pressed_attack_05(){
		ClearAllBool();
		myAnimator.SetBool ("attack_05", true);
	}
	public void Pressed_up(){
		ClearAllBool();
		myAnimator.SetBool ("up", true);
	}
	public void Pressed_down(){
		ClearAllBool();
		myAnimator.SetBool ("down", true);
	}
}
