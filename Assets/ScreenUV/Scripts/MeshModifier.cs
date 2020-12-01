using UnityEngine;

namespace YouSingStudio.Rendering {
	public class MeshModifier
		:MonoBehaviour
	{
		#region Fields

		public Transform root;
		public Mesh mesh;
		public MeshFilter meshFilter;
		public SkinnedMeshRenderer skinnedMeshRenderer;

		[System.NonSerialized]protected bool m_IsStartCalled;
		[System.NonSerialized]protected Mesh m_Mesh;

		#endregion Fields

		#region Unity Messages

		protected virtual void Awake() {
			if(meshFilter==null) {meshFilter=GetComponent<MeshFilter>();}
			if(skinnedMeshRenderer==null) {skinnedMeshRenderer=GetComponent<SkinnedMeshRenderer>();}
		}

		protected virtual void Start() {
			//
			m_IsStartCalled=true;
			ModifyMesh();
		}

		protected virtual void OnEnable() { 
			if(m_IsStartCalled) {
				ModifyMesh();
			}
		}

		protected virtual void Update() {
			ModifyMesh();
		}

		#endregion Unity Messages

		#region Methods

		protected virtual void InitMesh() {
			if(m_Mesh!=null) {
				return;
			}
			//
			if(root==null) {root=transform;}
			Mesh tmp=mesh;
				m_Mesh=GetMesh();
				if(m_Mesh!=null) { 
					m_Mesh=Mesh.Instantiate(m_Mesh);
					SetMesh(m_Mesh);
				}
			mesh=tmp;
		}

		public virtual Mesh GetMesh() {
			if(skinnedMeshRenderer!=null) {return skinnedMeshRenderer.sharedMesh;}
			if(meshFilter!=null) {return meshFilter.sharedMesh;}
			return mesh;
		}

		public virtual void SetMesh(Mesh value) {
			if(skinnedMeshRenderer!=null) {skinnedMeshRenderer.sharedMesh=value;}
			if(meshFilter!=null) {meshFilter.sharedMesh=value;}
			mesh=value;
		}

		public virtual void ModifyMesh() {
		}

		#endregion Methods
	}
}
