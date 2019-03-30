using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;


namespace OpenGL_cube {
	public partial class Form1 : Form {
		int perspective = 1;
		Camera cam = new Camera();

		private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == 'A' || e.KeyChar == 'a') { cam.Rotate_Position(2.5f, 0, 1, 0); Draw(); }
			if (e.KeyChar == 'D' || e.KeyChar == 'd') { cam.Rotate_Position(2.5f, 0, -1, 0); Draw(); }
			if (e.KeyChar == 'S' || e.KeyChar == 's') { cam.upDown(-0.2f); Draw(); }
			if (e.KeyChar == 'W' || e.KeyChar == 'w') { cam.upDown(0.2f); Draw(); }
			if (e.KeyChar == 'E' || e.KeyChar == 'e') { perspective = 2; ResizeGLScene(); Draw(); }
			if (e.KeyChar == 'Q' || e.KeyChar == 'q') { perspective = 1; ResizeGLScene(); Draw(); }
		}

		private void simpleOpenGlControl1_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Up) {
				cam.Move_Camera(1f);
				Draw();
			}
			else if (e.KeyCode == Keys.Down) {
				cam.Move_Camera(-1f);
				Draw();
			}
			else if (e.KeyCode == Keys.Right) {
				cam.Rotate_View(-0.1f);
				Draw();
			}
			else if (e.KeyCode == Keys.Left) {
				cam.Rotate_View(0.1f);
				Draw();
			}
		}

		public Form1() {
			InitializeComponent();
			simpleOpenGlControl1.InitializeContexts();

			Init();
			ResizeGLScene();
			Draw();
		}

		private void timer1_Tick(object sender, EventArgs e) {
			//this.simpleOpenGlControl1.Invalidate();
			//cam.update();
			Draw();
		}

		private void Init() {
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

			Gl.glClearColor(255, 255, 255, 1);

			Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();

		//	Glu.gluPerspective(45.0f, simpleOpenGlControl1.Width / simpleOpenGlControl1.Height, 0.01f, 5000.0f);

		//	Gl.glMatrixMode(Gl.GL_MODELVIEW);
			//Gl.glLoadIdentity();

			//Gl.glEnable(Gl.GL_DEPTH_TEST);
					   
		//	cam.Position_Camera(0, 0, -10, 0, 0, 0, 0, 1, 0);

		}

		private void ResizeGLScene() {
			if (simpleOpenGlControl1.Height == 0) {
				simpleOpenGlControl1.Height = 1;
			}

			Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();

			if (perspective == 1)
				Glu.gluPerspective(45.0f, simpleOpenGlControl1.Width / simpleOpenGlControl1.Height, 0.1f, 30.0f);
			else if (perspective == 2)
				Gl.glOrtho(-20, 10, -10, 10, -1000, 1000);

			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			cam.Position_Camera(0, 6, -15, 0, 3, 0, 0, 1, 0);
			timer1.Start();
		}

		private void simpleOpenGlControl1_Resize(object sender, EventArgs e) {
			ResizeGLScene();
			Draw();
		}

		private void Draw() {
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
			Gl.glLoadIdentity();			

			double l = 1;

			if (perspective == 1) {
				Gl.glTranslated(0, 3, 10);
				l = 1;
			} else if (perspective == 1) {
				Gl.glTranslated(-5, 3, 10);
				l = 5;
			}

			cam.Look();

			Gl.glBegin(Gl.GL_QUADS);
			Gl.glColor3d(0, l, 0);                            
			Gl.glVertex3d(l, l, -l);
			Gl.glVertex3d(-l, l, -l);
			Gl.glVertex3d(-l, l, l);
			Gl.glVertex3d(l, l, l);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);
			Gl.glColor3d(l, 0.5f, 0);                
			Gl.glVertex3d(l, -l, l);
			Gl.glVertex3d(-l, -l, l);
			Gl.glVertex3d(-l, -l, -l);
			Gl.glVertex3d(l, -l, -l);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);
			Gl.glColor3d(l, 0, 0);                      
			Gl.glVertex3d(l, l, l);
			Gl.glVertex3d(-l, l, l);
			Gl.glVertex3d(-l, -l, l);
			Gl.glVertex3d(l, -l, l);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);
			Gl.glColor3d(l, l, 0);                   
			Gl.glVertex3d(l, -l, -l);
			Gl.glVertex3d(-l, -l, -l);
			Gl.glVertex3d(-l, l, -l);
			Gl.glVertex3d(l, l, -l);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);
			Gl.glColor3d(0, 0, l);                      
			Gl.glVertex3d(-l, l, l);
			Gl.glVertex3d(-l, l, -l);
			Gl.glVertex3d(-l, -l, -l);
			Gl.glVertex3d(-l, -l, l);
			Gl.glEnd();

			Gl.glBegin(Gl.GL_QUADS);
			Gl.glColor3d(l, 0, l); 
			Gl.glVertex3d(l, l, -l);
			Gl.glVertex3d(l, l, l);
			Gl.glVertex3d(l, -l, l);
			Gl.glVertex3d(l, -l, -l);
			Gl.glEnd();

			Gl.glFlush();
			simpleOpenGlControl1.Invalidate();
		}

		class Camera {
			public struct Vector3D { public float x, y, z; };

			public Vector3D mPos; // Вектор позиции камеры 
			private Vector3D mView; // Направление, куда смотрит камера 
			private Vector3D mUp; // Вектор верхнего направления 
			private Vector3D mStrafe; // Вектор для стрейфа (движения влево и вправо) камеры

			//private Vector3D Cross(Vector3D vV1, Vector3D vV2, Vector3D vVector2) {
			//	Vector3D vNormal;
			//	Vector3D vVector1;
			//	vVector1.x = vV1.x - vV2.x;
			//	vVector1.y = vV1.y - vV2.y; vVector1.z = vV1.z - vV2.z;
			//	vNormal.x = ((vVector1.y * vVector2.z) - (vVector1.z * vVector2.y));
			//	vNormal.y = ((vVector1.z * vVector2.x) - (vVector1.x * vVector2.z));
			//	vNormal.z = ((vVector1.x * vVector2.y) - (vVector1.y * vVector2.x));
			//	return vNormal;
			//}

			private float Magnitude(Vector3D vNormal) {
				return (float)Math.Sqrt((vNormal.x * vNormal.x) + (vNormal.y * vNormal.y) + (vNormal.z * vNormal.z));
			}

			private Vector3D Normalize(Vector3D vVector) {
				float magnitude = Magnitude(vVector);
				vVector.x = vVector.x / magnitude;
				vVector.y = vVector.y / magnitude;
				vVector.z = vVector.z / magnitude;
				return vVector;
			}

			public void Position_Camera(float pos_x, float pos_y, float pos_z, float view_x, float view_y, float view_z, float up_x, float up_y, float up_z) {
				mPos.x = pos_x; // Позиция камеры 
				mPos.y = pos_y;
				mPos.z = pos_z;
				mView.x = view_x; // Куда смотрит, т.е. взгляд 
				mView.y = view_y;
				mView.z = view_z;
				mUp.x = up_x; // Вертикальный вектор камеры 
				mUp.y = up_y;
				mUp.z = up_z;
			}

			public void Rotate_View(float speed) {
				Vector3D vVector; // Полчим вектор взгляда 
				vVector.x = mView.x - mPos.x;
				vVector.y = mView.y - mPos.y;
				vVector.z = mView.z - mPos.z;
				mView.z = (float)(mPos.z + Math.Sin(speed) * vVector.x + Math.Cos(speed) * vVector.z);
				mView.x = (float)(mPos.x + Math.Cos(speed) * vVector.x - Math.Sin(speed) * vVector.z);
			}

			public void Rotate_Position(float angle, float x, float y, float z) {
				mPos.x = mPos.x - mView.x;
				mPos.y = mPos.y - mView.y;
				mPos.z = mPos.z - mView.z;
				Vector3D vVector = mPos;
				Vector3D AVector;
				float SinA = (float)Math.Sin(Math.PI * angle / 180.0);
				float CosA = (float)Math.Cos(Math.PI * angle / 180.0);

				// Найдем новую позицию X для вращаемой точки 
				AVector.x = (CosA + (1 - CosA) * x * x) * vVector.x;
				AVector.x += ((1 - CosA) * x * y - z * SinA) * vVector.y;
				AVector.x += ((1 - CosA) * x * z + y * SinA) * vVector.z;

				// Найдем позицию Y 
				AVector.y = ((1 - CosA) * x * y + z * SinA) * vVector.x;
				AVector.y += (CosA + (1 - CosA) * y * y) * vVector.y;
				AVector.y += ((1 - CosA) * y * z - x * SinA) * vVector.z;

				// И позицию Z 
				AVector.z = ((1 - CosA) * x * z - y * SinA) * vVector.x;
				AVector.z += ((1 - CosA) * y * z + x * SinA) * vVector.y;
				AVector.z += (CosA + (1 - CosA) * z * z) * vVector.z;

				mPos.x = mView.x + AVector.x;
				mPos.y = mView.y + AVector.y;
				mPos.z = mView.z + AVector.z;
			}

			public void Move_Camera(float speed) {
				Vector3D vVector; // Получаем вектор взгляда 
				vVector.x = mView.x - mPos.x;
				vVector.y = mView.y - mPos.y;
				vVector.z = mView.z - mPos.z;

				vVector.y = 0.0f; // Это запрещает камере подниматься вверх 
				vVector = Normalize(vVector);

				mPos.x += vVector.x * speed;
				mPos.z += vVector.z * speed;
				mView.x += vVector.x * speed;
				mView.z += vVector.z * speed;
			}

			//public void Strafe(float speed) {
			//	// добавим вектор стрейфа к позиции 
			//	mPos.x += mStrafe.x * speed;
			//	mPos.z += mStrafe.z * speed;
			//	// Добавим теперь к взгляду 
			//	mView.x += mStrafe.x * speed;
			//	mView.z += mStrafe.z * speed;
			//}

			//public void update() {
			//	Vector3D vCross = Cross(mView, mPos, mUp);

			//	// Нормализуем вектор стрейфа 
			//	mStrafe = Normalize(vCross);
			//}

			public void upDown(float speed) {
				mPos.y += speed;
			}

			public void Look() {
				Glu.gluLookAt(mPos.x, mPos.y, mPos.z,
							  mView.x, mView.y, mView.z,
							  mUp.x, mUp.y, mUp.z);
			}

			//public double getPosX() { // Возвращает позицию камеры по Х  
			//	return mPos.x;
			//}
			//public double getPosY() { // Возвращает позицию камеры по Y  
			//	return mPos.y;
			//}
			//public double getPosZ() { // Возвращает позицию камеры по Z  
			//	return mPos.z;
			//}
			//public double getViewX() { // Возвращает позицию взгляда по Х  
			//	return mView.x;
			//}
			//public double getViewY() { // Возвращает позицию взгляда по Y  
			//	return mView.y;
			//}
			//public double getViewZ() { // Возвращает позицию взгляда по Z  
			//	return mView.z;
			//}

		}
	}
}
