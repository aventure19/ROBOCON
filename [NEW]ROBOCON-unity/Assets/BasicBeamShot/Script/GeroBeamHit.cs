using UnityEngine;
using System.Collections;

public class GeroBeamHit : MonoBehaviour {
	private GameObject ParticleA;
	private GameObject ParticleB;
	private GameObject HitFlash;
	
	private float PatA_rate;
	private float PatB_rate;

	private ParticleSystem PatA;
	private ParticleSystem PatB;
    public Color col;
	public void SetViewPat(bool b)
	{
		if(b){
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
			PatA.emissionRate = PatA_rate;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
            PatB.emissionRate = PatB_rate;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
            HitFlash.GetComponent<Renderer>().enabled = true;
		}else{
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
            PatA.emissionRate = 0;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
            PatB.emissionRate = 0;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
			HitFlash.GetComponent<Renderer>().enabled = false;
		}
	}

	// Use this for initialization
	void Start () {
        col = new Color(1, 1, 1);
		ParticleA = transform.FindChild("GeroParticleA").gameObject;
		ParticleB = transform.FindChild("GeroParticleB").gameObject;
		HitFlash = transform.FindChild("BeamFlash").gameObject;
		PatA = ParticleA.gameObject.GetComponent<ParticleSystem>();
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
        PatA_rate = PatA.emissionRate;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
        PatA.emissionRate = 0;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
		PatB = ParticleB.gameObject.GetComponent<ParticleSystem>();
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
		PatB_rate = PatB.emissionRate;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
		PatB.emissionRate = 0;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です

		HitFlash.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
        PatA.startColor = col;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
        PatB.startColor = col;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
        HitFlash.GetComponent<Renderer>().material.SetColor("_Color", col*1.5f);
    }
}
