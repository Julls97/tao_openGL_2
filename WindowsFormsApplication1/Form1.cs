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
		double speed = 3;
		uint[] textures = new uint[6]; 
		private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == 'A' || e.KeyChar == 'a') { zrot += -speed; }
			if (e.KeyChar == 'D' || e.KeyChar == 'd') { zrot += speed; }
			if (e.KeyChar == 'S' || e.KeyChar == 's') { xrot += speed; }
			if (e.KeyChar == 'W' || e.KeyChar == 'w') { xrot += -speed; }
			if (e.KeyChar == 'E' || e.KeyChar == 'e') { yrot += -speed; }
			if (e.KeyChar == 'Q' || e.KeyChar == 'q') { yrot += speed; }
		}

		
		//private void InitialiseTexture(ref OpenGL gl) {

		//	gImage1 = new Bitmap(new Bitmap(@"C:\Users\Арсений\Desktop\RunAnimation\Runolololo.bmp"), 512, 1024); // Ширина и высота текстуры должна быть кратной 2^n (зависит от видеокарты, на некоторых может работать и любой размер, но в стандарте OpenGL описано так), иначе получим белую область вместо текстуры
		//	System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, gImage1.Width, gImage1.Height);
		//	gbitmapdata = gImage1.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb); // формат должен соответствовать здесь и в TexImage2D
		//	gl.GenTextures(1, gtexture);
		//	gl.BindTexture(OpenGL.GL_TEXTURE_2D, gtexture[0]);
		//	gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, (int)OpenGL.GL_RGBA, gImage1.Width, gImage1.Height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, gbitmapdata.Scan0); // исправлен формат: internalformat = OpenGL.GL_RGBA, format = OpenGL.GL_BGRA
		//	gImage1.UnlockBits(gbitmapdata); // UnlockBits нужно делать после чтения данных изображения, иначе есть вероятность, что картинка может быть перемещена в другую область памяти (для оптимизации) и указатель gbitmapdata.Scan0 будет указывать на неправильный участок памяти

		//	gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
		//	gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);

		//	TexturesInitialised = true;

		//}
		//private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args) {
		//	OpenGL gl = openGlControl.OpenGL;

		//	//  Set the clear color.
		//	gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);

		//	InitialiseTexture(ref gl); // лучше загрузить текстуру здесь, чтобы не проверять каждый раз при отрисовке кадра
		//}


		//private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args) {
		//OpenGL gl = openGlControl.OpenGL;

		////  Clear the color and depth buffer.
		//gl.Clear(Gl.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

		//  Load the identity matrix.
		// gl.LoadIdentity(); // это лишнее, так как трансформаций  GL_MODELVIEW нигде нет в коде

		// перенесено в OpenGLControl_OpenGLInitialized
		//    if (!TexturesInitialised)
		//    {
		//        InitialiseTexture(ref gl);
		//    }

		//gl.Enable(OpenGL.GL_TEXTURE_2D);
		//gl.BindTexture(OpenGL.GL_TEXTURE_2D, gtexture[0]);
		//gl.Color(1.0f, 1.0f, 1.0f, 0.1f);
		//gl.Begin(OpenGL.GL_QUADS);

		//gl.TexCoord(1.0f, 1.0f);
		//gl.Vertex(gImage1.Width, gImage1.Height, 1.0f);

		//gl.TexCoord(0.0f, 1.0f);
		//gl.Vertex(0.0f, gImage1.Height, 1.0f);

		//gl.TexCoord(0.0f, 0.0f);
		//gl.Vertex(0.0f, 0.0f, 1.0f);

		//gl.TexCoord(1.0f, 0.0f);
		//gl.Vertex(gImage1.Width, 0.0f, 1.0f);

		//gl.End();
		//gl.Disable(OpenGL.GL_TEXTURE_2D);

		// противоречит настройке матрицы OpenGLControl_Resized в OpenGLControl_Resized
		//gl.MatrixMode(OpenGL.GL_PROJECTION);
		//gl.LoadIdentity();
		//gl.Ortho(0.0, (double)openGlControl.Width, (double)openGlControl.Height, 0.0, -1.0, 1.0);
		//gl.MatrixMode(OpenGL.GL_MODELVIEW);

		// не нужно это делать при отрисовке каждого кадра
		//gl.Disable(OpenGL.GL_DEPTH_TEST);
		//	}


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

			Gl.glGenTextures(2, textures);
			creatTexture(@"E:\git projects\tao_openGL_2\3.bmp", 1);
		
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[0]);

			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
			creatTexture(@"E:\git projects\tao_openGL_2\4.bmp", 0);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[1]);
			//Gl.glEnable(Gl.GL_TEXTURE_GEN_S); //enable texture coordinate generation
			//Gl.glEnable(Gl.GL_TEXTURE_GEN_T);



			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[2]);

			//LoadTexture(new Bitmap("1.bmp"), 0, true);
			//quadr = Glu.gluNewQuadric();
			//Glu.gluQuadricTexture(quadr, Gl.GL_TRUE);
			//Gl.glEnable(Gl.GL_TEXTURE_2D);			//Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
			//Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
			//Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
			//Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
			//Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
			//Glu.gluSphere(quadr, 0.4, 50, 50);
			//Gl.glDisable(Gl.GL_TEXTURE_2D);
		}
		private void creatTexture(String path, int level) {
			var bmp = new Bitmap(path);
			var bmpData = bmp.LockBits(
				new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format24bppRgb);
			Gl.glGenTextures(2, textures);
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, textures[level]);

			Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGB8,
			bmp.Width, bmp.Height, 0, Gl.GL_BGR_EXT,
			Gl.GL_UNSIGNED_BYTE, bmpData.Scan0);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);  // Linear Filtering 
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);  // Linear Filtering 

			bmp.UnlockBits(bmpData);
		}
		private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e) {
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			Gl.glTranslated(0, 0, -5);
			Gl.glRotated(xrot, 1, 0, 0);
			Gl.glRotated(yrot, 0, 1, 0);
			Gl.glRotated(zrot, 0, 0, 1);

			Gl.glPointSize(3);
			Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_LINES);
			Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_LINES);
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

			//Gl.glEnd();

			//Gl.glBindTexture(Gl.GL_TEXTURE_2D, 1);

			//Gl.glBegin(Gl.GL_QUADS);
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

			// Левая грань
			/*Gl.glTexCoord2f(0.0f, 0.0f);*/ Gl.glVertex3d(-1.0f, -1.0f, -1.0f);    // Низ лево
			/*Gl.glTexCoord2f(1.0f, 0.0f);*/ Gl.glVertex3d(-1.0f, -1.0f, 1.0f); // Низ право
			/*Gl.glTexCoord2f(1.0f, 1.0f);*/ Gl.glVertex3d(-1.0f, 1.0f, 1.0f);  // Верх право
			/*Gl.glTexCoord2f(0.0f, 1.0f);*/ Gl.glVertex3d(-1.0f, 1.0f, -1.0f); // Верх лево

																			//////Вид сзади
																			//Gl.glColor3ub(255, 0, 255);
																			//Gl.glVertex3d(1, 1, -1);
																			//Gl.glVertex3d(1, -1, -1);
																			//Gl.glVertex3d(-1, -1, -1);
																			//Gl.glVertex3d(-1, 1, -1);

			//////вид снизу
			//Gl.glColor3ub(0, 255, 255);
			//Gl.glVertex3d(-1, -1, -1);
			//Gl.glVertex3d(1, -1, -1);
			//Gl.glVertex3d(1, -1, 1);
			//Gl.glVertex3d(-1, -1, 1);

			//////вид слева
			//Gl.glColor3ub(255, 255, 0);
			//Gl.glVertex3d(-1, 1, -1);
			//Gl.glVertex3d(-1, -1, -1);
			//Gl.glVertex3d(-1, -1, 1);
			//Gl.glVertex3d(-1, 1, 1);

			//////вид справа
			//Gl.glColor3ub(0, 0, 255);
			//Gl.glVertex3d(1, 1, 1);
			//Gl.glVertex3d(1, -1, 1);
			//Gl.glVertex3d(1, -1, -1);
			//Gl.glVertex3d(1, 1, -1);

			/////вид сверху
			//Gl.glColor3ub(0, 255, 0);
			//Gl.glVertex3d(-1, 1, -1);
			//Gl.glVertex3d(-1, 1, 1);
			//Gl.glVertex3d(1, 1, 1);
			//Gl.glVertex3d(1, 1, -1);

			//////вид спереди
			////Gl.glColor3ub(255, 0, 0);
			//Gl.glColor3ub(255, 255, 255);
			//Gl.glVertex3d(-1, 1, 1);
			//Gl.glVertex3d(-1, -1, 1);
			//Gl.glVertex3d(1, -1, 1);
			//Gl.glVertex3d(1, 1, 1);

			Gl.glEnd();

		}

		private void timer1_Tick(object sender, EventArgs e) {
			this.simpleOpenGlControl1.Invalidate();
		}
	}
}
