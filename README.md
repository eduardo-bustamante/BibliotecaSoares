# 📚 Sistema de Gestão de Biblioteca

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows_Forms-0078D7?style=for-the-badge&logo=windows&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

Um sistema completo de gestão para bibliotecas escolares desenvolvido em C# com Windows Forms. A aplicação permite o controle eficiente do acervo literário, gestão de alunos e um robusto motor de empréstimos com cálculo automático de multas e inadimplência.

## ✨ Funcionalidades Principais

* **Gestão de Acervo:** Cadastro de livros com controle de estoque (quantidade total, emprestada e disponível).
* **Controle de Empréstimos:** Registro de saídas e entradas de livros de forma rápida.
* **Motor de Multas *On The Fly*:** Cálculo automático de multas por dias de atraso no momento da devolução.
* **Painel de Inadimplência:** Relatório em tempo real de alunos com livros atrasados ou dívidas pendentes.
* **Resolução em Massa:** Quitação inteligente de dívidas e devoluções agrupadas por aluno.
* **Geração de Relatórios:** Impressão de relatórios de pendências formatados para papel ou PDF.

## 🏗️ Arquitetura e Tecnologias

Este projeto foi construído visando a manutenibilidade e escalabilidade do código, aplicando:

* **Linguagem:** C# (.NET)
* **Interface:** Windows Forms (WinForms)
* **ORM:** Entity Framework Core
* **Padrões de Projeto:** * Padrão **Repository** para abstração do acesso a dados.
  * Princípios de **Clean Architecture** para separação de responsabilidades.
  * Uso de **DTOs** (Data Transfer Objects) para otimização de tráfego de dados na interface.

## 🚀 Como executar o projeto

1. Clone este repositório:
   ```bash
   git clone [https://github.com/seu-usuario/nome-do-repositorio.git](https://github.com/seu-usuario/nome-do-repositorio.git)
