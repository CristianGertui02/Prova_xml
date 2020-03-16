using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Prova_xml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        CancellationTokenSource fermo;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Btn_Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            Lst_allenamenti.Items.Clear();
            Task.Factory.StartNew(() => CaricaDati());
            fermo = new CancellationTokenSource();
           
            
        }

        private void CaricaDati()
        {
            

            string path = @"Allenamenti.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlallenamenti = xmlDoc.Element("allenamenti");
            var xmlallenamento = xmlallenamenti.Elements("allenamento");

            foreach (var item in xmlallenamento)
            {
                XElement xmlTipoAllenamento = item.Element("tipo");
                XElement xmlDurataAllenamento = item.Element("durata");
                XElement xmlCalorieConsumate = item.Element("calorie");
                Allenamneto a = new Allenamneto();
                a.Tipo = xmlTipoAllenamento.Value;
                a.Durata = Convert.ToDouble(xmlDurataAllenamento.Value);
                a.Calorie = Convert.ToInt32(xmlCalorieConsumate.Value);
                Dispatcher.Invoke(() => Lst_allenamenti.Items.Add(a));
                Thread.Sleep(1000);

                if(fermo.IsCancellationRequested)
                {
                    break;
                }


            }

            
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            fermo.Cancel();
        }
    }
}
