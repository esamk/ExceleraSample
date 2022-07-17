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
            _productsRepo = productRepository;
            _ordersRepo = orderRepository;
            _orderLinesRepo = orderLineRepository;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var products = await _productsRepo.ListAsync(0, 100);

            // Luodaan tuote ja lisätään se tietokantaan.
            var testProduct = await _productsRepo.Create();
            testProduct.Name = "Testituote " + products.Count().ToString();
            await _productsRepo.Add(testProduct);
            await _productsRepo.SaveAsync();

            // Ladataan tuotteet tietokannasta
            products = await _productsRepo.ListAsync(0, 100);
            foreach(var p in products)
            {
                Console.WriteLine("Nro: " + p.ProductNumber.ToString() + ", Nimi: " + p.Name);
            }
        }
    }
}
