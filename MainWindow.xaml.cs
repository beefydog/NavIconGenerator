using System.Diagnostics;
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
            .Replace("currentColor", txtIconForegroundColor.Text.Trim());

        StringBuilder cssOutputBuilder = new StringBuilder();
        cssOutputBuilder.AppendLine("." + BootstrapClassValue + " {")
            .AppendLine("\tbackground-image: url(\"data:image/svg+xml," + NewSvgData + "\");")
            .AppendLine("}");

        txtOutputCSS.Text = cssOutputBuilder.ToString();

        StringBuilder navMenuOutputBuilder = new StringBuilder();
        navMenuOutputBuilder.AppendLine("<div class=\"nav-item px-3\">")
            .AppendLine("\t<NavLink class=\"nav-link\" href=\"pagename\">")
            .AppendLine("\t\t<span class=\"bi " + BootstrapClassValue + "\" aria-hidden=\"true\"></span> Your Page Name")
            .AppendLine("\t</NavLink>")
            .AppendLine("</div>");

        txtOutputNavMenuTemplate.Text = navMenuOutputBuilder.ToString();
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


    private void btnVisitBootstrapIconsPage_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://icons.getbootstrap.com/",
            UseShellExecute = true
        });
    }
}