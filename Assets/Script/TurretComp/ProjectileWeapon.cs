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

        int ammoCount = 1;

        bool canAttack = false;
        Transform target;
        ITurretSensor sensor;

        /// <summary>
        /// Muzzle socket where projectile shoot from
        /// </summary>
        public Transform muzzleSocket;

        public override void Initialize()
        {
            //get sensor of turret
            sensor = GetComponent<ITurretSensor>();
 
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

        IEnumerator AimTarget()
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
                    target = sensor.PickTarget<Transform>();
                }

                if(target != null)
                {
                    if(!sensor.IsTargetInRange(target.gameObject))
                    {
                        target = null;
                    }
                }
                    

                yield return null;
            }

        }

        IEnumerator AttackTarget()
        {
            while(true)
            {
                if(canAttack && target != null)
                {
                    while(ammoCount > 0 && target != null)
                    {

                        Debug.Log("Fire cannon shot at target");
                        ProjectileMovement p = Instantiate(projectileModule.projectile, muzzleSocket.position, muzzleSocket.rotation).GetComponent<ProjectileMovement>();
                        p.SetTarget(target.position);

                        ammoCount -= projectileModule.ammoPerShot;

                        if (ammoCount <= 0)
                            break;
                        
                        yield return new WaitForSeconds(projectileModule.fireInterval);
                    }
                    
                }

                if(ammoCount <= 0)
                {
                    //reload cannon
                    yield return StartCoroutine(Reload());

                    continue;
                }

                yield return null;
            }
            
        }

        IEnumerator Reload()
        {
            Debug.Log("Reloading cannon...");
            yield return new WaitForSeconds(projectileModule.reloadTime);

            ammoCount = projectileModule.shotPerFire;
        }
    }
}
