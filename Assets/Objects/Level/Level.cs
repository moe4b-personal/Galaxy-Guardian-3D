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
    [DefaultExecutionOrder(ExecutionOrder)]
	public class Level : MonoBehaviour
	{
        public const int ExecutionOrder = -100;

        public static Level Instance { get; protected set; }

        public Planet Planet { get; protected set; }

        public Player Player { get; protected set; }

        public GameMenu Menu { get; protected set; }

        public Spawner Spawner { get; protected set; }

        protected virtual void Awake()
        {
            Instance = this;

            Planet = FindObjectOfType<Planet>();

            Player = FindObjectOfType<Player>();

            Menu = FindObjectOfType<GameMenu>();

            Spawner = FindObjectOfType<Spawner>();
        }

        public virtual void Begin()
        {
            Menu.Main.Element.Visible = false;
            Menu.Gameplay.Element.Visible = true;

            Spawner.Begin();

            Player.hasControl = true;

            Planet.Entity.OnDeath += OnPlanetDied;
            Player.Entity.OnDeath += OnPlayerDied;
        }

        void OnPlayerDied(Entity damager)
        {
            End();
        }

        void OnPlanetDied(Entity damager)
        {
            End();
        }

        void End()
        {
            Planet.Entity.OnDeath -= OnPlanetDied;
            Player.Entity.OnDeath -= OnPlayerDied;

            Spawner.Stop();

            Player.hasControl = false;
        }
    }
}