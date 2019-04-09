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
		int width;
		int height;
		uint[] textures = new uint[6];

		Vector3 cameraPos = new Vector3(0.0f, 0.0f, 3.0f);
		Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
		Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
		Vector3 cameraSpeed = new Vector3(0.01, 0.01, 0.01);
		double pitch = 0;
		double yaw = -90;
		double sensetive = 2;

		bool a = false;
		bool d = false;
		bool s = false;
		bool w = false;
		bool e = false;
		bool q = false;
		bool z = false;
		bool c = false;

		private void KeyDown(object sender, KeyEventArgs ev) {
			if (ev.KeyCode == Keys.A) { a = true; }
			if (ev.KeyCode == Keys.D) { d = true; }
			if (ev.KeyCode == Keys.S) { s = true; }
			if (ev.KeyCode == Keys.W) { w = true; }
			if (ev.KeyCode == Keys.E) { e = true; }
			if (ev.KeyCode == Keys.Q) { q = true; }
			if (ev.KeyCode == Keys.Z) { z = true; }
			if (ev.KeyCode == Keys.C) { c = true; }
		}
		private void KeyUp(object sender, KeyEventArgs ev) {
			if (ev.KeyCode == Keys.A) { a = false; }
			if (ev.KeyCode == Keys.D) { d = false; }
			if (ev.KeyCode == Keys.S) { s = false; }
			if (ev.KeyCode == Keys.W) { w = false; }
			if (ev.KeyCode == Keys.E) { e = false; }
			if (ev.KeyCode == Keys.Q) { q = false; }
			if (ev.KeyCode == Keys.Z) { z = false; }
			if (ev.KeyCode == Keys.C) { c = false; }
		}
		private void KeyboardControl() {
			if (a) { cameraPos -= Vector3.Cross(cameraFront, cameraUp).Normalize() * cameraSpeed; }
			if (d) { cameraPos += Vector3.Cross(cameraFront, cameraUp).Normalize() * cameraSpeed; }
			if (s) { cameraPos -= cameraSpeed * cameraFront; }
			if (w) { cameraPos += cameraSpeed * cameraFront; }
			if (e) { yaw += sensetive; }
			if (q) { yaw -= sensetive; }
			if (z) { pitch += sensetive; }
			if (c) { pitch -= sensetive; }
		}

		public Form1() {
			InitializeComponent();
			simpleOpenGlControl1.InitializeContexts();
			width = simpleOpenGlControl1.Width;
			height = simpleOpenGlControl1.Height;
			Gl.glViewport(0, 0, width, height);
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();
			Glu.gluPerspective(45.0f, (double)width / (double)height, 0.01f, 100.0f);
			Gl.glEnable(Gl.GL_CULL_FACE);
			Gl.glCullFace(Gl.GL_BACK);
			Gl.glEnable(Gl.GL_TEXTURE_2D);

			float[] diffuseColorMaterial = { 1.0f, 1.0f, 1.0f, 1.0f };
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, diffuseColorMaterial);
			Gl.glTranslated(0, 0, -10);
			creatTexture(@"E:\git projects\cube\3.bmp", 0);
			creatTexture(@"E:\git projects\cube\4.bmp", 1);
			creatTexture(@"E:\git projects\cube\1.bmp", 2);
		}
		private void creatTexture(String path, int level) {
			var bmp = new Bitmap(path);
			var bmpData = bmp.LockBits(
				new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format24bppRgb);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, level);
			Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGB8,
			bmp.Width, bmp.Height, 0, Gl.GL_BGR_EXT,
			Gl.GL_UNSIGNED_BYTE, bmpData.Scan0);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, level);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);  
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);  

			bmp.UnlockBits(bmpData);
		}

		private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e) {
			mouseControl();
			KeyboardControl();
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 1);
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			Gl.glPointSize(3);
			Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_LINES);
			Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_LINES);

			Look();

			Gl.glBegin(Gl.GL_QUADS);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(-1.0f, -1.0f, 1.0f); // Низ лево
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(1.0f, -1.0f, 1.0f);  // Низ право
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(1.0f, 1.0f, 1.0f);   // Верх право
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(-1.0f, 1.0f, 1.0f);  // Верх лево

			// Задняя грань
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(-1.0f, -1.0f, -1.0f);    // Низ право
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(-1.0f, 1.0f, -1.0f); // Верх право
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(1.0f, 1.0f, -1.0f);  // Верх лево
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(1.0f, -1.0f, -1.0f); // Низ лево
			Gl.glEnd();

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);

			Gl.glBegin(Gl.GL_QUADS);
			// Верхняя грань
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(-1.0f, 1.0f, -1.0f); // Верх лево
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(-1.0f, 1.0f, 1.0f);  // Низ лево
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(1.0f, 1.0f, 1.0f);   // Низ право
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(1.0f, 1.0f, -1.0f);  // Верх право

			// Нижняя грань
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(-1.0f, -1.0f, -1.0f);    // Верх право
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(1.0f, -1.0f, -1.0f); // Верх лево
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(1.0f, -1.0f, 1.0f);  // Низ лево
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(-1.0f, -1.0f, 1.0f); // Низ право

			// Правая грань
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(1.0f, -1.0f, -1.0f); // Низ право
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(1.0f, 1.0f, -1.0f);  // Верх право
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(1.0f, 1.0f, 1.0f);   // Верх лево
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(1.0f, -1.0f, 1.0f);  // Низ лево
			Gl.glEnd();

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 2);

			Gl.glBegin(Gl.GL_QUADS);
			// Левая грань
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(-1.0f, -1.0f, -1.0f); // Низ лево
			Gl.glTexCoord2f(5.0f, 0.0f); Gl.glVertex3d(-1.0f, -1.0f, 1.0f);  // Низ право
			Gl.glTexCoord2f(5.0f, 5.0f); Gl.glVertex3d(-1.0f, 1.0f, 1.0f);   // Верх право
			Gl.glTexCoord2f(0.0f, 5.0f); Gl.glVertex3d(-1.0f, 1.0f, -1.0f);  // Верх лево			
			Gl.glEnd();
		}

		private bool firstMouse = true;
		private int lastX;
		private int lastY;
		private double sens = 0.05;
		private void mouseControl() {
			int xpos = Cursor.Position.X;
			int ypos = Cursor.Position.Y;

			if (firstMouse) {
				lastX = xpos;
				lastY = ypos;
				firstMouse = false;
			}
			double xoffset = xpos - lastX;
			double yoffset = lastY - ypos;
			lastX = xpos;
			lastY = ypos;

			xoffset *= sens;
			yoffset *= sens;
			yaw += xoffset;
			pitch += yoffset;

			if (pitch > 89.0f)
				pitch = 89.0f;
			if (pitch < -89.0f)
				pitch = -89.0f;

			cameraFront = getDirection(pitch, yaw);
		}

		private void timer1_Tick(object sender, EventArgs e) {
			simpleOpenGlControl1.Invalidate();
		}
		
		private void Look() {
			gluLookAt(cameraPos, cameraPos + cameraFront, cameraUp);
		}
		public void gluLookAt(Vector3 eye, Vector3 pos, Vector3 up) {
			Glu.gluLookAt(eye.x, eye.y, eye.z, pos.x, pos.y, pos.y, up.x, up.y, up.z);
		}
		private double r = 10;
		private Vector3 getDirection(double pitch, double yaw) {
			Vector3 direction;
			direction.x = r*Math.Cos(DegreeToRadian(pitch)) * Math.Cos(DegreeToRadian(yaw));
			direction.y = r*Math.Sin(DegreeToRadian(pitch));
			direction.z = r*Math.Cos(DegreeToRadian(pitch)) * Math.Sin(DegreeToRadian(yaw));
			return direction;
		}
		private double DegreeToRadian(double angle) {
			return Math.PI * angle / 180.0;
		}

		public struct Vector3 {
			public double x, y, z;

			public Vector3(double x, double y, double z) {
				this.x = x;
				this.y = y;
				this.z = z;
			}

			public static Vector3 operator +(Vector3 a, Vector3 b) {
				return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
			}
			public static Vector3 operator -(Vector3 a, Vector3 b) {
				return a + new Vector3(-b.x, -b.y, -b.z);
			}
			public static Vector3 operator *(Vector3 a, Vector3 b) {
				return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
			}
			public static Vector3 Cross(Vector3 a, Vector3 b) {
				return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
			}
			public double Length() {
				return Math.Sqrt((x * x) + (y * y) + (z * z));
			}
			public Vector3 Normalize() {
				double l = Length();
				Vector3 v = new Vector3();
				v.x = x / l;
				v.y = y / l;
				v.z = z / l;
				return v;
			}
		}
	}
	
}
