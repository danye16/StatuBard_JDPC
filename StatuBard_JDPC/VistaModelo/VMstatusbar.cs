using System;
using System.Collections.Generic;
using System.Text;

namespace StatuBard_JDPC.VistaModelo
{
    public interface VMstatusbar
    {
        void OcultarStatusBar();
        void MostrarStatus();
        void Traslucido();
        void Transparente();
        void CambiarColor();
    }
}
