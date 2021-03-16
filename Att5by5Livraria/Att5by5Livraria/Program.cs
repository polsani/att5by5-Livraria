using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace Att5by5Livraria
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cria diretório, caso necessário
            if (!Directory.Exists(@"C:\Att5by5Livraria\Att5by5Livraria"))
            {
                Directory.CreateDirectory(@"C:\Att5by5Livraria\Att5by5Livraria");
            }

            //Declara Arquivo Cliente e insere os caminhos dos arquivos
            Arquivo arquivoCliente = new Arquivo();
            arquivoCliente.PathCliente = @"C:\Att5by5Livraria\Att5by5Livraria\CLIENTE.csv";
            arquivoCliente.PathLivro = @"C:\Att5by5Livraria\Att5by5Livraria\LIVRO.csv";
            arquivoCliente.PathEmprestimo = @"C:\Att5by5Livraria\Att5by5Livraria\EMPRESTIMO.csv";

            //Cria o arquivo, caso necessário
            if (!File.Exists(arquivoCliente.PathCliente))
            {
                using (StreamWriter sw = new StreamWriter(arquivoCliente.PathCliente))
                {
                    sw.Write("IdCliente;CPF;Nome;DataNascimento;DDD;Numero;Tipo;Logradouro;Bairro;Cidade;Estado;CEP");
                    sw.Close();
                }
            }

            //Declara Arquivo Livro e insere os caminhos dos arquivos
            Arquivo arquivoLivro = new Arquivo();
            arquivoLivro.PathCliente = @"C:\Att5by5Livraria\Att5by5Livraria\LIVRO.csv";
            arquivoLivro.PathLivro = @"C:\Att5by5Livraria\Att5by5Livraria\LIVRO.csv";
            arquivoLivro.PathEmprestimo = @"C:\Att5by5Livraria\Att5by5Livraria\EMPRESTIMO.csv";

            //Cria o arquivo, caso necessário
            if (!File.Exists(arquivoLivro.PathLivro))
            {
                using (StreamWriter sw = new StreamWriter(arquivoLivro.PathLivro))
                {
                    sw.Write("NumeroTombo;ISBN;Titulo;Genero;DataPublicacao;Autor");
                    sw.Close();
                }
            }

            //Declara Arquivo Emprestimo e insere os caminhos dos arquivos
            Arquivo arquivoEmprestimo = new Arquivo();
            arquivoEmprestimo.PathCliente = @"C:\Att5by5Livraria\Att5by5Livraria\EMPRESTIMO.csv";
            arquivoEmprestimo.PathLivro = @"C:\Att5by5Livraria\Att5by5Livraria\LIVRO.csv";
            arquivoEmprestimo.PathEmprestimo = @"C:\Att5by5Livraria\Att5by5Livraria\EMPRESTIMO.csv";

            //Cria o arquivo, caso necessário
            if (!File.Exists(arquivoEmprestimo.PathEmprestimo))
            {
                using (StreamWriter sw = new StreamWriter(arquivoEmprestimo.PathEmprestimo))
                {
                    sw.Write("IdCliente;NumeroTombo;DataEmprestimo;DataDevolucao;StatusEmprestimo");
                    sw.Close();
                }              
            }

            //Instância dos Objetos:
            Cliente Cliente = new Cliente();
            List<Cliente> Clientes = new List<Cliente>();

            Livro Livro = new Livro();
            List<Livro> Livros = new List<Livro>();

            Emprestimo Emprestimo = new Emprestimo();
            List<Emprestimo> Emprestimos = new List<Emprestimo>();

            //Variáveis
            int op = 0, flag;

            do
            {
                Console.Clear();
                op = Menu(op); //Menu Principal
                switch (op)
                {
                    case 1:
                        Console.Write("\nDigite o CPF do cliente para verificação: "); //Verificação
                        string cpf = Console.ReadLine();

                        flag = arquivoCliente.ProcuraCPF(cpf);
                        if (flag == 1)
                        {
                            Console.Write("Cliente já cadastrado.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Cliente = InserirCliente(); //Cria Objeto Cliente
                            Cliente.Inserir(Clientes, Cliente); //Inseri o Objeto em uma Lista
                            arquivoCliente.SalvarCliente(Cliente); //Inseri a lista em um arquivo
                        }
                        break;
                    case 2:
                        Console.Write("\nDigite o ISBN do livro para verificação: "); //Verificação
                        string isbn = Console.ReadLine();
                        flag = arquivoLivro.ProcuraISBN(isbn);
                        if (flag == 1)
                        {
                            Console.Write("ISBN já cadastrado.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Livro = InserirLivro(); //Cria Objeto Livro
                            Livro.Inserir(Livros, Livro); //Inseri o Objeto em uma Lista
                            arquivoLivro.SalvarLivro(Livro); //Inseri a lista em um arquivo
                        }
                        break;
                    case 3:
                        Console.Write("\nDigite o ID do cliente para verificação: "); //Verificação
                        long id = long.Parse(Console.ReadLine());

                        flag = arquivoCliente.ProcuraIdCliente(id);
                        if (flag == 1)
                        {
                            Console.Write("\nDigite o número do tombo para verificação: "); //Verificação
                            long numTombo = long.Parse(Console.ReadLine());
                            flag = arquivoLivro.ProcuraNumTombo(numTombo);
                            if (flag == 1)
                            {
                                Emprestimo = InserirEmprestimo(id, numTombo); //Cria Objeto Emprestimo
                                Emprestimo.Inserir(Emprestimos, Emprestimo); //Inseri o Objeto em uma Lista
                                arquivoEmprestimo.SalvarEmprestimo(Emprestimo); //Inseri a lista em um arquivo
                            }
                            else
                            {
                                Console.Write("Número do Tombo não encontrado!");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.Write("ID do Cliente não encontrado!");
                            Console.ReadKey();
                        }
                        break;
                    case 4:
                        Console.Write("\nDigite o número do tombo para verificação: "); //Verificação
                        long numTomboVerificar = long.Parse(Console.ReadLine());
                        flag = arquivoEmprestimo.ProcuraNumTomboEmprestimo(numTomboVerificar);
                        if (flag == 1)
                        {
                            arquivoEmprestimo.Devolucao(numTomboVerificar);
                        }
                        else
                        {
                            Console.Write("Livro não encontrado para devolução!");
                            Console.ReadKey();
                        }
                        break;
                    case 5:
                        arquivoEmprestimo.GerarRelatorio();
                        break;
                    default:
                        break;
                }
            } while (op != 0);

            Console.ReadKey();
        }

        private static int Menu(int op)
        {
            Console.Write(">>>Menu Principal - Biblioteca 5by5<<<\n\n" +
                "1- Cadastro de Cliente\n" +
                "2- Cadastro de Livro\n" +
                "3- Empréstimo de Livro\n" +
                "4- Devolução de Livro\n" +
                "5- Relatório de Empréstimos e Devoluções\n" +
                "0- Sair\n\n" +
                "Digite a opção desejada: ");
            op = int.Parse(Console.ReadLine());

            return op;
        }

        private static Cliente InserirCliente()
        {
            CultureInfo cultureBr = new CultureInfo(name: "pt-BR");
            long idCliente;
            string cpf, nome;
            string dataNascimento;
            string ddd, numero, tipo;
            string logradouro, bairro, cidade, estado, cep;

            Console.WriteLine("\n>>>CADASTRO DO CLIENTE<<<");

            Console.Write("Informe o ID: ");
            idCliente = long.Parse(Console.ReadLine());

            Console.Write("Informe o CPF: ");
            cpf = Console.ReadLine();

            Console.Write("Informe o nome: ");
            nome = Console.ReadLine();

            Console.Write("Informe a data de nascimento(dd/MM/yyyy): ");
            dataNascimento = Console.ReadLine();
            DateTime dataConvertida = DateTime.ParseExact(dataNascimento, "d", cultureBr);

            Console.Write("Informe o DDD: ");
            ddd = Console.ReadLine();

            Console.Write("Informe o número do telefone: ");
            numero = Console.ReadLine();

            Console.Write("Informe o tipo do telefone: ");
            tipo = Console.ReadLine();

            Console.Write("Informe o logradouro: ");
            logradouro = Console.ReadLine();

            Console.Write("Informe o bairro: ");
            bairro = Console.ReadLine();

            Console.Write("Informe a cidade: ");
            cidade = Console.ReadLine();

            Console.Write("Informe a UF: ");
            estado = Console.ReadLine();

            Console.Write("Informe o CEP: ");
            cep = Console.ReadLine();

            return new Cliente
            {
                IdCLiente = idCliente,
                CPF = cpf,
                Nome = nome,
                DataNascimento = dataConvertida,

                Telefone = new Telefone
                {
                    DDD = ddd,
                    Numero = numero,
                    Tipo = tipo
                },

                Endereco = new Endereco
                {
                    Logradouro = logradouro,
                    Bairro = bairro,
                    Cidade = cidade,
                    Estado = estado,
                    CEP = cep
                }

            };
        }

        private static Livro InserirLivro()
        {
            CultureInfo cultureBr = new CultureInfo(name: "pt-BR");
            long numeroTombo;
            string isbn, titulo, genero, autor;
            string dataPublicacao;

            Console.WriteLine("\n>>>CADASTRO DO LIVRO<<<");

            Console.Write("Informe o número do Tombo: ");
            numeroTombo = long.Parse(Console.ReadLine());

            Console.Write("Informe o ISBN: ");
            isbn = Console.ReadLine();

            Console.Write("Informe o título: ");
            titulo = Console.ReadLine();

            Console.Write("Informe o gênero: ");
            genero = Console.ReadLine();

            Console.Write("Informe a data de publicação(dd/MM/yyyy): ");
            dataPublicacao = Console.ReadLine();
            DateTime dataConvertida = DateTime.ParseExact(dataPublicacao, "d", cultureBr);

            Console.Write("Informe o autor: ");
            autor = Console.ReadLine();

            return new Livro
            {
                NumeroTombo = numeroTombo,
                ISBN = isbn,
                Titulo = titulo,
                Genero = genero,
                DataPublicacao = dataConvertida,
                Autor = autor,
            };
        }

        private static Emprestimo InserirEmprestimo(long id, long numTombo)
        {
            Console.WriteLine("\n>>>CADASTRO DO EMPRÉSTIMO<<<");
            CultureInfo cultureBr = new CultureInfo(name: "pt-BR");

            string dataDevolucao;

            Console.Write("Informe a data de devolução(dd/MM/yyyy): ");
            dataDevolucao = Console.ReadLine();
            DateTime DataConvertida = DateTime.ParseExact(dataDevolucao, "d", cultureBr);

            return new Emprestimo
            {
                IdCliente = id,
                NumeroTombo = numTombo,
                DataEmprestimo = DateTime.Now.Date,
                DataDevolucao = DataConvertida,
                Situacao = 1
            };
        }
    }
}
