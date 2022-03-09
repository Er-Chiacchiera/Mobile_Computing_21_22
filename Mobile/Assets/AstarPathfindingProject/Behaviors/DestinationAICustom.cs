using UnityEngine;
using System.Collections;

namespace Pathfinding
{
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class DestinationAICustom : VersionedMonoBehaviour
	{
		[SerializeField]
		private float distanzaStop = 10;
		/// <summary>The object that the AI should move to</summary>
		IAstarAI ai;
		private GameObject player;
		public GameObject officer;
		public Animator animator;
		void OnEnable()
		{
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable()
		{
			if (ai != null) ai.onSearchPath -= Update;
		}

        private void Start()
        {
			player = GameObject.Find("Robot");
		}


        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
		{
			//calcolo distanza auto-robot
			float distanzaX = (gameObject.transform.position.x - player.GetComponent<Transform>().position.x);
			float distanzaY = (gameObject.transform.position.y - player.GetComponent<Transform>().position.y);
			float distanza = Mathf.Sqrt(distanzaX * distanzaX + distanzaY * distanzaY);

			/*if (distanza > distanzaStop && !arrived)
			{
				if (target != null && ai != null) ai.destination = target.position;
			}
			else
            {
				if (!arrived)
                {

                }
				arrived = true;
				ai.destination = gameObject.GetComponent<Transform>().position;
				
            }*/

			if (player.GetComponent<Transform>().position != null && ai != null) ai.destination = player.GetComponent<Transform>().position;

			if (distanza <= distanzaStop)
            {
				animator.SetTrigger("Open");

				Vector3 spawnPos = gameObject.GetComponent<Transform>().position;
				float rot = gameObject.GetComponent<Rigidbody2D>().rotation;

				
				float dis = 1;
				spawnPos.x = spawnPos.x + dis * Mathf.Cos(rot*Mathf.Deg2Rad);
				spawnPos.y = spawnPos.y + dis * Mathf.Sin(rot*Mathf.Deg2Rad);
				Instantiate(officer, spawnPos, new Quaternion(0,0,rot,0));
				ai.destination = player.GetComponent<Transform>().position;
				enabled = false;
			}
		}
	}
}
