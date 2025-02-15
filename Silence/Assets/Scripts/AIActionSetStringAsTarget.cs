using MoreMountains.Tools;
using System;
using UnityEngine;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// An AIACtion used to set a specified String as the target
	/// </summary>
	[AddComponentMenu("Corgi Engine/Character/AI/Actions/AI Action Set String As Target")]
	public class AIActionSetStringAsTarget : AIAction
	{
		public String TargetName;
		public bool OnlyRunOnce = true;

		protected bool _alreadyRan = false;

		/// <summary>
		/// On init we initialize our action
		/// </summary>
		public override void Initialization()
		{
			if (!ShouldInitialize) return;
			base.Initialization();
			_alreadyRan = false;
		}

		/// <summary>
		/// Sets a new target
		/// </summary>
		public override void PerformAction()
		{
			if (OnlyRunOnce && _alreadyRan)
			{
				return;
			}
			_brain.Target = GameObject.Find(TargetName).transform;
		}

		/// <summary>
		/// On enter state we reset our flag
		/// </summary>
		public override void OnEnterState()
		{
			base.OnEnterState();
			_alreadyRan = false;
		}
	}
}