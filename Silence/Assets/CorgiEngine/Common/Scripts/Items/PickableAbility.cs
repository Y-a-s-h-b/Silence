using UnityEngine;
using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using MoreMountains.Feedbacks;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// Add this class to an object with a trigger box collider 2D, and it'll become a pickable object, able to permit or forbid an ability on a Character
	/// </summary>
	[AddComponentMenu("Corgi Engine/Items/Pickable Ability")]
	public class PickableAbility : PickableItem
	{
		public MMF_Player DestroyFeedback;
		public enum Methods
		{
			Permit,
			Forbid
		}

		[Header("Pickable Ability")] 
		/// whether this object should permit or forbid an ability when picked
		[Tooltip("whether this object should permit or forbid an ability when picked")]
		public Methods Method = Methods.Permit;
		/// whether or not only characters of Player type should be able to pick this 
		[Tooltip("whether or not only characters of Player type should be able to pick this")]
		public bool OnlyPickableByPlayerCharacters = true;
		public string AbilityTypeAsString;
        public float timeToFreeze;
        /// <summary>
        /// Checks if the object is pickable 
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        protected override bool CheckIfPickable()
		{
			_character = _pickingCollider.GetComponent<Character>();

			// if what's colliding with the coin ain't a characterBehavior, we do nothing and exit
			if (_character == null)
			{
				return false;
			}

			if (OnlyPickableByPlayerCharacters && (_character.CharacterType != Character.CharacterTypes.Player))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// on pick, we permit or forbid our target ability
		/// </summary>
		protected override void Pick(GameObject picker)
		{
			
			bool newState = Method == Methods.Permit ? true : false;
			(_character.gameObject.GetComponent(AbilityTypeAsString) as CharacterAbility)?.PermitAbility(newState);
            StartCoroutine(FreezePlayerFor(timeToFreeze, _character));
            StartCoroutine(MoveObjectCoroutine(this.transform, _character.gameObject.transform.position + new Vector3(0, 3, 0)));
        }
        public IEnumerator FreezePlayerFor(float timeToFreeze, Character playerGO)
        {
            _character.ChangeCharacterConditionTemporarily(CharacterStates.CharacterConditions.Stunned,timeToFreeze,true,false);
            _character._animator.SetBool("AbilityGain", true);
			_character.Reset();
            yield return new WaitForSeconds(timeToFreeze);
            
        }
		public void SetTriggerFalse()
		{
            _character._animator.SetBool("AbilityGain", false);
        }
        private IEnumerator MoveObjectCoroutine(Transform objectToMove, Vector3 targetPosition)
        {
            Vector3 startPosition = objectToMove.position;
            Vector3 distance = targetPosition - startPosition;
            float elapsedTime = 0f;

            while (elapsedTime < 1)
            {
                // Calculate the movement for this frame
                float deltaTime = Time.deltaTime;
                Vector3 movement = distance * (deltaTime / 1);
                objectToMove.Translate(movement);

                elapsedTime += deltaTime;
                yield return null; // Wait until the next frame
            }

            // Ensure the object ends at the exact target position
            objectToMove.position = targetPosition;
			DestroyFeedback.PlayFeedbacks();
        }
		public void SetDashUIActive()
		{
            foreach (var bar in GUIManager.Instance.dashCooldownBars)
            {
                bar.gameObject.SetActive(true);
            }
        }
    
	}
}