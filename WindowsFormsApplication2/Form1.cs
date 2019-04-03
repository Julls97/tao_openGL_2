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
		float xrot, yrot, zrot;
		int width;
		int height;
		float speed = 5f;
		private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == 'A' || e.KeyChar == 'a') { zrot += -speed; }
			if (e.KeyChar == 'D' || e.KeyChar == 'd') { zrot += speed; }
			if (e.KeyChar == 'S' || e.KeyChar == 's') { xrot += speed; }
			if (e.KeyChar == 'W' || e.KeyChar == 'w') { xrot += -speed; }
			if (e.KeyChar == 'E' || e.KeyChar == 'e') { yrot += -speed; }
			if (e.KeyChar == 'Q' || e.KeyChar == 'q') { yrot += speed; }
		}

		public Form1() {
			InitializeComponent();
			simpleOpenGlControl1.InitializeContexts();
			width = simpleOpenGlControl1.Width;
			height = simpleOpenGlControl1.Height;
			Gl.glViewport(0, 0, width, height);
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();
			Glu.gluPerspective(45.0f, (double)width / (double)height, 0.01f, 5000.0f);
			Gl.glEnable(Gl.GL_CULL_FACE);
			Gl.glCullFace(Gl.GL_BACK);

			Gl.glEnable(Gl.GL_TEXTURE_2D);
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_LIGHT0);

			float[] ambientColorLight = { 1.0f, 1.0f, 1.0f, 1.0f };
			float[] diffuseColorLight = { 1.0f, 1.0f, 1.0f, 1.0f };
			float[] positionLight0 = { 1.0f, 1.0f, 1.0f, 0.0f };
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, ambientColorLight);
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, diffuseColorLight);
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, positionLight0);

			float[] diffuseColorMaterial = { 1.0f, 1.0f, 1.0f, 1.0f };
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, diffuseColorMaterial);

			//Gl.glGenTextures(2, textures);
			creatTexture(@"E:\git projects\tao_openGL_2\3.bmp", 0);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);

		
		
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
		private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e) {
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
			//Gl.glBindTexture(Gl.GL_TEXTURE_2D,1);
			

			//Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, 1);
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			Gl.glTranslated(0, 0, -5);

			Gl.glPointSize(3);
			Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_LINES);
			Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_LINES);

			//Sphere(10, 100, 100);
			//return;
			Gl.glRotated(xrot, 1, 0, 0);
			Gl.glRotated(yrot, 0, 1, 0);
			Gl.glRotated(zrot, 0, 0, 1);
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
			Gl.glTexCoord2f(10.0f, 0.0f); Gl.glVertex3d(-1.0f, -1.0f, 1.0f);  // Низ право
			Gl.glTexCoord2f(10.0f, 10.0f); Gl.glVertex3d(-1.0f, 1.0f, 1.0f);   // Верх право
			Gl.glTexCoord2f(0.0f, 10.0f); Gl.glVertex3d(-1.0f, 1.0f, -1.0f);  // Верх лево

			Gl.glEnd();

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
	
	}

}
