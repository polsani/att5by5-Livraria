using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Att5by5Livraria
{
    class Arquivo
    {
        public string PathCliente { get; set; }
        public string PathLivro { get; set; }
        public string PathEmprestimo { get; set; }

        public int ProcuraCPF(string cpf)
        {
            string[] lines = File.ReadAllLines(PathCliente);
            if (lines.Length > 1)
            {
                foreach (string line in lines)
                {
                    string[] clienteCSV = line.Split(';');
                    if (clienteCSV.FirstOrDefault() == cpf)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public int ProcuraISBN(string isbn)
        {
            string[] lines = File.ReadAllLines(PathLivro);
            if (lines.Length > 1)
            {
                foreach (string line in lines)
                {
                    string[] livroCSV = line.Split(';');
                    if (livroCSV.FirstOrDefault() == isbn)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public int ProcuraIdCliente(long id)
        {
            string[] lines = File.ReadAllLines(PathCliente);
            if (lines.Length > 1)
            {
                foreach (string line in lines)
                {
                    string[] clienteCSV = line.Split(';');
                    if (clienteCSV.FirstOrDefault() == id.ToString())
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public int ProcuraNumTombo(long numTombo)
        {
            string[] lines = File.ReadAllLines(PathLivro); //lines recebe todas as linhas do arquivo
            if (lines.Length > 1)
            {
                foreach (string line in lines)
                {
                    string[] livroCSV = line.Split(';'); //livroCSV recebe cada linha já splitado
                    if (livroCSV.FirstOrDefault() == numTombo.ToString())
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public int ProcuraNumTomboEmprestimo(long numTombo)
        {
            string[] lines = File.ReadAllLines(PathEmprestimo);
            if (lines.Length > 1)
            {
                foreach (string line in lines)
                {
                    string[] livroCSV = line.Split(';');
                    if (livroCSV.FirstOrDefault() == numTombo.ToString())
                    {
                        if (livroCSV[4] == "1")
                        {
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }

        public void SalvarCliente(Cliente cliente)
        {
            string linecliente = $"{cliente.IdCLiente};" +
                                 $"{cliente.CPF};" +
                                 $"{cliente.Nome};" +
                                 $"{cliente.DataNascimento.ToString("dd/MM/yyyy")};" +
                                 $"{cliente.Telefone.DDD};" +
                                 $"{cliente.Telefone.Numero};" +
                                 $"{cliente.Telefone.Tipo};" +
                                 $"{cliente.Endereco.Logradouro};" +
                                 $"{cliente.Endereco.Bairro};" +
                                 $"{cliente.Endereco.Cidade};" +
                                 $"{cliente.Endereco.Estado};" +
                                 $"{cliente.Endereco.CEP}";

            StreamWriter sw = File.AppendText(PathCliente);
            sw.Write("\n" + linecliente);
            sw.Close();
        }

        public void SalvarLivro(Livro livro)
        {
            string linelivro = $"{livro.NumeroTombo};" +
                               $"{livro.ISBN};" +
                               $"{livro.Titulo};" +
                               $"{livro.Genero};" +
                               $"{livro.DataPublicacao.ToString("dd/MM/yyyy")};" +
                               $"{livro.Autor}";

            StreamWriter sw = File.AppendText(PathLivro);
            sw.Write("\n" + linelivro);
            sw.Close();
        }

        public void SalvarEmprestimo(Emprestimo emprestimo)
        {
            string lineemprestimo = $"{emprestimo.IdCliente};" +
                                    $"{emprestimo.NumeroTombo};" +
                                    $"{emprestimo.DataEmprestimo.ToString("dd/MM/yyyy")};" +
                                    $"{emprestimo.DataDevolucao.ToString("dd/MM/yyyy")};" +
                                    $"{emprestimo.Situacao}";

            StreamWriter sw = File.AppendText(PathEmprestimo);
            sw.Write("\n" + lineemprestimo);
            sw.Close();
        }

        public void Devolucao(long numTombo)
        {
            string[] lines = File.ReadAllLines(PathEmprestimo); //Ler todas as linhas do arquivo
            if (lines.Length > 1)
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] emprestimoCSV = line.Split(';'); //Separar cada elemento da linha por split
                    if (emprestimoCSV[1] == numTombo.ToString()) //Se a posição do split for igual o número do tombo digitado
                    {
                        DateTime dataLimite = DateTime.Parse(emprestimoCSV[3]); //data limite recebe a data de devolução

                        if (emprestimoCSV[4] == "1") //Se status(posição 4) for igual a 1:
                        {
                            if (dataLimite < DateTime.Now) //Se a data atual for maior que a data limite:
                            {
                                double multa = ((DateTime.Now.Date - dataLimite).TotalDays) * 0.10;
                                Console.WriteLine($"Valor da multa: R$ {multa}");
                                Console.Write("PRESSIONE QUALQUER TECLA PARA CONTINUAR...");
                            }
                            else
                            {
                                Console.WriteLine("Devolução dentro do prazo!");
                            }
                            emprestimoCSV[3] = DateTime.Now.ToString("dd/MM/yyyy");
                            emprestimoCSV[4] = "2";

                            lines[i] = string.Join(";", emprestimoCSV); //linha na posição de referencia recebe os novos valores                            
                        }
                    }
                }

                File.WriteAllText(PathEmprestimo, string.Join(";", lines[0])); //"Zera" o arquivo, deixando apenas o cabeçalho

                StreamWriter sw = File.AppendText(PathEmprestimo); //sw recebe o caminho

                for (int i = 1; i < lines.Length; i++)
                {
                    sw.Write("\n" + string.Join(";", lines[i])); //Escreve a linha com os valores atualizados
                }
                
                sw.Close(); //Fechar arquivo

            }

            Console.Write("PRESSIONE QUALQUER TECLA PARA CONTINUAR...");
            Console.ReadKey();
            return;
        }

        public void GerarRelatorio()
        {
            string[] linesCliente = File.ReadAllLines(PathCliente);
            string[] linesLivro = File.ReadAllLines(PathLivro);
            string[] linesEmprestimo = File.ReadAllLines(PathEmprestimo);

            List<string[]> dadosRelatorio = new List<string[]>();

            if (linesEmprestimo.Length > 1)
            {
                for(int i = 1; i < linesEmprestimo.Length; i++)
                {
                    string lineEmprestimo = linesEmprestimo[i];
                    string[] emprestimoCSV = lineEmprestimo.Split(';');

                    string[] itemRelatorio = new string[5];
                    itemRelatorio[0] = emprestimoCSV[4]; //status
                    itemRelatorio[1] = emprestimoCSV[2]; //empréstimo
                    itemRelatorio[2] = emprestimoCSV[3]; //devolução                   

                    for (int j = 1; j < linesLivro.Length; j++)
                    {
                        string lineLivro = linesLivro[j];
                        string[] livroCSV = lineLivro.Split(';');

                        if(livroCSV[0] == emprestimoCSV[1]) 
                        {
                            itemRelatorio[3] = livroCSV[2]; //Titulo
                        }
                    }
                    
                    for (int k = 1; k < linesCliente.Length; k++)
                    {
                        string lineCliente = linesCliente[k];
                        string[] clienteCSV = lineCliente.Split(';');

                        if (clienteCSV[0] == emprestimoCSV[0])
                        {
                            itemRelatorio[4] = clienteCSV[1]; //CPF
                        }
                    }

                    dadosRelatorio.Add(itemRelatorio);
                }
            }

            foreach(string[] item in dadosRelatorio)
            {
                Console.WriteLine($"\nCPF: {item[4]}\n" +
                                  $"Título do Livro: {item[3]}\n" +
                                  $"Status: {item[0]}\n" +
                                  $"Data Empréstimo: {item[1]}\n" +
                                  $"Data Devolução: {item[2]}\n");
            }
            Console.ReadKey();
        }
    }
}
