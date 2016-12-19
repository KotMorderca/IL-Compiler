using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Xml;

using ICSharpCode.AvalonEdit.Highlighting;

namespace ILCompiler
{
    public partial class MainWindow : Window
    {
        string currentFilePath = null;

        public MainWindow()
        {
            IHighlightingDefinition ILHighlighting;
            using (Stream s = typeof(MainWindow).Assembly.GetManifestResourceStream("ILCompiler.ILHighlighting.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Błędne źródło kolorowania");
                using (XmlReader reader = new XmlTextReader(s))
                {
                    ILHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
                        HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            HighlightingManager.Instance.RegisterHighlighting("ILHighlighting", new string[] { ".cool" }, ILHighlighting);
            
            InitializeComponent();

            textEditor.ShowLineNumbers = true;
            textEditor.SyntaxHighlighting = ILHighlighting;

            txt_boxConsole.FontFamily = new System.Windows.Media.FontFamily("Courier New");

            Title = "ILCompiler";
        }



        private void MenuFileNew_Click(object sender, RoutedEventArgs e)
        {
            currentFilePath = null;
            Title = "ILCompiler";
            textEditor.Text = "";
        }
        
        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Kod IL (*.il)|*.il|Wszystkie pliki (*.*)|*.*";
            ofd.FilterIndex = 0;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream file;
                if ((file = ofd.OpenFile()) != null)
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        FileStream fs = reader.BaseStream as FileStream;
                        currentFilePath = fs.Name;
                        Title = "ILCompiler - " + currentFilePath;
                        textEditor.Text = reader.ReadToEnd();
                    }
                    file.Close();
                }
            }
        }

        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            tryToSaveFile();
        }

        private void MenuFileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            saveFile();
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuEditUndo_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Undo();
        }

        private void MenuEditRedo_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Redo();
        }

        private void Compile_Click(object sender, RoutedEventArgs e)
        {
            tryToSaveFile();
            if(currentFilePath !=null)
                compileProject();
        }

        private void tryToSaveFile()
        {
            if (currentFilePath == null)
            {
                saveFile();
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(currentFilePath))
                {
                    FileStream fs = writer.BaseStream as FileStream;
                    currentFilePath = fs.Name;
                    Title = "ILCompiler - " + currentFilePath;
                    writer.Write(textEditor.Text);
                }
            }
        }

        private void saveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Kod IL (*.il)|*.il|Wszystkie pliki (*.*)|*.*";
            sfd.FilterIndex = 0;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream file;
                if ((file = sfd.OpenFile()) != null)
                {
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        FileStream fs = writer.BaseStream as FileStream;
                        currentFilePath = fs.Name;
                        Title = "ILCompiler - " + currentFilePath;
                        writer.Write(textEditor.Text);
                    }
                    file.Close();
                }
            }
        }

        private void compileProject()
        {
            Compiler compiler = new Compiler(currentFilePath);
            compiler.txtOutput += logText;
            compiler.beginCompilation();
        }

        private void logText(string text)
        {
            txt_boxConsole.AppendText(text + "\n");
            txt_boxConsole.ScrollToEnd();
        }
    }
}
