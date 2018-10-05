using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    [RequireComponent(typeof(Team))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]

    /// <summary>
    /// Unit class represent each unit in game world
    /// </summary>
    public class Unit : MonoBehaviour
    {
        /// <summary>
        /// Event for unity disabled
        /// </summary>
        [Tooltip("Event\n When unity disabled")]
        public GameEvent evt_unityDisabled;

        /// <summary>
        /// How much gravity can effect this unit
        /// </summary>
        [Tooltip("How much gravity can effect this unit")]
        public float UnitGravityScale = 0f;

        /// <summary>
        /// Team component that define team for this unit
        /// </summary>
        protected Team teamComp;

        /// <summary>
        /// Return Team component
        /// </summary>
        public Team GetTeam
        {
            get { return teamComp; }
        }

        /// <summary>
        /// Unit's equipment
        /// </summary>
        protected IEquipment[] equipments = null;

        /// <summary>
        /// Return unit's all equipments
        /// </summary>
        public IEquipment[] GetEquipments
        {
            get{ return equipments; }
        }

        /// <summary>
        /// Rigidbody of this unit
        /// </summary>
        protected Rigidbody2D rigidbodyComp = null;

        // Use this for initialization
        protected virtual void Start()
        {
            rigidbodyComp = GetComponent<Rigidbody2D>();
            rigidbodyComp.gravityScale = 0f;

            teamComp = GetComponent<Team>();

            //Equipments
            if(equipments == null)
            {
                equipments = GetComponentsInChildren<IEquipment>();

                //init equipments
                for (int i = 0; i < equipments.Length; i++)
                    equipments[i].Initialize();

                //active equipments
                for (int i = 0; i < equipments.Length; i++)
                    equipments[i].Active();
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {
 
        }

        protected virtual void OnDisable()
        {
            //Post unit disable event
            if(evt_unityDisabled != null)
                evt_unityDisabled.PostEvent(this);
      
        }
    }
}

