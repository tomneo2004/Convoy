using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AIMovement : MonoBehaviour
    {

        [HideInInspector]public Vector2 dest;

        public float speed = 1f;
        Rigidbody2D rBody;

        // Use this for initialization
        void Start()
        {
            dest = transform.position;
            rBody = GetComponent<Rigidbody2D>();

            //StartCoroutine(Move());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator Move()
        {

            while(true)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y);

                if (pos != dest)
                {
                    Vector2 velocity = dest.normalized * speed * Time.fixedDeltaTime;
                    rBody.velocity = velocity;
                }

                yield return new WaitForFixedUpdate();
            }
            
        }
    }
}


