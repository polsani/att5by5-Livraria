namespace Att5by5Livraria
{
    public class Telefone
    {
        public string DDD { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }

        public override string ToString()
        {
            return $"{DDD},{Numero},{Tipo}\n";
        }
    }
}