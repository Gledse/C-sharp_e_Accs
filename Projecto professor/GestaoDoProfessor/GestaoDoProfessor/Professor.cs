using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDoProfessor
{
    class Professor
    {
        private int codigo;
        private string nome;
        private int contacto;
        private string nivelAcademico;
        private double salarioHora;
        private int cargaHoraria;
        private string sexo;
        private string estadoCivil;

       
        public Professor(int codigo, string nome, int contacto, string sexo, string estadoCivil, string nivelAcademico, double salarioHora, int cargaHoraria)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.contacto = contacto;
            this.sexo = sexo;
            this.estadoCivil = estadoCivil;
            this.nivelAcademico = nivelAcademico;
            this.salarioHora = salarioHora;
            this.cargaHoraria = cargaHoraria;
        }
        Professor() { }
        public void setCodigo(int codigo) { this.codigo = codigo; }
        public void setNome(string nome) { this.nome = nome; }
        public void setContacto(int contacto) { this.contacto = contacto; }
        public void setSexo(string sexo) { this.sexo = sexo; }
        public void setEstadoCivil(string estadoCivil) { this.estadoCivil = estadoCivil; }
        public void setNivelAcademico(string nivelAcademico) { this.nivelAcademico = nivelAcademico; }
        public void setSalarioHora(double salarioHora) { this.salarioHora = salarioHora; }
        public void setCargaHoraria(int cargaHoraria) { this.cargaHoraria = cargaHoraria; }

        public int getCodigo() { return codigo; }
        public string getNome() { return nome; }
        public int getContacto() { return contacto; }
        public string getSexo() { return sexo; }
        public string getEstadoCivil() { return estadoCivil; }
        public string getNivelAcademico() { return nivelAcademico; }
        public double getSalarioHora() { return salarioHora; }
        public int getCargaHoraria() { return cargaHoraria; }

        public double salarioMensal()
        {
            double med = 0;
            return med = salarioHora * cargaHoraria / 2;
        }
    }
}
