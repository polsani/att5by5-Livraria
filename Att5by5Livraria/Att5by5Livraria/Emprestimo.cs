using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Att5by5Livraria
{
    class Emprestimo
    {
        public long IdCliente { get; set; }
        public long NumeroTombo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int Situacao { get; set; }

        public override string ToString()
        {
            return $"ID do Cliente: {IdCliente}\n" +
                $"Número do Tombo: {NumeroTombo}\n" +
                $"Data de Empréstimo: {DataEmprestimo.ToString("dd/MM/yyyy")}\n" +
                $"Data de Devolução: {DataDevolucao.ToString("dd/MM/yyyy")}\n" +
                $"Situação: {Situacao}";
        }

        public void Inserir(List<Emprestimo> emprestimos, Emprestimo emprestimo)
        {
            emprestimos.Add(emprestimo);

            return;
        }
    }
}
