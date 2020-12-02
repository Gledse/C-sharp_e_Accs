using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace teste
{
    class Teste
    {
        private int codigo;
        private string nome;

        public Teste() { }
        public Teste(int codigo, string nome) 
        {
            this.codigo = codigo;
            this.nome = nome;
        }
        public void setNome(string nome) { this.nome = nome; }
        public void setCodigo(int codigo) { this.codigo = codigo; }

        public int getCodigo() { return codigo; }
        public string getNome() { return nome; }

    }
}
