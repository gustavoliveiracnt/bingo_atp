# Jogo de Bingo - Trabalho Prático (Algoritmo de Técnicas de Programação)

## Apresentação
Olá, rede! Me chamo Gustavo, sou graduando em Sistemas de Informação na PUC MINAS. Na data dessa publicação (12/2023) acabo de encerrar o meu primeiro semestre no curso. Como trabalho prático da disciplina introdutória à programação, tivemos a tarefa de criar um jogo de Bingo, o qual deixarei abaixo mais instruções sobre como jogar. O trabalho foi desenvolvido em CSharp (C#), como disse anteriormente, usando conceitos ainda introdutórios sobre programação. Ademais, espero que gostem. Obrigado pela atenção. :)

## Visão Geral
Esta é uma implementação simples do jogo de Bingo com regras específicas para manter uniformidade. O jogo suporta múltiplos jogadores em uma única rodada, cada um escolhendo o número de cartelas com as quais desejam jogar. O jogo começa com um sorteio de número aleatório, e os jogadores marcam suas cartelas de acordo. O objetivo é completar padrões pré-definidos nas cartelas e chamar "Bingo". O jogo continua até que o penúltimo jogador chame Bingo.

## Regras
1. *Número de Jogadores:*
   - A aplicação permite de 2 a 5 jogadores em uma única rodada.

2. *Número de Cartelas:*
   - Cada jogador pode escolher jogar com 1 a 4 cartelas.

3. *Inicialização do Jogo:*
   - O programa seleciona aleatoriamente um número entre 1 e 75.
   - Os números não se repetem ao longo do jogo.

4. *Chamando Bingo:*
   - Os jogadores chamam Bingo quando completam os padrões especificados em suas cartelas.
   - Se um jogador tiver várias cartelas, completar o padrão em qualquer uma das cartelas é suficiente.

5. *Verificação do Bingo:*
   - Após um jogador chamar Bingo, o programa verifica se o padrão reclamado é válido.
   - Por exemplo, se um jogador alega ter completado uma linha, o programa verifica se todos os números dessa linha foram sorteados.
   - Se válido, o jogador encerra a rodada; caso contrário, a cartela é invalidada.

6. *Chamada Incorreta de Bingo:*
   - Se um jogador com apenas uma cartela chama Bingo incorretamente, ele automaticamente perde a rodada e é classificado como último.

7. *Fim do Jogo:*
   - O jogo conclui quando o penúltimo jogador chama Bingo.
   - O último jogador a chamar Bingo vence o jogo.

8. *Exibição do Ranking:*
   - Após o término do jogo, o programa apresenta um ranking dos jogadores.
   - O ranking inclui informações do jogador: nome, idade e sexo.

9. *Informações do Jogador:*
   - As informações dos jogadores, incluindo nome, idade e sexo, são armazenadas em uma classe de jogador.

## Observações sobre a Implementação do Jogo
- A implementação deve seguir rigorosamente essas regras para manter a consistência.
- Variações dessas regras não são permitidas.


## Agradecimento e contato
Mais uma vez, agradeço por dedicar tempo para ler sobre o meu trabalho. Fico aberto a qualquer feedback ou sugestão que você possa ter. Entre em contato comigo pelo [LinkedIn](https://www.linkedin.com/in/gustaavoliveira/).

Obrigado e espero que aproveitem o jogo! ;)
