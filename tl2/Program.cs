using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace tl2
{
    class Program
    {
        #region Métodos do programa princiapal
        /// <summary>
        /// Método responsável pela transferência do dinheiro de uma conta para a outra no programa principal
        /// </summary>
        /// <param name="dinheiro">Valor a depositar na conta. O tipo de dados é double</param>
        /// <param name="nr_conta_destino">Número da conta destino</param>
        /// <param name="ContasOrdem">ArrayList com todos os objectos do tipo ContaOrdem para fazer a pesquisa da conta destino</param>
        static void efectuar_transf(double dinheiro, string nr_conta_destino, ArrayList Contas)
    {
        string nr_conta_actual;

        foreach (Conta conta in Contas)
                    {

                        nr_conta_actual = conta.dar_nr_conta();

                        if (nr_conta_actual == nr_conta_destino)
                        {
                            conta.receber_transferencia(dinheiro);
                            break;
                        }
                    }
    }

        /// <summary>
        /// Método responsável pela apresentação das opções do menu
        /// Devolve um valor do tipo inteiro para se usar num ciclo while que só termina quando o valor devolvido for 9 (Sair).
        /// </summary>
        static int menu() {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nEscolha uma operação:\n");
            Console.WriteLine("1 - Criar nova conta");
            Console.WriteLine("2 - Pesquisar uma conta dado o bi da pessoa");
            Console.WriteLine("3 - Pesquisar uma conta dado o número da conta");
            Console.WriteLine("4 - Eliminar uma conta dado o nr de conta.\n\nNota: Não faz sentido apagar directamente dando o BI.\nQualquer operação que se faça hoje em dia num banco,\né sempre necessário recorrer ao número de conta devido\na ser um identificador único.\n");
            Console.WriteLine("5 - Depositar dinheiro dado o número de conta");
            Console.WriteLine("6 - Levantar dinheiro dado o número de conta");
            Console.WriteLine("7 - Listar todos os movimentos de uma conta");
            Console.WriteLine("8 - Transferir Dinheiro entre duas contas");
            Console.WriteLine("9 - Listar todas as contas bancárias\n");
            Console.WriteLine("10 - Sair");
            Console.ResetColor();
            return Convert.ToInt32(Console.ReadLine());
        }

        //Método para criar um objecto do tipo Pessoa depois de pedir os dados ao utilizador. Devolve o objecto do tipo Pessoa
        /// <summary>
        /// Método responsável pela criação do objecto do tipo pessoa que contém as informações do proprietário da conta
        /// </summary>
        static Pessoa criar_pessoa() {

            Console.Clear();
            string nome, bi, morada, data, tel, email;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Nome da pessoa:");
            Console.ResetColor();
            nome = Console.ReadLine();
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("BI da pessoa:");
                Console.ResetColor();
                bi = Console.ReadLine();
            } while (bi.Length != 8);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Morada da pessoa:");
            Console.ResetColor();
            morada = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Data de nascimento da pessoa:");
            Console.ResetColor();
            data = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("telefone da pessoa:");
            Console.ResetColor();
            tel = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("email da pessoa:");
            Console.ResetColor();
            email = Console.ReadLine();

            Pessoa pessoacriada = new Pessoa(nome, bi, morada, data, tel, email);

            return pessoacriada;
        }

        #endregion
        //***********************************************************************            Programa principal           ********************************************************************************************************************************

        static void Main()
        {
            #region Variáveis do programa principal
            //Variáveis necessárias para o funcionamento do software
            int operacao, prazo, juros;
            int contador,contador2,contadorat;
            double valor_inicial_conta_prazo, dinheiro_a_transferir;
            string tipo_conta, bi_a_pesquisar, bi_a_comparar, nr_a_pesquisar, nr_a_comparar, nr__conta_a_retirar, nr__conta_a_depositar;    //variáveis para guardar na arraylist e pesquisar
            Pessoa pessoa_a_adicionar;  //Variável do tipo Pessoa da classe que se criou para guardar na conta a ser criada

            //Variáveis temporárias para a criação das contas            
            ContaOrdem contaordemcriada;                        //Objecto Temporário
            ContaPrazo contaprazocriada;                        //Objecto Temporário
            ArrayList database = new ArrayList();               //Guarda todas as contas na arraylist

            #endregion

            #region Algumas pessoas e contas criadas para efeitos de teste do programa
            //algumas pessoas e contas declaradas manualmente para efeitos de teste
            Pessoa teste1 = new Pessoa("Paulo Carvalho", "87654321", "Palheira, Estrada Principal", "19-06-1986", "915555557", "paulo@hotmail.com");
            Pessoa teste2 = new Pessoa("Helena Sofia", "12345678", "Coimbra, Barcouço, Rua Fonte nova", "04-10-1991", "915995557", "helena@hotmail.com");
            ContaOrdem contateste1 = new ContaOrdem(teste1);
            database.Add(contateste1);
            ContaPrazo contateste2 = new ContaPrazo(teste2, 5, 1,500.12);
            database.Add(contateste2);
            ContaPrazo contateste3 = new ContaPrazo(teste1, 2, 1,735.50);
            database.Add(contateste3);
            ContaOrdem contateste4 = new ContaOrdem(teste1);
            database.Add(contateste4);
            ContaPrazo contateste5 = new ContaPrazo(teste2, 1, 2,2000);
            database.Add(contateste5);
            #endregion

            //Mensagem de boas vindas
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Bem vindo ao programa de gestão do BANCO -NOME DO BANCO-\n\n");
            Console.ResetColor();

            //Programa principal - Operações do menu();
            operacao =menu();
            do
            {
                #region Option 1: Criar uma nova conta bancária
                if (operacao == 1)
                {
                    //criar a pessoa que vai ser o proprietário da conta
                    pessoa_a_adicionar = criar_pessoa();

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine ("Qual o tipo de conta?\n\n1- Conta Ordem\n2- Conta Prazo");
                    Console.ResetColor();
                    tipo_conta=Console.ReadLine();

                    //definir o tipo de conta
                    if (tipo_conta=="1")
                    {
                        contaordemcriada= new ContaOrdem(pessoa_a_adicionar);
                        database.Add(contaordemcriada);
                    }
                    //se o tipo de conta for do tipo conta a Prazo é necessário questionar informações relativas aos juros e duração da conta a prazo
                    else if (tipo_conta=="2")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Qual a duração do prazo da conta? Unidade: ano(s)");
                        Console.ResetColor();
                        prazo = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Qual o valor da taxa de juros? Unidade: percentagem (Valor inteiro)");
                        Console.ResetColor();
                        juros = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Qual o valor que deseja meter a prazo em euro(s)");
                        Console.ResetColor();
                        valor_inicial_conta_prazo = Convert.ToDouble(Console.ReadLine());
                        
                        contaprazocriada= new ContaPrazo(pessoa_a_adicionar,juros,prazo,valor_inicial_conta_prazo);
                        database.Add(contaprazocriada);
                    }
                    operacao = menu();
                }
                #endregion
                #region Option 2: Pesquisar uma conta por número de bilhete de identidade
                else if (operacao == 2)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Qual o nr de BI da pessoa a qual se associa a conta?\n\n");
                    Console.ResetColor();
                    bi_a_pesquisar = Console.ReadLine();

                    //contador para sabermos quantas contas foram encontradas
                    contador = 0;
                    foreach (Conta conta in database)
                    {
                        bi_a_comparar = conta.dar_bi_da_pessoa();

                        if (bi_a_pesquisar == bi_a_comparar)
                        {
                            contador++;
                            conta.mostrar_info();
                        }
                    }

                    if (contador == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNão foi encontrada nenhuma conta através do número de BI que inseriu.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Número total de contas: {0}", contador);
                        Console.ResetColor();
                    }

                    operacao = menu();
                }
                #endregion
                #region Option 3: Pesquisar uma conta por número de conta
                else if (operacao == 3)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Qual o nr de conta?\n\n");
                    Console.ResetColor();
                    nr_a_pesquisar = Console.ReadLine();

                    contador = 0;
                    
                    foreach (Conta conta in database)
                    {
                        nr_a_comparar = conta.dar_nr_conta();
                     
                        if (nr_a_pesquisar == nr_a_comparar)
                        {
                            contador++;
                            conta.mostrar_info();
                            break;
                        }
                    }

                    if (contador == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNão foi encontrada nenhuma conta através do número de conta que inseriu.");
                        Console.ResetColor();
                    }

                    operacao = menu();
                }
                #endregion
                #region Option 4: Eliminar uma conta
                else if (operacao == 4)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Qual o nr de conta que deseja eliminar?\n\n");
                    Console.ResetColor();
                    nr_a_pesquisar = Console.ReadLine();

                    contador = 0;
                    
                    //contador necessário para saber a posição da conta na arraylist assim que encontrada
                    contadorat = 0;
                    foreach (Conta conta in database)
                    {
                        
                        nr_a_comparar = conta.dar_nr_conta();

                        if (nr_a_pesquisar == nr_a_comparar)
                        {
                            contador++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("A seguinte conta foi apagada:\n\n");
                            Console.ResetColor();
                            conta.mostrar_info();
                            //remover a conta da arraylist
                            database.RemoveAt(contadorat);
                            break;
                        }
                        contadorat++;
                    }
                
                    if (contador == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNão foi encontrada nenhuma conta através do número de conta que inseriu.");
                        Console.ResetColor();
                    }

                    operacao = menu();
                }
                #endregion
                #region Option 5: Depositar dinheiro numa conta
                else if (operacao == 5)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Qual o nr de conta em que deseja depositar dinheiro?\n\n");
                    Console.ResetColor();
                    nr_a_pesquisar = Console.ReadLine();

                    contador = 0;
                    foreach (Conta conta in database)
                    {

                        nr_a_comparar = conta.dar_nr_conta();

                        if (nr_a_pesquisar == nr_a_comparar)
                        {
                            contador++;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Depositar dinheiro na seguinte conta:\n\n");
                            Console.ResetColor();
                            conta.mostrar_info();
                            conta.depositar_dinheiro();
                            break;
                        }
                    }
                    
                    if (contador == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNão foi encontrada nenhuma conta através do número de conta que inseriu.");
                        Console.ResetColor();
                    }

                    operacao = menu();
                }
                #endregion
                #region Option 6: Levantar dinheiro de uma conta
                else if (operacao == 6)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Qual o nr de conta em que deseja levantar dinheiro?\n\n");
                    Console.ResetColor();
                    nr_a_pesquisar = Console.ReadLine();

                    contador = 0;
                    foreach (Conta conta in database)
                    {

                        nr_a_comparar = conta.dar_nr_conta();

                        if (nr_a_pesquisar == nr_a_comparar)
                        {
                            contador++;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("\nDepositar dinheiro na seguinte conta:\n\n\n");
                            Console.ResetColor();
                            conta.mostrar_info();
                            conta.levantar_dinheiro();
                            break;
                        }
                    }
                   
                    if (contador == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNão foi encontrada nenhuma conta através do número de conta que inseriu.");
                        Console.ResetColor();
                    }

                    operacao = menu();
                }
                #endregion
                #region Option 7: Listar todos os movimentos de uma conta
                else if (operacao == 7)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Qual o nr de conta da qual deseja listar movimentos?\n\n");
                    Console.ResetColor();
                    nr_a_pesquisar = Console.ReadLine();

                    contador = 0;
                    foreach (Conta conta in database)
                    {

                        nr_a_comparar = conta.dar_nr_conta();

                        if (nr_a_pesquisar == nr_a_comparar)
                        {
                            contador++;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("\nMovimentos da seguinte conta:\n\n\n");
                            Console.ResetColor();
                            conta.mostrar_info();
                            conta.listar_movimentos();
                            break;
                        }
                    }

                    if (contador == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNão foi encontrada nenhuma conta através do número de conta que inseriu.");
                        Console.ResetColor();
                    }

                    operacao = menu();
                }
                #endregion
                #region Option 8: Transferir dinheiro entre duas contas
                else if (operacao == 8)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Insira o nº de conta remetente:\n\n");
                    Console.ResetColor();
                    nr__conta_a_retirar = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Insira o nº de conta destinatário:\n\n");
                    Console.ResetColor();
                    nr__conta_a_depositar = Console.ReadLine();

                    //verificar se a conta destino existe para o dinheiro não ir para o ar.
                    contador = 0;
                    contador2 = 0;
                    foreach (Conta conta in database)
                    {

                        nr_a_comparar = conta.dar_nr_conta();

                        if (nr__conta_a_depositar == nr_a_comparar)
                        {
                            contador++;
                        }
                        if (nr__conta_a_retirar == nr_a_comparar)
                        {
                            contador2++;
                        }
                    }

                    if (contador2 > 0)
                    {

                        //se a conta de destino existir então continua com a transferência
                        if (contador > 0)
                        {
                            foreach (Conta conta in database)
                            {

                                nr_a_comparar = conta.dar_nr_conta();

                                if (nr__conta_a_retirar == nr_a_comparar)
                                {
                                    dinheiro_a_transferir = conta.transferir_dinheiro();
                                    efectuar_transf(dinheiro_a_transferir, nr__conta_a_depositar, database);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nOperação cancelada!\n\nNão existe nenhuma conta destino com o número que selecionou");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOperação cancelada!\n\nNão existe nenhuma conta de remetente com o número que inseriu");
                        Console.ResetColor();
                    }
                    operacao = menu();
                }
                #endregion
                #region Option 9: Listar todas as contas bancárias
                else if (operacao == 9)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Todas as contas da base de dados:\n\n");
                    Console.ResetColor();

                    //contador para sabermos quantas contas foram encontradas
                    contador = 0;
                    foreach (Conta conta in database)
                    {
                            contador++;
                            conta.mostrar_info();
                    }

                    if (contador == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNão foi encontrada nenhuma conta através do número de BI que inseriu.");
                        Console.ResetColor();
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nForam encontradas {0} contas bancárias.",contador);
                        Console.ResetColor();
                        }
                    operacao = menu();
                }
                #endregion
            } while (operacao != 10);
        }
    }
}