using UnityEngine;

namespace YouSingStudio.Rendering {
	public class UVRemapper_WorldToScreen
		:MeshModifier
	{
		#region Fields

		public new Camera camera;
		public Mesh source;

		[System.NonSerialized]protected Vector3[] m_Vertices;
		[System.NonSerialized]protected Vector2[] m_Uv;

		#endregion Fields

		#region Methods

		protected override void InitMesh() {
			if(m_Mesh==null) {
				if(source==null) {
					source=GetMesh();
				}
				if(source!=null) {
					m_Vertices=source.vertices;
					m_Uv=source.uv;
				}
				if(camera==null) {
					camera=Camera.main;
				}
			}
			base.InitMesh();
		}

		[ContextMenu("Remap")]
		public override void ModifyMesh() {
			if(m_Mesh==null) {InitMesh();}
			//
			int i=0,imax=m_Vertices?.Length??0;
			if(imax>0) {
				Matrix4x4 m=root.localToWorldMatrix;
				Vector2 v;
				for(;i<imax;++i) {
					v=camera.WorldToViewportPoint(m.MultiplyPoint3x4(m_Vertices[i]));
					m_Uv[i]=v;
				}
				m_Mesh.uv=m_Uv;
			}
		}

		#endregion Methods
	}
}
