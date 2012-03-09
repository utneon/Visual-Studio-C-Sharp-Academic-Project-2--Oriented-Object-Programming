using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace tl2
{
    class Conta
    {
        //Atributos da conta
        //Atributo ESTÁTICO -> que permite ter um valor comum em todas as instancias da classe
        static int numero_conta = 0;

        //Outros atributos para o uso interno da classe. Não estão acessíveis ao main mas estão disponíveis para as classes de herança.
        protected int nr_conta_actual;
        protected double saldo_conta;
        protected string data_criacao;
        protected Pessoa pessoa_da_conta;
        
        /// <summary>
        /// Construtor por defeito 
        /// Protected porque não se vão criar objectos do tipo conta apenas
        /// </summary>
        protected Conta(Pessoa pessoa_para_adicionar_a_conta)
        {
            numero_conta++;
            nr_conta_actual=numero_conta;
            DateTime data = DateTime.Now;
            this.data_criacao = Convert.ToString(data);
            this.pessoa_da_conta = pessoa_para_adicionar_a_conta;
            this.saldo_conta = 0;
        }

        //Métodos públicos

        //Depositar dinheiro
        /// <summary>
        /// Método para depositar dinheiro da classe conta
        /// </summary>
        public virtual void depositar_dinheiro()
        {
            Console.WriteLine("Quantia a depositar:");
            double quantida_a_depositar = Convert.ToDouble(Console.ReadLine());
            this.saldo_conta = saldo_conta + quantida_a_depositar;
        }

        /// <summary>
        /// Método para fazer levantamento de dinheiro pontual.
        /// </summary>
        public virtual void levantar_dinheiro()
        {
            Console.WriteLine("Quanto dinheiro deseja levantar?");
            double quantia_a_levantar = Convert.ToDouble(Console.ReadLine());

            //foi criada 1 verificação para evitar que se levante dinheiro de uma conta a ordem caso não tenha saldo suficiente.
            if (this.saldo_conta - quantia_a_levantar < 0)
            {
                Console.WriteLine("Não foi possível efectuar a operação devido à quantia exceder o saldo disponível.");
            }
            else if (this.saldo_conta - quantia_a_levantar >= 0)
            {
                this.saldo_conta= this.saldo_conta - quantia_a_levantar;
                Console.WriteLine("Operação concluída com sucesso. Saldo restante: {0} euro(s)",this.saldo_conta);
            }
        }

        /// <summary>
        /// Método para efectuar a transferência de dinheiro para uma outra conta.
        /// </summary>
        public virtual double transferir_dinheiro()
        {
            Console.WriteLine("Quanto dinheiro deseja transferir?");
            double quantia_a_levantar = Convert.ToDouble(Console.ReadLine());

            if (this.saldo_conta - quantia_a_levantar < 0)
            {
                Console.WriteLine("Não foi possível efectuar a operação devido à quantia exceder o saldo disponível.");
                quantia_a_levantar = 0;
                return 0;
            }
            else if (this.saldo_conta - quantia_a_levantar >= 0)
            {
                this.saldo_conta = this.saldo_conta - quantia_a_levantar;
                Console.WriteLine("Operação concluída com sucesso. Saldo restante: {0} euro(s)", this.saldo_conta);
                return quantia_a_levantar;
            }
            return quantia_a_levantar;
        }

        /// <summary>
        /// Método que permite receber o dinheiro na conta de uma transferência. No programa principal é procurado a conta de destino e são executados 2 métodos. O de transferir dinheiro da conta remetente e o método, receber dinheiro da conta destino.
        /// </summary>
        /// <param name="dinheiro">Valor do dinheiro a receber na conta.</param>
        public virtual void receber_transferencia(double dinheiro)
        {
            //não necessita de ter nada visto que nunca vai ser usado um objecto do tipo Conta.
        }

        /// <summary>
        /// Lista a informação da conta juntamente com o Nome e BI do proprietário.
        /// Informação da conta mostrada: nº de conta, saldo de conta e data de criação
        /// </summary>
        //Método para Listar toda a informação disponível
        public virtual void mostrar_info()
        {
            Console.WriteLine("****************************************************************\n");

            Console.WriteLine("Nr de conta: {0}\n", nr_conta_actual);
            Console.WriteLine("Nome do proprietário: {0}\n", this.pessoa_da_conta.nome);
            Console.WriteLine("BI do proprietário: {0}\n", this.pessoa_da_conta.bi);
            Console.WriteLine("Saldo actual da conta: {0} euro(s)\n", this.saldo_conta);
            Console.WriteLine("Data de criação da conta: {0}\n", this.data_criacao);

            Console.WriteLine("****************************************************************\n");
        }

        /// <summary>
        /// Devolve o número de conta no tipo de variável string.
        /// </summary>
        public virtual string dar_nr_conta(){
            return Convert.ToString(this.nr_conta_actual);
        }
        /// <summary>
        /// Devolve o número do Bilhete de Identidade do proprietário da conta no tipo de variável STRING
        /// </summary>
        public virtual string dar_bi_da_pessoa()
        {
            return this.pessoa_da_conta.bi;
        }
        /// <summary>
        /// Lista os movimentos da conta
        /// </summary>
        public virtual void listar_movimentos()
        {
            //não se listam movimentos aqui porque não se dá a possiblidade de criar um objecto só do tipo conta.
        }
    }
    class ContaOrdem : Conta
        {
            //Atributos da conta à ordem
            private ArrayList movimentos;

            //Construtor para a conta a prazo
            /// <summary>
            /// Construtor único para a classe ContaOrdem 
            /// Só é disponibilizado um constructor devido a ser necessário todos os dados requeridos pelo banco respectivos ao proprietário da conta.
            /// </summary>
            /// <param name="pessoa_a_adicionar_na_conta_ordem"> Objecto do tipo Pessoa que contém toda a informação do proprietário da conta </param>
            public ContaOrdem(Pessoa pessoa_a_adicionar_na_conta_ordem):base(pessoa_a_adicionar_na_conta_ordem)
            {
                this.movimentos = new ArrayList();
            }

            //Métodos Públicos
            
            //Método para depositar dinheiro
            /// <summary>
            /// Método para efectuar um deposito pontual
            /// </summary>
            public override void depositar_dinheiro()
            {
                Console.WriteLine("Quantia a depositar na conta à Ordem:");
                double quantida_a_depositar = Convert.ToDouble(Console.ReadLine());
                base.saldo_conta = saldo_conta + quantida_a_depositar;
                this.movimentos.Add("+" + Convert.ToString(quantida_a_depositar) + " - Depósito Pontual");
            }

            /// <summary>
            /// Método que permite receber o dinheiro na conta de uma transferência. No programa principal é procurado a conta de destino e são executados 2 métodos. O de transferir dinheiro da conta remetente e o método, receber dinheiro da conta destino.
            /// </summary>
            /// <param name="dinheiro">Valor do dinheiro a receber na conta.</param>
            public override void receber_transferencia(double dinheiro)
            {
                double quantida_a_depositar = dinheiro;
                base.saldo_conta = saldo_conta + quantida_a_depositar;
                this.movimentos.Add("+" + Convert.ToString(quantida_a_depositar) + " - Transferência Bancária");
            }

            /// <summary>
            /// Este método permite efectuar o levantamento pontual de dinheiro da conta à Ordem.
            /// </summary>
            public override void levantar_dinheiro()
            {
                Console.WriteLine("Quanto dinheiro deseja levantar da sua conta à ordem?");
                double quantia_a_levantar = Convert.ToDouble(Console.ReadLine());

                if (base.saldo_conta - quantia_a_levantar < 0)
                {
                    Console.WriteLine("Não foi possível efectuar a operação devido à quantia exceder o saldo disponível.");
                }
                else if (base.saldo_conta - quantia_a_levantar >= 0)
                {
                    base.saldo_conta = base.saldo_conta - quantia_a_levantar;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Operação concluída com sucesso. Saldo restante: {0} euro(s)", base.saldo_conta);
                    Console.ResetColor();
                    this.movimentos.Add("-" + Convert.ToString(quantia_a_levantar) + " - Levantamento Pontual");
                }
            }

            /// <summary>
            /// Este método permite efectuar a transferência de uma conta para a outra.
            /// Devolve um valor do tipo double que é a quantidade a transferir para a conta destinatário.
            /// O valor é questionado ao utilizador através da console.
            /// </summary>
            public override double transferir_dinheiro()
            {
                Console.WriteLine("Quanto dinheiro deseja transferir?");
                double quantia_a_levantar = Convert.ToDouble(Console.ReadLine());

                if (base.saldo_conta - quantia_a_levantar < 0)
                {
                    Console.WriteLine("Não foi possível efectuar a operação devido à quantia exceder o saldo disponível.");
                    quantia_a_levantar = 0;
                    return 0;
                }
                else if (base.saldo_conta - quantia_a_levantar >= 0)
                {
                    this.saldo_conta = base.saldo_conta - quantia_a_levantar;
                    Console.WriteLine("Operação concluída com sucesso. Saldo restante: {0} euro(s)", base.saldo_conta);
                    this.movimentos.Add("-" + Convert.ToString(quantia_a_levantar) + " - Transferência bancária");
                    return quantia_a_levantar;
                }
                return quantia_a_levantar;
            }

            /// <summary>
            /// Este método apresenta no ecrãn a listagem dos movimentos da conta
            /// Também apresenta o saldo actual da conta
            /// </summary>
            public override void listar_movimentos()
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Lista de Movimentos: \n");
                Console.ResetColor();
                foreach (string value in movimentos)
                {
                    Console.WriteLine(value);
                }
                Console.WriteLine("Saldo Actual: {0} euro(s)", base.saldo_conta);
            }

            /// <summary>
            /// Apresenta na console o Nome do proprietário e o BI do mesmo.
            /// Também é apresentado o nº da conta, saldo da conta, tipo de conta e data de criação.
            /// </summary>
            public override void mostrar_info()
            {
                Console.WriteLine("****************************************************************\n");

                Console.WriteLine("Nº de conta: {0} \n", base.nr_conta_actual);
                Console.WriteLine("Nome do proprietário: {0} \n", base.pessoa_da_conta.nome);
                Console.WriteLine("BI do proprietário: {0} \n", base.pessoa_da_conta.bi);

                Console.WriteLine("Tipo de conta: Conta a Ordem \n");

                Console.WriteLine("Saldo actual da conta: {0} euro(s) \n", base.saldo_conta);
                Console.WriteLine("Data de criação da conta: {0} \n", base.data_criacao);

                Console.WriteLine("****************************************************************\n");
            }

            /// <summary>
            /// Devolve o número de conta no tipo de variável string.
            /// </summary>
            public override string dar_nr_conta()
            {
                return base.dar_nr_conta();
            }

            /// <summary>
            /// Devolve o BI do proprietário da conta no tipo de variável string.
            /// </summary>
            public override string dar_bi_da_pessoa()
            {
                return pessoa_da_conta.bi;
            }
        }
    class ContaPrazo : Conta
    {
        //Atributos da conta a prazo
        private double taxa_juros;      //Valor inteiro para percentagem
        private int duracao;            //Valor inteiro para duração em anos
        public int estado_de_conta;     //Estado da conta. 1-Activa, 0 - Encerrada
        private ArrayList movimentos;   //Atributo do tipo ArrayList que contém todos os movimentos da conta

        //Construtor para a conta a prazo
        /// <summary>
        /// Construtor único para a classe ContaPrazo 
        /// Só é disponibilizado um constructor devido a ser necessário todos os dados requeridos pelo banco.
        /// </summary>
        /// <param name="pessoa_a_adicionar_na_conta_prazo"> Objecto do tipo Pessoa que contém toda a informação do proprietário da conta </param>
        /// <param name="TaxaJuros"> Valor da taxa de juros do tipo inteiro respectivo à percentagem proposta</param>
        /// <param name="dur"> Valor da duração da conta do tipo inteiro respectivo à duração em unidade de ano(s)</param>
        /// <param name="valor_inicial"> Valor inicial do montante da conta a prazo em euro(s) - Valor do tipo double</param>
        public ContaPrazo(Pessoa pessoa_a_adicionar_na_conta_prazo, int TaxaJuros, int dur, double valor_inicial)
            : base(pessoa_a_adicionar_na_conta_prazo)
        {
            this.taxa_juros = TaxaJuros;
            this.duracao = dur;
            this.movimentos = new ArrayList();
            this.estado_de_conta = 1;
            base.saldo_conta = base.saldo_conta + valor_inicial;
        }
    

        //Métodos publicos
        /// <summary>
        /// Método que permite efectuar o levantamento pontual de dinheiro.
        /// É pedido ao utilizador uma confirmação para verificar se já passou a duração do prazo
        /// Caso o prazo tenha passado só é possível levantar o dinheiro na sua totalidade e a conta é dada como encerrada
        /// É perguntado na console a quantidade de dinheiro a levantar e a confirmação da duração.
        /// </summary>
        public override void levantar_dinheiro()
        {
            Console.WriteLine("Quantos anos passaram desde que a conta foi criada. Se não chegou a 12 meses, assuma que passaram 0 anos");
            int duracao_resposta = Convert.ToInt32(Console.ReadLine());
            if (duracao_resposta < this.duracao)
            {
                Console.WriteLine("Quanto dinheiro deseja levantar da sua conta a Prazo?");
                double quantia_a_levantar = Convert.ToDouble(Console.ReadLine());

                if (base.saldo_conta - quantia_a_levantar < 0)
                {
                    Console.WriteLine("Não foi possível efectuar a operação devido à quantia exceder o saldo disponível.");
                }
                else if (base.saldo_conta - quantia_a_levantar >= 0)
                {
                    base.saldo_conta = base.saldo_conta - quantia_a_levantar;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Operação concluída com sucesso. Saldo restante: {0} euro(s)", base.saldo_conta);
                    Console.ResetColor();
                    this.movimentos.Add("-" + Convert.ToString(quantia_a_levantar) + " - Levantamento Pontual");
                }
            }
            else
            {
                double valorjuros = base.saldo_conta * (this.taxa_juros / 100);
                base.saldo_conta = base.saldo_conta + valorjuros;
                Console.WriteLine("Só pode levantar se fôr a totalidade do dinheiro? 1- Sim 2-Não");
                int resposta = Convert.ToInt16(Console.ReadLine());

                if (resposta == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Operação concluida com sucesso. Você levantou {0} euros", base.saldo_conta);
                    Console.ResetColor();
                    this.movimentos.Add("-" + Convert.ToString(base.saldo_conta) + " - Levantamento Total");
                    base.saldo_conta = 0;
                    //actualização da data de criação para o momento do deposito após o prazo ter passado como foi pedido no enunciado
                    DateTime data = DateTime.Now;
                    base.data_criacao = Convert.ToString(data);
                    //encerramento da conta
                    this.estado_de_conta = 0;
                }
                else
                {
                    Console.WriteLine("Operação cancelada. Saldo disponivel: {0} euro(s)", base.saldo_conta);
                }

            }
        }

        /// <summary>
        /// Este método permite efectuar a transferência de uma conta a Prazo para outra conta.
        /// Se a duração do prazo da conta já tiver passado a conta é encerrada.
        /// É apresentado na console a quantidade de dinheiro a transferir e uma confirmação do prazo da conta
        /// </summary>
        public override double transferir_dinheiro()
        {
            Console.WriteLine("Quantos anos passaram desde que a conta foi criada. Se não chegou a 12 meses, assuma que passaram 0 anos");
            int duracao_resposta = Convert.ToInt32(Console.ReadLine());
            if (duracao_resposta < this.duracao)
            {
                Console.WriteLine("Quanto dinheiro deseja transferir da sua conta a Prazo?");
                double quantia_a_levantar = Convert.ToDouble(Console.ReadLine());

                if (base.saldo_conta - quantia_a_levantar < 0)
                {
                    Console.WriteLine("Não foi possível efectuar a operação devido à quantia exceder o saldo disponível.");
                    quantia_a_levantar = 0;
                    return 0;
                }
                else if (base.saldo_conta - quantia_a_levantar >= 0)
                {
                    base.saldo_conta = base.saldo_conta - quantia_a_levantar;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Operação concluída com sucesso. Saldo restante: {0} euro(s)", base.saldo_conta);
                    Console.ResetColor();
                    this.movimentos.Add("-" + Convert.ToString(quantia_a_levantar) + " - Transferência Bancária");
                    return quantia_a_levantar;
                }
            }
            else
            {
                double valorjuros = base.saldo_conta * (this.taxa_juros / 100);
                base.saldo_conta = base.saldo_conta + valorjuros;
                Console.WriteLine("Só pode transferir dinheiro se for a totalidade do valor actual? 1- Sim 2-Não");
                int resposta = Convert.ToInt16(Console.ReadLine());

                if (resposta == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Operação concluida com sucesso. Você levantou {0} euros", base.saldo_conta);
                    Console.ResetColor();
                    this.movimentos.Add("-" + Convert.ToString(base.saldo_conta) + " - Transferência Bancária");
                    double quantia_a_levantar = base.saldo_conta;
                    base.saldo_conta = 0;
                    this.estado_de_conta = 0;
                    return quantia_a_levantar;
                }
                else
                {
                    Console.WriteLine("Operação cancelada. Saldo disponivel: {0} euro(s)", base.saldo_conta);
                    return 0;
                }

            }
            return 0;

        }

        /// <summary>
        /// Método que permite receber o dinheiro na conta de uma transferência. No programa principal é procurado a conta de destino e são executados 2 métodos. O de transferir dinheiro da conta remetente e o método, receber dinheiro da conta destino.
        /// </summary>
        /// <param name="dinheiro">Valor do dinheiro a receber na conta.</param>
        public override void receber_transferencia(double dinheiro)
        {
            //não é permitido efectuar transferências para contas a prazo. por isso mesmo, não se criou código para esta operação
        }

        /// <summary>
        /// Método usado para efectuar o depósito do dinheiro na conta.
        /// É apresentado na console a quantia de dinheiro a depositar
        /// </summary>
        public override void depositar_dinheiro()
        {
            Console.WriteLine("Quantos anos passaram desde que a conta foi criada. Se não chegou a 12 meses, assuma que passaram 0 anos");
            int duracao_resposta = Convert.ToInt32(Console.ReadLine());

            if (duracao_resposta > this.duracao)
            {
                //adicionar os juros ao saldo actual antes de depositar o dinheiro
                double valorjuros = base.saldo_conta * (this.taxa_juros / 100);
                base.saldo_conta = base.saldo_conta + valorjuros;

                Console.WriteLine("Quantia a depositar na conta a Prazo:");
                double quantida_a_depositar = Convert.ToDouble(Console.ReadLine());
                base.saldo_conta = saldo_conta + quantida_a_depositar;
                this.movimentos.Add("+" + Convert.ToString(quantida_a_depositar) + " - Depósito Pontual");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não é possível depositar dinheiro enquanto o prazo não tiver sido vencido.");
                Console.ResetColor();
            }
        }

            /// <summary>
            /// Apresenta na console a listagem dos movimentos da conta.
            /// Apresenta também o saldo actual.
            /// </summary>
            public override void listar_movimentos()
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Lista de Movimentos: \n");
                Console.ResetColor();
                foreach (string value in movimentos)
                {
                    Console.WriteLine(value);
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Saldo Actual: {0} euro(s)", base.saldo_conta);
                Console.ResetColor();
            }

            /// <summary>
            /// Apresenta na console o nome do Proprietário da conta e o BI.
            /// Também apresenta informações relativas á conta como, saldo, número de conta, data de criação e estado da conta
            /// </summary> 
            public override void mostrar_info()
            {
                Console.WriteLine("****************************************************************\n");

                Console.WriteLine("Nº de conta: {0}\n", base.nr_conta_actual);
                Console.WriteLine("Nome do proprietário: {0}\n", base.pessoa_da_conta.nome);
                Console.WriteLine("BI do proprietário: {0}\n", base.pessoa_da_conta.bi);

                Console.WriteLine("Tipo de conta: Conta a prazo\n");

                //mostrar o estado da conta:
                if (this.estado_de_conta == 0)
                {
                    Console.WriteLine("Estado da conta: Encerrada");
                }
                else if (this.estado_de_conta == 1)
                {
                    Console.WriteLine("Estado da conta: Activa");
                }

                Console.WriteLine("Tempo de duração da conta a prazo:{0} ano(s)\n",this.duracao);
                Console.WriteLine("Valor da taxa de juros da conta {0}: {1}%",this.nr_conta_actual,this.taxa_juros);

                Console.WriteLine("Saldo actual da conta: {0} euro(s)\n", base.saldo_conta);
                Console.WriteLine("Data de criação da conta: {0}\n", base.data_criacao);

                Console.WriteLine("****************************************************************\n");
            }

            /// <summary>
            /// Devolve o número de conta no tipo de variável string.
            /// </summary>
            public override string dar_nr_conta()
            {
                return base.dar_nr_conta();
            }
            /// <summary>
            /// Devolve o BI do proprietário da conta no tipo de variável string.
            /// </summary>
            public override string dar_bi_da_pessoa()
            {
                return base.pessoa_da_conta.bi;
            }
        }
}