using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FEVSF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            documentation.Text = File.ReadAllText("documentation.txt");
            ListEvents subWindow = new ListEvents();
            subWindow.Show();

            this.Closed += new EventHandler(MainWindow_Closed);
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void FEVS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
                CtrlSave();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".fevs";
            dlg.Filter = "FEVS Files (*.fevs)|*.fevs";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                Title = "FEVS - " + filename;
                string text = File.ReadAllText(filename);
                SourceCode.Text = text;
                CheckSave();
            }
            else
            {
                CtrlSave();
                Title = "FEVS";
                SourceCode.Text = "";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string[] filename = Title.Split(new[] { " - " }, StringSplitOptions.None);
            if (filename.Length == 2)
            {
                string content = SourceCode.Text;
                string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                using (StreamWriter sw = new StreamWriter(filename[1]))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i > 0)
                            sw.WriteLine();
                        sw.Write(lines[i]);
                    }
                }
                CheckSave();
            }
            else
            {
                MessageBox.Show("You don't have any loaded file to save.");
            }
        }

        private void Transform_Click(object sender, RoutedEventArgs e)
        {
            string[] filename = Title.Split(new[] { " - " }, StringSplitOptions.None);
            if (filename.Length != 2)
            {
                MessageBox.Show("You don't have any loaded file to save.");
            }
            else
            {
                string[] text = File.ReadAllLines(filename[1]);
                int[][] finishedCommands = new int[32][];

                if (text.Length > 32)
                    MessageBox.Show("Too many lines to encode.");
                else
                {
                    bool error = false;
                    for (int q = 0; q < finishedCommands.Length; q++)
                    {
                        finishedCommands[q] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    }
                    for (int i = 0; i < text.Length; i++)
                    {
                        string[] parsed = text[i].Split('(');
                        string cmd = parsed[0];
                        string argument = parsed[1].Remove(parsed[1].Length - 1);
                        string[] arguments = argument.Split(',');
                        for (int t = 0; t < arguments.Length; t++)
                        {
                            arguments[t] = arguments[t].Trim();
                        }

                        // Création de l'instance de la classe avec System.Reflection notre Dieu.
                        // C'est pour invoquer la commande donnée ça.
                        Type type = Type.GetType("FEVSF.Commands");
                        ConstructorInfo magicConstructor = type.GetConstructor(Type.EmptyTypes);
                        object CommandsObject = magicConstructor.Invoke(new object[] { });

                        // Là, c'est pour invoke la commande avec les arguments directement
                        MethodInfo magicMethod = type.GetMethod(cmd);

                        // Enculée de méthode
                        LinkedList<string> parametersArray = new LinkedList<string>();
                        for (int j = 0; j < arguments.Length; j++)
                        {
                            parametersArray.AddLast(arguments[j]);
                        }
                        object[] args = parametersArray.ToArray();

                        try
                        {
                            // Si les args et la commande sont bons, alors on va l'appeler et modifier le finishedCommands[i]
                            for (int a = 0; a < args.Length; a++)
                            {
                                Console.Write(args[a].ToString());
                            }
                            Console.Write("\n");
                            finishedCommands[i] = (int[])magicMethod.Invoke(CommandsObject, args);
                        }
                        catch (NullReferenceException)
                        {
                            error = true;
                            MessageBox.Show("Something went wrong : Check your commands names.\nSome bytes were set to 0 because of\nthis error to avoid crashing.");
                        }
                        catch (TargetInvocationException)
                        {
                            error = true;
                            MessageBox.Show("Something went wrong : One or multiple arguments are not valid.\nSome bytes were set to 0 because of\nthis error to avoid crashing.");
                        }
                        catch (TargetParameterCountException)
                        {
                            error = true;
                            MessageBox.Show("Something went wrong : Check the number of arguments of one or more of your commands.");
                        }
                    }
                    if (!error)
                    {
                        string[] filename1 = Title.Split(new[] { " - " }, StringSplitOptions.None);
                        string[] file = filename1[1].Split('.');
                        using (StreamWriter sw = new StreamWriter(file[0] + ".sevs"))
                        {
                            for (int i = 0; i < finishedCommands.Length; i++)
                            {
                                if (i > 0)
                                    sw.WriteLine();
                                for (int j = 0; j < finishedCommands[i].Length; j++)
                                {
                                    if (j > 0)
                                        sw.Write(", ");
                                    sw.Write(Convert.ToString(finishedCommands[i][j], 16).ToUpper());
                                }
                            }
                        }
                        MessageBox.Show("Done.");
                    }
                }
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog1.Filter = "FEVS Files|*.fevs";
            saveFileDialog1.Title = "Create a new .fevs file";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                fs.Write(new byte[] { }, 0, 0);
                fs.Close();

                // Load le fichier après avoir été créé (équivalent de Open_Click sans le dialogue)
                string filename = saveFileDialog1.FileName;
                Title = "FEVS - " + filename;
                string text = File.ReadAllText(filename);
                SourceCode.Text = text;
                SourceCode.IsReadOnly = false;
            }
        }

        private void CtrlSave()
        {
            string[] filename = Title.Split(new[] { " - " }, StringSplitOptions.None);
            if (filename.Length == 2)
            {
                string content = SourceCode.Text;
                string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                using (StreamWriter sw = new StreamWriter(filename[1]))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i > 0)
                            sw.WriteLine();
                        sw.Write(lines[i]);
                    }
                }
                CheckSave();
            }
            else
            {
                MessageBox.Show("You don't have any loaded file to save.");
            }
        }

        // Appelé par le KeyUp de SourceCode
        private void CheckSave(object sender, KeyEventArgs e)
        {
            string[] filename = Title.Split(new[] { " - " }, StringSplitOptions.None);
            if (filename.Length != 2)
                return;
            string text = File.ReadAllText(filename[1]);
            if (text != SourceCode.Text)
            {
                Title = "*FEVS - " + filename[1];
            }
            else
            {
                Title = "FEVS - " + filename[1];
            }
        }

        // Surchage appelée par Save
        private void CheckSave()
        {
            string[] filename = Title.Split(new[] { " - " }, StringSplitOptions.None);
            if (filename.Length != 2)
                return;
            string text = File.ReadAllText(filename[1]);
            if (text != SourceCode.Text)
            {
                Title = "*FEVS - " + filename[1];
            }
            else
            {
                Title = "FEVS - " + filename[1];
            }
        }
    }
}
