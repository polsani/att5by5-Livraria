using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Att5by5Livraria
{
    class Livro
    {
        public long NumeroTombo { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }

        public override string ToString()
        {
            return $"Número do Tombo: {NumeroTombo}\n" +
                    $"ISBN: {ISBN}\n" +
                    $"Título: {Titulo}\n" +
                    $"Gênero: {Genero}\n" +
                    $"Data de Publicação: {DataPublicacao.ToString("dd/MM/yyyy")}\n" +               
                    $"Autor: {Autor}";         
        }
        public void Inserir(List<Livro> livros, Livro livro)
        {
            livros.Add(livro);

            return;
        }
    }
}
