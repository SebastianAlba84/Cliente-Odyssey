﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Odyssey : Form
    {
        string name = "";
        static int tipo = 0;
        public static int GetTipo()
        {
            return tipo;
        }

        public Odyssey(string ob)
        {
            this.name = ob;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Odyssey_Load(object sender, EventArgs e)
        {
            string ml =Sockets.Conectar(20, name, "1", "", "", "", "");
            int z = 0;
            for (int x=0; x<ml.Length;x++)
            {
                if (ml.Substring(x, 1).Equals("/"))
                {
                    Biblioteca.Items.Add(ml.Substring(z,x-z));
                    z = x+1;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Amigo amigo = new Amigo(name);
            amigo.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lista.Hide();
            mensajes.Hide();
            general.Show();
            tipo = 1;
            Buscar buscar = new Buscar();
            buscar.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            lista.Hide();
            mensajes.Hide();
            general.Show();
            tipo = 2;
            Buscar buscar = new Buscar();
            buscar.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            lista.Hide();
            mensajes.Hide();
            general.Show();
            tipo = 3;
            Buscar buscar = new Buscar();
            buscar.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            lista.Hide();
            mensajes.Hide();
            general.Show();
            tipo = 4;
            Buscar buscar = new Buscar();
            buscar.Show();
        }
        //CHUNK,El "HEADER " de cada bloque es sólo el valor de longitud de 32 bits, no necesita proporcionar su propio buffering.
        //Simplemente se usa así:
        //foreach (var block in ReadChunks("MyFileName"))
        //    {
        // Process block.
        //     }
        //
        /**
        Si desea leer un archivo de este tipo, la forma más fácil es, probablemente, encapsular la lectura en un método que devuelve IEnumerable así<byte[]> :</byte[]>
        public static IEnumerable<byte[]> ReadChunks(string path)
        {
            var lengthBytes = new byte[sizeof(int)];

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int n = fs.Read(lengthBytes, 0, sizeof(int));  // Read block size.

                if (n == 0)      // End of file.
                    yield break;

                if (n != sizeof(int))
                    throw new InvalidOperationException("Invalid header");

                int blockLength = BitConverter.ToInt32(lengthBytes, 0);
                var buffer = new byte[blockLength];
                n = fs.Read(buffer, 0, blockLength);

                if (n != blockLength)
                    throw new InvalidOperationException("Missing data");

                yield return buffer;
            }
        }**/
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.balance = trackBar3.Value;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) // Ver como validar error
        {
            axWindowsMediaPlayer1.URL = "C:/Users/mende_000/Downloads/Nueva carpeta/"+Biblioteca.SelectedItem+".mp3";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            int t = 0;
            foreach (string track in openFileDialog1.FileNames){
                string validacion = Sockets.Conectar(13, name, track, "", "", "", "");
                if (validacion == "true")
                {
                    t++;
                    Biblioteca.Items.Add((Path.GetFileName(track)).Substring(0, (Path.GetFileName(track)).Length-4));
                }
                else if (validacion == "ya")
                {
                    MessageBox.Show("Ya tienes esta cancion en la biblioteca");
                }
                else
                {
                    MessageBox.Show("El archivo elegido no es mp3");
                }
            }
            MessageBox.Show("Se han cargado "+ t + " canciones");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lista.Hide();
            mensajes.Hide();
            Biblioteca.Items.Clear();
            string ml = Sockets.Conectar(20, name, "1", "", "", "", "");
            int z = 0;
            for (int x = 0; x < ml.Length; x++)
            {
                if (ml.Substring(x, 1).Equals("/"))
                {
                    Biblioteca.Items.Add(ml.Substring(z, x - z));
                    z = x + 1;
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //try
            //{
            //    trackBar1.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration;
            //    trackBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            //}
            //catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lista.Items.Clear();
            mensajes.Hide();
            string nombre = Sockets.Conectar(19, name, "", "", "", "", "");
            if (nombre.Length != 0)
            {
                string[] gen = new string[10];
                int z = 0;
                int j = 0;
                int n = 1;
                for (int c = 0; nombre.Length > c; c++)
                {
                    if (nombre.Substring(c, 1) == "/")
                    {
                        gen[z] = nombre.Substring(j, n - 1);
                        z++;
                        j = c + 1;
                        n = 0;
                    }
                    n++;
                }
                foreach (string usuario in gen)
                {
                    if (usuario != null)
                    {
                        lista.Items.Add(usuario);
                    }
                }
            }
            else
            {
                lista.Items.Add("No tienes amigos");
            }

            lista.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lista.Hide();
            mensajes.Items.Clear();
            string nombre = Sockets.Conectar(18, name, "", "", "", "", "");
            int x = 1;
            int z = 0;
            int k = 0;
            int j = 0;
            int t = 0;
            int y = 1;
            if (nombre.Length != 0)
            {
                for (int i = 0; nombre.Length > i; i++)
                {
                    if (nombre.Substring(i, 1).Equals("/"))
                    {
                        if (x == 1)
                        {
                            x = 0;
                            t = y-1;
                            j = 0 - 1;
                            z = i + 1;
                        }
                        else
                        {
                            mensajes.Items.Add(nombre.Substring(k, t) +" - "+ nombre.Substring(z, j));
                            k = i + 1;
                            y = 0;
                            x++;
                        }
                    }
                    y++;
                    j++;
                }
            }
            else
            {
                mensajes.Items.Add("No tienes nuevos mensajes");
            }
            mensajes.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lista.Hide();
            mensajes.Hide();
            Biblioteca.Items.Clear();
            string ml = Sockets.Conectar(20, name, "2", "", "", "", "");
            Console.WriteLine(ml);
            int z = 0;
            for (int x = 0; x < ml.Length; x++)
            {
                if (ml.Substring(x, 1).Equals("/"))
                {
                    Biblioteca.Items.Add(" - "+ml.Substring(z, x - z));
                    z = x + 1;
                }
                if (ml.Substring(x, 1).Equals(","))
                {
                    Biblioteca.Items.Add(ml.Substring(z, x - z));
                    z = x + 1;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lista.Hide();
            mensajes.Hide();
            Biblioteca.Items.Clear();
            string ml = Sockets.Conectar(20, name, "3", "", "", "", "");
            Console.WriteLine(ml);
            int z = 0;
            for (int x = 0; x < ml.Length; x++)
            {
                if (ml.Substring(x, 1).Equals("/"))
                {
                    Biblioteca.Items.Add(" - " + ml.Substring(z, x - z));
                    z = x + 1;
                }
                if (ml.Substring(x, 1).Equals(","))
                {
                    Biblioteca.Items.Add(ml.Substring(z, x - z));
                    z = x + 1;
                }
            }
        }

        private void general_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void enviar_msj(object sender, EventArgs e)
        {
            Mensaje mensaje = new Mensaje(name, (string)lista.SelectedItem);
            mensaje.Show();
        }

        private void leer_msj(object sender, EventArgs e)
        {
            Recomendacion recomendacion = new Recomendacion(name, (string)mensajes.SelectedItem);
            recomendacion.Show();
        }

        public Login Login
        {
            get => default(Login);
            set
            {
            }
        }

        internal Sockets Sockets
        {
            get => default(Sockets);
            set
            {
            }
        }

        public Login Login1
        {
            get => default(Login);
            set
            {
            }
        }

        internal Sockets Sockets1
        {
            get => default(Sockets);
            set
            {
            }
        }

        public Amigo Amigo
        {
            get => default(Amigo);
            set
            {
            }
        }

        public Buscar Buscar
        {
            get => default(Buscar);
            set
            {
            }
        }

        public Mensaje Mensaje
        {
            get => default(Mensaje);
            set
            {
            }
        }

        public Recomendacion Recomendacion
        {
            get => default(Recomendacion);
            set
            {
            }
        }

        private void pausa(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Space)) {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            if (e.KeyChar == Convert.ToChar(Keys.D7))
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void volumen(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.J:
                    if (trackBar2.Value != 0)
                    {
                        trackBar2.Value -= 5;
                        axWindowsMediaPlayer1.settings.volume = trackBar2.Value;
                    }
                    break;
                case Keys.K:
                    if (trackBar2.Value != 100)
                    {
                        trackBar2.Value += 5;
                        axWindowsMediaPlayer1.settings.volume = trackBar2.Value;
                    }
                    break;

            }
        }
    }
}