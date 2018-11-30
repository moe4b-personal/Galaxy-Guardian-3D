﻿using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game
{
	public class EnemyAIController : AIController
	{
        public Transform Target { get { return Planet.transform; } }

        public float rotationSpeed = 40f;

        public float movementSpeed = 4f;

        public override void Init(AI AI)
        {
            base.Init(AI);

            StartCoroutine(Procedure());
        }

        protected virtual IEnumerator Procedure()
        {
            while(true)
            {
                var distance = Vector3.Distance(transform.position, Target.position);

                var angles = transform.eulerAngles;
                angles.y += rotationSpeed * Time.deltaTime;
                transform.eulerAngles = angles;

                transform.position = Target.position + transform.rotation * Vector3.back * (Mathf.MoveTowards(distance, 0f, movementSpeed * Time.deltaTime));

                yield return new WaitForEndOfFrame();
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            var target = collision.gameObject.GetComponent<Entity>();

            if (target == null) return;

            Entity.DoDamage(target, 20f);
            Entity.DoDamage(Entity, Entity.Health);
        }
    }
}