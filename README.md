# TicketHub 🎫

Sistema de controle de atendimentos e chamados internos desenvolvido em **ASP.NET Core** com **PostgreSQL**.

## 🎥 Demonstração

[![Demonstração do TicketHub](https://img.youtube.com/vi/J4tJMLB7C2k/0.jpg)](https://www.youtube.com/watch?v=J4tJMLB7C2k)

## ✨ Funcionalidades

- **Cadastro de Setores** — com validação de duplicidade e proteção contra exclusão de setores vinculados a chamados
- **Cadastro de Prioridades** — com tempo estimado em horas, validação de duplicidade e proteção contra exclusão
- **Abertura de Chamados** — vinculados a um Setor e uma Prioridade, com status inicial **Aberto**
- **Check-in** — registro automático da data e hora de início do atendimento
- **Check-out** — registro automático da data e hora de término com solução obrigatória
- **Cancelamento** — com tela de confirmação, bloqueado para chamados já finalizados ou cancelados
- **Listagem com filtros** — por Setor, Prioridade e Status
- **Dashboard** — cards com totais de chamados Abertos, Em Atendimento, Atrasados e Finalizados
- **Destaque de atraso** — chamados que ultrapassaram o tempo estimado da prioridade ficam em vermelho com status **Atrasado**
- **Tooltip de solução** — passe o mouse no badge "Concluído" para ver a solução aplicada

## 🛠️ Tecnologias

- ASP.NET Core 10 (MVC)
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose
- Bootstrap 5

## 🚀 Como rodar

### Com Docker (recomendado)

**Requisitos:** [Docker](https://www.docker.com/products/docker-desktop)

```bash
# 1. Clone o repositório
git clone https://github.com/luisvso/TicketHub.git
cd TicketHub

# 2. Configure as variáveis de ambiente
cp .env_template .env
```

Abra o `.env` e preencha com suas credenciais:

```env
DB_NAME=ticketDB
DB_USER=seu_usuario
DB_PASSWORD=sua_senha
DB_HOST=db_ticket_hub
DB_PORT=5432
```

```bash
# 3. Suba a aplicação
docker-compose up --build
```

Acesse: **http://localhost:8080**

Isso irá:
- Subir o banco de dados PostgreSQL
- Buildar e subir a aplicação
- Rodar as migrations automaticamente
- Popular o banco com dados iniciais

---

### Sem Docker

**Requisitos:** [.NET 10 SDK](https://dotnet.microsoft.com/download) + PostgreSQL instalado

```bash
# 1. Clone o repositório
git clone https://github.com/luisvso/TicketHub.git
cd TicketHub

# 2. Configure a connection string no appsettings.json
```

```json
{
  "ConnectionStrings": {
    "AppDbConnectionString": "Host=localhost;Port=5432;Database=ticketDB;Username=seu_user;Password=sua_senha"
  }
}
```

```bash
# 3. Instale as dependências e rode
dotnet restore
dotnet run
```

Acesse: **http://localhost:5000**

## 📦 Dados iniciais

Ao subir a aplicação, o banco é populado automaticamente com:

**Setores:** TI, Suporte, Financeiro, RH, Comercial

**Prioridades:**
| Nome | Tempo Estimado |
|---|---|
| Baixa | 8 horas |
| Média | 4 horas |
| Alta | 2 horas |
| Crítica | 1 hora |

**Chamados de exemplo:**
| Chamado | Status |
|---|---|
| Sem acesso ao sistema | Aberto |
| Impressora offline | Em Atendimento |
| Servidor fora do ar | Atrasado 🔴 |
| Troca de mouse | Finalizado |
| Solicitação duplicada | Cancelado |

## ⏹️ Parar a aplicação

```bash
# Para a aplicação
docker-compose down

# Para e remove os dados do banco
docker-compose down -v
```
