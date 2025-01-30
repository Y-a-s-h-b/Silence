using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// This action will cause your AI character to start running
	/// </summary>
	[AddComponentMenu("Corgi Engine/Character/AI/Actions/AI Action Roll")]
	public class AIActionRoll : AIAction
	{
		protected CharacterRoll _characterRoll;

		/// <summary>
		/// On init we grab our CharacterRun component
		/// </summary>
		public override void Initialization()
		{
			if (!ShouldInitialize) return;
			_characterRoll = this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterRoll>();
		}

		/// <summary>
		/// On PerformAction we start running
		/// </summary>
		public override void PerformAction()
		{
			_characterRoll.StartRoll();
		}

        public override void OnExitState()
        {
			_characterRoll.StopRoll();
        }
    }
}