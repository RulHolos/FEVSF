using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FEVSF
{
    public partial class ListEvents : Window
    {
        public ListEvents()
        {
            InitializeComponent();
            workingdirpath.Text = Properties.Settings.Default.WorkingDirPath;

            this.Closed += new EventHandler(ListEvents_Closed);
        }

        void ListEvents_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void workingdirpath_KeyUp(object sender, KeyEventArgs e)
        {
            Properties.Settings.Default.WorkingDirPath = workingdirpath.Text;
            Properties.Settings.Default.Save();
            workingdirpath.Text = Properties.Settings.Default.WorkingDirPath;
        }

        /// <summary>
        /// Lis tout les fichiers dans le working directiory via workingdirpath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string WorkingDirPath = Properties.Settings.Default.WorkingDirPath + "\\gn_dat5.arc\\script\\event";
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(WorkingDirPath));

                events_view.Items.Clear();

                // Permet de remplir le TreeView (events_view)
                foreach (var dir in dirs)
                {
                    string directory = dir.Substring(dir.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                    TreeViewItem treeNode = new TreeViewItem();
                    treeNode.Header = directory;
                    treeNode.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                    events_view.Items.Add(treeNode);

                    string direc = Properties.Settings.Default.WorkingDirPath + "\\gn_dat5.arc\\script\\event\\" + directory;
                    DirectoryInfo di = new DirectoryInfo(direc);
                    FileInfo[] files = di.GetFiles("*.evs");

                    // Permet de remplir le TreeView avec les childs des nodes (events_view)
                    foreach (var file in files)
                    {
                        TreeViewItem childNode = new TreeViewItem();
                        childNode.Header = file.Name;
                        childNode.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                        treeNode.Items.Add(childNode);
                    }
                }
                Properties.Settings.Default.WorkMode = "global";
                Properties.Settings.Default.Save();
            }
            catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message); }
            catch (PathTooLongException ex) { MessageBox.Show(ex.Message); }
            catch (DirectoryNotFoundException) { MessageBox.Show("No working directory in the provided path, aborting."); }
        }

        private void itemDoubleClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem selectedTVI = (TreeViewItem)events_view.SelectedItem;
                string selected = selectedTVI.Header.ToString();
                if (selected.Length > 3)
                {
                    TreeViewItem parentI = (TreeViewItem)selectedTVI.Parent;
                    string parent = parentI.Header.ToString();
                    string FilePath = Properties.Settings.Default.WorkingDirPath
                        + "\\gn_dat5.arc\\script\\event\\"
                        + parent
                        + "\\"
                        + selected;

                    // Get Window Title
                    var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                    string title = mainWin.Title;
                    string[] filename = title.Split(new[] { " - " }, StringSplitOptions.None);
                    bool confirmation = true;
                    if (filename.Length == 2)
                        confirmation = UnloadFile(true);
                    if (confirmation)
                    {
                        if (Properties.Settings.Default.WorkMode == "global")
                        {

                        }
                        else
                        {
                            string[] newselected = selected.Split('.');
                            string newpath = Properties.Settings.Default.LocalPath
                                            + "\\"
                                            + parent
                                            + "\\"
                                            + newselected[0] + ".fevs";
                            mainWin.Title = "FEVS - " + newpath;
                            string text = File.ReadAllText(newpath);
                            mainWin.SourceCode.Text = text;
                            mainWin.SourceCode.IsReadOnly = false;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong, aborting.");
            }
        }

        /// <summary>
        /// Unload un fichier de manière safe ou non.
        /// </summary>
        /// <param name="safe">true of false</param>
        /// <returns>true ou false, Si False = L'utilisateur n'a pas voulu changer de fichier</returns>
        private bool UnloadFile(bool safe)
        {
            if (safe)
            {
                if (MessageBox.Show("Do you want to close your open file and load another one?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    UnloadFile(false);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).SourceCode.Text = "";
                        (window as MainWindow).Title = "FEVS";
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Gère les fichiers (transforme les bytes en listes de décimales et les transforme en fichier sevs.)
        /// </summary>
        /// <param name="FilePath"></param>
        private void HandleEVSFile(string FilePath, string localPath)
        {
            byte[] bytes = File.ReadAllBytes(FilePath);
            // Ici, il faut transformer les valeurs en hexadécimal
            string[] Sbytes = new string[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                Sbytes[i] = bytes[i].ToString("X");
            }

            string[][] lists = new string[32][];
            string[] buffer;
            int l = 0;
            for (int i = 0; i < Sbytes.Length; i += 20)
            {
                buffer = new string[20];
                Array.Copy(Sbytes, i, buffer, 0, 20);
                lists[l] = buffer;
                l++;
            }
            
            // Swap de valeurs 2 par 2 dans chaque index de lists
            for (int i = 0; i < lists.Length; i++)
            {
                for (int j = 0; j < lists[i].Length - 1; j += 2)
                {
                    string Temp = lists[i][j];
                    lists[i][j] = lists[i][j + 1];
                    lists[i][j + 1] = Temp;
                }
            }

            // Gérer l'addition des valeurs hexa pour transformer le code en code final lisible par TPDP
            string[][] finishedBytes = new string[32][];
            for (int i = 0; i < lists.Length; i++)
            {
                int a = 0;
                finishedBytes[i] = new string[10];
                for (int j = 0; j < lists[i].Length - 1; j += 2)
                {
                    finishedBytes[i][a] = lists[i][j] + lists[i][j + 1];
                    finishedBytes[i][a] = finishedBytes[i][a].TrimStart('0');
                    finishedBytes[i][a] = finishedBytes[i][a].Length > 0 ? finishedBytes[i][a] : "0";
                    a++;
                }
            }
            Utils.transformEVStoFEVS(localPath, finishedBytes);
            /*for (int j = 0; j < finishedBytes.Length; j++)
            {
                MessageBox.Show(String.Join(", ", finishedBytes[j]));
            }*/
        }

        private void Transform_Click(object sender, RoutedEventArgs e)
        {
            if (!events_view.HasItems)
                MessageBox.Show("No events to transform");
            else {
                if (MessageBox.Show($"Do you want to use the {Properties.Settings.Default.LocalPath} file path to store the data?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    System.Windows.Forms.FolderBrowserDialog browse = new System.Windows.Forms.FolderBrowserDialog();
                    browse.Description = "Choose where to store all .fevs files";
                    browse.ShowDialog();

                    if (browse.SelectedPath == "")
                    {
                        MessageBox.Show("No folden choosen, aborting.");
                        return;
                    }
                    else
                    {
                        Properties.Settings.Default.LocalPath = browse.SelectedPath;
                        Properties.Settings.Default.Save();
                    }
                }
                foreach (TreeViewItem oSubNode in events_view.Items)
                {
                    string localPathDir = Properties.Settings.Default.LocalPath
                                        + "\\"
                                        + oSubNode.Header.ToString();
                    if (!Directory.Exists(localPathDir))
                    {
                        Directory.CreateDirectory(localPathDir);
                    }
                    foreach (TreeViewItem childNode in oSubNode.Items)
                    {
                        string fileName = Properties.Settings.Default.WorkingDirPath
                                        + "\\gn_dat5.arc\\script\\event\\"
                                        + oSubNode.Header.ToString()
                                        + "\\"
                                        + childNode.Header.ToString();
                        string[] childSplit = childNode.Header.ToString().Split('.');
                        string localPath = Properties.Settings.Default.LocalPath
                                        + "\\"
                                        + oSubNode.Header.ToString()
                                        + "\\"
                                        + childSplit[0] + ".fevs";
                        HandleEVSFile(fileName, localPath);
                    }
                }
                Properties.Settings.Default.WorkMode = "local";
                Properties.Settings.Default.Save();
                foreach (TreeViewItem oSubNode in events_view.Items)
                {
                    foreach (TreeViewItem childNode in oSubNode.Items)
                    {
                        string[] head = childNode.Header.ToString().Split('.');
                        childNode.Header = head[0] + ".fevs";
                    }
                }
                MessageBox.Show("Done");
            }
        }

        private void LoadFEVS_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browse = new System.Windows.Forms.FolderBrowserDialog();
            browse.Description = "Choose where to load .fevs files from";
            browse.ShowDialog();

            if (browse.SelectedPath == "")
                MessageBox.Show("No folden choosen, aborting.");
            else
            {
                Properties.Settings.Default.LocalPath = browse.SelectedPath;
                Properties.Settings.Default.Save();

                events_view.Items.Clear();

                List<string> dirs = new List<string>(Directory.EnumerateDirectories(Properties.Settings.Default.LocalPath));

                // Permet de remplir le TreeView (events_view)
                foreach (var dir in dirs)
                {
                    string directory = dir.Substring(dir.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                    TreeViewItem treeNode = new TreeViewItem();
                    treeNode.Header = directory;
                    treeNode.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                    events_view.Items.Add(treeNode);

                    string direc = Properties.Settings.Default.LocalPath + "\\" + directory;
                    DirectoryInfo di = new DirectoryInfo(direc);
                    FileInfo[] files = di.GetFiles("*.fevs");

                    // Permet de remplir le TreeView avec les childs des nodes (events_view)
                    foreach (var file in files)
                    {
                        TreeViewItem childNode = new TreeViewItem();
                        childNode.Header = file.Name;
                        childNode.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                        treeNode.Items.Add(childNode);
                    }
                }
                Properties.Settings.Default.WorkMode = "local";
                Properties.Settings.Default.Save();
            }
        }

        private void ChangeLocalPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browse = new System.Windows.Forms.FolderBrowserDialog();
            browse.Description = "Choose where to store all .fevs files";
            browse.ShowDialog();

            if (browse.SelectedPath == "")
                MessageBox.Show("No folden choosen, aborting.");
            else
            {
                Properties.Settings.Default.LocalPath = browse.SelectedPath;
                Properties.Settings.Default.Save();
                MessageBox.Show($"Directory path saved. at : {Properties.Settings.Default.LocalPath}");
            }
        }
    }
}