using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodParticles : MonoBehaviour {

	//Happy
    public void happyAnimation(int intensity) {
        transform.Find("HappyParticle").GetComponent<ParticleSystem>().Emit(intensity * 5);
    }

    public void neutralAnimation() {
        transform.Find("ConfusedParticle").GetComponent<ParticleSystem>().Play();
    }

    public void angryAnimation() {
        transform.Find("AngryParticle").GetComponent<ParticleSystem>().Play();
    }
}
