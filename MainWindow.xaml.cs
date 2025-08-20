using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ProyectoCalculadora
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /*logica
         En base a que se seleccione un numero almacenarlo en Number area 
         si se selecciona un operdaro tomar el numero  y ponerlo en subnumberArea (ya sea > 0 o 0)
         
         
    */
    //TODO hacer que se limpie el textarea al dar resultado 
    //TODO hacer que se ponga en negativo el numero +/-
    public partial class MainWindow : Window
    {
        private List<string> datosOperacion = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            // Define el estilo para todos los botones
            Style buttonStyle = new Style(typeof(Button));
            //tamaño de boton
            buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 50.0));
            //Margen boton
            buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(2)));
            // Agrega el estilo a los recursos de la aplicación
            this.Resources.Add(typeof(Button), buttonStyle);

            //asigna los valores de los botones
            Asignacion_BTN_Numeros();
            Asignacion_BTN_operadores();
        }
        private void Asignacion_BTN_operadores()
        {
            Button[] buttons = { resultado, suma, resta, multiplicacion, coma, cambio_de_signo };

            var operadores = new Dictionary<Button, string>
            {
            { buttons[0], "=" },
            { buttons[1], "+" },
            { buttons[2], "-" },
            { buttons[3], "X" },
            //{ buttons[4], "," },
            //{ buttons[5], "+/-" }
            };

            // Asignar los valores a los botones
            foreach (var operador in operadores)
            {
                // Asignar el texto del botón
                operador.Key.Content = operador.Value;
                if (operador.Key == buttons[0])
                {
                    operador.Key.Click += hacerOperacion;
                    //limpiar todos los datos/de la lista tambien
                }
                else
                { 
                operador.Key.Click += GuardaDato;
                }
            }

        }
        private void Asignacion_BTN_Numeros()
        {
            // Crear un arreglo de botones
            Button[] buttons = { num3, num6, num7, num8, num10, num11, num12, num14, num15, num16 };

            // Asignar texto a cada botón
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Content = (i).ToString();
                buttons[i].Click += MostrarDato; // Asignar el manejador de eventos
            }
        }
        private void MostrarDato(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que fue clickeado
            Button clickedButton = sender as Button;
            string valorBoton = clickedButton.Content.ToString();
            int currentValue;
            // Obtener el texto actual del botón y valida si es numerico
            if (int.TryParse(clickedButton.Content.ToString(), out currentValue))
            {
                if (NumberArea.Text == "0")
                {
                    NumberArea.Text = currentValue.ToString();
                }
                else if (currentValue >= 0)
                {
                    NumberArea.Text += currentValue.ToString();
                }
            }
        }
        private void GuardaDato(object sender, RoutedEventArgs e)//muestra los datos en SubNumberArea
        {
            Button clickedButton = sender as Button;
            string operacion = clickedButton.Content.ToString();
            //implementar validacion
            string valorNumerico = NumberArea.Text;
            datosOperacion.Add(valorNumerico);
            if (valorNumerico == "0") datosOperacion.Add("0");
            datosOperacion.Add(operacion);
            LimpiarPantallaPrincipal();
        }
        private void hacerOperacion(object sender, RoutedEventArgs e)//muestra los datos en SubNumberArea
        {
            Button clickedButton = sender as Button;
            
            int operacionBoton=int.Parse(NumberArea.Text);
            //implementar validacion
            string valorNumerico = NumberArea.Text;


            string calculo = "";
            int valor = 0;
            foreach (var valores in datosOperacion)
            {
                calculo += valores;
            }
            calculo += operacionBoton;
            var dataTable = new DataTable();
            valor =Convert.ToInt32(dataTable.Compute(calculo, string.Empty));
            datosOperacion.Add(NumberArea.Text);
            LimpiarPantallaPrincipal();
            NumberArea.Text = valor.ToString();
        }
        private void LimpiarPantallaPrincipal()
        {
            SubNumberArea.Text = "";
            //mostrar dato como valor (esta mostrando el ultimo valor no el combinado de todos) 
            foreach (var valores in datosOperacion)
            {
                SubNumberArea.Text += valores;
            }
        }

    }

}
