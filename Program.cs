using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo
{
    class Jogador
    {
        public string nome;
        public int idade;
        public string genero;
        public Cartela[] cartelas = new Cartela[4];
        public Cartela[] copiaCartela = new Cartela[4];
        public void armazenaDados(string nome, int idade, string genero)
        {
            this.nome = nome;
            this.idade = idade;
            this.genero = genero;
        }
        public void verificaNum(Jogador jgdr, int n)
        {
            for (int i = 0; i < jgdr.cartelas.Length; i++)
            {
                if (jgdr.cartelas[i] != null) //se cartela não for nula, ou seja, se estiver em jogo
                {
                    for (int j = 0; j < jgdr.cartelas[i].cartela_matriz.GetLength(0); j++)
                    {
                        for (int k = 0; k < jgdr.cartelas[i].cartela_matriz.GetLength(1); k++)
                        {
                            if (jgdr.cartelas[i].cartela_matriz[j, k] == n)
                            {
                                jgdr.cartelas[i].cartela_matriz[j, k] = 0;
                                jgdr.cartelas[i].contacertos++; //contabiliza acerto
                            }
                        }
                    }
                    jgdr.cartelas[i].imprimirCartela(jgdr.cartelas[i]);
                }
            }
        }
    }
    class Cartela
    {
        public int[,] cartela_matriz = new int[5, 5];
        public int[] cartela_vetor = new int[25];
        public int contacertos = 0;

        public void imprimirCartela(Cartela cartela)
        {
            Console.ForegroundColor = ConsoleColor.Cyan; //cor no console (opcional)
            string cabecalho = "BINGO";
            Console.WriteLine("\n------------------------");
            for (int i = 0; i < cabecalho.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;//cor no console (opcional)
                Console.Write(" " + cabecalho[i] + "   ");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;//cor no console (opcional)
            Console.WriteLine("\n------------------------");
            Console.ResetColor();
            for (int i = 0; i < cartela.cartela_matriz.GetLength(0); i++)
            {
                for (int k = 0; k < cartela.cartela_matriz.GetLength(1); k++)
                {
                    if (cartela.cartela_matriz[i, k] < 10)
                    {
                        Console.Write(" ");
                    }
                    if (cartela.cartela_matriz[i, k] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;//cor no console (opcional)
                        Console.Write(cartela.cartela_matriz[i, k]);
                        Console.ResetColor();
                    }
                    else { Console.Write(cartela.cartela_matriz[i, k]); }

                    Console.ForegroundColor = ConsoleColor.Cyan;//cor no console (opcional)
                    Console.Write(" | ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------");//cor no console (opcional)

            Console.ResetColor();
        }

        public Cartela[] preencherCartela(Jogador jogadorTemp, int qtd)
        {
            int number;
            Random nums = new Random();
            Cartela[] crt = new Cartela[4];
            if (qtd <= jogadorTemp.cartelas.Length)
            {
                for (int i = 0; i < qtd; i++)
                {
                    crt[i] = new Cartela();
                    for (int j = 0; j < 25; j++)
                    {
                        if (j % 5 == 0)
                        {
                            number = nums.Next(1, 16);
                        }
                        else if (j % 5 == 1)
                        {
                            number = nums.Next(16, 31);
                        }
                        else if (j % 5 == 2)
                        {
                            number = nums.Next(31, 46);
                        }
                        else if (j % 5 == 3)
                        {
                            number = nums.Next(46, 61);
                        }
                        else
                        {
                            number = nums.Next(61, 76);
                        }

                        if (j == 12)
                        {
                            number = 0;
                        }
                        cartela_vetor[j] = number;

                        for (int k = 0; k <= j; k++) //comando de repetição para verificar se número já foi sorteado anteriormente
                        {
                            if (cartela_vetor[j] == cartela_vetor[k] && k != j)
                            {
                                j--;
                                break;
                            }
                        }
                    }

                    for (int j = 0, l = 0; j < crt[i].cartela_matriz.GetLength(0); j++)
                    {
                        for (int k = 0; k < crt[i].cartela_matriz.GetLength(1); k++, l++)
                        {
                            crt[i].cartela_matriz[j, k] = cartela_vetor[l]; //gerando cartela (matriz) através de um vetor
                        }
                    }
                    jogadorTemp.cartelas[i] = crt[i]; //atribuindo cartela gerada para uma posição do vetor Cartela do jogador
                    jogadorTemp.copiaCartela[i] = crt[i]; //copiando cartela para mesma posição
                    imprimirCartela(jogadorTemp.cartelas[i]);
                }
            }
            else
            {
                for (int i = qtd; i < jogadorTemp.cartelas.Length; i++) //atribuindo cartelas restantes do vetor como nulas, ou seja, não serão visíveis para os jogadores
                {
                    crt[i] = new Cartela();
                    crt[i] = null;
                    jogadorTemp.cartelas[i] = crt[i];
                    jogadorTemp.copiaCartela[i] = crt[i];
                }
            }
            return crt;
        }
    }
    class Bingo
    {
        public int[] sorteados = new int[75];

        public int[] sorteio()
        {
            Random r = new Random();
            int novoSorteio;

            for (int i = 0; i < sorteados.Length; i++)
            {
                sorteados[i] = r.Next(1, 76);

                for (int j = 0; j <= i; j++) //verifica se número gerado é repetido
                {
                    if (sorteados[i] == sorteados[j] && i != j)
                    {
                        novoSorteio = r.Next(1, 76);

                        sorteados[i] = novoSorteio;
                        i--; //volta posição
                        break; //quebra só o 2º comando de repetição
                    }
                }
            }

            return sorteados;
        }

        public bool eBingo(Jogador jgdr, int pos, int linhaoucoluna)
        {
            int[] numerosLinhas = new int[5];
            int[] numerosColunas = new int[5];


            for (int i = 0; i < numerosLinhas.Length; i++)
            {
                numerosLinhas[i] = jgdr.copiaCartela[pos].cartela_matriz[linhaoucoluna, i]; //armazena números da linha específica da cartela em um vetor
            }

            for (int i = 0; i < numerosLinhas.Length; i++)
            {
                numerosColunas[i] = jgdr.copiaCartela[pos].cartela_matriz[i, linhaoucoluna]; //armazena números da coluna específica da cartela em um vetor
            }

            bool verificadoLinha = true;
            foreach (int busca in numerosLinhas)
            {
                for (int i = 0; i < sorteados.Length; i++) //compara número de busca com números sorteados
                {
                    if (busca != 0 && busca != sorteados[i])
                    {
                        verificadoLinha = false;
                        break;
                    }
                }
            }

            bool verificadoColuna = true;
            foreach (int busca in numerosColunas)
            {
                for (int i = 0; i < sorteados.Length; i++) //compara número de busca com números sorteados
                {
                    if (busca != 0 && busca != sorteados[i])
                    {
                        verificadoColuna = false;
                        break;
                    }
                }
            }

            if (!verificadoColuna && !verificadoLinha) //se ambos forem falsos, não é bingo
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    internal class Program
    {
        public static Jogador[] dadosJogadores(out int qtd_jogador)
        {
            string nome, genero; //variáveis que poderão ser usadas mais tarde, com a finalidade de obter informações de uma classe
            int idade; //mesmo caso das variáveis acima
            bool controle = false; //verificar se é possível ou não dar continuidade no jogo baseado na quantidade de jogadores informada
            Jogador[] jogadoreRecebe = new Jogador[5];
            qtd_jogador = 0; //variável que será usada posteriormente no código, por isso declarada como "global"

            Console.WriteLine("---------------------");
            Console.WriteLine(" Vamos jogar BINGO!");
            Console.WriteLine("---------------------");
            while (!controle)
            {
                Console.Write("\nInforme quantidade de jogadores: ");
                qtd_jogador = int.Parse(Console.ReadLine());

                if (qtd_jogador > 5 || qtd_jogador < 2) //verificação de quantidade de jogadores
                {
                    controle = false;

                    if (qtd_jogador > 5)
                    {
                        Console.WriteLine("\nPossível no máximo 5 jogadores por rodada. Informe novamente uma quantidade válida.");
                    }
                    else if (qtd_jogador < 2)
                    {
                        Console.WriteLine("\nNão é possível jogar BINGO com menos de 2 jogadores. Informe novamente uma quantidade válida.");
                    }
                }
                else
                {
                    controle = true; //recebe novo valor e continua o jogo

                    for (int i = 0; i < qtd_jogador; i++) //receber dados dos jogadores
                    {
                        Console.WriteLine("...................................");
                        Console.Write($"\nInforme nome do jogador {i + 1}: ");
                        nome = Console.ReadLine();

                        do //verificação e validação de idade
                        {
                            Console.Write($"{nome}, informe sua idade: ");
                            idade = int.Parse(Console.ReadLine());
                            if (idade <= 0)
                            {
                                Console.WriteLine("\nIdade inválida, informe novamente.");
                            }
                        } while (idade <= 0);

                        Console.Write($"{nome}, informe seu gênero: ");
                        genero = Console.ReadLine();

                        jogadoreRecebe[i] = new Jogador(); //nova instância. Por ser um vetor, a cada repetição é necessário instanciar novo objeto
                        jogadoreRecebe[i].armazenaDados(nome, idade, genero);

                        bool valido = true;
                        do //verificação e validação de quantidade de cartelas para o jogador
                        {
                            Console.Write($"\n{nome}, informe quantidade de cartelas gostaria de jogar: ");
                            int qtd_cartelas = int.Parse(Console.ReadLine());

                            if (qtd_cartelas >= 1 && qtd_cartelas <= 4)
                            {
                                valido = true;

                                Cartela cartelaTemp = new Cartela(); //variável temporária apenas para acessar método
                                jogadoreRecebe[i].cartelas = cartelaTemp.preencherCartela(jogadoreRecebe[i], qtd_cartelas);

                            }
                            else if (qtd_cartelas < 1 || qtd_cartelas > 4)
                            {
                                valido = false;
                                Console.WriteLine("Quantidade inválida. Informe uma quantidade de 1 à 4.");
                            }
                        } while (!valido);
                    }
                }
            }
            return jogadoreRecebe;
        }
        public static Jogador[] Jogo()
        {
            Bingo bing = new Bingo();
            int[] sorteados = bing.sorteio();
            Jogador[] ranking = new Jogador[5];
            int qtd_jogadores;
            Jogador[] jogadores = dadosJogadores(out qtd_jogadores); //recebe array com dados dos jogadores com a quantidade de jogadores

            for (int i = 0, v = 0; i < sorteados.Length && qtd_jogadores > 1; i++) //se ao menos uma dessas condições forem falso, jogo acaba
            {
                string respostas;
                int acertos = 0;
                if (i > 0)
                {
                    Console.WriteLine("\n------------------------");
                    Console.WriteLine("      Nova rodada");
                    Console.WriteLine("------------------------");
                }
                Console.WriteLine("\n------------------------");
                Console.WriteLine("  Número sorteado: " + sorteados[i]);
                Console.WriteLine("------------------------");

                for (int j = 0; j < jogadores.Length; j++)
                {
                    if (jogadores[j] != null) //se jogar ainda estiver em jogo, faça
                    {
                        Console.WriteLine(".......................................................................................");
                        Console.WriteLine($"\nCartelas do jogador {jogadores[j].nome}: ");
                        for (int x = 0; x < jogadores[j].cartelas.Length; x++)
                        {
                            int[] vetAcertos = new int[jogadores[j].cartelas.Length]; //vetor com tamanho N (quantidade de cartelas do jogador)
                            if (jogadores[j].cartelas[x] != null)
                            {
                                jogadores[j].cartelas[x].imprimirCartela(jogadores[j].cartelas[x]);

                                vetAcertos[x] = jogadores[j].cartelas[x].contacertos;

                                if (vetAcertos[x] >= 4) //se quantidade de acertos da cartela for maior/igual que 4
                                {
                                    acertos++; //será usado para jogador informar se fez bingo ou não
                                }
                            }
                        }

                        Console.WriteLine(".......................................................................................");
                        Console.WriteLine($"\nJogador {jogadores[j].nome}, você possui o número {sorteados[i]} em alguma de suas cartelas? (SIM/NÃO)");
                        respostas = Console.ReadLine();
                        respostas = respostas.ToUpper();

                        if (respostas == "SIM")
                        {
                            jogadores[j].verificaNum(jogadores[j], sorteados[i]);
                        }

                        if (acertos > 0 && respostas == "SIM")
                        {
                            Console.WriteLine($"\nJogador {jogadores[j].nome}, você fez BINGO? (SIM/NÃO)");
                            respostas = Console.ReadLine();
                            respostas = respostas.ToUpper();

                            if (respostas == "SIM")
                            {
                                int qtd_cartelasAtivas = 0;

                                for (int x = 0; x < jogadores[j].cartelas.Length; x++) //obter quantidade de cartelas que são diferentes de null
                                {
                                    if (jogadores[j].cartelas[x] != null)
                                    {
                                        qtd_cartelasAtivas++;
                                    }
                                }
                                int num_cartela = 0;
                                if (qtd_cartelasAtivas > 1)
                                {
                                    bool cartelaValida = true;
                                    do //verifica se cartela informada é valida
                                    {
                                        Console.Write("Informe em qual de suas cartelas você fez BINGO: ");
                                        num_cartela = int.Parse(Console.ReadLine());

                                        if (num_cartela > qtd_cartelasAtivas || num_cartela < 0)
                                        {
                                            cartelaValida = false;
                                            Console.WriteLine("\nCartela informada inválida. Insira novamente.");
                                            num_cartela = 0;
                                        }
                                        else if (num_cartela <= qtd_cartelasAtivas && num_cartela > 0)
                                        {
                                            cartelaValida = true;
                                        }

                                    } while (!cartelaValida);
                                    num_cartela -= 1;
                                }

                                bool linhaeColuna = true;
                                int aonde_bingo;
                                do
                                { //verificação se linha/coluna é um valor válido
                                    Console.Write("Informe em qual linha ou coluna você fez bingo? ");
                                    aonde_bingo = int.Parse(Console.ReadLine());
                                    if (aonde_bingo > 5 || aonde_bingo < 1)
                                    {
                                        linhaeColuna = false;
                                        Console.WriteLine("\nLinha ou coluna inváida. Tente novamente, inserindo um valor válido (1 á 5).");
                                    }
                                    else
                                    {
                                        linhaeColuna = true;
                                    }
                                } while (!linhaeColuna);


                                Bingo ebingo = new Bingo();
                                int indice;
                                bool resultadoBingo = ebingo.eBingo(jogadores[j], num_cartela, (aonde_bingo - 1));
                                if (resultadoBingo)
                                {
                                    Console.WriteLine("............................................");
                                    Console.WriteLine($" Bingo! Parabéns {jogadores[j].nome}, você fez bingo.");
                                    Console.WriteLine("............................................");
                                    ranking[v] = new Jogador();
                                    ranking[v].nome = jogadores[j].nome;
                                    ranking[v].idade = jogadores[j].idade;
                                    ranking[v].genero = jogadores[j].genero;
                                    jogadores[j] = null; //jogador sai do jogo, será pulado nas próximas rodadas
                                    v++; //ranking sobe uma posição para o próximo ganhador assumir
                                    qtd_jogadores--;
                                    if (qtd_jogadores == 1) //se quantidade de jogadores restantes for igual a 1, faça
                                    {
                                        int pos = 0;
                                        for (int x = 0; x < jogadores.Length; x++) //encontra em qual posição do jogo o jogador restante ficou
                                        {
                                            if (jogadores[x] != null)
                                            {
                                                pos = x; break;
                                            }
                                        }
                                        ranking[v] = new Jogador();
                                        ranking[v].nome = jogadores[pos].nome;
                                        ranking[v].idade = jogadores[pos].idade;
                                        ranking[v].genero = jogadores[pos].genero;
                                        qtd_jogadores--;
                                        break;
                                    }
                                }
                                else if (!resultadoBingo)
                                {
                                    Console.WriteLine("\n.......................................................................................");
                                    Console.WriteLine($"  {jogadores[j].nome}, você gritou BINGO antes da hora, infelizmente você perdeu essa cartela.");
                                    Console.WriteLine(".......................................................................................");

                                    if (qtd_cartelasAtivas == 1)
                                    {
                                        if (qtd_jogadores > 1)
                                        {
                                            indice = qtd_jogadores - 1; //asume a última posição vazia do tanking
                                            ranking[indice] = new Jogador();
                                            ranking[indice].nome = jogadores[j].nome;
                                            ranking[indice].idade = jogadores[j].idade;
                                            ranking[indice].genero = jogadores[j].genero;
                                            jogadores[j] = null; //jogador sai do jogo, será pulado nas próximas rodadas
                                            qtd_jogadores--;

                                        }
                                        if (qtd_jogadores == 1) //após isso, se jogador restante for igual a 1 coloque-o no ranking
                                        {
                                            int posicao_jogador = 0;

                                            for (int x = 0; x < jogadores.Length; x++) //encontra a posição do jogador que sobrou
                                            {
                                                if (jogadores[x] != null)
                                                {
                                                    posicao_jogador = x;
                                                    break;
                                                }
                                            }

                                            for (int x = 0; x < ranking.Length; x++) //encontrar a próxima posição vazia no ranking
                                            {
                                                if (ranking[x] == null)
                                                {
                                                    ranking[x] = new Jogador();
                                                    ranking[x].nome = jogadores[posicao_jogador].nome;
                                                    ranking[x].idade = jogadores[posicao_jogador].idade;
                                                    ranking[x].genero = jogadores[posicao_jogador].genero;
                                                    break;
                                                }

                                            }
                                            qtd_jogadores--;
                                            break;
                                        }
                                    }
                                    else if (qtd_cartelasAtivas > 1) //se tiver mais de uma cartela em jogo
                                    {
                                        if (num_cartela >= 0 && num_cartela < jogadores[j].cartelas.Length && jogadores[j].cartelas[num_cartela] != null) //verifica se cartela informada é válida
                                        {
                                            Cartela cartelaTemp = new Cartela(); //variável para trocar posições das cartelas
                                            jogadores[j].cartelas[num_cartela] = null; //cartela é anulada
                                            jogadores[j].copiaCartela[num_cartela] = null; //copia também anulada
                                            for (int x = 0; x < jogadores[j].cartelas.Length - 1; x++)
                                            {
                                                if (jogadores[j].cartelas[x] == null && qtd_cartelasAtivas < 4) //encontra a posição da cartela que foi anulada e troca as posições ~ cartelas vazias sempre ficam por último
                                                {
                                                    cartelaTemp = jogadores[j].cartelas[x];
                                                    jogadores[j].cartelas[x] = jogadores[j].cartelas[x + 1];
                                                    jogadores[j].cartelas[x + 1] = cartelaTemp;
                                                }

                                                if (jogadores[j].copiaCartela[x] == null && qtd_cartelasAtivas < 4)
                                                {
                                                    cartelaTemp = jogadores[j].copiaCartela[x];
                                                    jogadores[j].copiaCartela[x] = jogadores[j].copiaCartela[x + 1];
                                                    jogadores[j].copiaCartela[x + 1] = cartelaTemp;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Número de cartela inválido.");
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            return ranking;
        }
        static void Main(string[] args)
        {
            Jogador[] ranking = Jogo();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nJogo finalizado.");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Ranking dos jogadores: ");
            Console.ResetColor();
            int contador = 0;

            for (int i = 0; i < ranking.Length; i++) //obter quantidade de jogadores;
            {
                if (ranking[i] != null)
                {
                    contador++;
                }
            }

            for (int y = 0; y < contador; y++)
            {
                Console.WriteLine($"\n{y + 1}º Lugar: ");
                Console.WriteLine($"Nome: {ranking[y].nome}\nIdade: {ranking[y].idade}" + " anos" + $"\nGênero: {ranking[y].genero}\n");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nObrigado por jogar! :)");

            Console.ReadLine();
        }
    }
}
