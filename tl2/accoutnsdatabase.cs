using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tl2
{
    class Accoutnsdatabase
    {
        //Atributos
        private string nr_conta,bi;
        private ContaOrdem conta;
        private ContaPrazo conta2;
        private int tipo;

        //Construtores

        public Accoutnsdatabase(string nr_de_conta, string bi_pessoa, ContaOrdem contaordem)
        {
            this.nr_conta = nr_de_conta;
            this.bi = bi_pessoa;
            this.conta = contaordem;
            this.tipo = 1;
        }

        //Construtor
        public Accoutnsdatabase(string nr_de_conta, string bi_pessoa, ContaPrazo contaprazo)
        {
            this.nr_conta = nr_de_conta;
            this.bi = bi_pessoa;
            this.conta2 = contaprazo;
            this.tipo = 2;
        }

        //Métodos publicos
        public string verificar_bi()
        {
            return this.bi;
        }
        public void mostrar_dados_conta()
        {
            if (tipo == 1)
            {
                conta.mostrar_info();
            }
            else if (tipo == 2)
            {
                conta2.mostrar_info();
            }
        }

    }
}
