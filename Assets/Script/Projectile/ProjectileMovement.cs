using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class ProjectileMovement : MonoBehaviour
    {

        public ProjectileMove projectileMovement;
        float t;
        Vector3 target;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(target != null)
            {
                //face target
                Vector2 dir = Vector2.zero;
                dir.x = target.x - transform.position.x;
                dir.y = target.y - transform.position.y;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                angle -= 90.0f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                //move projectile
                t += Time.deltaTime / projectileMovement.timeToReachTarget;
                transform.position = Vector3.Lerp(transform.position, target, t);

                if (transform.position == target)
                    Destroy(gameObject);
            }
        }

        public void SetTarget(Vector3 dest)
        {
            t = 0f;
            target = dest;
            target.z = transform.position.z;
        }
    }
}


