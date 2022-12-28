using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmDentalOffice
{
    public class Print
    {

        public void imprimirInicio()
        {
            try
            {
                //tela do comeco
            }catch(Exception e) 
            {
                Console.WriteLine(e);
                imprimirInicio();
            }
        }
    }
}
