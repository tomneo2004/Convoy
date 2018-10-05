using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Convoy
{
    public class ProjectileWeapon : TurretComponent
    {
        /// <summary>
        /// A module that can define this projectile weapon
        /// </summary>
        public ProjectileModule projectileModule;

        /// <summary>
        /// Current ammo left
        /// </summary>
        protected int ammoCount = 0;

        /// <summary>
        /// Determine whether weapon can attack
        /// AimTarget is responsible for this value change
        /// </summary>
        protected bool canAttack = false;

        /// <summary>
        /// Target
        /// AimTarget is responsible for this value change
        /// </summary>
        protected Transform target;

        /// <summary>
        /// Sensor on turret
        /// </summary>
        protected ITurretSensor sensor;

        /// <summary>
        /// Muzzle socket where projectile shoot from
        /// </summary>
        public Transform[] muzzleSockets;

        public override void Initialize()
        {
            //get sensor of turret
            sensor = GetComponent<ITurretSensor>();

            //init module
            projectileModule.Initialize(gameObject);
 
            ammoCount = projectileModule.shotPerFire;
        }

        public override void Active()
        {
            StartCoroutine(AimTarget());
            StartCoroutine(AttackTarget());
        }

        public override void Deactive()
        {
            StopAllCoroutines();
        }

        protected virtual IEnumerator AimTarget()
        {
            while(true)
            {
                //if have target turn and face it
                if(target != null)
                {
                    Vector2 dir = Vector2.zero;
                    dir.x = target.position.x - transform.position.x;
                    dir.y = target.position.y - transform.position.y;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    angle -= 90.0f;
                    Quaternion desireRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desireRotation, projectileModule.turnSpeed * Time.deltaTime);

                    //check if we are facing target and target is in attack angle
                    if (Vector2.Angle(transform.up, dir) <= projectileModule.attackAngle)
                    {
                        canAttack = true;
                    }
                    else
                    {
                        canAttack = false;
                    }
                }

                if(target == null)
                {
                    canAttack = false;

                    //find new target
                    if (sensor != null)
                        target = sensor.PickTarget<Transform>();
                    else
                        Debug.LogError("Turret require a sensor in order to pick target");
                }

                if(target != null && sensor != null)
                {
                    if(!sensor.IsTargetInRange(target.gameObject))
                    {
                        target = null;
                    }
                }
                    

                yield return null;
            }

        }

        protected virtual IEnumerator AttackTarget()
        {
            while(true)
            {
                if(canAttack && target != null)
                {
                    //Fire logic
                    while(ammoCount > 0 && target != null)
                    {

                        //get first muzzle socket
                        Transform muzzleSocket = muzzleSockets[0];
                        if(muzzleSocket != null)
                        {
                            Debug.Log("Fire cannon shot at target");

                            //spawn projectile
                            ProjectileMovement p = Instantiate(projectileModule.projectile, muzzleSockets[0].position, muzzleSockets[0].rotation).GetComponent<ProjectileMovement>();

                            Vector2 newTargetPos = ApplyProjectileSpread(target.position);

                            //set projectile target
                            p.SetTarget(newTargetPos);

                            //consume ammo
                            ammoCount -= projectileModule.ammoPerShot;

                            if (ammoCount <= 0)
                                break;

                            yield return new WaitForSeconds(projectileModule.fireInterval);
                        }
                        else
                        {
                            Debug.LogError("Projectile weapon require a muzzle socket in order to shoot");
                            break;
                        }
                        
                    }
                    
                }

                if(ammoCount <= 0)
                {
                    //wait for reload cannon
                    yield return StartCoroutine(Reload());

                    continue;
                }

                yield return null;
            }
            
        }

        protected virtual IEnumerator Reload()
        {
            Debug.Log("Reloading cannon...");
            yield return new WaitForSeconds(projectileModule.reloadTime);

            ammoCount = projectileModule.shotPerFire;
        }

        protected virtual Vector2 ApplyProjectileSpread(Vector2 position)
        {
            Vector2 turretPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 dir = position - turretPos;
            float destFromTarget = Vector2.Distance(turretPos, position);
            
            float angle = projectileModule.maxSpreadAngle - projectileModule.accuracyFactor * projectileModule.maxSpreadAngle;
            angle = Random.Range(-angle, angle);
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            dir = rot * dir.normalized * destFromTarget;

            return turretPos + dir;

        }
    }
}
