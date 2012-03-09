using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tl2
{
    class Pessoa
    {
        //Propriedades
        private string nome_pessoa;
        public string nome
        {
            get { return nome_pessoa; }
            set
            {
                if (value == "") nome_pessoa = "N/A";
                else nome_pessoa = value;
            }
        }

        private string bi_pessoa;
        public string bi
        {
            get { return bi_pessoa; }
            set
            {
                string tamanhobi = value;
                if (tamanhobi.Length == 8) bi_pessoa = value;
                else bi_pessoa = "N/A";
            }
        }

        private string morada_pessoa;
        public string morada
        {
            get { return morada_pessoa; }
            set
            {
                if (value == "") morada_pessoa = "N/A";
                else morada_pessoa = value;
            }
        }

        private string datanasc_pessoa;
        public string data_nascimento
        {
            get { return datanasc_pessoa; }
            set
            {
                if (value == "") datanasc_pessoa = "N/A";
                else datanasc_pessoa = value;
            }
        }

        private string email_pessoa;
        public string email
        {
            get { return email_pessoa; }
            set
            {
                if (value == "") email_pessoa = "N/A";
                else email_pessoa = value;
            }
        }

        private string telefone_pessoa;
        public string telefone
        {
            get { return telefone_pessoa; }
            set
            {
                if (value == "") telefone_pessoa = "N/A";
                else telefone_pessoa = value;
            }
        }

        //Constructor único
        public Pessoa(string nomePessoa, string biPessoa, string moradaPessoa, string dataNascimentoPessoa, string telefonePessoa, string emailPessoa)
        {
            this.nome = nomePessoa;
            this.bi = biPessoa;
            this.morada = moradaPessoa;
            this.data_nascimento = dataNascimentoPessoa;
            this.telefone = telefonePessoa;
            this.email = emailPessoa;
        }

        //Métodos públicos

        //Método que devolve a string com todas as informações como pedido
        public string mostrar_dados_pessoais()
        {
            string dados ="Nome: "+this.nome+"\nBI: "+this.bi+"\nMorada: "+this.morada+"\nData de Nascimento: "+this.data_nascimento+"\nTelefone: "+this.telefone+"\nCorreio Electrónico: "+this.email;
            return dados;
        }

        //Método que devolve o bi da pessoa
        public string dar_bi()
        {
            return this.bi_pessoa;
        }

    }
}