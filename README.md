# Blackjack 21 com Sockets UDP 🎴

Implementação do jogo de cartas **"21" (Blackjack)** usando **C#** e **Sockets UDP**.  
Projeto desenvolvido como trabalho prático da disciplina de Redes de Computadores.

## 👨‍💻 Alunos

- Guilherme Augusto Dos Santos  
- Pedro Cesar de Barros Couto  
- Andoni Francisco Rodriguez Alves.

---

## 📦 Estrutura do Projeto


---

## 🚀 Como Executar

### ✅ Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado
- Terminal (cmd, PowerShell ou terminal do VS Code)
- VS Code recomendado com extensão C# da Microsoft

### ⚙️ Clonando o repositório
git clone https://github.com/GuilhermeADev/Trabalho-Pr-tico-Final.git
cd seu-repositorio

▶️ Executando o Servidor
cd BlackjackServer
dotnet run

▶️ Executando o Cliente (em outro terminal)
cd BlackjackClient
dotnet run

🔄 Protocolo de Comunicação
ENTRAR:<nome> — Cliente → Servidor: entra no jogo

PEDIR_CARTA — Cliente → Servidor: solicita nova carta

PARAR — Cliente → Servidor: finaliza jogada

CARTA:<valor> — Servidor → Cliente: envia carta

RESULTADO:<ganhou/perdeu> — Servidor → Cliente: resultado final

MENSAGEM:<texto> — Servidor → Cliente: mensagens gerais


📌 Regras do Jogo
Jogadores pedem cartas até decidir PARAR ou ultrapassar 21 pontos.

O servidor calcula quem venceu a rodada.

O jogo só inicia com pelo menos 2 jogadores conectados.

