using SampleDB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceleraSample
{
    public partial class Form1 : Form
    {
        private readonly IProductRepository _productsRepo;
        private readonly IOrderRepository _ordersRepo;
        private readonly IOrderLineRepository _orderLinesRepo;
        public Form1(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IOrderLineRepository orderLineRepository
            )
        {
            InitializeComponent();
        }

    }
}
