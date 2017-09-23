using UnityEngine;
using System.Collections;

public class GetColor_Particle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystem ps = this.gameObject.GetComponent<ParticleSystem>();
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
        ps.startColor = transform.root.gameObject.GetComponent<BeamParam>().BeamColor;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
        ps.startSize *= this.transform.root.gameObject.GetComponent<BeamParam>().Scale;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
