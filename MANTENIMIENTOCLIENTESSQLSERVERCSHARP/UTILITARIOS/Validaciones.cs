using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANTENIMIENTOCLIENTESSQLSERVERCSHARP.UTILITARIOS
{
    class Validaciones
    {
        //Procedimiento que chequea que solamente se puedan digitar Números
        //en un objeto correspondiente
        public void SoloNumeros(KeyPressEventArgs e)
        {
            //Sentencias para validar que en el TextBox correspondiente
            //solamente se puedan digitar números
            if (Char.IsLetter(e.KeyChar))
                if (Char.IsDigit(e.KeyChar))
                    e.Handled = false;
                else
                    if (Char.IsControl(e.KeyChar))
                        e.Handled = true;
                    else
                        e.Handled = true;
        }

        //Procedimiento que chequea que solamente se puedan digitar Letras
        //en un objeto correspondiente
        public void SoloLetras(KeyPressEventArgs e)
        {
            //Sentencias para validar que en el TextBox correspondiente
            //solamente se puedan digitar letras
            if (Char.IsDigit(e.KeyChar))
                e.Handled = true;
            else
                if (Char.IsControl(e.KeyChar))
                    e.Handled = false;
                else
                    e.Handled = false;
        }
    }
}
