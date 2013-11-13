using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudienarbeitsProjekt
{
    public partial class PDFView : UserControl
    {
        public PDFView(String fileName)
        {
            InitializeComponent();

            this.axAcroPDF1.LoadFile(fileName);
        }
    }
}
