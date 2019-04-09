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
		double xrot, yrot, zrot;
		int width;
		int height;
		double speed = 1;
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

		private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e) {

		}
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
			//Glu.gluPerspective(45.0f, (double)width / (double)height, 0.01f, 5000.0f);
			Glu.gluPerspective(45.0f, (double)width / (double)height, 0.01f, 100.0f);
			Gl.glEnable(Gl.GL_CULL_FACE);
			Gl.glCullFace(Gl.GL_BACK);

			Gl.glEnable(Gl.GL_TEXTURE_2D);



			float[] diffuseColorMaterial = { 1.0f, 1.0f, 1.0f, 1.0f };
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, diffuseColorMaterial);
			Gl.glTranslated(0, 0, -10);
			creatTexture(@"E:\git projects\tao_openGL_2\3.bmp", 0);

			creatTexture(@"E:\git projects\tao_openGL_2\4.bmp", 1);
			creatTexture(@"E:\git projects\tao_openGL_2\1.bmp", 2);

			float[] light0_diffuse = { 0.4f, 0.7f, 0.2f };
			float[] light0_direction = { 1, 0.0f, 0.0f, 0.0f };
			//Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_LIGHT0);
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, light0_diffuse);
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0_direction);

			System.Media.SoundPlayer player = new System.Media.SoundPlayer();

			player.SoundLocation = @"E:\git projects\tao_openGL_2\1.wav";
			player.Play();
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
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);  // Linear Filtering 
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);  // Linear Filtering 

			bmp.UnlockBits(bmpData);
		}
		float rot = 0;
		float gearspeed = 0.3f;
		private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e) {
			mouseControl();
			KeyboardControl();
			//Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
			//Gl.glBindTexture(Gl.GL_TEXTURE_2D,1);
			//Sphere(10, 100, 100);
			//return;
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 1);
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			//Gl.glTranslated(0, 0, -5);

			Gl.glPointSize(3);
			Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_LINES);
			Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_LINES);

			Look();

			//GearsDraw();
			//return;
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
			Gl.glTexCoord2f(100.0f, 0.0f); Gl.glVertex3d(-1.0f, -1.0f, 1.0f);  // Низ право
			Gl.glTexCoord2f(100.0f, 100.0f); Gl.glVertex3d(-1.0f, 1.0f, 1.0f);   // Верх право
			Gl.glTexCoord2f(0.0f, 100.0f); Gl.glVertex3d(-1.0f, 1.0f, -1.0f);  // Верх лево

			
			Gl.glEnd();

		}
		private void GearsDraw() {
			Gl.glPushMatrix();
			rot += gearspeed;
			Gl.glTranslated(-3.3, 0, 0);
			Gl.glRotated(rot, 0, 0, 1);
			Gear(1f, 3f, 1, 30, 1);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			rot += gearspeed;

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
			Gl.glTranslated(3.3, 0, 0);
			Gl.glRotated(-rot, 0, 0, 1);
			Gear(1f, 3f, 1, 30, 1);
			Gl.glPopMatrix();
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
			this.simpleOpenGlControl1.Invalidate();
		}

		void Sphere(double r, int nx, int ny) {
			int i, ix, iy;
			double x, y, z;

			for (iy = 0; iy < ny; ++iy) {
				Gl.glBegin(Gl.GL_QUAD_STRIP);
				for (ix = 0; ix <= nx; ++ix) {
					x = r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
					y = r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
					z = r * Math.Cos(iy * Math.PI / ny);
					Gl.glNormal3d(x, y, z);//нормаль направлена от центра
					Gl.glTexCoord2d((double)ix / (double)nx, (double)iy / (double)ny);
					Gl.glVertex3d(x, y, z);

					x = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
					y = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
					z = r * Math.Cos((iy + 1) * Math.PI / ny);
					Gl.glNormal3d(x, y, z);
					Gl.glTexCoord2d((double)ix / (double)nx, (double)(iy + 1) / (double)ny);
					Gl.glVertex3d(x, y, z);
				}

				Gl.glEnd();
			}
		}
		public void Gear(float inner_radius, float outer_radius, float width, int teeth, float tooth_depth) {
			float r0, r1, r2;
			float angle, da;
			float u, v, len;

			r0 = inner_radius;
			r1 = outer_radius - tooth_depth / 2.0f;
			r2 = outer_radius + tooth_depth / 2.0f;
			float Pi = (float)Math.PI;
			da = 2.0f * Pi / teeth / 4.0f;
			Gl.glShadeModel(Gl.GL_FLAT);
			Gl.glNormal3f(0.0f, 0.0f, 1.0f);

			// передняя сторона
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			for (int i = 0; i < teeth +1; i++) {
				angle = i * 2.0f * Pi / teeth;
				Gl.glVertex3f(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), width * 0.5f);
				Gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), width * 0.5f);
				Gl.glVertex3f(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), width * 0.5f);
				Gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), width * 0.5f);
			}
			Gl.glEnd();

			// передняя сторона зубьев
			Gl.glBegin(Gl.GL_QUADS);
			da = 2.0f * Pi / teeth / 4.0f;
			for (int i = 0; i < teeth; i++) {

				//			for (int i = 0; i < teeth - 1; i++) {
				angle = i * 2.0f * Pi / teeth;

				Gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), width * 0.5f);
				Gl.glVertex3f(r2 * (float)Math.Cos(angle + da), r2 * (float)Math.Sin(angle + da), width * 0.5f);
				Gl.glVertex3f(r2 * (float)Math.Cos(angle + 2 * da), r2 * (float)Math.Sin(angle + 2 * da), width * 0.5f);
				Gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), width * 0.5f);
			}
			Gl.glEnd();

			Gl.glNormal3f(0.0f, 0.0f, -1.0f);

			// задняя сторона
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			for (int i = 0; i < teeth +1; i++) {
				angle = i * 2.0f * Pi / teeth;
				Gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), -width * 0.5f);
				Gl.glVertex3f(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), -width * 0.5f);
				Gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), -width * 0.5f);
				Gl.glVertex3f(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), -width * 0.5f);

			}
			Gl.glEnd();

			// обратная сторона зубьев
			Gl.glBegin(Gl.GL_QUADS);
			da = 2.0f * Pi / teeth / 4.0f;
			//			for (int i = 0; i < teeth - 1; i++) {
			for (int i = 0; i < teeth; i++) {

				angle = i * 2.0f * Pi / teeth;

				Gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), -width * 0.5f);
				Gl.glVertex3f(r2 * (float)Math.Cos(angle + 2 * da), r2 * (float)Math.Sin(angle + 2 * da), -width * 0.5f);
				Gl.glVertex3f(r2 * (float)Math.Cos(angle + da), r2 * (float)Math.Sin(angle + da), -width * 0.5f);
				Gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), -width * 0.5f);
			}
			Gl.glEnd();

			// внешняя сторона зубьев
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			//	for (int i = 0; i < teeth - 1; i++) {
			for (int i = 0; i < teeth; i++) {

				angle = i * 2.0f * Pi / teeth;

				Gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), width * 0.5f);
				Gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), -width * 0.5f);
				u = r2 * (float)Math.Cos(angle + da) - r1 * (float)Math.Cos(angle);
				v = r2 * (float)Math.Sin(angle + da) - r1 * (float)Math.Sin(angle);
				len = (float)Math.Sqrt(u * u + v * v);
				u = u / len;
				v = v / len;
				Gl.glNormal3f(v, -u, 0.0f);
				Gl.glVertex3f(r2 * (float)Math.Cos(angle + da), r2 * (float)Math.Sin(angle + da), width * 0.5f);
				Gl.glVertex3f(r2 * (float)Math.Cos(angle + da), r2 * (float)Math.Sin(angle + da), -width * 0.5f);
				Gl.glNormal3f((float)Math.Cos(angle), (float)Math.Sin(angle), 0.0f);
				Gl.glVertex3f(r2 * (float)Math.Cos(angle + 2 * da), r2 * (float)Math.Sin(angle + 2 * da), width * 0.5f);
				Gl.glVertex3f(r2 * (float)Math.Cos(angle + 2 * da), r2 * (float)Math.Sin(angle + 2 * da), -width * 0.5f);
				u = r1 * (float)Math.Cos(angle + 3 * da) - r2 * (float)Math.Cos(angle + 2 * da);
				v = r1 * (float)Math.Sin(angle + 3 * da) - r2 * (float)Math.Sin(angle + 2 * da);
				Gl.glNormal3f(v, -u, 0.0f);
				Gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), width * 0.5f);
				Gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), -width * 0.5f);
				Gl.glNormal3f((float)Math.Cos(angle), (float)Math.Sin(angle), 0.0f);
			}
			Gl.glVertex3f(r1 * (float)Math.Cos(0), r1 * (float)Math.Sin(0), width * 0.5f);
			Gl.glVertex3f(r1 * (float)Math.Cos(0), r1 * (float)Math.Sin(0), -width * 0.5f);

			Gl.glEnd();

			Gl.glShadeModel(Gl.GL_SMOOTH);

			// внутренний цилиндр
			Gl.glBegin(Gl.GL_QUAD_STRIP);
			for (int i = 0; i < teeth+1; i++) {
				angle = i * 2.0f * Pi / teeth;
				Gl.glNormal3f(-(float)Math.Cos(angle), -(float)Math.Sin(angle), 0.0f);
				Gl.glVertex3f(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), -width * 0.5f);
				Gl.glVertex3f(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), width * 0.5f);
			}
			Gl.glEnd();
		}
		private void Look() {
			gluLookAt(cameraPos, cameraPos + cameraFront, cameraUp);
			//Glu.gluLookAt(xrot, yrot, zrot, 0, 0, 0, 0, 1, 0);
			//Glu.gluLookAt(0,0,0, xrot, yrot, zrot, 0, 1, 0);
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
