using EmployeeRegister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmployeeRegister
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }


        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Employee employee = new Employee
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    MotherLastName = txtMotherLastName.Text,
                    //Age = int.Parse(txtAge.Text),
                    //Telephone = int.Parse(txtTelephone.Text)
                };

                await App.SQLiteDB.SaveEmployeeAsync(employee);


                //txtAge.Text = "";
                //txtTelephone.Text = "";
                CleanControlls();
                llenarDatos();

                await DisplayAlert("Registro", "Se guardo de manera existosa", "OK");
            }
            else
            {
                await DisplayAlert("Aviso", "Ingresa todos los datos", "OK");
            }
        }

        public async void llenarDatos()
        {
            var EmployeeList = await App.SQLiteDB.GetEmployeesAsync();
            if (EmployeeList != null)
            {
                lstEmployee.ItemsSource = EmployeeList;
            }
        }




        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtLastName.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtMotherLastName.Text))
            {
                respuesta = false;
            }
            //else if (string.IsNullOrEmpty(txtAge.Text))
            //{
            //    respuesta = false;
            //}
            //else if (string.IsNullOrEmpty(txtTelephone.Text))
            //{
            //    respuesta = false;
            //}
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmployee.Text))
            {
                Employee employee = new Employee()
                {
                    IdEmpl = Convert.ToInt32(txtIdEmployee.Text),
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    MotherLastName = txtMotherLastName.Text

                };

                await App.SQLiteDB.SaveEmployeeAsync(employee);
                await DisplayAlert("Registro", "Se actualizo de manera existosa", "OK");

                CleanControlls();
                txtIdEmployee.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegister.IsVisible = true;
                llenarDatos();
            }
        }


        private async void lstEmployee_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Employee)e.SelectedItem;
            btnRegister.IsVisible = false;
            txtIdEmployee.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdEmpl.ToString()))
            {
                var employee = await App.SQLiteDB.GetEmployeeByIdAsync(obj.IdEmpl);

                if (employee != null)
                {
                    txtIdEmployee.Text = employee.IdEmpl.ToString();
                    txtName.Text = employee.Name;
                    txtLastName.Text = employee.LastName;
                    txtMotherLastName.Text = employee.MotherLastName;
                }
            }
        }


        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var employee = await App.SQLiteDB.GetEmployeeByIdAsync(Convert.ToInt32(txtIdEmployee.Text));

            if (employee != null)
            {
                await App.SQLiteDB.DeleteEmployee(employee);
                await DisplayAlert("Empleado", "Se elimino de manera existosa", "OK");
                CleanControlls();
                llenarDatos();

                txtIdEmployee.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegister.IsVisible = true;
            }

        }

        private void CleanControlls()
        {
            txtIdEmployee.Text = "";
            txtName.Text = "";
            txtLastName.Text = "";
            txtMotherLastName.Text = "";
        }

    }
}
