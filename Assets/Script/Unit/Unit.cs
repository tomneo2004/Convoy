using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [RequireComponent(typeof(Team))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Unit : MonoBehaviour
    {
        /// <summary>
        /// Event for unity destroy
        /// </summary>
        public GameEvent unityDestroy;

        /// <summary>
        /// Define team for this unit
        /// </summary>
        Team teamComp;

        /// <summary>
        /// Unit's equipment
        /// </summary>
        IEquipment[] equipments;

        float _angle;
        Vector2 center;

        // Use this for initialization
        void Start()
        {
            center = transform.position;

            if(equipments != null)
            {
                //init equipments
                for (int i = 0; i < equipments.Length; i++)
                    equipments[i].Initialize();

                //active equipments
                for (int i = 0; i < equipments.Length; i++)
                    equipments[i].Active();
            }
        }

        // Update is called once per frame
        void Update()
        {
            /**
             * test purpose
             **/
            Vector2 newPos = Vector2.zero;
            _angle += 1f * Time.deltaTime;
            newPos = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * 3f;
            transform.position = center + newPos;
        }

        private void OnDisable()
        {
            unityDestroy.PostEvent(this);
      
        }
    }
}

