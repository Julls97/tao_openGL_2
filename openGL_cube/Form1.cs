using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace openGL_cube {
	public partial class Form1 : Form {

		//double xrot, yrot, zrot;

		float[,] cubeVertexArray = {
			{ 0.0f, 0.0f, 1.0f },
			{ 0.0f, 1.0f, 1.0f },
			{ 1.0f, 1.0f, 1.0f },
			{ 1.0f, 0.0f, 1.0f },
			{ 0.0f, 0.0f, 0.0f },
			{ 0.0f, 1.0f, 0.0f },
			{ 1.0f, 1.0f, 0.0f },
			{ 1.0f, 0.0f, 0.0f }
		};
		float[,] cubeColorArray = {
			{ 0.0f, 0.0f, 1.0f },
			{ 0.6f, 0.98f, 0.6f },
			{ 1.0f, 0.84f, 0.8f },
			{ 0.8f, 0.36f, 0.36f },
			{ 1.0f, 0.27f, 0.0f },
			{ 0.82f, 0.13f, 0.56f },
			{ 0.54f, 0.17f, 0.89f },
			{ 0.0f, 1.0f, 1.0f }
		};
		byte[,] cubeIndexArray = {
			{ 0, 3, 2, 1 },
			{ 0, 1, 5, 4 },
			{ 7, 4, 5, 6 },
			{ 3, 7, 6, 2 },
			{ 1, 2, 6, 5 },
			{ 0, 4, 7, 3 }
		};

		public Form1() {
			InitializeComponent();
			simpleOpenGlControl1.InitializeContexts();

			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

			//Gl.glEnable(Gl.GL_CULL_FACE);
			//Gl.glCullFace(Gl.GL_BACK);

			//ResizeGLScene();
			//DrawGLScene();
		}


		//private void ResizeGLScene() {
		//	if (simpleOpenGlControl1.Height == 0) {
		//		simpleOpenGlControl1.Height = 1;
		//	}

		//	Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
		//	Gl.glMatrixMode(Gl.GL_PROJECTION);
		//	Gl.glLoadIdentity();
		//	Glu.gluPerspective(45.0f, (float)simpleOpenGlControl1.Width / (float)simpleOpenGlControl1.Height, 0.1f, 200.0f);
		//	Gl.glMatrixMode(Gl.GL_MODELVIEW);


		//}

		//private void DrawGLScene() {
		//	Gl.glClearColor(1, 1, 1, 1.0f);
		//	Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
		//	Gl.glLoadIdentity();

		//	Gl.glPushMatrix();

		//	Gl.glTranslated(0, 0, -4);

		//	Gl.glPopMatrix();

		//	Gl.glFlush();
		//	simpleOpenGlControl1.Invalidate();
		//}

		//private void Form1_Resize(object sender, EventArgs e) {
		//	ResizeGLScene();
		//	DrawGLScene();
		//}

		private void AnT_Paint(object sender, PaintEventArgs e) {
			Gl.glEnable(Gl.GL_CULL_FACE);
			Gl.glCullFace(Gl.GL_BACK);

			Gl.glClearColor(1, 1, 1, 1.0f);
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
			Gl.glLoadIdentity();

			Gl.glPushMatrix();

			Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, cubeVertexArray);
			Gl.glColorPointer(3, Gl.GL_FLOAT, 0, cubeColorArray);
			Gl.glDrawElements(Gl.GL_QUADS, 24, Gl.GL_UNSIGNED_BYTE, cubeIndexArray);

			Gl.glPopMatrix();

			Gl.glFlush();
			simpleOpenGlControl1.Invalidate();
		}
	}
}

//namespace Engine {
//	class Camera {

//		private Vector3D mPos;
//		// Вектор позиции камеры 
//		private Vector3D mView;
//		// Направление, куда смотрит камера 
//		private Vector3D mUp;
//		// Вектор верхнего направления 
//		private Vector3D mStrafe;
//		// Вектор для стрейфа (движения влево и вправо) камеры

//		private struct Vector3D {
//			public float x, y, z;
//		};
//	}
//}
