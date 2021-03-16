using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Att5by5Livraria
{
    class Cliente
    {
        public long IdCLiente { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Telefone Telefone { get; set; }
        public Endereco Endereco { get; set; }

        public override string ToString()
        {
            return  $"ID Cliente: {IdCLiente}\n" +
                    $"CPF: {CPF}\n" +
                    $"Nome: {Nome}\n" +
                    $"Data de Nascimento: {DataNascimento.ToString("dd/MM/yyyy")}\n" +
                    $"Telefone: {Telefone.ToString()}\n" +
                    $"Endereço: {Endereco.ToString()}";        
        }

        public void Inserir(List<Cliente> clientes, Cliente cliente)
        {
            clientes.Add(cliente);

            return;
        }
    }
}
