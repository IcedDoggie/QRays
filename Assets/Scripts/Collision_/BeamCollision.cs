using UnityEngine;
using System.Collections;

public class BeamCollision : MonoBehaviour {

	public float thrust = 10f;
    public bool Reflect = false;
	private BeamLine BL;

	public GameObject HitEffect = null;

	private bool bHit = false;

	private BeamParam BP;

	// Use this for initialization
	void Start () {
		if(this.gameObject.transform.childCount > 0)
		BL = (BeamLine)this.gameObject.transform.FindChild("BeamLine").GetComponent<BeamLine>();
		
		BP = this.transform.root.gameObject.GetComponent<BeamParam>();
	}
	
	// Update is called once per frame
	void Update () {
		//RayCollision
		RaycastHit hit;
        int layerMask = ~(1 << LayerMask.NameToLayer("NoBeamHit") | 1 << 2);


//		if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
//		{
//			GameObject hitobj = hit.collider.gameObject;
//			if (hit.collider.tag == "Enemy")
//			{
//				//BL.StopLength(hit.distance);
//				//bHit = true;
//				hit.collider.GetComponent<EnemyMover>().DieSoon();
//				hitobj.GetComponent<Rigidbody> ().AddForce (transform.forward * thrust);
////				Destroy(hitobj);
//				Destroy(gameObject, 0.5f);
//			}
//			//print("find" + hit.collider.gameObject.name);
//		}

        if (HitEffect != null && !bHit && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            GameObject hitobj = hit.collider.gameObject;
	
			if (hit.collider.tag == "Enemy")
			{
				//BL.StopLength(hit.distance);
				//bHit = true;
				hit.collider.GetComponent<EnemyMover>().DieSoon();

				//				Destroy(hitobj);
//				Destroy(gameObject, 0.5f);
			}

			if(hit.collider.tag == "item_C")
			{
				hit.collider.GetComponent<Item> ().getHit ();
			}

			if(hit.distance < BL.GetNowLength())
		    {
				BL.StopLength(hit.distance);
				bHit = true;

                Quaternion Angle;
                //Reflect to Normal
                if (Reflect)
                {
                    Angle = Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal));
                }
                else
                {
                    Angle = Quaternion.AngleAxis(180.0f, transform.up) * this.transform.rotation;
                }
                GameObject obj = (GameObject)Instantiate(HitEffect,this.transform.position+this.transform.forward*hit.distance,Angle);
				obj.GetComponent<BeamParam>().SetBeamParam(BP);
				obj.transform.localScale = this.transform.localScale;
			}

            if(hit.collider.tag == "Boss")
            {
                hit.collider.GetComponent<BossMover>().DieSoon();
            }

		
			//print("find" + hit.collider.gameObject.name);
		}
		/*
		if(bHit && BL != null)
		{
			BL.gameObject.renderer.material.SetFloat("_BeamLength",HitTimeLength / BL.GetNextLength() + 0.05f);
		}
		*/
	}
}
