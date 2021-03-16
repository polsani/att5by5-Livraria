namespace Att5by5Livraria
{
    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public override string ToString()
        {
            return $"{Logradouro}, {Bairro}. {Cidade}, {Estado}. CEP: {CEP}";
        }
    }
}