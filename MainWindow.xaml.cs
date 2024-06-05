using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnConvert_Click(object sender, RoutedEventArgs e)
    {
        string SVGInput = txtInput.Text.Trim();
        string BootstrapClassValue = GetClassAttributeValue(SVGInput).Split(' ')[1] + "-nav-menu";
        string NewSvgData = SVGInput.Replace(Environment.NewLine, "")
            .Replace('"', '\'')
            .Replace("<", "%3C")
            .Replace(">", "%3E")
            .Replace("currentColor", "white");
        string CSSOutput = "." + BootstrapClassValue + " {" + Environment.NewLine + "\t" +
            @"background-image: url(""data:image/svg+xml," +
            NewSvgData + @""");" + Environment.NewLine + "}";
        txtOutputCSS.Text = CSSOutput;
        string NavMenuOutput = @"<div class=""nav-item px-3"">" + Environment.NewLine + "\t" +
            @"<NavLink class=""nav-link"" href=""pagename"">" + Environment.NewLine + "\t" + "\t" +
            @"<span class=""bi " + BootstrapClassValue + @""" aria-hidden=""true""></span> Your Page Name" + Environment.NewLine + "\t" +
          "</NavLink>" + Environment.NewLine + "</div>";
        txtOutputNavMenuTemplate.Text = NavMenuOutput;
    }

    public static string GetClassAttributeValue(string html)
    {
        string classAttribute = "class=\"";
        int classIndex = html.IndexOf(classAttribute);
        if (classIndex == -1)
        {
            return string.Empty;
        }

        int startIndex = classIndex + classAttribute.Length;
        int endIndex = html.IndexOf("\"", startIndex);

        if (endIndex == -1)
        {
            return string.Empty;
        }

        return html.Substring(startIndex, endIndex - startIndex);
    }
}