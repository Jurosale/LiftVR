using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour {

	/*
	 * Attached to objects that are interactable, and will interact with 
	 * 
	 * 
	 */ 
	public float rad = .2f; 
	public Collider[] controllerColliders;
	Material init;
	Material highlight;

	// Use this for initialization
	void Start () {
		// 
		if (this.name =="DoorHandle") 
			init = this.GetComponent<MeshRenderer>().materials [1];
		if(this.name == "Rotator") 
			init = this.GetComponent<MeshRenderer>().materials[2];
	}
	
	// Update is called once per frame
	void Update () {
		if (highlight == null) {
			highlight = (Material)Resources.Load ("Materials/Highlight");
		}
		withinInteract (this.transform.position, rad);
	}


	void withinInteract(Vector3 center, float radius) {
		MeshRenderer cachedRenderer;
		Material[] intMaterials;
		controllerColliders = Physics.OverlapSphere (center, rad); 
	//	Debug.Log ("radius " + rad + " center " + center);
		bool inRange = false;

		foreach (Collider obj in controllerColliders) {
			if (obj.gameObject.tag == "grabPoint") {
				inRange = true;
				// set the color of the object
				switch (this.name) {
				case "DoorHandle":
					//Debug.Log ("doorHandle");
					intMaterials = new Material[this.GetComponent<MeshRenderer> ().materials.Length];
					if (intMaterials != null) {
						for (int i = 0; i < intMaterials.Length; i++) {
							intMaterials [i] = this.GetComponent<MeshRenderer> ().materials [i];
						}
						for (int i = 0; i < intMaterials.Length; i++) {
							if (intMaterials [i].name == "eLiftHandle3 (Instance)") {
								intMaterials [i] = highlight;
							}
						}
						this.GetComponent<MeshRenderer> ().materials = intMaterials;
					}
					break;
				case "Rotator":
					intMaterials = new Material[this.GetComponent<MeshRenderer> ().materials.Length];
					if (intMaterials != null) {
						for (int i = 0; i < intMaterials.Length; i++) {
							intMaterials [i] = this.GetComponent<MeshRenderer> ().materials [i];
						}
						for (int i = 0; i < intMaterials.Length; i++) {
							//Debug.Log (intMaterials [i].name);
							if (intMaterials [i].name == "newLever2 (Instance)") {
								intMaterials [i] = highlight;
							}
						}
						this.GetComponent<MeshRenderer> ().materials = intMaterials;
					}
					break;
               case "rope": 
                        intMaterials = new Material[this.GetComponent<MeshRenderer>().materials.Length];
                        if (intMaterials != null) {
                            for (int i = 0; i < intMaterials.Length; i++) {
                                intMaterials[i] = this.GetComponent<MeshRenderer>().materials[i];
                            }
                            for (int i = 0; i < intMaterials.Length; i++) {
                                //Debug.Log (intMaterials [i].name);
                                if (intMaterials[i].name == "eDoorHalf2") {
                                    intMaterials[i] = highlight;
                                }
                            }
                            this.GetComponent<MeshRenderer>().materials = intMaterials;
                        }
                        break;
                    default:
					break;
				}
			} else {

			}
		}

		// this is where we set the color back to normal
		if (!inRange) {
			intMaterials = new Material[this.GetComponent<MeshRenderer> ().materials.Length];
			if (intMaterials != null) {
				for (int i = 0; i < intMaterials.Length; i++) {
					intMaterials [i] = this.GetComponent<MeshRenderer> ().materials [i];
				}
				for (int i = 0; i < intMaterials.Length; i++) {
					//Debug.Log (intMaterials [i].name);
					if (intMaterials [i].name == "Highlight (Instance)") {
						intMaterials [i] = init;
					}
				}
				this.GetComponent<MeshRenderer> ().materials = intMaterials;
			}
		}
	}
}
