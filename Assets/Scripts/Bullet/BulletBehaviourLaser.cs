using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviourLaser : BulletBehaviourBase
{

    private float range;
    private float width;
    private bool activated;
    private LineRenderer line;

    #region Builder
    public BulletBehaviourLaser SetLaserRange(float range)
    {
        this.range = range;
        return this;
    }
    public BulletBehaviourLaser SetLaserWidth(float width)
    {
        this.width = width;
        return this;
    }
    #endregion

    public override void Disable()
    {
        line.enabled = false;
        base.Disable();
        //Acordarse de desubscribir de eventos aca, si te subscribis
    }

    public override void Enable()
    {
        base.Enable();

        //apariencia
        //Podes cambiar esto a tu criterio
        transform.localScale = new Vector3(1f, 1f, 1f);
        var render = transform.GetComponent<SpriteRenderer>();
        if (render != null)
            render.enabled = false;
        //visuals
        line = transform.GetComponent<LineRenderer>();
        if (line != null)
        {
            line.enabled = true;
        }
        //configura collider y rigidbody
        var col = transform.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        var rb = transform.GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = true;
        activated = false;
    }

    public override void Update()
    {
        if (!activated)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + (0.5f * width * transform.up),
            new Vector2(width, width), Vector2.SignedAngle(Vector2.up, transform.up), transform.up, range);
            Collider2D[] cols = new Collider2D[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                cols[i] = hits[i].collider;
            }
            EventManager.CallEvent("LaserStrike", new ExplosionDataPackage { source = transform, colliders = cols });
            //insert visuals here
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.up * range);
            line.startWidth = width * 0.9f;
            line.endWidth = width;
            activated = true;
        }
        else NotifyDisable();

        //Llamar NotifyDisable() para desactivar el objeto
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BulletBehaviourLaser : BulletBehaviourBase {

//	private float range;

//	#region Builder
//	public BulletBehaviourLaser SetLaserRange(float range) {
//		this.range = range;
//		return this;
//	}
//	#endregion

//	public override void Disable() {
//		base.Disable();
//		//Acordarse de desubscribir de eventos aca, si te subscribis
//	}

//	public override void Enable() {
//		base.Enable();

//		//apariencia
//		//Podes cambiar esto a tu criterio
//		transform.localScale = new Vector3(1f, 1f, 1f);
//		var render = transform.GetComponent<SpriteRenderer>();
//		if (render != null)
//			render.enabled = false;

//		//configura collider y rigidbody
//		var col = transform.GetComponent<Collider2D>();
//		if (col != null) col.enabled = false;
//		var rb = transform.GetComponent<Rigidbody2D>();
//		if (rb != null) rb.isKinematic = true;

//	}

//	public override void Update() {
//		//Marcos: trabajar en esto
//		//Tiene que durar un frame
//		throw new System.NotImplementedException();

//		//Llamar NotifyDisable() para desactivar el objeto
//	}
//}
