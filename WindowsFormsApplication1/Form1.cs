using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;


namespace OpenGL_cube
{
    public partial class Form1 : Form
    {
     double xrot, yrot, zrot;
     int width;
     int height;


        public Form1()
        { 
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

        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glTranslated(0, 0, -5);
            Gl.glRotated(xrot += 0.5, 1, 0, 0);
            Gl.glRotated(yrot += 0.3, 0, 1, 0);
            Gl.glRotated(zrot += 0.2, 0, 0, 1);

            Gl.glPointSize(3);
            Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_LINES);
            Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_LINES);
            Gl.glBegin(Gl.GL_QUADS);

            ////Vista posterior
            Gl.glColor3ub(255, 0, 255);
            Gl.glVertex3d(1, 1, -1);
            Gl.glVertex3d(1, -1, -1);
            Gl.glVertex3d(-1, -1, -1);
            Gl.glVertex3d(-1, 1, -1);

            ////DEBAJO
            Gl.glColor3ub(0, 255, 255);
            Gl.glVertex3d(-1, -1, -1);
            Gl.glVertex3d(1, -1, -1);
            Gl.glVertex3d(1, -1, 1);
            Gl.glVertex3d(-1, -1, 1);

            ////POR LA IZQUIERDA
            Gl.glColor3ub(255, 255, 0);
            Gl.glVertex3d(-1, 1, -1);
            Gl.glVertex3d(-1, -1, -1);
            Gl.glVertex3d(-1, -1, 1);
            Gl.glVertex3d(-1, 1, 1);

            ////POR LA DERECHA
            Gl.glColor3ub(0, 0, 255);
            Gl.glVertex3d(1, 1, 1);
            Gl.glVertex3d(1, -1, 1);
            Gl.glVertex3d(1, -1, -1);
            Gl.glVertex3d(1, 1, -1);

            ///ENCIMA
            Gl.glColor3ub(0, 255, 0);
            Gl.glVertex3d(-1, 1, -1);
            Gl.glVertex3d(-1, 1, 1);
            Gl.glVertex3d(1, 1, 1);
            Gl.glVertex3d(1, 1, -1);

            ////PARTE ANTERIOR
            Gl.glColor3ub(255, 0, 0);
            Gl.glVertex3d(-1, 1, 1);
            Gl.glVertex3d(-1, -1, 1);
            Gl.glVertex3d(1, -1, 1);
            Gl.glVertex3d(1, 1, 1);

            Gl.glEnd();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.simpleOpenGlControl1.Invalidate();
        }
    }
}
